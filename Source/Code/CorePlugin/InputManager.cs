using System;
using System.Collections.Generic;
using Duality;
using Duality.Editor;

namespace mfep.Duality.Plugins.InputPlugin
{
	/// <summary>
	/// Tracks the status of the <see cref="VirtualButton"/>s described in the referenced <see cref="InputMapping"/>.
	/// </summary>
	[EditorHintCategory (ResNames.EditorCategory)]
	[EditorHintImage(ResNames.IconResource)]
	public class InputManager : Component, ICmpInitializable
	{
		private Dictionary<string, VirtualButton> ButtonDict => inputMapping.Res.ButtonDict;
		private ContentRef<InputMapping> inputMapping;

		/// <summary>
		/// The <see cref="InputMapping"/> used by this <see cref="InputManager"/>.
		/// </summary>
		public ContentRef<InputMapping> InputMapping { get => inputMapping; set => inputMapping = value; }

		void ICmpInitializable.OnInit (InitContext context)
		{
			if (context != InitContext.AddToGameObject) {
				return;
			}
			var presentInputManager = this.InputManager ();
			if (presentInputManager != null && presentInputManager != this) {
				Log.Core.WriteError ($"An InputManager is already present in the scene: {presentInputManager}");
			}
		}

		void ICmpInitializable.OnShutdown (ShutdownContext context)
		{
		}

		/// <summary>
		/// Checks if any of the <see cref="KeyValue"/>s associated with a Virtual Button is pressed at the moment.
		/// </summary>
		/// <param name="buttonName">The string identifier of the Virtual Button.</param>
		/// <returns>Returns true, if any of the <see cref="KeyValue"/>s is pressed.</returns>
		public bool IsButtonPressed (string buttonName)
		{
			if (ButtonDict.ContainsKey (buttonName)) return ButtonDict[buttonName]?.IsPressed ?? throw new NullReferenceException ("VirtualButton dictionary value is null!");
			throw new ArgumentException ($"The button named {buttonName} does not exist.");
		}

		/// <summary>
		/// Checks if any of the <see cref="KeyValue"/>s associated with a Virtual Button has been hit in the current frame.
		/// </summary>
		/// <param name="buttonName">The string identifier of the Virtual Button.</param>
		/// <returns>Returns true, if any of the <see cref="KeyValue"/>s is hit.</returns>
		public bool IsButtonHit (string buttonName)
		{
			if (ButtonDict.ContainsKey (buttonName)) return ButtonDict[buttonName]?.IsHit ?? throw new NullReferenceException ("VirtualButton dictionary value is null!"); ;
			throw new ArgumentException ($"The button named {buttonName} does not exist.");
		}

		/// <summary>
		/// Checks if any of the <see cref="KeyValue"/>s associated with a Virtual Button is released in the current frame.
		/// </summary>
		/// <param name="buttonName">The string identifier of the Virtual Button.</param>
		/// <returns>Returns true, if any of the <see cref="KeyValue"/>s is released.</returns>
		public bool IsButtonReleased (string buttonName)
		{
			if (ButtonDict.ContainsKey (buttonName)) return ButtonDict[buttonName]?.IsReleased ?? throw new NullReferenceException ("VirtualButton dictionary value is null!"); ;
			throw new ArgumentException ($"The button named {buttonName} does not exist.");
		}

		/// <summary>
		/// Returns the axis value of the <see cref="VirtualButton"/> with the given name.
		/// This is affected by the RiseTime and DeadZone of the button.
		/// </summary>
		public float GetAxis (string buttonName)
		{
			if (ButtonDict.ContainsKey(buttonName)) return ButtonDict[buttonName]?.Value ?? throw new NullReferenceException ("VirtualButton dictionary value is null!"); ;
			throw new ArgumentException($"The button named {buttonName} does not exist.");
		}

		internal void UpdateButtons (float dt)
		{
			if (ButtonDict == null) {
				return;
			}
			foreach (var buttonPair in ButtonDict) {
				buttonPair.Value?.Update (dt);
			}
		}
	}
}
