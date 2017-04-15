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
		Last = GamepadButtonType
	}

	/// <summary>
	/// Handles different <see cref="InputPlugin.KeyType"/>s uniformly.
	/// </summary>
	public struct KeyValue
	{
		private const int mouseButtonOffset   = 1000;
		private const int gamepadButtonOffset = 2000;
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
				return KeyType.GamepadButtonType;
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
					default:
						throw new ArgumentOutOfRangeException ();
				}
			}
		}

		internal bool IsPressed
		{
			get {
				switch (KeyType) {
					case KeyType.KeyboardType:
						return DualityApp.Keyboard.KeyPressed ((Key)this);
					case KeyType.MouseButtonType:
						return DualityApp.Mouse.ButtonPressed ((MouseButton)this);
					case KeyType.GamepadButtonType:
						return DualityApp.Gamepads[0].ButtonPressed ((GamepadButton)this);
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
					default:
						throw new ArgumentOutOfRangeException ();
				}
			}
		}
	}
}