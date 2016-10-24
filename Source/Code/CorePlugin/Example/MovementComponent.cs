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
			if (InputManager.IsButtonPressed ("Right")) direction += Vector2.UnitX;
			if (InputManager.IsButtonPressed ("Left")) direction -= Vector2.UnitX;
			if (InputManager.IsButtonPressed ("Up")) direction -= Vector2.UnitY;
			if (InputManager.IsButtonPressed ("Down")) direction += Vector2.UnitY;
			GameObj.Transform.MoveByAbs (direction * MovementSpeed * Time.TimeMult);
		}
	}
}