using System.Collections.Generic;
using System.Linq;
using Duality;
using Duality.Editor;

namespace mfep.Duality.Plugins.InputPlugin
{
	public class VirtualButton
	{
		private List<AbstractKey> positiveKeyVals;
		private List<AbstractKey> negativeKeyVals;
		private float riseTime = 0.01f;
		private float incrementPerSecond = 100.0f;
		private float deadZone = 0.3f;
		private bool directionSnap = false;
		[DontSerialize] private float currentValue;

		/// <summary>
		/// Time in seconds that the axis value needs to reach maximum after a key has been hit.
		/// </summary>
		[EditorHintRange(0.0f, 15.0f)]
		public float RiseTime
		{
			get => riseTime;
			set { riseTime = value;
				incrementPerSecond = 1.0f / value; }
		}

		/// <summary>
		/// Below this value the input received from an analog controller is registered as 0. 
		/// </summary>
		[EditorHintRange(0.0f, 1.0f)]
		public float DeadZone
		{
			get => deadZone;
			set => deadZone = MathF.Clamp01 (value);
		}

		/// <summary>
		/// When true, change in input direction immediately sets the current value to 0.
		/// </summary>
		public bool DirectionSnap
		{
			get => directionSnap;
			set => directionSnap = value;
		}

		public List<AbstractKey> PositiveKeys { get => positiveKeyVals; set => positiveKeyVals = value; }
		public List<AbstractKey> NegativeKeys { get => negativeKeyVals; set => negativeKeyVals = value; }

		internal bool IsPressed =>
			positiveKeyVals?.Union (negativeKeyVals).Any (keyVal => keyVal.IsPressed (deadZone)) ?? false;

		internal bool IsHit =>
			positiveKeyVals?.Union (negativeKeyVals).Any (keyVal => keyVal.IsHit) ?? false;

		internal bool IsReleased =>
			positiveKeyVals?.Union (negativeKeyVals).Any (keyVal => keyVal.IsReleased) ?? false;

		internal float Value => currentValue;

		internal void Update (float dt)
		{
			var target = 0.0f;
			if (positiveKeyVals != null && positiveKeyVals.Count > 0) {
				target += positiveKeyVals.Select (keyVal => keyVal?.GetAxis (deadZone) ?? 0.0f).OrderByDescending (MathF.Abs).First ();
			}
			if (negativeKeyVals != null && negativeKeyVals.Count > 0) {
				target -= negativeKeyVals.Select (keyVal => keyVal?.GetAxis (deadZone) ?? 0.0f).OrderByDescending (MathF.Abs).First ();
			}

			var newValue = currentValue + MathF.Sign (target - currentValue) * incrementPerSecond * dt;
			if ((currentValue - target) * (newValue - target) < 0.0f) {
				newValue = target;
			}
			if (directionSnap && newValue * target < 0.0f) {
				newValue = 0.0f;
			}
			currentValue = MathF.Clamp (newValue, -1.0f, 1.0f);
		}
	}
}