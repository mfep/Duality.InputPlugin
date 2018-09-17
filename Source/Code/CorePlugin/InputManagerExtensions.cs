using Duality;

namespace mfep.Duality.Plugins.InputPlugin
{
	public static class InputManagerExtensions
	{
		public static InputManager InputManager (this Component cmp) => cmp?.GameObj?.Scene?.FindComponent<InputManager> ();
	}
}