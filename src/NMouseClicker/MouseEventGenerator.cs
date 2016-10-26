using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NMouseClicker
{
	internal static class MouseEventGenerator
	{
		public static void LClick(int x, int y)
		{
			Cursor.Position = new Point(x, y);
			mouse_event(MouseEventFlags.LeftDown, 0, 0, 0, 0);
			mouse_event(MouseEventFlags.LeftUp, 0, 0, 0, 0);
		}

		[DllImport("user32.dll")]
		private static extern void mouse_event(MouseEventFlags dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

		[Flags]
		private enum MouseEventFlags : uint
		{
			LeftDown = 0x00000002,
			LeftUp = 0x00000004,
			MiddleDown = 0x00000020,
			MiddleUp = 0x00000040,
			Move = 0x00000001,
			Absolute = 0x00008000,
			RightDown = 0x00000008,
			RightUp = 0x00000010
		}
	}
}
