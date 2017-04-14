using System;
using Duality.Input;

namespace MFEP.Duality.Plugins.InputPlugin
{
	/// <summary>
	/// Holds a Virtual Button name, and the <see cref="KeyValue"/>s associated with that name.
	/// </summary>
	public class ButtonTuple
	{
		/// <summary>
		/// The unique identifier string of the button.
		/// </summary>
		public string ButtonName { get; }

		public KeyValue[] PositiveKeys { get; } = new KeyValue[0];
		public KeyValue[] NegativeKeys { get; } = new KeyValue[0];
		public float RiseTime { get; set; }

		/// <summary>
		/// The array of <see cref="KeyValue"/>s associated with <see cref="ButtonName"/>.
		/// </summary>
		[Obsolete]
		public KeyValue[] KeyValues => PositiveKeys; // TODO consider joining positive and negative keys here

		/// <summary>
		/// Construct a <see cref="ButtonTuple"/> from an identifier string and an array of <see cref="KeyValue"/>s.
		/// This alone does not register it to the <see cref="InputManager"/>. Use <see cref="InputManager.RegisterButton (ButtonTuple)"/> for that.
		/// </summary>
		public ButtonTuple (string buttonName, KeyValue[] positiveKeys = null, KeyValue[] negativeKeys = null)
		{
			ButtonName = buttonName;
			if (positiveKeys != null) {
				PositiveKeys = positiveKeys;
			}
			if (negativeKeys != null) {
				NegativeKeys = negativeKeys;
			}
		}

		/// <summary>
		/// Construct a <see cref="ButtonTuple"/> from an identifier string and a <see cref="Key"/>.
		/// This alone does not register it to the <see cref="InputManager"/>. Use <see cref="InputManager.RegisterButton (ButtonTuple)"/> for that.
		/// </summary>
		public ButtonTuple (string buttonName, Key key, KeyRole role = KeyRole.Positive)
		{
			ButtonName = buttonName;
			if (role == KeyRole.Positive) {
				PositiveKeys = new[] { new KeyValue (key) };
			} else {
				NegativeKeys = new[] { new KeyValue(key) };
			}
		}

		/// <summary>
		/// Construct a <see cref="ButtonTuple"/> from an identifier string and a <see cref="MouseButton"/>.
		/// This alone does not register it to the <see cref="InputManager"/>. Use <see cref="InputManager.RegisterButton (ButtonTuple)"/> for that.
		/// </summary>
		public ButtonTuple (string buttonName, MouseButton mouseButton, KeyRole role = KeyRole.Positive)
		{
			ButtonName = buttonName;
			if (role == KeyRole.Positive) {
				PositiveKeys = new[] { new KeyValue (mouseButton) };
			} else {
				NegativeKeys = new[] { new KeyValue (mouseButton) };
			}
		}

		internal ButtonTuple (string buttonName, VirtualButton button)
		{
			ButtonName = buttonName;
			PositiveKeys = button.PositiveKeyVals;
			NegativeKeys = button.NegativeKeyVals;
			RiseTime = button.RiseTime;
		}
	}
}
