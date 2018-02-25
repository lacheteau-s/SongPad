using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace SongPad.Tools
{
    public static class SystemMenuManager
    {
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

		[DllImport("user32.dll")]
		internal static extern int TrackPopupMenuEx(IntPtr hmenu, uint fuFlags, int x, int y, IntPtr hwnd, IntPtr lptpm);

		[DllImport("user32.dll")]
		internal static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll")]
		internal static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

		private enum CMD
		{
			WM_SYSCOMMAND = 0x112,
			TPM_LEFTALIGN = 0x0000,
			TPM_RETURNCMD = 0x0100
		};

		public static void ShowMenu(Window targetWindow, Point coordinates)
		{
			if (targetWindow == null)
				throw new ArgumentNullException("Target window is null");

			var x = 0;
			var y = 0;

			try
			{
				x = Convert.ToInt32(coordinates.X);
				y = Convert.ToInt32(coordinates.Y);
			}
			catch (OverflowException)
			{
				x = 0;
				y = 0;
			}

			var windowPtr = new WindowInteropHelper(targetWindow).Handle;
			var menuPtr = GetSystemMenu(windowPtr, false);
			var command = TrackPopupMenuEx(menuPtr, (uint)(CMD.TPM_LEFTALIGN | CMD.TPM_RETURNCMD), x, y, windowPtr, IntPtr.Zero);

			if (command == 0)
				return;

			PostMessage(windowPtr, (uint)CMD.WM_SYSCOMMAND, new IntPtr(command), IntPtr.Zero);
		}
    }
}
