using System.Collections.Generic;
using System.Linq;
using Duality;

namespace MFEP.Duality.Plugins.InputPlugin
{
	internal class VirtualButton
	{
		private readonly HashSet<KeyValue> positiveKeyVals = new HashSet<KeyValue> ();
		private readonly HashSet<KeyValue> negativeKeyVals = new HashSet<KeyValue> ();
		private float riseTime;
		private float deadZone;
		[DontSerialize] private float incrementPerSecond;
		[DontSerialize] private float currentValue;

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

		public float RiseTime
		{
			get => riseTime;
			set { riseTime = value;
				incrementPerSecond = 1.0f / value; }
		}

		public float DeadZone
		{
			get { return deadZone; }
			set { deadZone = value; }
		}

		public KeyValue[] PositiveKeyVals => positiveKeyVals.ToArray ();
		public KeyValue[] NegativeKeyVals => negativeKeyVals.ToArray ();
		internal KeyValue[] AllKeyVals => positiveKeyVals.Union (negativeKeyVals).ToArray ();

		public static VirtualButton GetDefault ()
		{
			var btn = new VirtualButton
			{
				RiseTime = 0.0001f,
				DeadZone = 0.3f
			};
			return btn;
		}

		public bool IsPressed
		{
			get { return positiveKeyVals.Union (negativeKeyVals).Any (keyVal => keyVal.IsPressed (deadZone)); }
		}

		public bool IsHit
		{
			get { return positiveKeyVals.Union (negativeKeyVals).Any (keyVal => keyVal.IsHit); }
		}

		public bool IsReleased
		{
			get { return positiveKeyVals.Union (negativeKeyVals).Any (keyVal => keyVal.IsReleased); }
		}

		public float Get ()
		{
			return currentValue;
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

		internal void Update (float dt)
		{
			float targ = 0.0f;
			if (positiveKeyVals.Count > 0) {
				targ += positiveKeyVals.Select (keyVal => keyVal.Get (deadZone)).OrderByDescending (MathF.Abs).First ();
			}
			if (negativeKeyVals.Count > 0) {
				targ -= negativeKeyVals.Select (keyVal => keyVal.Get (deadZone)).OrderByDescending (MathF.Abs).First ();
			}

			float newValue = currentValue + MathF.Sign(targ - currentValue) * incrementPerSecond * dt;
			if ((currentValue - targ) * (newValue - targ) < 0.0f) {
				newValue = targ;
			}
			currentValue = MathF.Clamp (newValue, -1.0f, 1.0f);
		}

		internal void CalculateData ()
		{
			incrementPerSecond = 1.0f / riseTime;
		}
	}
}