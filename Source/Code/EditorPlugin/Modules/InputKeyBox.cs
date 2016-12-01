using System;
using System.Linq;
using System.Windows.Forms;
using Duality.Input;
using MFEP.Duality.Editor.Plugins.InputPlugin.Properties;
using MFEP.Duality.Plugins.InputPlugin;

namespace MFEP.Duality.Editor.Plugins.InputPlugin.Modules
{
	internal partial class InputKeyBox : UserControl
	{
		private KeyType selectedKeyType = KeyType.KeyboardType;

		protected InputKeyBox ()
		{
			InitializeComponent ();

			UpdateControls ();
		}

		protected KeyValue SelectedKeyValue
		{
			get
			{
				var selectedItem = comboBox.SelectedItem;
				return selectedKeyType == KeyType.KeyboardType ? new KeyValue ((Key)selectedItem) : new KeyValue ((MouseButton)selectedItem);
			}
		}

		protected void SelectKeyValue (KeyValue keyValue)
		{
			selectedKeyType = keyValue.KeyType;
			UpdateControls ();
			comboBox.SelectedIndex = keyValue.Index;
		}

		protected void UpdateControls ()
		{
			switch (selectedKeyType) {
				case KeyType.KeyboardType:
					var keys = Enum.GetValues (typeof(Key)).Cast<Key> ().ToList ();
					comboBox.DataSource = keys;
					KeyTypeBtn.Image = Resources.keyboard;
					toolTip1.SetToolTip (comboBox, "Physical key on the international keyboard");
					break;
				case KeyType.MouseButtonType:
					var mouseButtons = Enum.GetValues (typeof(MouseButton)).Cast<MouseButton> ().ToList ();
					comboBox.DataSource = mouseButtons;
					KeyTypeBtn.Image = Resources.mouse_pc;
					toolTip1.SetToolTip(comboBox, "Physical button on the mouse");
					break;
				default:
					throw new ArgumentOutOfRangeException ();
			}
		}

		private void SelectNextKeyType ()
		{
			selectedKeyType = selectedKeyType == KeyType.KeyboardType ? KeyType.MouseButtonType : KeyType.KeyboardType;
			UpdateControls ();
		}

		private void KeyTypeBtn_Click (object sender, EventArgs e)
		{
			SelectNextKeyType ();
		}
	}
}