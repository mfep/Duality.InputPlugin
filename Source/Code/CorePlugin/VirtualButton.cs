using System.Collections.Generic;
using System.Linq;
using Duality;
using Duality.Input;

namespace MFEP.Duality.Plugins.InputPlugin
{
	internal class VirtualButton
	{
		private readonly HashSet<Key> associatedKeys = new HashSet<Key> ();

		public VirtualButton (Key[] keyArray)
		{
			if (keyArray == null) return;
			foreach (var key in keyArray) AssociateKey (key);
		}

		public bool IsPressed
		{
			get { return associatedKeys.Any (key => DualityApp.Keyboard.KeyPressed (key)); }
		}

		public bool IsHit
		{
			get { return associatedKeys.Any (key => DualityApp.Keyboard.KeyHit (key)); }
		}

		public bool IsReleased
		{
			get { return associatedKeys.Any (key => DualityApp.Keyboard.KeyReleased (key)); }
		}

		public Key[] Keys => associatedKeys.ToArray ();

		public bool AssociateKey (Key key)
		{
			if (associatedKeys.Contains (key)) return false;
			associatedKeys.Add (key);
			return true;
		}

		public bool RemoveKey (Key key)
		{
			return associatedKeys.Remove (key);
		}
	}
}