using System.Collections.Generic;
using Duality;
using Duality.Editor;

namespace MFEP.Duality.Plugins.InputPlugin
{
	[EditorHintCategory (ResNames.EditorCategory)]
	public class InputMapping : Resource
	{
		private Dictionary<string, VirtualButton> buttonDict;
		public Dictionary<string, VirtualButton> ButtonDict { get => buttonDict; set => buttonDict = value; }
	}
}