using System;
using Duality;
using Duality.Input;

namespace MFEP.Duality.Plugins.InputPlugin
{
	public abstract class KeyValue
	{
		internal abstract bool IsHit { get; }
		internal abstract bool IsReleased { get; }
		internal abstract bool IsPressed (float deadZone);

		internal virtual float GetAxis (float deadZone)
		{
			return IsPressed (deadZone) ? 1.0f : 0.0f;
		}
	}

	public class KeyboardKey : KeyValue
	{
		private Key key;
		public Key Key { get => key; set => key = value; }

		internal override bool IsHit => DualityApp.Keyboard.KeyHit (key);
		internal override bool IsReleased => DualityApp.Keyboard.KeyReleased (key);
		internal override bool IsPressed (float deadZone) => DualityApp.Keyboard.KeyPressed (key);
	}

	public class MouseButton : KeyValue
	{
		private global::Duality.Input.MouseButton mouseButton;
		public global::Duality.Input.MouseButton Button { get => mouseButton; set => mouseButton = value; }

		internal override bool IsHit => DualityApp.Mouse.ButtonHit (mouseButton);
		internal override bool IsReleased => DualityApp.Mouse.ButtonReleased (mouseButton);
		internal override bool IsPressed (float deadZone) => DualityApp.Mouse.ButtonPressed (mouseButton);
	}

	public class GamepadButton : KeyValue
	{
		private global::Duality.GamepadButton gamepadButton;
		public global::Duality.GamepadButton Button { get => gamepadButton; set => gamepadButton = value; }

		internal override bool IsHit => DualityApp.Gamepads[0].ButtonHit (gamepadButton);
		internal override bool IsReleased => DualityApp.Gamepads[0].ButtonReleased (gamepadButton);
		internal override bool IsPressed (float deadZone) => DualityApp.Gamepads[0].ButtonPressed (gamepadButton);
	}

	public class GamepadAxis : KeyValue
	{
		private global::Duality.Input.GamepadAxis gamepadAxis;
		public global::Duality.Input.GamepadAxis Axis { get => gamepadAxis; set => gamepadAxis = value; }

		internal override bool IsHit => false;
		internal override bool IsReleased => false;
		internal override bool IsPressed (float deadZone) => MathF.Abs (DualityApp.Gamepads[0].AxisValue (gamepadAxis)) > deadZone;
		internal override float GetAxis (float deadZone)
		{
			return ClampWithDeadZone (DualityApp.Gamepads[0].AxisValue (gamepadAxis), deadZone);
		}

		private static float ClampWithDeadZone (float x, float deadZone)
		{
			return MathF.Abs (x) < deadZone ? 0.0f : x;
		}
	}
}