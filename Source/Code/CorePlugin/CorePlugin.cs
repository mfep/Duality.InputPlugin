using Duality;
using Duality.Resources;
//using MFEP.Duality.Plugins.InputPlugin.Serialization;

namespace MFEP.Duality.Plugins.InputPlugin
{
	public class InputPluginCorePlugin : CorePlugin
	{
		protected override void OnBeforeUpdate ()
		{
			base.OnBeforeUpdate ();
			Scene.Current.FindComponent<InputManager> ()?.UpdateButtons (Time.TimeMult * Time.SPFMult);
		}
	}
}