using System;
using System.Collections.Generic;
using System.Linq;
using Duality;
using Duality.Input;

namespace MFEP.Duality.Plugins.InputPlugin
{
	public static class InputManager
	{
		[DontSerialize] private static Dictionary<string, VirtualButton> buttonDict = new Dictionary<string, VirtualButton> ();
		[DontSerialize] private static IMappingSerializer serializer;

		public static IEnumerable<ButtonTuple> Buttons
		{
			get {
				return buttonDict.Select (buttonPair => new ButtonTuple (buttonPair.Key, buttonPair.Value.KeyVals));
			}
		}

		public static event Action<ButtonTuple> ButtonAdded;
		public static event Action<string> ButtonRemoved;
		public static event Action<string, string> ButtonRenamed;
		public static event Action<string, KeyValue> KeyAddedToButton;
		public static event Action<string, KeyValue> KeyRemovedFromButton;

		public static KeyValue[] GetKeysOfButton (string buttonName)
		{
			if (!buttonDict.ContainsKey (buttonName)) {
				LogNonExistingButton (buttonName);
				return null;
			}
			return buttonDict[buttonName].KeyVals;
		}

		public static void RegisterButton ()
		{
			var newName = GetUnusedButtonName ();
			buttonDict[newName] = new VirtualButton ();
			SaveMapping ();
			ButtonAdded?.Invoke (new ButtonTuple (newName, new KeyValue[0]));
		}

		public static bool RegisterButton (ButtonTuple newButton)
		{
			if (string.IsNullOrWhiteSpace (newButton?.ButtonName) || (newButton.KeyValues == null)) return false;
			if (buttonDict.ContainsKey (newButton.ButtonName)) {
				Log.Core.WriteWarning ($"Overwriting virtual button '{newButton.ButtonName}'");
			}
			buttonDict[newButton.ButtonName] = new VirtualButton (newButton.KeyValues);
			SaveMapping ();
			ButtonAdded?.Invoke (newButton);
			return true;
		}

		public static bool RemoveButton (string buttonName)
		{
			if (!buttonDict.ContainsKey (buttonName)) {
				LogNonExistingButton (buttonName, "Cannot remove button. ");
				return false;
			}
			buttonDict.Remove (buttonName);
			SaveMapping ();
			ButtonRemoved?.Invoke (buttonName);
			return true;
		}

		public static bool AddToButton (string buttonName, KeyValue keyValue)
		{
			if (!buttonDict.ContainsKey (buttonName)) {
				LogNonExistingButton (buttonName, "Cannot add key to button. ");
				return false;
			}
			if (!buttonDict[buttonName].Associate (keyValue)) return false;
			SaveMapping ();
			KeyAddedToButton?.Invoke (buttonName, keyValue);
			return true;
		}

		public static bool AddToButton (string buttonName, Key newKey)
		{
			return AddToButton (buttonName, new KeyValue (newKey));
		}

		public static bool AddToButton (string buttonName, MouseButton mouseButton)
		{
			return AddToButton (buttonName, new KeyValue (mouseButton));
		}

		public static bool RemoveFromButton (string buttonName, KeyValue keyValue)
		{
			if (!buttonDict.ContainsKey (buttonName)) {
				LogNonExistingButton (buttonName, "Cannot remove key from button. ");
				return false;
			}
			if (!buttonDict[buttonName].Remove (keyValue)) return false;
			SaveMapping ();
			KeyRemovedFromButton?.Invoke (buttonName, keyValue);
			return true;
		}

		public static bool RemoveFromButton (string buttonName, Key key)
		{
			return RemoveFromButton (buttonName, new KeyValue (key));
		}

		public static bool RemoveFromButton (string buttonName, MouseButton mouseButton)
		{
			return RemoveFromButton (buttonName, new KeyValue (mouseButton));
		}

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
			return true;
		}

		public static bool IsButtonPressed (string buttonName)
		{
			if (buttonDict.ContainsKey (buttonName)) return buttonDict[buttonName].IsPressed;
			throw new ArgumentException ($"The button named {buttonName} does not exist.");
		}

		public static bool IsButtonHit (string buttonName)
		{
			if (buttonDict.ContainsKey (buttonName)) return buttonDict[buttonName].IsHit;
			throw new ArgumentException ($"The button named {buttonName} does not exist.");
		}

		public static bool IsButtonReleased (string buttonName)
		{
			if (buttonDict.ContainsKey (buttonName)) return buttonDict[buttonName].IsReleased;
			throw new ArgumentException ($"The button named {buttonName} does not exist.");
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
			buttonDict = new Dictionary<string, VirtualButton> ();
			Log.Core.WriteWarning ("Input mapping not found. Creating empty.");
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
	}
}