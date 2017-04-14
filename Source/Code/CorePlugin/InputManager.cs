using System;
using System.Collections.Generic;
using System.Linq;
using Duality;
using Duality.Input;
using MFEP.Duality.Plugins.InputPlugin.Serialization;

namespace MFEP.Duality.Plugins.InputPlugin
{
	/// <summary>
	/// Manager class for every Virtual Button related operation, such as adding and removing buttons,
	/// associating keyboard keys and mouse buttons with them, renaming buttons, and getting their status.
	/// </summary>
	public static class InputManager
	{
		public delegate void ButtonEventHandler (ButtonEventArgs args);

		[DontSerialize] private static Dictionary<string, VirtualButton> buttonDict = new Dictionary<string, VirtualButton> ();
		[DontSerialize] private static IMappingSerializer serializer;

		/// <summary>
		/// Enumerate through all the <see cref="ButtonTuple"/>s used by the game at the moment.
		/// </summary>
		public static IEnumerable<ButtonTuple> Buttons
		{
			get {
				return buttonDict.Select (buttonPair => new ButtonTuple (buttonPair.Key, buttonPair.Value));
			}
		}

		public static event ButtonEventHandler ButtonsChanged;

		/// <summary>
		/// Called when a new Button is added to the <see cref="InputManager"/>.
		/// </summary>
		[Obsolete] public static event Action<ButtonTuple> ButtonAdded;

		/// <summary>
		/// Called when a currently associated Button has been removed from the <see cref="InputManager"/>.
		/// </summary>
		[Obsolete] public static event Action<string> ButtonRemoved;

		/// <summary>
		/// Called when a currently associated Button has been renamed.
		/// </summary>
		[Obsolete] public static event Action<string, string> ButtonRenamed;

		/// <summary>
		/// Called when a new <see cref="KeyValue"/> has been added to an existing Button.
		/// </summary>
		[Obsolete] public static event Action<string, KeyValue, KeyRole> KeyAddedToButton; // TODO document breaking change

		/// <summary>
		/// Called when a <see cref="KeyValue"/> has been removed from an existing Button.
		/// </summary>
		[Obsolete] public static event Action<string, KeyValue> KeyRemovedFromButton;

		public static KeyValue[] GetKeysOfButton(string buttonName)
		{
			if (!buttonDict.ContainsKey(buttonName)) {
				LogNonExistingButton(buttonName);
				return null;
			}
			return buttonDict[buttonName].AllKeyVals;
		}

		/// <summary>
		/// Get the <see cref="KeyValue"/>s associated with a particular Button identifier string.
		/// </summary>
		/// <param name="buttonName">The identifier string.</param>
		/// <returns></returns>
		public static KeyValue[] GetKeysOfButton (string buttonName, KeyRole role)
		{
			if (!buttonDict.ContainsKey (buttonName)) {
				LogNonExistingButton (buttonName);
				return null;
			}
			return role == KeyRole.Positive ? buttonDict[buttonName].PositiveKeyVals : buttonDict[buttonName].NegativeKeyVals;
		}

		public static ButtonTuple GetButton (string buttonName)
		{
			if (!buttonDict.ContainsKey(buttonName)) {
				LogNonExistingButton(buttonName);
				return null;
			}
			return new ButtonTuple (buttonName, buttonDict[buttonName]);
		}

		/// <summary>
		/// Registers a new empty Virtual Button with a default name.
		/// </summary>
		public static void RegisterButton ()
		{
			var newName = GetUnusedButtonName ();
			buttonDict[newName] = new VirtualButton ();
			SaveMapping ();
			ButtonAdded?.Invoke (new ButtonTuple (newName, new KeyValue[0], new KeyValue[0]));
			OnButtonEvent (new AddButtonEventArgs (newName, new KeyValue[0], new KeyValue[0]));
		}

		/// <summary>
		/// Registers a new Virtual Button.
		/// </summary>
		/// <param name="newButton">The identifier string and <see cref="KeyValue"/>s of the new Button.</param>
		/// <returns>If the registration was successful, the method returns true, otherwise false.</returns>
		public static bool RegisterButton (ButtonTuple newButton)
		{
			if (string.IsNullOrWhiteSpace (newButton?.ButtonName) || (newButton.PositiveKeys == null && newButton.NegativeKeys == null)) return false;
			if (buttonDict.ContainsKey (newButton.ButtonName)) {
				Log.Core.WriteWarning ($"Overwriting virtual button '{newButton.ButtonName}'");
			}
			buttonDict[newButton.ButtonName] = new VirtualButton (newButton.PositiveKeys, newButton.NegativeKeys);
			SaveMapping ();
			ButtonAdded?.Invoke (newButton);
			OnButtonEvent (new AddButtonEventArgs (newButton.ButtonName, newButton.PositiveKeys, newButton.NegativeKeys));
			return true;
		}

		/// <summary>
		/// Removes an existing Virtual Button from the <see cref="InputManager"/>.
		/// </summary>
		/// <param name="buttonName">The particular string identifier of the Button to remove.</param>
		/// <returns>Returns true if the operation succeeded.</returns>
		public static bool RemoveButton (string buttonName)
		{
			if (!buttonDict.ContainsKey (buttonName)) {
				LogNonExistingButton (buttonName, "Cannot remove button. ");
				return false;
			}
			buttonDict.Remove (buttonName);
			SaveMapping ();
			ButtonRemoved?.Invoke (buttonName);
			OnButtonEvent (new RemoveButtonEventArgs (buttonName));
			return true;
		}

		/// <summary>
		/// Associates a new <see cref="KeyValue"/> with an existing Virtual Button.
		/// </summary>
		/// <param name="buttonName">The string identifier of the Virtual Button.</param>
		/// <param name="keyValue">The new <see cref="KeyValue"/> to associate.</param>
		/// <returns>Returns true if the operation succeeded.</returns>
		public static bool AddToButton (string buttonName, KeyValue keyValue, KeyRole role = KeyRole.Positive)
		{
			if (!buttonDict.ContainsKey (buttonName)) {
				LogNonExistingButton (buttonName, "Cannot add key to button. ");
				return false;
			}
			if (!buttonDict[buttonName].Associate (keyValue, role)) return false;
			SaveMapping ();
			KeyAddedToButton?.Invoke (buttonName, keyValue, role);
			OnButtonEvent (new AddKeyToButtonEventArgs (buttonName, keyValue, role));
			return true;
		}

		/// <summary>
		/// Associates a new <see cref="Key"/> with an existing Virtual Button.
		/// </summary>
		/// <param name="buttonName">The string identifier of the Virtual Button.</param>
		/// <param name="newKey">The new <see cref="Key"/> to associate.</param>
		/// <returns>Returns true if the operation succeeded.</returns>
		public static bool AddToButton (string buttonName, Key newKey, KeyRole role = KeyRole.Positive)
		{
			return AddToButton (buttonName, new KeyValue (newKey), role);
		}

		/// <summary>
		/// Associates a new <see cref="MouseButton"/> with an existing Virtual Button.
		/// </summary>
		/// <param name="buttonName">The string identifier of the Virtual Button.</param>
		/// <param name="mouseButton">The new <see cref="MouseButton"/> to associate.</param>
		/// <returns>Returns true if the operation succeeded.</returns>
		public static bool AddToButton (string buttonName, MouseButton mouseButton, KeyRole role = KeyRole.Positive)
		{
			return AddToButton (buttonName, new KeyValue (mouseButton), role);
		}

		/// <summary>
		/// Removes a <see cref="KeyValue"/> from an existing Virtual Button.
		/// </summary>
		/// <param name="buttonName">The string identifier of the Virtual Button.</param>
		/// <param name="keyValue">The <see cref="KeyValue"/> to remove.</param>
		/// <returns>Returns true if the operation succeeded.</returns>
		public static bool RemoveFromButton (string buttonName, KeyValue keyValue)
		{
			if (!buttonDict.ContainsKey (buttonName)) {
				LogNonExistingButton (buttonName, "Cannot remove key from button. ");
				return false;
			}
			if (!buttonDict[buttonName].Remove (keyValue)) return false;
			SaveMapping ();
			KeyRemovedFromButton?.Invoke (buttonName, keyValue);
			OnButtonEvent (new RemoveKeyFromButtonEventArgs (buttonName, keyValue));
			return true;
		}

		/// <summary>
		/// Removes a <see cref="Key"/> from an existing Virtual Button.
		/// </summary>
		/// <param name="buttonName">The string identifier of the Virtual Button.</param>
		/// <param name="key">The <see cref="Key"/> to remove.</param>
		/// <returns>Returns true if the operation succeeded.</returns>
		public static bool RemoveFromButton (string buttonName, Key key)
		{
			return RemoveFromButton (buttonName, new KeyValue (key));
		}

		/// <summary>
		/// Removes a <see cref="MouseButton"/> from an existing Virtual Button.
		/// </summary>
		/// <param name="buttonName">The string identifier of the Virtual Button.</param>
		/// <param name="mouseButton">The <see cref="MouseButton"/> to remove.</param>
		/// <returns>Returns true if the operation succeeded.</returns>
		public static bool RemoveFromButton (string buttonName, MouseButton mouseButton)
		{
			return RemoveFromButton (buttonName, new KeyValue (mouseButton));
		}

		/// <summary>
		/// Changes the string identifier of an existing Virtual Button.
		/// </summary>
		/// <param name="originalName">The original identifier.</param>
		/// <param name="newName">The new identifier.</param>
		/// <returns>Returns true if the operation succeeded.</returns>
		public static bool RenameButton (string originalName, string newName)
		{
			if (!buttonDict.ContainsKey (originalName) || string.IsNullOrWhiteSpace (newName) || buttonDict.ContainsKey (newName)) {
				Log.Core.WriteError ($"Cannot rename button '{originalName}' to '{newName}'");
				return false;
			}
			var button = buttonDict[originalName];
			buttonDict.Remove (originalName);
			buttonDict[newName] = button;
			SaveMapping ();
			ButtonRenamed?.Invoke (originalName, newName);
			OnButtonEvent (new ButtonRenamedEventArgs (newName, originalName));
			return true;
		}

		public static void SetButtonRiseTime (string buttonName, float value)
		{
			if (!buttonDict.ContainsKey (buttonName)) {
				LogNonExistingButton (buttonName);
				return;
			}
			buttonDict[buttonName].RiseTime = value; // TODO argument check
			OnButtonEvent (new ButtonRiseTimeChangedEventArgs (buttonName, value));
		}

		public static void SetButtonDeadZone (string buttonName, float value)
		{
			if (!buttonDict.ContainsKey(buttonName))
			{
				LogNonExistingButton(buttonName);
				return;
			}
			buttonDict[buttonName].DeadZone = value; // TODO argument check
			OnButtonEvent (new ButtonDeadZoneChangedEventArgs (buttonName, value));
		}

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
			if (buttonDict.ContainsKey(buttonName)) return buttonDict[buttonName].Get ();
			throw new ArgumentException($"The button named {buttonName} does not exist.");
		}

		internal static void SetSerializer (IMappingSerializer _serializer)
		{
			serializer = _serializer;
		}

		internal static void SaveMapping ()
		{
			serializer?.SaveMapping (buttonDict);
		}

		internal static void LoadMapping ()
		{
			Log.Core.Write ("Loading input mapping.");
			buttonDict = serializer?.LoadMapping () as Dictionary<string, VirtualButton>;
			if (buttonDict != null) return;
			Log.Core.WriteWarning("Input mapping not found. Creating empty.");
			buttonDict = new Dictionary<string, VirtualButton> ();
			foreach (var virtualButtonPair in buttonDict) {
				virtualButtonPair.Value.CalculateData ();
			}
		}

		internal static void UpdateButtons (float dt)
		{
			foreach (var buttonPair in buttonDict) {
				buttonPair.Value.Update (dt);
			}
		}

		private static string GetUnusedButtonName ()
		{
			var buttonName = "Button0";
			var i = 0;
			while (buttonDict.Keys.Contains (buttonName)) buttonName = $"Button{++i}";
			return buttonName;
		}

		private static void LogNonExistingButton (string name, string addition = "")
		{
			Log.Game.WriteError ($"{addition}The button named '{name}' does not exist.");
		}

		private static void OnButtonEvent (ButtonEventArgs args)
		{
			ButtonsChanged?.Invoke (args);
		}
	}
}