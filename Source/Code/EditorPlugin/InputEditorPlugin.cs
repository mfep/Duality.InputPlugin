using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Duality.Editor;
using Duality.Editor.Forms;
using Duality.Editor.Properties;

using WeifenLuo.WinFormsUI.Docking;

using AdamsLair.WinForms.ItemModels;

namespace MFEP.Duality.Editor.Plugins.InputPlugin
{
    public class InputEditorPlugin : EditorPlugin
	{

		public override string Id
		{
			get { return "InputEditorPlugin"; }
		}

        protected override void InitPlugin(MainForm main)
        {
            base.InitPlugin(main);
            MenuModelItem viewItem = main.MainMenu.RequestItem(GeneralRes.MenuName_View);
            viewItem.AddItem(new MenuModelItem
            {
                Name = "Input Mapping",
                ActionHandler = menuItem_Click,
                Icon = Properties.Resources.keyboard
            });
        }

        private void RequestInputEditor()
        {            
            var inputEditor = new InputEditor();
            inputEditor.FormClosed += (sender, e) => { inputEditor = null; };
            inputEditor.Show(DualityEditorApp.MainForm.MainDockPanel);         
        }

        private void menuItem_Click(object sender, EventArgs e)
        {            
               RequestInputEditor();            
        }
    }
}
