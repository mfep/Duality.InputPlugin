using System;
using System.Collections.Generic;
using System.Linq;
using Duality;
using Duality.Input;
using Duality.Serialization;
using ButtonTuple = System.Tuple<string, Duality.Input.Key[]>;

namespace MFEP.Duality.Plugins.InputPlugin
{
	public static class InputManager
	{
		[DontSerialize] private static Dictionary<string, VirtualButton> buttonDict = new Dictionary<string, VirtualButton> ();

		public static ButtonTuple[] Buttons
		{
			get { return buttonDict.Select (pair => new ButtonTuple (pair.Key, pair.Value.Keys)).ToArray (); }
		}

		public static event Action<ButtonTuple> ButtonAdded;
		public static event Action<string> ButtonRemoved;
		public static event Action<string, string> ButtonRenamed;
		public static event Action<string, Key> KeyAddedToButton;
		public static event Action<string, Key> KeyRemovedFromButton;

		public static void RegisterButton ()
		{
			var newName = GetUnusedButtonName ();
			buttonDict[newName] = new VirtualButton (new Key[0]);
			SaveMapping ();
			ButtonAdded?.Invoke (new ButtonTuple (newName, new Key[0]));
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

		public static bool AddKeyToButton (string name, Key newKey)
		{
			if (!buttonDict.ContainsKey (name)) {
				LogNonExistingButton (name, "Cannot add key to button. ");
				return false;
			}
			if (!buttonDict[name].AssociateKey (newKey)) return false;
			SaveMapping ();
			KeyAddedToButton?.Invoke (name, newKey);
			return true;
		}

		public static bool RemoveKeyFromButton (string name, Key key)
		{
			if (!buttonDict.ContainsKey (name)) {
				LogNonExistingButton (name, "Cannot remove key from button. ");
				return false;
			}
			if (!buttonDict[name].RemoveKey (key)) return false;
			SaveMapping ();
			KeyRemovedFromButton?.Invoke (name, key);
			return true;
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
			LogNonExistingButton (name);
			return false;
		}

		public static bool IsButtonHit (string name)
		{
			if (buttonDict.ContainsKey (name)) return buttonDict[name].IsHit;
			LogNonExistingButton (name);
			return false;
		}

		public static bool IsButtonReleased (string name)
		{
			if (buttonDict.ContainsKey (name)) return buttonDict[name].IsReleased;
			LogNonExistingButton (name);
			return false;
		}

		internal static void SaveMapping ()
		{
			Serializer.WriteObject (buttonDict, ResNames.MappingFileName, typeof(XmlSerializer));
		}

		internal static void LoadMapping ()
		{
			Log.Core.Write ("Loading input mapping.");
			buttonDict = Serializer.TryReadObject<Dictionary<string, VirtualButton>> (ResNames.MappingFileName, typeof(XmlSerializer));
			if (buttonDict != null) return;
			buttonDict = new Dictionary<string, VirtualButton> ();
			Log.Core.Write ("Input mapping not found. Creating empty.");
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