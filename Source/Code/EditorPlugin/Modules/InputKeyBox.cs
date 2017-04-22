using System;
using System.Linq;
using System.Windows.Forms;
using Duality;
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
				switch (selectedKeyType) {
					case KeyType.KeyboardType:
						return (KeyValue)(Key)selectedItem;
					case KeyType.MouseButtonType:
						return (KeyValue)(MouseButton)selectedItem;
					case KeyType.GamepadButtonType:
						return (KeyValue)(GamepadButton)selectedItem;
					case KeyType.GamepadAxisType:
						return (KeyValue)(GamepadAxis)selectedItem;
					default:
						throw new ArgumentOutOfRangeException ();
				}
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
				case KeyType.GamepadButtonType:
					var gamepadButtons = Enum.GetValues (typeof(GamepadButton)).Cast<GamepadButton> ().ToList ();
					comboBox.DataSource = gamepadButtons;
					KeyTypeBtn.Image = Resources.controller;
					toolTip1.SetToolTip (comboBox, "Physical button on a gamepad");
					break;
				case KeyType.GamepadAxisType:
					var gamepadAxes = Enum.GetValues (typeof(GamepadAxis)).Cast<GamepadAxis> ().ToList ();
					comboBox.DataSource = gamepadAxes;
					KeyTypeBtn.Image = Resources.joystick;
					toolTip1.SetToolTip (comboBox, "Analog control on a gamepad");
					break;
				default:
					throw new ArgumentOutOfRangeException ();
			}
		}

		private void SelectNextKeyType ()
		{
			selectedKeyType = selectedKeyType + 1;
			if (selectedKeyType > KeyType.Last) {
				selectedKeyType = 0;
			}
			UpdateControls ();
		}

		private void KeyTypeBtn_Click (object sender, EventArgs e)
		{
			SelectNextKeyType ();
		}
	}
}