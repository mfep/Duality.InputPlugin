using Duality;
using Duality.Editor;

namespace MFEP.Duality.Plugins.InputPlugin.Example
{
	[EditorHintCategory (ResNames.EditorCategory)]
	public class MovementComponent : Component, ICmpUpdatable
	{
		public float MovementSpeed { get; set; }

		public void OnUpdate ()
		{
			var direction = Vector2.Zero;
			direction += this.InputManager ().GetAxis ("Horizontal") * Vector2.UnitX;
			direction -= this.InputManager ().GetAxis ("Vertical") * Vector2.UnitY;
			GameObj.Transform.MoveByAbs (direction * MovementSpeed * Time.TimeMult);
		}
	}
}