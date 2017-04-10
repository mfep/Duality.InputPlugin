using System;
using System.Diagnostics;
using System.Windows.Forms;
using MFEP.Duality.Plugins.InputPlugin;
using Duality.Editor.Controls.ToolStrip;
using WeifenLuo.WinFormsUI.Docking;

namespace MFEP.Duality.Editor.Plugins.InputPlugin.Modules
{
	public partial class InputEditor : DockContent
	{
		public InputEditor ()
		{
			InitializeComponent ();
			toolStrip.Renderer = new DualitorToolStripProfessionalRenderer ();
			foreach (var buttonTuple in InputManager.Buttons) CreateButtonControl (buttonTuple);
			SubscribeToInputManager ();
		}

		private void SubscribeToInputManager ()
		{
			InputManager.ButtonsChanged += args =>
			{
				if (args is AddButtonEventArgs addArgs) {
					CreateButtonControl (new ButtonTuple (addArgs.ButtonName, addArgs.PositiveKeyValues, addArgs.NegativeKeyValues));
				}
			};
		}

		private void CreateButtonControl (ButtonTuple buttonTuple)
		{
			var buttonControl = new ButtonControl (buttonTuple)
			{
				Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
				Dock = DockStyle.Top
			};
			virtualButtonsPanel.Controls.Add (buttonControl);
			Focus ();
		}

		private void addButton_Click(object sender, EventArgs e)
		{
			InputManager.RegisterButton ();
		}

		private void helpButton_Click(object sender, EventArgs e)
		{
			Process.Start ("https://github.com/mfep/Duality.InputPlugin");
		}

		private void issueButton_Click(object sender, EventArgs e)
		{
			Process.Start ("http://forum.adamslair.net/viewtopic.php?f=18&t=832");
		}
	}
}