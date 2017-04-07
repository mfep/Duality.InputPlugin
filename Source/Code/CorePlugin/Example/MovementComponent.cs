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
			direction += InputManager.GetAxis ("Horizontal") * Vector2.UnitX;
			direction -= InputManager.GetAxis ("Vertical") * Vector2.UnitY;
			GameObj.Transform.MoveByAbs (direction * MovementSpeed * Time.TimeMult);
		}
	}
}