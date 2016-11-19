using System;
using System.Windows.Forms;
using MFEP.Duality.Plugins.InputPlugin;
using WeifenLuo.WinFormsUI.Docking;
using ButtonTuple = System.Tuple<string, Duality.Input.Key[]>;

namespace MFEP.Duality.Editor.Plugins.InputPlugin.Modules
{
	public partial class InputEditor : DockContent
	{
		public InputEditor ()
		{
			InitializeComponent ();
			foreach (var buttonTuple in InputManager.Buttons) CreateButtonControl (buttonTuple);
			SubscribeToInputManager ();
		}

		private void SubscribeToInputManager ()
		{
			InputManager.ButtonAdded += ManagerButtonAdded;
		}

		private void ManagerButtonAdded (ButtonTuple buttonTuple)
		{
			CreateButtonControl (buttonTuple);
		}

		private void CreateButtonControl (ButtonTuple buttonTuple)
		{
			var buttonControl = new ButtonControl (buttonTuple)
			{
				Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
				Dock = DockStyle.Top
			};
			virtualButtonsPanel.Controls.Add (buttonControl);
			buttonControl.BringToFront ();
			newButton.BringToFront ();
		}

		private void newButton_Click (object sender, EventArgs e)
		{
			InputManager.RegisterButton ();
		}
	}
}