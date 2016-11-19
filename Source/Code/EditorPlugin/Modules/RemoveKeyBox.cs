using System;
using Duality.Input;

namespace MFEP.Duality.Editor.Plugins.InputPlugin.Modules
{
	internal partial class RemoveKeyBox : InputKeyBox
	{
		public RemoveKeyBox (Key key)
		{
			InitializeComponent ();
			comboBox.SelectedItem = key;
		}

		public event Action<Key> RemoveButtonClicked;

		private void button_Click (object sender, EventArgs e)
		{
			RemoveButtonClicked?.Invoke (SelectedKey);
		}
	}
}