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

namespace MFEP.Duality.Editor.Plugins.InputPlugin
{
    public partial class InputKeyBox : UserControl
    {
        public Key SelectedKey { get; private set; }      

        public string BtnTooltip
        {
            get
            {
                return toolTip1.GetToolTip(deleteButton);
            }
            set
            {
                toolTip1.SetToolTip(deleteButton, value);
            }
        }        

        public event Action DeleteButtonClick;
        public event Action<InputKeyBox, Key> KeySelectionChanged;

        public InputKeyBox()
        {            
            InitializeComponent();
            List<Key> keys = Enum.GetValues(typeof(Key)).Cast<Key>().ToList();
            comboBox.DataSource = keys;            
        }

        public void SetKey(Key key)
        {
            comboBox.SelectedItem = key;
            SelectedKey = key;
        }

        protected virtual void deleteButton_Click(object sender, EventArgs e)
        {
            DeleteButtonClick?.Invoke();
            Dispose();
        }

        private void comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var oldKey = SelectedKey;
            SelectedKey = (Key)comboBox.SelectedItem;
            KeySelectionChanged?.Invoke(this, oldKey);
        }
    }
}
