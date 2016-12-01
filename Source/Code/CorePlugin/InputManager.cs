using System;
using System.Collections.Generic;
using System.Linq;
using Duality;
using Duality.Input;
using ButtonTuple = System.Tuple<string, MFEP.Duality.Plugins.InputPlugin.KeyValue[]>;

namespace MFEP.Duality.Plugins.InputPlugin
{
	public static class InputManager
	{
		[DontSerialize] private static Dictionary<string, VirtualButton> buttonDict = new Dictionary<string, VirtualButton> ();
		[DontSerialize] private static IMappingSerializer serializer;

		public static ButtonTuple[] Buttons
		{
			get { return buttonDict.Select (pair => new ButtonTuple (pair.Key, pair.Value.KeyVals)).ToArray (); }
		}

		public static event Action<ButtonTuple> ButtonAdded;
		public static event Action<string> ButtonRemoved;
		public static event Action<string, string> ButtonRenamed;
		public static event Action<string, KeyValue> KeyAddedToButton;
		public static event Action<string, KeyValue> KeyRemovedFromButton;

		public static void RegisterButton ()
		{
			var newName = GetUnusedButtonName ();
			buttonDict[newName] = new VirtualButton ();
			SaveMapping ();
			ButtonAdded?.Invoke (new ButtonTuple (newName, new KeyValue[0]));
		}

		public static bool RegisterButton (ButtonTuple newButton)
		{
			if (string.IsNullOrWhiteSpace (newButton?.Item1) || (newButton.Item2 == null)) return false;
			if (buttonDict.ContainsKey (newButton.Item1)) return false;
			buttonDict[newButton.Item1] = new VirtualButton (newButton.Item2);
			SaveMapping ();
			ButtonAdded?.Invoke (newButton);
			return true;
		}

		public static bool RemoveButton (string name)
		{
			if (!buttonDict.ContainsKey (name)) {
				LogNonExistingButton (name, "Cannot remove button. ");
				return false;
			}
			buttonDict.Remove (name);
			SaveMapping ();
			ButtonRemoved?.Invoke (name);
			return true;
		}

		public static bool AddKeyValueToButton (string name, KeyValue keyValue)
		{
			if (!buttonDict.ContainsKey (name)) {
				LogNonExistingButton (name, "Cannot add key to button. ");
				return false;
			}
			if (!buttonDict[name].Associate (keyValue)) return false;
			SaveMapping ();
			KeyAddedToButton?.Invoke (name, keyValue);
			return true;
		}

		public static bool AddKeyToButton (string name, Key newKey)
		{
			return AddKeyValueToButton (name, new KeyValue (newKey));
		}

		public static bool AddMouseButtonToButton (string name, MouseButton mouseButton)
		{
			return AddKeyValueToButton (name, new KeyValue (mouseButton));
		}

		public static bool RemoveKeyValueFromButton (string name, KeyValue keyValue)
		{
			if (!buttonDict.ContainsKey (name)) {
				LogNonExistingButton (name, "Cannot remove key from button. ");
				return false;
			}
			if (!buttonDict[name].Remove (keyValue)) return false;
			SaveMapping ();
			KeyRemovedFromButton?.Invoke (name, keyValue);
			return true;
		}

		public static bool RemoveKeyFromButton (string name, Key key)
		{
			return RemoveKeyValueFromButton (name, new KeyValue (key));
		}

		public static bool RemoveMouseButtonFromButton (string name, MouseButton mouseButton)
		{
			return RemoveKeyValueFromButton (name, new KeyValue (mouseButton));
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

		public static bool IsButtonPressed (string name)
		{
			if (buttonDict.ContainsKey (name)) return buttonDict[name].IsPressed;
			throw new ArgumentException ($"The button named {name} does not exist.");
		}

		public static bool IsButtonHit (string name)
		{
			if (buttonDict.ContainsKey (name)) return buttonDict[name].IsHit;
			throw new ArgumentException ($"The button named {name} does not exist.");
		}

		public static bool IsButtonReleased (string name)
		{
			if (buttonDict.ContainsKey (name)) return buttonDict[name].IsReleased;
			throw new ArgumentException ($"The button named {name} does not exist.");
		}

		public static void SetSerializer (IMappingSerializer _serializer)
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