using System.Collections.Generic;
using Duality.Serialization;

namespace MFEP.Duality.Plugins.InputPlugin
{
	internal class DualityXmlSerializer : IMappingSerializer
	{
		public object LoadMapping ()
		{
			return Serializer.TryReadObject<Dictionary<string, VirtualButton>> (ResNames.MappingFileName, typeof(XmlSerializer));
		}

		public void SaveMapping (object obj)
		{
			Serializer.WriteObject (obj, ResNames.MappingFileName, typeof(XmlSerializer));
		}
	}
}