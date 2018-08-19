using System;
using System.Collections.Generic;
using Duality;

namespace MFEP.Duality.Plugins.InputPlugin
{
	/// <summary>
	/// Manager class for every Virtual Button related operation, such as adding and removing buttons,
	/// associating keyboard keys and mouse buttons with them, renaming buttons, and getting their status.
	/// </summary>
	public class InputManager : Component, ICmpInitializable
	{
		[DontSerialize] private Dictionary<string, VirtualButton> buttonDict;
		private ContentRef<InputMapping> inputMapping;

		public ContentRef<InputMapping> InputMapping 
		{
			get => inputMapping;
			set {
				inputMapping = value;
				buttonDict = inputMapping.Res.ButtonDict;
			}
		}

		void ICmpInitializable.OnInit (InitContext context)
		{
			if (context != InitContext.AddToGameObject) {
				return;
			}
			var presentInputManager = this.InputManager ();
			if (presentInputManager != null && presentInputManager != this) {
				Log.Core.WriteError ($"An InputManager is already present in the scene: {presentInputManager}");
			}
		}

		void ICmpInitializable.OnShutdown (ShutdownContext context)
		{
		}

		/// <summary>
		/// Checks if any of the <see cref="KeyValue"/>s associated with a Virtual Button is pressed at the moment.
		/// </summary>
		/// <param name="buttonName">The string identifier of the Virtual Button.</param>
		/// <returns>Returns true, if any of the <see cref="KeyValue"/>s is pressed.</returns>
		public bool IsButtonPressed (string buttonName)
		{
			if (buttonDict.ContainsKey (buttonName)) return buttonDict[buttonName].IsPressed;
			throw new ArgumentException ($"The button named {buttonName} does not exist.");
		}

		/// <summary>
		/// Checks if any of the <see cref="KeyValue"/>s associated with a Virtual Button has been hit in the current frame.
		/// </summary>
		/// <param name="buttonName">The string identifier of the Virtual Button.</param>
		/// <returns>Returns true, if any of the <see cref="KeyValue"/>s is hit.</returns>
		public bool IsButtonHit (string buttonName)
		{
			if (buttonDict.ContainsKey (buttonName)) return buttonDict[buttonName].IsHit;
			throw new ArgumentException ($"The button named {buttonName} does not exist.");
		}

		/// <summary>
		/// Checks if any of the <see cref="KeyValue"/>s associated with a Virtual Button is released in the current frame.
		/// </summary>
		/// <param name="buttonName">The string identifier of the Virtual Button.</param>
		/// <returns>Returns true, if any of the <see cref="KeyValue"/>s is released.</returns>
		public bool IsButtonReleased (string buttonName)
		{
			if (buttonDict.ContainsKey (buttonName)) return buttonDict[buttonName].IsReleased;
			throw new ArgumentException ($"The button named {buttonName} does not exist.");
		}

		public float GetAxis (string buttonName)
		{
			if (buttonDict.ContainsKey(buttonName)) return buttonDict[buttonName].Value;
			throw new ArgumentException($"The button named {buttonName} does not exist.");
		}

		internal void UpdateButtons (float dt)
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
