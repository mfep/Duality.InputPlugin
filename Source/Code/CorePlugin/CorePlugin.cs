using Duality;
using Duality.Resources;
//using mfep.Duality.Plugins.InputPlugin.Serialization;

namespace mfep.Duality.Plugins.InputPlugin
{
	public class InputPluginCorePlugin : CorePlugin
	{
		protected override void OnBeforeUpdate ()
		{
			base.OnBeforeUpdate ();
			Scene.Current.FindComponent<InputManager> ()?.UpdateButtons (Time.DeltaTime);
		}
	}
}