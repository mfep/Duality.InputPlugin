using Duality;

namespace MFEP.Duality.Plugins.InputPlugin
{
	/// <summary>
	///     Defines a Duality core plugin.
	/// </summary>
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