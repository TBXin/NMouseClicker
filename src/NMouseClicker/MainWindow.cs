using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace NMouseClicker
{
	internal partial class MainWindow : Form
	{
		private IKeyboardMouseEvents m_hook;
		private readonly List<KeyFilter> m_filters = new List<KeyFilter>();
		private bool m_isRecording;
		private bool m_isPlaying;

		public MainWindow()
		{
			InitializeComponent();

			m_filters.Add(new KeyFilter(false, false, false, Keys.F5, PlayHotkey_Pressed));
			m_filters.Add(new KeyFilter(false, false, false, Keys.F6, RecordHotkey_Pressed));

			Load += MainWindow_Load;
			Closing += MainWindow_Closing;
		}

		private void SubscribeToEvents()
		{
			m_hook = Hook.GlobalEvents();
			m_hook.KeyDown += Hook_KeyDown;
			m_hook.MouseDown += Hook_MouseDown;
		}

		public void UnsubscribeFromEvents()
		{
			m_hook.KeyDown -= Hook_KeyDown;
			m_hook.MouseClick -= Hook_MouseDown;
			m_hook.Dispose();
		}

		private void Log(string format, params object[] args)
		{
			var str = string.Format(CultureInfo.InvariantCulture, format, args);
			textBox1.AppendText(str + Environment.NewLine);
		}

		private void UpdateWindowTitle()
		{
			Text = $@"NMouseClicker, Recording: {m_isRecording}, Playing: {m_isPlaying}";
		}

		private void PlayHotkey_Pressed()
		{
			m_isPlaying = !m_isPlaying;
			UpdateWindowTitle();
		}

		private void RecordHotkey_Pressed()
		{
			m_isRecording = !m_isRecording;
			UpdateWindowTitle();
		}

		private void Hook_KeyDown(object sender, KeyEventArgs e)
		{
			foreach (var keyFilter in m_filters)
			{
				if (keyFilter.IsSatisfy(e)) keyFilter.Action();
			}
		}

		private void Hook_MouseDown(object sender, MouseEventArgs e)
		{
			if (!m_isRecording) return;
			Log($"MouseDown: {e.Button}, X:{e.X}, Y:{e.Y}");
		}

		private void MainWindow_Load(object sender, EventArgs eventArgs)
		{
			SubscribeToEvents();
			UpdateWindowTitle();
		}

		private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			UnsubscribeFromEvents();
		}
	}
}
