using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Duality;

namespace MFEP.Duality.Plugins.InputPlugin.Example
{
    public class VirtualButtonsFromCode : Component
    {
        [DontSerialize] private Random random;

        public VirtualButtonsFromCode()
        {
            DualityApp.Keyboard.KeyDown += KeyDownCallback;
            random = new Random(DateTime.Now.Millisecond);
        }

        private void KeyDownCallback(object sender, global::Duality.Input.KeyboardKeyEventArgs e)
        {
            if (DualityApp.Keyboard.KeyHit(global::Duality.Input.Key.Space)) {
                AddRandomButton();
            }
        }

        private void AddRandomButton()
        {
            VirtualButton button = new VirtualButton();
            AssociateRandomKeys(button, 2);
            InputManager.RegisterButton(button);            
        }

        private void AssociateRandomKeys(VirtualButton button, int count)
        {
            for (int i = 0; i < count; i++) {
                button.AssociateKey((global::Duality.Input.Key)random.Next((int)global::Duality.Input.Key.Last));
            }
        }
    }
}
