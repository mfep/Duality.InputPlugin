using System;
using MFEP.Duality.Plugins.InputPlugin;

namespace MFEP.Duality.Editor.Plugins.InputPlugin.Modules
{
	internal partial class AddKeyBox : InputKeyBox
	{
		public AddKeyBox ()
		{
			InitializeComponent ();
		}

		public event Action<KeyValue> AddButtonClicked;

		private void button_Click (object sender, EventArgs e)
		{
			AddButtonClicked?.Invoke (SelectedKeyValue);
		}
	}
}