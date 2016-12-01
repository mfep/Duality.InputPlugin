using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MFEP.Duality.Plugins.InputPlugin;
using ButtonTuple = System.Tuple<string, MFEP.Duality.Plugins.InputPlugin.KeyValue[]>;

namespace MFEP.Duality.Editor.Plugins.InputPlugin.Modules
{
	internal partial class ButtonControl : UserControl
	{
		private readonly Dictionary<KeyValue, RemoveKeyBox> removeKeyBoxDict = new Dictionary<KeyValue, RemoveKeyBox> ();
		private AddKeyBox addKeyBox;
		private string btnName;

		public ButtonControl (ButtonTuple buttonTuple)
		{
			InitializeComponent ();
			btnName = buttonTuple.Item1;
			textBox1.Text = buttonTuple.Item1;
			CreateAddKeybox ();
			foreach (var keyValue in buttonTuple.Item2) CreateRemoveKeyBox (keyValue);
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

		private void ManagerKeyAddedToButton (string name, KeyValue keyValue)
		{
			if (name == btnName) CreateRemoveKeyBox (keyValue);
		}

		private void ManagerButtonRemoved (string buttonName)
		{
			if (buttonName == btnName)
				Dispose ();
		}

		private void CreateAddKeybox ()
		{
			if (addKeyBox != null)
				return;

			addKeyBox = new AddKeyBox { Dock = DockStyle.Top };
			keysPanel.Controls.Add (addKeyBox);
			addKeyBox.BringToFront ();
			addKeyBox.AddButtonClicked += AddKeyboxButtonClicked;
		}

		private void AddKeyboxButtonClicked (KeyValue keyValue)
		{
			InputManager.AddKeyValueToButton (btnName, keyValue);
		}

		private void CreateRemoveKeyBox (KeyValue keyValue)
		{
			var removeKeyBox = new RemoveKeyBox (keyValue) { Dock = DockStyle.Top };
			keysPanel.Controls.Add (removeKeyBox);
			removeKeyBox.BringToFront ();
			addKeyBox.BringToFront ();
			removeKeyBox.RemoveButtonClicked += RemoveKeyBoxButtonClicked;
			removeKeyBoxDict[keyValue] = removeKeyBox;
		}

		private void RemoveKeyBoxButtonClicked (KeyValue keyValue)
		{
			InputManager.RemoveKeyValueFromButton (btnName, keyValue);
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