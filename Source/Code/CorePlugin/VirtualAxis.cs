using System.Collections.Generic;
using System.Linq;

namespace MFEP.Duality.Plugins.InputPlugin
{
	internal class VirtualAxis
	{
		private readonly HashSet<KeyValue> positiveKeyVals = new HashSet<KeyValue> ();
		private readonly HashSet<KeyValue> negativeKeyVals = new HashSet<KeyValue> ();

		public VirtualAxis (KeyValue[] positiveKeyValues = null, KeyValue[] negativeKeyValues = null)
		{
			if (positiveKeyValues != null) {
				foreach (var key in positiveKeyValues) {
					AssociatePositive (key);
				}
			}
			if (negativeKeyValues != null) {
				foreach (var key in negativeKeyValues) {
					AssociateNegative (key);
				}
			}
		}

		public KeyValue[] PositiveKeyVals => positiveKeyVals.ToArray ();
		public KeyValue[] NegativeKeyVals => negativeKeyVals.ToArray ();

		public float Get ()
		{
			var x = 0.0f;
			if (positiveKeyVals.Any (keyVal => keyVal.IsPressed)) {
				x += 1.0f;
			}
			if (negativeKeyVals.Any (keyVal => keyVal.IsPressed)) {
				x -= 1.0f;
			}
			return x;
		}

		public bool AssociatePositive (KeyValue key)
		{
			return positiveKeyVals.Add (key);
		}

		public bool AssociateNegative (KeyValue key)
		{
			return negativeKeyVals.Add (key);
		}

		public bool RemovePositive (KeyValue key)
		{
			return positiveKeyVals.Remove (key);
		}

		public bool RemoveNegative (KeyValue key)
		{
			return negativeKeyVals.Remove (key);
		}
	}
}
