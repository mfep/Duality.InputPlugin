using System;
using System.Collections.Generic;
using System.Linq;
using Duality;
using Duality.Input;

namespace MFEP.Duality.Plugins.InputPlugin
{
	public class VirtualButton
	{
		private readonly HashSet<Key> associatedKeys = new HashSet<Key> ();

		public VirtualButton ()
		{
			Name = InputManager.GetUnusedButtonName ();
		}

		public VirtualButton (string name)
		{
			Name = name;
		}

		public VirtualButton (string name, Key key)
		{
			Name = name;
			AssociateKey (key);
		}

		public VirtualButton (string name, Key[] keyArray)
		{
			Name = name;
			foreach (var key in keyArray) AssociateKey (key);
		}

		public string Name { get; set; }

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

		public Key[] AssociatedKeys => associatedKeys.ToArray ();

		public event Action ButtonChanged;

		public VirtualButton Clone () => new VirtualButton (Name, associatedKeys.ToArray ());

		public bool AssociateKey (Key key)
		{
			if (associatedKeys.Contains (key)) return false;
			associatedKeys.Add (key);
			ButtonChanged?.Invoke ();
			return true;
		}

		public void RemoveKey (Key key)
		{
			associatedKeys.Remove (key);
			ButtonChanged?.Invoke();
		}

		public void ChangeName (string newName)
		{
			Name = newName;
		}
	}
}