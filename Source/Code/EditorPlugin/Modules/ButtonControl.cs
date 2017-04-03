using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MFEP.Duality.Plugins.InputPlugin;

namespace MFEP.Duality.Editor.Plugins.InputPlugin.Modules
{
	internal partial class ButtonControl : UserControl
	{
		private readonly Dictionary<KeyValue, RemoveKeyBox> removeKeyBoxDict = new Dictionary<KeyValue, RemoveKeyBox> ();
		private readonly AddKeyBox[] addKeyBoxes = new AddKeyBox[2];
		private string btnName;

		public ButtonControl (ButtonTuple buttonTuple)
		{
			InitializeComponent ();
			btnName = buttonTuple.ButtonName;
			textBox1.Text = buttonTuple.ButtonName;
			CreateAddKeybox (KeyRole.Positive);
			CreateAddKeybox (KeyRole.Negative);
			foreach (var keyValue in buttonTuple.PositiveKeys) CreateRemoveKeyBox (keyValue, KeyRole.Positive);
			foreach (var keyValue in buttonTuple.NegativeKeys) CreateRemoveKeyBox (keyValue, KeyRole.Negative);
			SubscribeToInputManager ();
		}

		private void SubscribeToInputManager ()
		{
			InputManager.ButtonRemoved += ManagerButtonRemoved;
			InputManager.KeyAddedToButton += ManagerKeyAddedToButton;
			InputManager.KeyRemovedFromButton += ManagerKeyRemovedFromButton;
			InputManager.ButtonRenamed += ManagerButtonRenamed;
		}

		private void ManagerButtonRenamed (string origName, string newName)
		{
			if (origName == btnName) {
				btnName = newName;
				textBox1.Text = newName;
			}
		}

		private void ManagerKeyRemovedFromButton (string name, KeyValue keyValue)
		{
			if (name == btnName) removeKeyBoxDict[keyValue].Dispose ();
		}

		private void ManagerKeyAddedToButton (string name, KeyValue keyValue, KeyRole keyRole)
		{
			if (name == btnName) CreateRemoveKeyBox (keyValue, keyRole);
		}

		private void ManagerButtonRemoved (string buttonName)
		{
			if (buttonName == btnName)
				Dispose ();
		}

		private void CreateAddKeybox (KeyRole role)
		{
			var addKeyBox = new AddKeyBox { Dock = DockStyle.Top };

			var parent = role == KeyRole.Positive ? positiveKeysPanel : negativeKeysPanel;
			parent.Controls.Add (addKeyBox);

			addKeyBox.BringToFront ();
			addKeyBox.AddButtonClicked += keyValue => { AddKeyboxButtonClicked (keyValue, role); };
			addKeyBoxes[(int)role] = addKeyBox;
		}

		private void AddKeyboxButtonClicked (KeyValue keyValue, KeyRole role)
		{
			InputManager.AddToButton (btnName, keyValue, role);
		}

		private void CreateRemoveKeyBox (KeyValue keyValue, KeyRole role)
		{
			var removeKeyBox = new RemoveKeyBox (keyValue) { Dock = DockStyle.Top };

			var parent = role == KeyRole.Positive ? positiveKeysPanel : negativeKeysPanel;
			parent.Controls.Add (removeKeyBox);

			removeKeyBox.BringToFront ();
			addKeyBoxes[(int)role].BringToFront ();

			removeKeyBox.RemoveButtonClicked += RemoveKeyBoxButtonClicked;
			removeKeyBoxDict[keyValue] = removeKeyBox;
		}

		private void RemoveKeyBoxButtonClicked (KeyValue keyValue)
		{
			InputManager.RemoveFromButton (btnName, keyValue);
		}

		private void textBox1_Leave (object sender, EventArgs e)
		{
			if (btnName == textBox1.Text)
				return;
			if (!InputManager.RenameButton (btnName, textBox1.Text)) textBox1.Text = btnName;
		}

		private void removeButton_Click (object sender, EventArgs e)
		{
			InputManager.RemoveButton (btnName);
		}

		private void textBox1_KeyPress (object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Return) Parent.Focus ();
		}
	}
}