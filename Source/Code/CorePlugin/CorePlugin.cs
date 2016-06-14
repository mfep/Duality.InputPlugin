using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;

namespace MFEP.Duality.Plugins.InputPlugin
{
	/// <summary>
	/// Defines a Duality core plugin.
	/// </summary>
	public class InputPluginCorePlugin : CorePlugin
	{
        protected override void InitPlugin()
        {
            base.InitPlugin();
            InputManager.LoadMapping();
        }
        protected override void OnDisposePlugin()
        {
            base.OnDisposePlugin();
            InputManager.SaveMapping();
        }
    }
}
