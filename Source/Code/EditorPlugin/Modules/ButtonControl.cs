﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Duality;
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
			nameTextBox.Text = buttonTuple.ButtonName;
			CreateAddKeybox (KeyRole.Positive);
			CreateAddKeybox (KeyRole.Negative);
			foreach (var keyValue in buttonTuple.PositiveKeys) CreateRemoveKeyBox (keyValue, KeyRole.Positive);
			foreach (var keyValue in buttonTuple.NegativeKeys) CreateRemoveKeyBox (keyValue, KeyRole.Negative);
			riseTimeTextBox.Text = buttonTuple.RiseTime.ToString ("G");
			deadZoneTextBox.Text = buttonTuple.DeadZone.ToString ("G");
			SubscribeToInputManager ();
		}

		private void SubscribeToInputManager ()
		{
			InputManager.ButtonsChanged += buttonArgs =>
			{
				switch (buttonArgs) {
					case RemoveButtonEventArgs removeButtonArgs:
						ManagerButtonRemoved (removeButtonArgs.ButtonName);
						break;
					case ButtonRenamedEventArgs renamedButtonArgs:
						ManagerButtonRenamed (renamedButtonArgs.OldName, renamedButtonArgs.ButtonName);
						break;
					case AddKeyToButtonEventArgs addKeyArgs:
						ManagerKeyAddedToButton (addKeyArgs.ButtonName, addKeyArgs.NewKeyValue, addKeyArgs.NewKeyRole);
						break;
					case RemoveKeyFromButtonEventArgs removeKeyArgs:
						ManagerKeyRemovedFromButton (removeKeyArgs.ButtonName, removeKeyArgs.RemovedKeyValue);
						break;
					case ButtonRiseTimeChangedEventArgs riseTimeChangedArgs:
						ManagerRiseTimeChanged (riseTimeChangedArgs.ButtonName, riseTimeChangedArgs.NewRiseTime);
						break;
				}
			};
		}

		private void ManagerButtonRenamed (string origName, string newName)
		{
			if (origName == btnName) {
				btnName = newName;
				nameTextBox.Text = newName;
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

		private void ManagerRiseTimeChanged (string name, float value)
		{
			if (btnName == name) {
				riseTimeTextBox.Text = value.ToString ("G");
			}
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
			if (btnName == nameTextBox.Text)
				return;
			if (!InputManager.RenameButton (btnName, nameTextBox.Text)) nameTextBox.Text = btnName;
		}

		private void removeButton_Click (object sender, EventArgs e)
		{
			InputManager.RemoveButton (btnName);
		}

		private void textBox1_KeyPress (object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Return) Parent.Focus ();
		}

		private void riseTimeTextBox_Validating (object sender, System.ComponentModel.CancelEventArgs e)
		{
			var riseValue = InputManager.GetButton (btnName).RiseTime;
			if (ValidateFloatTextBox (riseTimeTextBox, ref riseValue, new Range (float.Epsilon, float.MaxValue))) {
				InputManager.SetButtonRiseTime (btnName, riseValue);
			}
		}

		private void deadZoneTextBox_Validating (object sender, System.ComponentModel.CancelEventArgs e)
		{
			var deadZoneValue = InputManager.GetButton (btnName).DeadZone;
			if (ValidateFloatTextBox (deadZoneTextBox, ref deadZoneValue, new Range (0.0f, 1.0f))) {
				InputManager.SetButtonDeadZone (btnName, deadZoneValue);
			}
		}

		private bool ValidateFloatTextBox (TextBox textBox, ref float value, Range range)
		{
			try {
				value = Convert.ToSingle (textBox.Text);
				if (!range.Contains (value)) {
					throw new ArgumentOutOfRangeException ();
				}
				return true;
			}
			catch (Exception) {
				return false;
			}
			finally {
				textBox.Text = value.ToString ("G");
			}
		}
	}
}