using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Key = Duality.Input.Key;

using MFEP.Duality.Plugins.InputPlugin;

namespace MFEP.Duality.Editor.Plugins.InputPlugin
{
    internal partial class VirtualButtonControl : UserControl
    {
        VirtualButton virtualButton;

        public VirtualButtonControl(VirtualButton button)
        {
            InitializeComponent();
            FillWithVirtualButton(button);
            inputKeyBoxCreator.AddButtonClicked += AddKeyToButton;
        }

        public bool HasKeyInVirtualButton(Key key)        
            => virtualButton.AssociatedKeys.Contains(key);
        
        private void FillWithVirtualButton(VirtualButton button)
        {
            virtualButton = button;

            textBox1.Text = button.Name;

            foreach (var key in button.AssociatedKeys) {
                AddInputKeyBox(button, key);
            }
        }

        private void DeleteKeyCallback(VirtualButton button, Key key)
        {
            button.RemoveKey(key);
        }

        private void KeyChangedCallback(InputKeyBox inputKeyBox, VirtualButton button, Key oldKey, Key newKey)
        {
            if (oldKey == newKey)            
                return;

            if (virtualButton.AssociatedKeys.Contains(newKey)) {
                inputKeyBox.SetKey(oldKey);
                return;
            }

            button.RemoveKey(oldKey);
            button.AssociateKey(newKey);
        }

        private void AddKeyToButton(Key key)
        {
            if (virtualButton.AssociateKey(key)) {
                AddInputKeyBox(virtualButton, key);
            }
        }

        private void AddInputKeyBox(VirtualButton button, Key key)
        {
            var inputKeyBox = new InputKeyBox();
            inputKeyBox.Dock = DockStyle.Top;
            keysPanel.Controls.Add(inputKeyBox);
            inputKeyBoxCreator.BringToFront();
            inputKeyBox.SetKey(key);

            inputKeyBox.DeleteButtonClick += () =>
            {
                DeleteKeyCallback(button, inputKeyBox.SelectedKey);
            };
            inputKeyBox.KeySelectionChanged += (keyBox, oldKey) =>
            {
                KeyChangedCallback(keyBox, button, oldKey, inputKeyBox.SelectedKey);
            };
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text)) return;
            var newButton = virtualButton.Clone();
            newButton.ChangeName(textBox1.Text);
            if (InputManager.RegisterButton(newButton)) {
                InputManager.RemoveButton(virtualButton.Name);
                virtualButton = newButton;
            } else {
                textBox1.Text = virtualButton.Name;
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            InputManager.RemoveButton(virtualButton.Name);
            Control parent = Parent;
            while(parent != null) {
                var inputEditor = parent as InputEditor;
                if(inputEditor != null) {
                    inputEditor.RemoveVirtualButtonControl(virtualButton.Name);
                }
                parent = parent.Parent;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Return) {
                Parent.Focus();
            }
        }
    }
}
