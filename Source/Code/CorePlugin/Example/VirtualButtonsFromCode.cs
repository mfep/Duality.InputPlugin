using System;
using Duality;
using Duality.Input;

namespace MFEP.Duality.Plugins.InputPlugin.Example
{
	public class VirtualButtonsFromCode : Component
	{
		[DontSerialize] private readonly Random random;

		public VirtualButtonsFromCode ()
		{
			DualityApp.Keyboard.KeyDown += KeyDownCallback;
			random = new Random (DateTime.Now.Millisecond);
		}

		private void KeyDownCallback (object sender, KeyboardKeyEventArgs e)
		{
			if (DualityApp.Keyboard.KeyHit (Key.Space)) AddRandomButton ();
		}

		private void AddRandomButton ()
		{
			VirtualButton button = new VirtualButton ();
			AssociateRandomKeys (button, 2);
			InputManager.RegisterButton (button);
		}

		private void AssociateRandomKeys (VirtualButton button, int count)
		{
			for (int i = 0; i < count; i++) button.AssociateKey ((Key)random.Next ((int)Key.Last));
		}
	}
}