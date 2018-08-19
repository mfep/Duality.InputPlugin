using System;
using System.Collections.Generic;
using System.Linq;
using Duality;
using Duality.Input;

namespace MFEP.Duality.Plugins.InputPlugin
{
	/// <summary>
	/// Manager class for every Virtual Button related operation, such as adding and removing buttons,
	/// associating keyboard keys and mouse buttons with them, renaming buttons, and getting their status.
	/// </summary>
	public static class InputManager
	{
		[DontSerialize] private static Dictionary<string, VirtualButton> buttonDict;
		public static Dictionary<string, VirtualButton> ButtonDict { get => buttonDict; set => buttonDict = value; }

		/// <summary>
		/// Checks if any of the <see cref="KeyValue"/>s associated with a Virtual Button is pressed at the moment.
		/// </summary>
		/// <param name="buttonName">The string identifier of the Virtual Button.</param>
		/// <returns>Returns true, if any of the <see cref="KeyValue"/>s is pressed.</returns>
		public static bool IsButtonPressed (string buttonName)
		{
			if (buttonDict.ContainsKey (buttonName)) return buttonDict[buttonName].IsPressed;
			throw new ArgumentException ($"The button named {buttonName} does not exist.");
		}

		/// <summary>
		/// Checks if any of the <see cref="KeyValue"/>s associated with a Virtual Button has been hit in the current frame.
		/// </summary>
		/// <param name="buttonName">The string identifier of the Virtual Button.</param>
		/// <returns>Returns true, if any of the <see cref="KeyValue"/>s is hit.</returns>
		public static bool IsButtonHit (string buttonName)
		{
			if (buttonDict.ContainsKey (buttonName)) return buttonDict[buttonName].IsHit;
			throw new ArgumentException ($"The button named {buttonName} does not exist.");
		}

		/// <summary>
		/// Checks if any of the <see cref="KeyValue"/>s associated with a Virtual Button is released in the current frame.
		/// </summary>
		/// <param name="buttonName">The string identifier of the Virtual Button.</param>
		/// <returns>Returns true, if any of the <see cref="KeyValue"/>s is released.</returns>
		public static bool IsButtonReleased (string buttonName)
		{
			if (buttonDict.ContainsKey (buttonName)) return buttonDict[buttonName].IsReleased;
			throw new ArgumentException ($"The button named {buttonName} does not exist.");
		}

		public static float GetAxis (string buttonName)
		{
			if (buttonDict.ContainsKey(buttonName)) return buttonDict[buttonName].Value;
			throw new ArgumentException($"The button named {buttonName} does not exist.");
		}

		internal static void UpdateButtons (float dt)
		{
			if (buttonDict == null) {
				return;
			}
			foreach (var buttonPair in buttonDict) {
				buttonPair.Value.Update (dt);
			}
		}
	}
}