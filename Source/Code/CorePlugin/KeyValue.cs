using System;
using Duality;
using Duality.Input;

namespace MFEP.Duality.Plugins.InputPlugin
{
	public enum KeyType
	{
		KeyboardType      = 0,
		MouseButtonType   = 1,
		GamepadButtonType = 2,
		GamepadAxisType   = 3,
		Last = GamepadAxisType
	}

	public abstract class KeyValue
	{
		public abstract KeyType KeyType { get; }
		public abstract int Index { get; }
		internal abstract bool IsHit { get; }
		internal abstract bool IsReleased { get; }
		internal abstract bool IsPressed (float deadZone);

		internal virtual float GetAxis (float deadZone)
		{
			return IsPressed (deadZone) ? 1.0f : 0.0f;
		}

		public static explicit operator KeyValue (Key key)
		{
			return new KeyboardKeyValue (key);
		}

		public static explicit operator Key (KeyValue keyValue)
		{
			if (keyValue.KeyType != KeyType.KeyboardType) throw new InvalidCastException ();
			return (Key)keyValue.Index;
		}

		public static explicit operator KeyValue (MouseButton mouseButton)
		{
			return new MouseKeyValue (mouseButton);
		}

		public static explicit operator MouseButton (KeyValue keyValue)
		{
			if (keyValue.KeyType != KeyType.MouseButtonType) throw new InvalidCastException ();
			return (MouseButton)keyValue.Index;
		}

		public static explicit operator KeyValue (GamepadButton gamepadButton)
		{
			return new GamepadButtonKeyValue (gamepadButton);
		}

		public static explicit operator GamepadButton (KeyValue keyValue)
		{
			if (keyValue.KeyType != KeyType.GamepadButtonType) throw new InvalidCastException ();
			return (GamepadButton)keyValue.Index;
		}

		public static explicit operator KeyValue (GamepadAxis gamepadAxis)
		{
			return new GamepadAxisKeyValue (gamepadAxis);
		}

		public static explicit operator GamepadAxis (KeyValue keyValue)
		{
			if (keyValue.KeyType != KeyType.GamepadAxisType) throw new InvalidCastException ();
			return (GamepadAxis)keyValue.Index;
		}
	}

	internal class KeyboardKeyValue : KeyValue
	{
		private readonly Key _key;

		public KeyboardKeyValue (Key key)
		{
			_key = key;
		}

		public override bool Equals (object obj)
		{
			if (obj is KeyboardKeyValue keyboardKeyValue) {
				return _key == keyboardKeyValue._key;
			}
			return false;
		}

		public override int GetHashCode ()
		{
			return (int)_key;
		}

		public override KeyType KeyType => KeyType.KeyboardType;
		public override int Index => (int)_key;
		internal override bool IsHit => DualityApp.Keyboard.KeyHit (_key);
		internal override bool IsReleased => DualityApp.Keyboard.KeyReleased (_key);
		internal override bool IsPressed (float deadZone) => DualityApp.Keyboard.KeyPressed (_key);
	}

	internal class MouseKeyValue : KeyValue
	{
		private readonly MouseButton _mouseButton;

		public MouseKeyValue (MouseButton mouseButton)
		{
			_mouseButton = mouseButton;
		}

		public override bool Equals (object obj)
		{
			if (obj is MouseKeyValue mouseKeyValue) {
				return _mouseButton == mouseKeyValue._mouseButton;
			}
			return false;
		}

		public override int GetHashCode ()
		{
			return (int)_mouseButton;
		}

		public override KeyType KeyType => KeyType.MouseButtonType;
		public override int Index => (int)_mouseButton;
		internal override bool IsHit => DualityApp.Mouse.ButtonHit (_mouseButton);
		internal override bool IsReleased => DualityApp.Mouse.ButtonReleased (_mouseButton);
		internal override bool IsPressed (float deadZone) => DualityApp.Mouse.ButtonPressed (_mouseButton);
	}

	internal class GamepadButtonKeyValue : KeyValue
	{
		private readonly GamepadButton _gamepadButton;

		public GamepadButtonKeyValue (GamepadButton gamepadButton)
		{
			_gamepadButton = gamepadButton;
		}

		public override bool Equals (object obj)
		{
			if (obj is GamepadButtonKeyValue gamepadButtonKeyValue) {
				return _gamepadButton == gamepadButtonKeyValue._gamepadButton;
			}
			return false;
		}

		public override int GetHashCode ()
		{
			return (int)_gamepadButton;
		}

		public override KeyType KeyType => KeyType.GamepadButtonType;
		public override int Index => (int)_gamepadButton;
		internal override bool IsHit => DualityApp.Gamepads[0].ButtonHit (_gamepadButton);
		internal override bool IsReleased => DualityApp.Gamepads[0].ButtonReleased (_gamepadButton);
		internal override bool IsPressed (float deadZone) => DualityApp.Gamepads[0].ButtonPressed (_gamepadButton);
	}

	internal class GamepadAxisKeyValue : KeyValue
	{
		private readonly GamepadAxis _gamepadAxis;

		public GamepadAxisKeyValue (GamepadAxis gamepadAxis)
		{
			_gamepadAxis = gamepadAxis;
		}

		public override bool Equals (object obj)
		{
			if (obj is GamepadAxisKeyValue gamepadAxisKeyValue) {
				return _gamepadAxis == gamepadAxisKeyValue._gamepadAxis;
			}
			return false;
		}

		public override int GetHashCode ()
		{
			return (int)_gamepadAxis;
		}

		public override KeyType KeyType => KeyType.GamepadAxisType;
		public override int Index => (int)_gamepadAxis;
		internal override bool IsHit => false;
		internal override bool IsReleased => false;
		internal override bool IsPressed (float deadZone) => MathF.Abs (DualityApp.Gamepads[0].AxisValue (_gamepadAxis)) > deadZone;
		internal override float GetAxis (float deadZone)
		{
			return ClampWithDeadZone (DualityApp.Gamepads[0].AxisValue (_gamepadAxis), deadZone);
		}

		private static float ClampWithDeadZone (float x, float deadZone)
		{
			return MathF.Abs (x) < deadZone ? 0.0f : x;
		}
	}
}