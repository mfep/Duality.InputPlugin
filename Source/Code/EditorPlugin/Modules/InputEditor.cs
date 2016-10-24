using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MFEP.Duality.Plugins.InputPlugin;
using WeifenLuo.WinFormsUI.Docking;

namespace MFEP.Duality.Editor.Plugins.InputPlugin
{
	public partial class InputEditor : DockContent
	{
		private readonly Dictionary<string, VirtualButtonControl> virtualButtonControlDict =
			new Dictionary<string, VirtualButtonControl> ();

		public InputEditor ()
		{
			InitializeComponent ();
			CreateButtonsFromManager ();
			InputManager.ButtonsChanged += ButtonsChangedCallback;
		}

		private void CreateButtonsFromManager ()
		{
			foreach (var button in InputManager.Buttons) AddVirtualButtonControl (button);
		}

		private void DisposeButtons ()
		{
			foreach (var virtualButtonControl in virtualButtonControlDict.Values.ToList ()) virtualButtonControl.Dispose ();
		}

		private void AddVirtualButtonControl (VirtualButton button)
		{
			if (virtualButtonControlDict.ContainsKey (button.Name)) return;
			var virtualButtonControl = new VirtualButtonControl (button)
			{
				Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
				Dock = DockStyle.Top
			};
			virtualButtonsPanel.Controls.Add (virtualButtonControl);
			virtualButtonControl.BringToFront ();
			newButton.BringToFront ();
			virtualButtonControlDict.Add (button.Name, virtualButtonControl);
		}

		internal void RemoveVirtualButtonControl (string buttonName)
		{
			if (virtualButtonControlDict.ContainsKey (buttonName) && !virtualButtonControlDict[buttonName].IsDisposed) {
				virtualButtonControlDict[buttonName].Dispose ();
				virtualButtonControlDict.Remove (buttonName);
			}
		}

		private void newButton_Click (object sender, EventArgs e)
		{
			var button = new VirtualButton (InputManager.GetUnusedButtonName ());
			InputManager.RegisterButton (button);
			AddVirtualButtonControl (button);
		}

		private void ButtonsChangedCallback (string buttonName)
		{
			if (!virtualButtonControlDict.ContainsKey (buttonName))
				AddVirtualButtonControl (InputManager.GetVirtualButton (buttonName));
			if ((InputManager.GetVirtualButton (buttonName) == null) && virtualButtonControlDict.ContainsKey (buttonName))
				RemoveVirtualButtonControl (buttonName);
		}
	}
}