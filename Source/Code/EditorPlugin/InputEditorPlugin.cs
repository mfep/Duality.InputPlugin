using System;
using AdamsLair.WinForms.ItemModels;
using Duality.Editor;
using Duality.Editor.Forms;
using Duality.Editor.Properties;
using MFEP.Duality.Editor.Plugins.InputPlugin.Modules;
using MFEP.Duality.Editor.Plugins.InputPlugin.Properties;

namespace MFEP.Duality.Editor.Plugins.InputPlugin
{
	public class InputEditorPlugin : EditorPlugin
	{
		public override string Id => "InputEditorPlugin";

		protected override void InitPlugin (MainForm main)
		{
			base.InitPlugin (main);
			var viewItem = main.MainMenu.RequestItem (GeneralRes.MenuName_View);
			viewItem.AddItem (new MenuModelItem
			{
				Name = "Input Mapping",
				ActionHandler = menuItem_Click,
				Icon = Resources.keyboard
			});
		}

		private void RequestInputEditor ()
		{
			var inputEditor = new InputEditor ();
			inputEditor.FormClosed += (sender, e) => { inputEditor = null; };
			inputEditor.Show (DualityEditorApp.MainForm.MainDockPanel);
		}

		private void menuItem_Click (object sender, EventArgs e)
		{
			RequestInputEditor ();
		}
	}
}