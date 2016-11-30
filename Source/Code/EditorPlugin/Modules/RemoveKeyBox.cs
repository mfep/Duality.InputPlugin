using System;
using MFEP.Duality.Plugins.InputPlugin;

namespace MFEP.Duality.Editor.Plugins.InputPlugin.Modules
{
	internal partial class RemoveKeyBox : InputKeyBox
	{
		public RemoveKeyBox (KeyValue keyValue)
		{
			InitializeComponent ();
			SelectKeyValue (keyValue);
		}

		public event Action<KeyValue> RemoveButtonClicked;

		private void removeButton_Click (object sender, EventArgs e)
		{
			RemoveButtonClicked?.Invoke (SelectedKeyValue);
		}
	}
}