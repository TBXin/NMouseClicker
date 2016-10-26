using System;
using System.Windows.Forms;

namespace NMouseClicker
{
	internal class KeyFilter
	{
		public KeyFilter(bool control, bool shift, bool alt, Keys key)
		{
			Control = control;
			Shift = shift;
			Alt = alt;
			Key = key;
		}

		public KeyFilter(bool control, bool shift, bool alt, Keys key, Action action) : this(control, shift, alt, key)
		{
			Action = action;
		}

		public bool Control { get; set; }

		public bool Shift { get; set; }

		public bool Alt { get; set; }

		public Keys Key { get; set; }

		public Action Action { get; set; }

		public bool IsSatisfy(KeyEventArgs e)
		{
			return e.Control == Control &&
			       e.Shift == Shift &&
			       e.Alt == Alt &&
			       e.KeyCode == Key;
		}
	}
}
