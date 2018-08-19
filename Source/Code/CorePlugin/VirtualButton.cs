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

		internal bool IsPressed =>
			positiveKeyVals?.Union (negativeKeyVals).Any (keyVal => keyVal.IsPressed (DeadZone)) ?? false;

		internal bool IsHit =>
			positiveKeyVals?.Union (negativeKeyVals).Any (keyVal => keyVal.IsHit) ?? false;

		internal bool IsReleased =>
			positiveKeyVals?.Union (negativeKeyVals).Any (keyVal => keyVal.IsReleased) ?? false;

		internal float Value => currentValue;

		internal void Update (float dt)
		{
			var targ = 0.0f;
			if (positiveKeyVals != null && positiveKeyVals.Count > 0) {
				targ += positiveKeyVals.Select (keyVal => keyVal.GetAxis (DeadZone)).OrderByDescending (MathF.Abs).First ();
			}
			if (negativeKeyVals != null && negativeKeyVals.Count > 0) {
				targ -= negativeKeyVals.Select (keyVal => keyVal.GetAxis (DeadZone)).OrderByDescending (MathF.Abs).First ();
			}

			var newValue = currentValue + MathF.Sign(targ - currentValue) * incrementPerSecond * dt;
			if ((currentValue - targ) * (newValue - targ) < 0.0f) {
				newValue = targ;
			}
			currentValue = MathF.Clamp (newValue, -1.0f, 1.0f);
		}
	}
}