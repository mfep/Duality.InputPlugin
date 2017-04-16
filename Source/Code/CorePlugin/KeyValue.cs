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

	/// <summary>
	/// Handles different <see cref="InputPlugin.KeyType"/>s uniformly.
	/// </summary>
	public struct KeyValue
	{
		private const int mouseButtonOffset   = 1000;
		private const int gamepadButtonOffset = 2000;
		private const int gamepadAxisOffset   = 3000;
		private readonly int storeField;

		/// <summary>
		/// Constructs a <see cref="KeyValue"/> from a <see cref="Key"/>.
		/// </summary>
		public KeyValue (Key key)
		{
			storeField = (int)key;
		}

		/// <summary>
		/// Constructs a <see cref="KeyValue"/> from a <see cref="MouseButton"/>.
		/// </summary>
		public KeyValue (MouseButton mouseButton)
		{
			storeField = (int)mouseButton + mouseButtonOffset;
		}

		public KeyValue (GamepadButton gamepadButton)
		{
			storeField = (int)gamepadButton + gamepadButtonOffset;
		}

		public KeyValue (GamepadAxis gamepadAxis)
		{
			storeField = (int)gamepadAxis + gamepadAxisOffset;
		}

		/// <summary>
		/// The <see cref="InputPlugin.KeyType"/> of this <see cref="KeyValue"/>.
		/// </summary>
		public KeyType KeyType
		{
			get {
				if (storeField < mouseButtonOffset) {
					return KeyType.KeyboardType;
				}
				if (storeField < gamepadButtonOffset) {
					return KeyType.MouseButtonType;
				}
				if (storeField < gamepadAxisOffset) {
					return KeyType.GamepadButtonType;
				}
				return KeyType.GamepadAxisType;
			}
		}

		public override bool Equals (object obj)
		{
			return storeField == (obj as KeyValue?)?.storeField;
		}

		public override int GetHashCode ()
		{
			return storeField;
		}

		public static explicit operator Key (KeyValue kv)
		{
			if (kv.KeyType != KeyType.KeyboardType) throw new InvalidCastException ();
			return (Key)kv.storeField;
		}

		public static explicit operator MouseButton (KeyValue kv)
		{
			if (kv.KeyType != KeyType.MouseButtonType) throw new InvalidCastException ();
			return (MouseButton)(kv.storeField - mouseButtonOffset);
		}

		public static explicit operator GamepadButton (KeyValue kv)
		{
			if (kv.KeyType != KeyType.GamepadButtonType) throw new InvalidCastException ();
			return (GamepadButton)(kv.storeField - gamepadButtonOffset);
		}

		public static explicit operator GamepadAxis (KeyValue kv)
		{
			if (kv.KeyType != KeyType.GamepadAxisType) throw new InvalidCastException ();
			return (GamepadAxis)(kv.storeField - gamepadAxisOffset);
		}

		/// <summary>
		/// The index of the enum member in the original enum.
		/// </summary>
		public int Index
		{
			get {
				switch (KeyType) {
					case KeyType.KeyboardType:
						return storeField;
					case KeyType.MouseButtonType:
						return storeField - mouseButtonOffset;
					case KeyType.GamepadButtonType:
						return storeField - gamepadButtonOffset;
					case KeyType.GamepadAxisType:
						return storeField - gamepadAxisOffset;
					default:
						throw new ArgumentOutOfRangeException ();
				}
			}
		}

		internal bool IsHit
		{
			get {
				switch (KeyType) {
					case KeyType.KeyboardType:
						return DualityApp.Keyboard.KeyHit ((Key)this);
					case KeyType.MouseButtonType:
						return DualityApp.Mouse.ButtonHit ((MouseButton)this);
					case KeyType.GamepadButtonType:
						return DualityApp.Gamepads[0].ButtonHit ((GamepadButton)this);
					case KeyType.GamepadAxisType:
						return false;
					default:
						throw new ArgumentOutOfRangeException ();
				}
			}
		}

		internal bool IsReleased
		{
			get {
				switch (KeyType) {
					case KeyType.KeyboardType:
						return DualityApp.Keyboard.KeyReleased ((Key)this);
					case KeyType.MouseButtonType:
						return DualityApp.Mouse.ButtonReleased ((MouseButton)this);
					case KeyType.GamepadButtonType:
						return DualityApp.Gamepads[0].ButtonReleased ((GamepadButton)this);
					case KeyType.GamepadAxisType:
						return false;
					default:
						throw new ArgumentOutOfRangeException ();
				}
			}
		}

		internal bool IsPressed (float deadZone)
		{
			switch (KeyType) {
				case KeyType.KeyboardType:
					return DualityApp.Keyboard.KeyPressed ((Key)this);
				case KeyType.MouseButtonType:
					return DualityApp.Mouse.ButtonPressed ((MouseButton)this);
				case KeyType.GamepadButtonType:
					return DualityApp.Gamepads[0].ButtonPressed ((GamepadButton)this);
				case KeyType.GamepadAxisType:
					return MathF.Abs (DualityApp.Gamepads[0].AxisValue ((GamepadAxis)this)) > deadZone;
				default:
					throw new ArgumentOutOfRangeException ();
			}
		}

		internal float Get (float deadZone)
		{
			switch (KeyType) {
				case KeyType.KeyboardType:
				case KeyType.MouseButtonType:
				case KeyType.GamepadButtonType:
					return IsPressed (deadZone) ? 1.0f : 0.0f;
				case KeyType.GamepadAxisType:
					return ClampWithDeadZone (DualityApp.Gamepads[0].AxisValue ((GamepadAxis)this), deadZone);
				default:
					throw new ArgumentOutOfRangeException ();
			}
		}

		private static float ClampWithDeadZone (float x, float deadZone)
		{
			return MathF.Abs (x) < deadZone ? 0.0f : x;
		}
	}
}