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
			if (DualityApp.Keyboard.KeyHit (Key.Number1)) AddButtons ();
			if (DualityApp.Keyboard.KeyHit (Key.Number2)) RemoveButton ();
			if (DualityApp.Keyboard.KeyHit (Key.Number3)) RenameButton ();
			if (DualityApp.Keyboard.KeyHit (Key.Number4)) AddKeys ();
			if (DualityApp.Keyboard.KeyHit (Key.Number5)) RemoveKeys ();
			if (DualityApp.Keyboard.KeyHit (Key.BackSpace)) AddDefaultButtonSet ();
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
			InputManager.RegisterButton (new ButtonTuple ("Eat", (KeyValue)Key.ControlLeft));
			InputManager.AddToButton ("Eat", Key.ControlRight);
			InputManager.RegisterButton (new ButtonTuple ("Sleep", (KeyValue)Key.AltLeft));
			InputManager.AddToButton ("Sleep", Key.AltRight);
		}

		private void AddDefaultButtonSet ()
		{
			InputManager.RegisterButton (new ButtonTuple ("_Horizontal",
				new[] { (KeyValue)Key.D, (KeyValue)Key.Right, (KeyValue)MouseButton.Right, (KeyValue)GamepadButton.DPadRight, (KeyValue)GamepadAxis.LeftThumbstickX },
				new[] { (KeyValue)Key.A, (KeyValue)Key.Left, (KeyValue)MouseButton.Left, (KeyValue)GamepadButton.DPadLeft })
			{
				RiseTime = 1.0f,
				DeadZone = 0.3f
			});
			InputManager.RegisterButton (new ButtonTuple ("_Vertical",
				new[] { (KeyValue)Key.W, (KeyValue)Key.Up, (KeyValue)GamepadButton.DPadUp },
				new[] { (KeyValue)Key.S, (KeyValue)Key.Down, (KeyValue)GamepadButton.DPadDown, (KeyValue)GamepadAxis.LeftThumbstickY })
			{
				RiseTime = 0.001f,
				DeadZone = 0.3f
			});
		}
	}
}