using Duality;
using MFEP.Duality.Plugins.InputPlugin.Serialization;

namespace MFEP.Duality.Plugins.InputPlugin
{
	public class InputPluginCorePlugin : CorePlugin
	{
		protected override void InitPlugin ()
		{
			base.InitPlugin ();
			InputManager.SetSerializer (new DualityXmlSerializer ());
			InputManager.LoadMapping ();
		}

		protected override void OnDisposePlugin ()
		{
			base.OnDisposePlugin ();
			InputManager.SaveMapping ();
		}
	}
}