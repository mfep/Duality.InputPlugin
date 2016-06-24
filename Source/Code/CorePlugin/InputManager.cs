using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.CompilerServices;

using Duality;
using Duality.Serialization;
using Key = Duality.Input.Key;

[assembly: InternalsVisibleTo("InputPlugin.editor")]

namespace MFEP.Duality.Plugins.InputPlugin
{
    static public class InputManager
    {
        private static Dictionary<string, VirtualButton> buttonMapping = new Dictionary<string, VirtualButton>();
        private const string mappingPath = "keyMapping.xml";

        public static event Action<string> ButtonsChanged = (unused) => { SaveMapping(); };

        public static VirtualButton[] Buttons
        {
            get
            {
                return buttonMapping.Values.ToArray();
            }
        }

        public static VirtualButton GetVirtualButton(string name) {
            if (!buttonMapping.ContainsKey(name)) return null;
            return buttonMapping[name];
        }

        public static bool RegisterButton(VirtualButton button)
        {
            if (button == null) return false;
            if (buttonMapping.ContainsKey(button.Name)) return false;
            buttonMapping.Add(button.Name, button);
            button.ButtonChanged += () => { ButtonsChanged?.Invoke(button.Name); };
            ButtonsChanged?.Invoke(button.Name);
            return true;
        }

        public static void RemoveButton(string name)
        {
            if (!buttonMapping.ContainsKey(name)) {
                LogNonExistingButton(name);
                return;
            }
            buttonMapping.Remove(name);
            ButtonsChanged?.Invoke(name);
        }

        public static bool IsButtonPressed(string name)
        {
            if (!buttonMapping.ContainsKey(name)) {
                LogNonExistingButton(name);
                return false;
            }
            return buttonMapping[name].IsPressed;
        }

        public static bool IsButtonHit(string name)
        {
            if (!buttonMapping.ContainsKey(name)) {
                LogNonExistingButton(name);
                return false;
            }
            return buttonMapping[name].IsHit;
        }
        
        public static bool IsButtonReleased(string name)
        {
            if (!buttonMapping.ContainsKey(name)) {
                LogNonExistingButton(name);
                return false;
            }
            return buttonMapping[name].IsReleased;
        }        

        public static string GetUnusedButtonName()
        {
            string buttonName = "Button0";
            int i = 0;
            while (buttonMapping.Keys.Contains(buttonName)) {
                buttonName = $"Button{++i}";
            }
            return buttonName;
        }

        internal static void SaveMapping()
        {
            Serializer.WriteObject(buttonMapping, mappingPath, typeof(XmlSerializer));
        }

        internal static void LoadMapping()
        {
            Log.Core.Write("Loading input mapping.");
            buttonMapping = Serializer.TryReadObject<Dictionary<string, VirtualButton>>(mappingPath, typeof(XmlSerializer));
            if (buttonMapping == null) {
                buttonMapping = new Dictionary<string, VirtualButton>();
                Log.Core.Write("Input mapping not found. Creating empty.");
            }
        }

        private static void LogNonExistingButton(string name)
        {
            Log.Game.WriteError($"The button named '{name}' does not exist.");
        }
    }
}
