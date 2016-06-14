using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Duality;
using Duality.Resources;
using Key = Duality.Input.Key;

namespace MFEP.Duality.Plugins.InputPlugin
{
    static public class InputManager
    {
        private static Dictionary<string, VirtualButton> buttonMapping = new Dictionary<string, VirtualButton>();

        public static event Action ButtonsChanged = SaveMapping;

        public static VirtualButton[] Buttons
        {
            get
            {
                return buttonMapping.Values.ToArray();
            }
        }        

        public static bool RegisterButton(VirtualButton button)
        {
            if (buttonMapping.ContainsKey(button.Name)) return false;
            buttonMapping.Add(button.Name, button);
            button.ButtonChanged += () => { ButtonsChanged?.Invoke(); };
            ButtonsChanged?.Invoke();
            return true;
        }

        public static void RemoveButton(string name)
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

        public static string GetUnusedButtonName()
        {
            string buttonName = "Button0";
            int i = 0;
            while (buttonMapping.Keys.Contains(buttonName)) {
                buttonName = $"Button{++i}";
            }
            return buttonName;
        }

        public static void SaveMapping()
        {
            var res = new InputMappingRes();
            res.SaveVirtualButtonDict(buttonMapping);
            res.Save("keyMapping.res");
        }

        public static void LoadMapping()
        {
            Resource.Load<InputMappingRes>("keyMapping.res", (res) =>
            {
                buttonMapping = null;
                res.LoadVirtualButtonDict(out buttonMapping);
                if (buttonMapping == null) buttonMapping = new Dictionary<string, VirtualButton>();
                ButtonsChanged();
            });
        }

        private static void LogNonExistingButton(string name)
        {
            Log.Game.WriteError($"The button named '{name}' does not exists.");
        }
    }
}
