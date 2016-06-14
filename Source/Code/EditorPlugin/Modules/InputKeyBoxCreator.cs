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
    public partial class InputKeyBoxCreator : InputKeyBox
    {
        public event Action<Key> AddButtonClicked;

        public InputKeyBoxCreator()
        {
            InitializeComponent();
        }

        protected override void deleteButton_Click(object sender, EventArgs e)
        {
            AddButtonClicked?.Invoke(SelectedKey);
        }
    }
}
