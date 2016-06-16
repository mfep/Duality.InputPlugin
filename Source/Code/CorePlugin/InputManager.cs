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

        internal static event Action ButtonsChanged = SaveMapping;

        internal static VirtualButton[] Buttons
        {
            get
            {
                return buttonMapping.Values.ToArray();
            }
        }        

        internal static bool RegisterButton(VirtualButton button)
        {
            if (buttonMapping.ContainsKey(button.Name)) return false;
            buttonMapping.Add(button.Name, button);
            button.ButtonChanged += () => { ButtonsChanged?.Invoke(); };
            ButtonsChanged?.Invoke();
            return true;
        }

        internal static void RemoveButton(string name)
        {
            if (!buttonMapping.ContainsKey(name)) {
                LogNonExistingButton(name);
                return;
            }
            buttonMapping.Remove(name);
            ButtonsChanged?.Invoke();
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

        internal static string GetUnusedButtonName()
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
            buttonMapping = Serializer.TryReadObject<Dictionary<string, VirtualButton>>(mappingPath, typeof(XmlSerializer));
            if (buttonMapping == null) buttonMapping = new Dictionary<string, VirtualButton>();
        }

        private static void LogNonExistingButton(string name)
        {
            Log.Game.WriteError($"The button named '{name}' does not exists.");
        }
    }
}
