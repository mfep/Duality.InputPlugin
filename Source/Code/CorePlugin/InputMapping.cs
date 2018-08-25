using System.Collections.Generic;
using Duality;
using Duality.Editor;

namespace MFEP.Duality.Plugins.InputPlugin
{
	[EditorHintCategory (ResNames.EditorCategory)]
	[EditorHintImage(ResNames.IconResource)]
	public class InputMapping : Resource
	{
		private Dictionary<string, VirtualButton> buttonDict = new Dictionary<string, VirtualButton> ();
		public Dictionary<string, VirtualButton> ButtonDict { get => buttonDict; set => buttonDict = value; }
	}
}