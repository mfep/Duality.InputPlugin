using System.Collections.Generic;
using System.Linq;
using Duality;

namespace MFEP.Duality.Plugins.InputPlugin
{
	public class VirtualButton
	{
		private List<KeyValue> positiveKeyVals;
		private List<KeyValue> negativeKeyVals;
		private float riseTime;
		[DontSerialize] private float incrementPerSecond;
		[DontSerialize] private float currentValue;

		public float RiseTime
		{
			get => riseTime;
			set { riseTime = value;
				incrementPerSecond = 1.0f / value; }
		}

		public float DeadZone { get; set; }
		public List<KeyValue> PositiveKeys { get => positiveKeyVals; set => positiveKeyVals = value; }
		public List<KeyValue> NegativeKeys { get => negativeKeyVals; set => negativeKeyVals = value; }

		internal KeyValue[] AllKeyVals => positiveKeyVals.Union (negativeKeyVals).ToArray ();

		internal bool IsPressed
		{
			get { return positiveKeyVals.Union (negativeKeyVals).Any (keyVal => keyVal.IsPressed (DeadZone)); }
		}

		internal bool IsHit
		{
			get { return positiveKeyVals.Union (negativeKeyVals).Any (keyVal => keyVal.IsHit); }
		}

		internal bool IsReleased
		{
			get { return positiveKeyVals.Union (negativeKeyVals).Any (keyVal => keyVal.IsReleased); }
		}

		internal float Get ()
		{
			return currentValue;
		}

		internal void Update (float dt)
		{
			float targ = 0.0f;
			if (positiveKeyVals.Count > 0) {
				targ += positiveKeyVals.Select (keyVal => keyVal.GetAxis (DeadZone)).OrderByDescending (MathF.Abs).First ();
			}
			if (negativeKeyVals.Count > 0) {
				targ -= negativeKeyVals.Select (keyVal => keyVal.GetAxis (DeadZone)).OrderByDescending (MathF.Abs).First ();
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