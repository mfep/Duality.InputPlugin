using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Duality;
using Duality.Serialization;

[assembly: InternalsVisibleTo ("InputPlugin.editor")]

namespace MFEP.Duality.Plugins.InputPlugin
{
	public static class InputManager
	{
		private const string mappingPath = "keyMapping.xml";
		private static Dictionary<string, VirtualButton> buttonMapping = new Dictionary<string, VirtualButton> ();

		public static VirtualButton[] Buttons => buttonMapping.Values.ToArray ();

		public static event Action<string> ButtonsChanged = unused => { SaveMapping (); };

		public static VirtualButton GetVirtualButton (string name)
		{
			return !buttonMapping.ContainsKey (name) ? null : buttonMapping[name];
		}

		public static bool RegisterButton (VirtualButton button)
		{
			if (button == null) return false;
			if (buttonMapping.ContainsKey (button.Name)) return false;
			buttonMapping.Add (button.Name, button);
			button.ButtonChanged += () => { ButtonsChanged?.Invoke (button.Name); };
			ButtonsChanged?.Invoke (button.Name);
			return true;
		}

		public static void RemoveButton (string name)
		{
			if (!buttonMapping.ContainsKey (name)) {
				LogNonExistingButton (name);
				return;
			}
			buttonMapping.Remove (name);
			ButtonsChanged?.Invoke (name);
		}

		public static bool IsButtonPressed (string name)
		{
			if (buttonMapping.ContainsKey (name)) return buttonMapping[name].IsPressed;
			LogNonExistingButton (name);
			return false;
		}

		public static bool IsButtonHit (string name)
		{
			if (buttonMapping.ContainsKey (name)) return buttonMapping[name].IsHit;
			LogNonExistingButton (name);
			return false;
		}

		public static bool IsButtonReleased (string name)
		{
			if (buttonMapping.ContainsKey (name)) return buttonMapping[name].IsReleased;
			LogNonExistingButton (name);
			return false;
		}

		public static string GetUnusedButtonName ()
		{
			string buttonName = "Button0";
			int i = 0;
			while (buttonMapping.Keys.Contains (buttonName)) buttonName = $"Button{++i}";
			return buttonName;
		}

		internal static void SaveMapping ()
		{
			Serializer.WriteObject (buttonMapping, mappingPath, typeof(XmlSerializer));
		}

		internal static void LoadMapping ()
		{
			Log.Core.Write ("Loading input mapping.");
			buttonMapping = Serializer.TryReadObject<Dictionary<string, VirtualButton>> (mappingPath, typeof(XmlSerializer));
			if (buttonMapping != null) return;
			buttonMapping = new Dictionary<string, VirtualButton> ();
			Log.Core.Write ("Input mapping not found. Creating empty.");
		}

		private static void LogNonExistingButton (string name)
		{
			Log.Game.WriteError ($"The button named '{name}' does not exist.");
		}
	}
}