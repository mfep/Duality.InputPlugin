using System;
using Duality.Input;

namespace MFEP.Duality.Editor.Plugins.InputPlugin
{
	public partial class InputKeyBoxCreator : InputKeyBox
	{
		public InputKeyBoxCreator ()
		{
			InitializeComponent ();
		}

		public event Action<Key> AddButtonClicked;

		protected override void deleteButton_Click (object sender, EventArgs e)
		{
			AddButtonClicked?.Invoke (SelectedKey);
		}
	}
}