using System;
using Duality;
using Duality.Editor;
using Duality.Input;
using ButtonTuple = System.Tuple<string, Duality.Input.Key[]>;

namespace MFEP.Duality.Plugins.InputPlugin.Example
{
	[EditorHintCategory (ResNames.EditorCategory)]
	public class VirtualButtonsFromCode : Component, ICmpUpdatable
	{
		[DontSerialize] private readonly Random random = new Random (DateTime.Now.Millisecond);

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
			InputManager.RemoveKeyFromButton ("Rave", Key.Space);
		}

		private void AddKeys ()
		{
			InputManager.AddKeyToButton ("Rave", Key.Space);
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
			InputManager.RegisterButton (new ButtonTuple ("Eat", new[] { Key.ControlLeft, Key.ControlRight }));
			InputManager.RegisterButton (new ButtonTuple ("Sleep", new[] { Key.AltLeft, Key.AltRight }));
		}
	}
}