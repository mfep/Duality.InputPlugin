﻿using System.Collections.Generic;
using Duality;
using Duality.Editor;

namespace mfep.Duality.Plugins.InputPlugin
{
	/// <summary>
	/// Contains the <see cref="AbstractKey"/>s associated with strings, i.e. <see cref="VirtualButton"/>s.
	/// </summary>
	[EditorHintCategory (ResNames.EditorCategory)]
	[EditorHintImage(ResNames.IconResource)]
	public class InputMapping : Resource
	{
		private Dictionary<string, VirtualButton> buttonDict = new Dictionary<string, VirtualButton> ();
		public Dictionary<string, VirtualButton> ButtonDict { get => buttonDict; set => buttonDict = value; }
	}
}