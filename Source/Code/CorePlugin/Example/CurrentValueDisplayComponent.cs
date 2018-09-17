using System;
using Duality;
using Duality.Components.Renderers;
using Duality.Editor;

namespace mfep.Duality.Plugins.InputPlugin.Example
{
	[EditorHintCategory (ResNames.ExamplesEditorCategory)]
	[RequiredComponent (typeof(SpriteRenderer))]
	public class CurrentValueDisplayComponent : Component, ICmpInitializable, ICmpUpdatable
	{
		public string ButtonName { get; set; }

		private SpriteRenderer spriteRenderer;
		private Rect originalRect;

		public void OnActivate ()
		{
			if (DualityApp.ExecContext == DualityApp.ExecutionContext.Game) {
				spriteRenderer = GameObj.GetComponent<SpriteRenderer> ();
				originalRect = spriteRenderer.Rect;
			}
		}

		public void OnDeactivate ()
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
