using System;
using Duality;
using Duality.Input;

namespace MFEP.Duality.Plugins.InputPlugin
{
	public enum KeyType
	{
		KeyboardType = 0,
		MouseButtonType = 1
	}

	public struct KeyValue
	{
		private const int mouseButtonOffset = 1000;
		private readonly int storeField;

		public KeyValue (Key key)
		{
			storeField = (int)key;
		}

		public KeyValue (MouseButton mouseButton)
		{
			storeField = (int)mouseButton + mouseButtonOffset;
		}

		public KeyType KeyType => storeField < mouseButtonOffset ? KeyType.KeyboardType : KeyType.MouseButtonType;

		public override bool Equals (object obj)
		{
			if (obj is KeyValue)
				return storeField == ((KeyValue)obj).storeField;
			return false;
		}

		public override int GetHashCode ()
		{
			return storeField;
		}

		public static explicit operator Key (KeyValue kv)
		{
			if (kv.KeyType != KeyType.KeyboardType) throw new InvalidCastException();
			return (Key)kv.storeField;
		}

		public static explicit operator MouseButton (KeyValue kv)
		{
			if (kv.KeyType != KeyType.MouseButtonType) throw new InvalidCastException();
			return (MouseButton)(kv.storeField - mouseButtonOffset);
		}

		public int Index
		{
			get
			{
				switch (KeyType) {
					case KeyType.KeyboardType:
						return storeField;
					case KeyType.MouseButtonType:
						return storeField - mouseButtonOffset;
					default:
						throw new ArgumentOutOfRangeException ();
				}
			}
		}

		internal bool IsHit
		{
			get
			{
				switch (KeyType) {
					case KeyType.KeyboardType:
						return DualityApp.Keyboard.KeyHit ((Key)this);
					case KeyType.MouseButtonType:
						return DualityApp.Mouse.ButtonHit ((MouseButton)this);
					default:
						throw new ArgumentOutOfRangeException ();
				}
			}
		}

		internal bool IsPressed
		{
			get
			{
				switch (KeyType) {
					case KeyType.KeyboardType:
						return DualityApp.Keyboard.KeyPressed ((Key)this);
					case KeyType.MouseButtonType:
						return DualityApp.Mouse.ButtonPressed ((MouseButton)this);
					default:
						throw new ArgumentOutOfRangeException ();
				}
			}
		}

		internal bool IsReleased
		{
			get
			{
				switch (KeyType) {
					case KeyType.KeyboardType:
						return DualityApp.Keyboard.KeyReleased ((Key)this);
					case KeyType.MouseButtonType:
						return DualityApp.Mouse.ButtonReleased ((MouseButton)this);
					default:
						throw new ArgumentOutOfRangeException ();
				}
			}
		}
	}
}