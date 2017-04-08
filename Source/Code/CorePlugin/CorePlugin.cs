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

		protected override void OnBeforeUpdate ()
		{
			base.OnBeforeUpdate ();
			InputManager.UpdateButtons (Time.TimeMult * Time.SPFMult);
		}
	}
}