using System;
using Duality;
using Duality.Components.Renderers;
using Duality.Editor;

namespace MFEP.Duality.Plugins.InputPlugin.Example
{
	[EditorHintCategory (ResNames.EditorCategory)]
	[RequiredComponent (typeof(SpriteRenderer))]
	public class CurrentValueDisplayComponent : Component, ICmpInitializable, ICmpUpdatable
	{
		public string ButtonName { get; set; }

		private SpriteRenderer spriteRenderer;
		private Rect originalRect;

		public void OnInit (InitContext context)
		{
			if (context == InitContext.Activate && DualityApp.ExecContext == DualityApp.ExecutionContext.Game) {
				spriteRenderer = GameObj.GetComponent<SpriteRenderer> ();
				originalRect = spriteRenderer.Rect;
			}
		}

		public void OnShutdown (ShutdownContext context)
		{
		}

		public void OnUpdate()
		{
			if (String.IsNullOrWhiteSpace (ButtonName)) return;

			float buttonValue = this.InputManager ().GetAxis (ButtonName);
			float newH = buttonValue * originalRect.H;
			Rect newRect = new Rect(originalRect.X, originalRect.Y, originalRect.W, newH);
			spriteRenderer.Rect = newRect;
		}
	}
}
