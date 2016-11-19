using System;
using Duality.Input;

namespace MFEP.Duality.Editor.Plugins.InputPlugin.Modules
{
	internal partial class AddKeyBox : InputKeyBox
	{
		public AddKeyBox ()
		{
			InitializeComponent ();
		}

		public event Action<Key> AddButtonClicked;

		private void button_Click (object sender, EventArgs e)
		{
			AddButtonClicked?.Invoke (SelectedKey);
		}
	}
}