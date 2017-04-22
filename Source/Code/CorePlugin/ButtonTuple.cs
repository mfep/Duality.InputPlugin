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
		public float DeadZone { get; set; }

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

		public ButtonTuple (string buttonName, KeyValue positiveKeyValue = null, KeyValue negativeKeyValue = null)
		{
			ButtonName = buttonName;
			if (positiveKeyValue != null) {
				PositiveKeys = new[] { positiveKeyValue };
			}
			if (negativeKeyValue != null) {
				NegativeKeys = new[] { negativeKeyValue };
			}
		}

		internal ButtonTuple (string buttonName, VirtualButton button)
		{
			ButtonName = buttonName;
			PositiveKeys = button.PositiveKeyVals;
			NegativeKeys = button.NegativeKeyVals;
			RiseTime = button.RiseTime;
			DeadZone = button.DeadZone;
		}
	}
}
