using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Duality.Input;
using MFEP.Duality.Plugins.InputPlugin;
using ButtonTuple = System.Tuple<string, Duality.Input.Key[]>;

namespace MFEP.Duality.Editor.Plugins.InputPlugin.Modules
{
	internal partial class ButtonControl : UserControl
	{
		private readonly Dictionary<Key, RemoveKeyBox> removeKeyBoxDict = new Dictionary<Key, RemoveKeyBox> ();
		private AddKeyBox addKeyBox;
		private string btnName;

		public ButtonControl (ButtonTuple buttonTuple)
		{
			InitializeComponent ();
			btnName = buttonTuple.Item1;
			textBox1.Text = buttonTuple.Item1;
			CreateAddKeybox ();
			foreach (var key in buttonTuple.Item2) CreateRemoveKeyBox (key);
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

		private void ManagerKeyRemovedFromButton (string name, Key key)
		{
			if (name == btnName) removeKeyBoxDict[key].Dispose ();
		}

		private void ManagerKeyAddedToButton (string name, Key key)
		{
			if (name == btnName) CreateRemoveKeyBox (key);
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

		private void AddKeyboxButtonClicked (Key key)
		{
			InputManager.AddKeyToButton (btnName, key);
		}

		private void CreateRemoveKeyBox (Key key)
		{
			var removeKeyBox = new RemoveKeyBox (key) { Dock = DockStyle.Top };
			keysPanel.Controls.Add (removeKeyBox);
			removeKeyBox.BringToFront ();
			addKeyBox.BringToFront ();
			removeKeyBox.RemoveButtonClicked += RemoveKeyBoxButtonClicked;
			removeKeyBoxDict[key] = removeKeyBox;
		}

		private void RemoveKeyBoxButtonClicked (Key key)
		{
			InputManager.RemoveKeyFromButton (btnName, key);
		}

		private void textBox1_Leave (object sender, EventArgs e)
		{
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