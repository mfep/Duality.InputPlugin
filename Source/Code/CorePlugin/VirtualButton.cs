using System.Collections.Generic;
using System.Linq;

namespace MFEP.Duality.Plugins.InputPlugin
{
	internal class VirtualButton
	{
		private readonly HashSet<KeyValue> associatedKeyVals = new HashSet<KeyValue> ();

		public VirtualButton (KeyValue[] keyValues = null)
		{
			if (keyValues == null) return;
			foreach (var key in keyValues) Associate (key);
		}

		public KeyValue[] KeyVals => associatedKeyVals.ToArray ();

		public bool IsPressed
		{
			get { return associatedKeyVals.Any (keyVal => keyVal.IsPressed); }
		}

		public bool IsHit
		{
			get { return associatedKeyVals.Any (keyVal => keyVal.IsHit); }
		}

		public bool IsReleased
		{
			get { return associatedKeyVals.Any (keyVal => keyVal.IsReleased); }
		}

		public bool Associate (KeyValue key)
		{
			if (associatedKeyVals.Contains (key)) return false;
			associatedKeyVals.Add (key);
			return true;
		}

		public bool Remove (KeyValue keyVal)
		{
			return associatedKeyVals.Remove (keyVal);
		}
	}
}