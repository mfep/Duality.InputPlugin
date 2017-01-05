using Duality;
using Duality.Editor;
using Duality.Input;

namespace MFEP.Duality.Plugins.InputPlugin.Example
{
	[EditorHintCategory (ResNames.EditorCategory)]
	public class VirtualButtonsFromCode : Component, ICmpUpdatable
	{
		public void OnUpdate ()
		{
			if (DualityApp.Keyboard[Key.Number1]) AddButtons ();
			if (DualityApp.Keyboard[Key.Number2]) RemoveButton ();
			if (DualityApp.Keyboard[Key.Number3]) RenameButton ();
			if (DualityApp.Keyboard[Key.Number4]) AddKeys ();
			if (DualityApp.Keyboard[Key.Number5]) RemoveKeys ();
		}

		private void RemoveKeys ()
		{
			InputManager.RemoveFromButton ("Rave", Key.Space);
		}

		private void AddKeys ()
		{
			InputManager.AddToButton ("Rave", Key.Space);
		}

		private void RenameButton ()
		{
			InputManager.RenameButton ("Sleep", "Rave");
		}

		private void RemoveButton ()
		{
			InputManager.RemoveButton ("Eat");
		}

		private void AddButtons ()
		{
			InputManager.RegisterButton (new ButtonTuple ("Eat", new[] { new KeyValue (Key.ControlLeft), new KeyValue (Key.ControlRight) }));
			InputManager.RegisterButton (new ButtonTuple ("Sleep", new[] { new KeyValue (Key.AltLeft), new KeyValue (Key.AltRight) }));
		}
	}
}