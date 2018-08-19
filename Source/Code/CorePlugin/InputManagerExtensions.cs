using Duality;

namespace MFEP.Duality.Plugins.InputPlugin
{
	public static class InputManagerExtensions
	{
		public static InputManager InputManager (this Component cmp) => cmp?.GameObj?.ParentScene?.FindComponent<InputManager> ();
	}
}