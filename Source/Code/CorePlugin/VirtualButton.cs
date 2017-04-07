using System.Collections.Generic;
using System.Linq;

namespace MFEP.Duality.Plugins.InputPlugin
{
	internal class VirtualButton
	{
		private readonly HashSet<KeyValue> positiveKeyVals = new HashSet<KeyValue>();
		private readonly HashSet<KeyValue> negativeKeyVals = new HashSet<KeyValue>();

		public VirtualButton (KeyValue[] positiveKeyValues = null, KeyValue[] negativeKeyValues = null)
		{
			if (positiveKeyValues != null) {
				foreach (var keyValue in positiveKeyValues) {
					Associate (keyValue, KeyRole.Positive);
				}
			}
			if (negativeKeyValues != null) {
				foreach (var keyValue in negativeKeyValues) {
					Associate (keyValue, KeyRole.Negative);
				}
			}
		}

		public KeyValue[] PositiveKeyVals => positiveKeyVals.ToArray ();
		public KeyValue[] NegativeKeyVals => negativeKeyVals.ToArray ();
		internal KeyValue[] AllKeyVals => positiveKeyVals.Union (negativeKeyVals).ToArray ();

		public bool IsPressed
		{
			get { return positiveKeyVals.Union (negativeKeyVals).Any (keyVal => keyVal.IsPressed); }
		}

		public bool IsHit
		{
			get { return positiveKeyVals.Union(negativeKeyVals).Any (keyVal => keyVal.IsHit); }
		}

		public bool IsReleased
		{
			get { return positiveKeyVals.Union(negativeKeyVals).Any (keyVal => keyVal.IsReleased); }
		}

		public float Get()
		{
			var x = 0.0f;
			if (positiveKeyVals.Any(keyVal => keyVal.IsPressed)) {
				x += 1.0f;
			}
			if (negativeKeyVals.Any(keyVal => keyVal.IsPressed)) {
				x -= 1.0f;
			}
			return x;
		}

		public bool Associate (KeyValue key, KeyRole role)
		{
			if (positiveKeyVals.Contains (key) || negativeKeyVals.Contains (key)) {
				return false;
			}
			return role == KeyRole.Positive ? positiveKeyVals.Add (key) : negativeKeyVals.Add (key);
		}

		public bool Remove (KeyValue key)
		{
			return positiveKeyVals.Remove (key) || negativeKeyVals.Remove (key);
		}
	}
}