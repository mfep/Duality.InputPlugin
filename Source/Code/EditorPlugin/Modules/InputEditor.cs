using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

using WeifenLuo.WinFormsUI.Docking;

using MFEP.Duality.Plugins.InputPlugin;

namespace MFEP.Duality.Editor.Plugins.InputPlugin
{
    public partial class InputEditor : DockContent
    {
        private List<VirtualButtonControl> virtualButtonControlList = new List<VirtualButtonControl>();

        public InputEditor()
        {
            InitializeComponent();
            CreateButtonsFromManager();
        }

        private void CreateButtonsFromManager()
        {            
            foreach (var button in InputManager.Buttons) {
                AddVirtualButtonControl(button);
            }
        }

        private void DisposeButtons()
        {
            foreach (var virtualButtonControl in virtualButtonControlList) {
                virtualButtonControl.Dispose();
            }
        }

        private void AddVirtualButtonControl(VirtualButton button)
        {
            var virtualButtonControl = new VirtualButtonControl(button);
            virtualButtonControl.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            virtualButtonControl.Dock = DockStyle.Top;
            virtualButtonsPanel.Controls.Add(virtualButtonControl);
            virtualButtonControl.BringToFront();
            newButton.BringToFront();
            virtualButtonControlList.Add(virtualButtonControl);
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            var button = new VirtualButton(InputManager.GetUnusedButtonName());
            InputManager.RegisterButton(button);            
            AddVirtualButtonControl(button);
        }
    }
}
