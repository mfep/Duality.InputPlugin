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

		/// <summary>
		/// The array of <see cref="KezValue"/>s associated with <see cref="ButtonName"/>.
		/// </summary>
		public KeyValue[] KeyValues { get; }

		/// <summary>
		/// Construct a <see cref="ButtonTuple"/> from an identifier string and an array of <see cref="KeyValues"/>.
		/// This alone does not register it to the <see cref="InputManager"/>. Use <see cref="InputManager.RegisterButton (ButtonTuple)"/> for that.
		/// </summary>
		public ButtonTuple (string buttonName, KeyValue[] keyValues)
		{
			ButtonName = buttonName;
			KeyValues = keyValues;
		}

		/// <summary>
		/// Construct a <see cref="ButtonTuple"/> from an identifier string and a <see cref="Key"/>.
		/// This alone does not register it to the <see cref="InputManager"/>. Use <see cref="InputManager.RegisterButton (ButtonTuple)"/> for that.
		/// </summary>
		public ButtonTuple (string buttonName, Key key)
		{
			ButtonName = buttonName;
			KeyValues = new[] { new KeyValue (key) };
		}

		/// <summary>
		/// Construct a <see cref="ButtonTuple"/> from an identifier string and a <see cref="MouseButton"/>.
		/// This alone does not register it to the <see cref="InputManager"/>. Use <see cref="InputManager.RegisterButton (ButtonTuple)"/> for that.
		/// </summary>
		public ButtonTuple (string buttonName, MouseButton mouseButton)
		{
			ButtonName = buttonName;
			KeyValues = new[] { new KeyValue (mouseButton) };
		}
	}
}
