using System;
using System.Linq;
using System.Windows.Forms;
using Duality.Input;

namespace MFEP.Duality.Editor.Plugins.InputPlugin.Modules
{
	internal partial class InputKeyBox : UserControl
	{
		protected InputKeyBox ()
		{
			InitializeComponent ();

			var keys = Enum.GetValues (typeof(Key)).Cast<Key> ().ToList ();
			comboBox.DataSource = keys;
		}

		protected Key SelectedKey => (Key)comboBox.SelectedItem;
	}
}