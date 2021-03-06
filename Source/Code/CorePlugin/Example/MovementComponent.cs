﻿using Duality;
using Duality.Editor;

namespace mfep.Duality.Plugins.InputPlugin.Example
{
	[EditorHintCategory (ResNames.ExamplesEditorCategory)]
	public class MovementComponent : Component, ICmpUpdatable
	{
		public float MovementSpeed { get; set; }

		public void OnUpdate ()
		{
			var direction = Vector2.Zero;
			direction += this.InputManager ().GetAxis ("Horizontal") * Vector2.UnitX;
			direction -= this.InputManager ().GetAxis ("Vertical") * Vector2.UnitY;
			GameObj.Transform.MoveBy (direction * MovementSpeed * Time.TimeMult);
		}
	}
}