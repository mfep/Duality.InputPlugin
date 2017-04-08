using System.Reflection;
using Duality;
using Duality.Serialization;

namespace MFEP.Duality.Plugins.InputPlugin.Serialization
{
	public class MappingSerializeErrorHandler : SerializeErrorHandler
	{
		public override void HandleError (SerializeError error)
		{
			if (error is ResolveMemberError resolveMemberError) {
				if (resolveMemberError.MemberId != @"F:MFEP.Duality.Plugins.InputPlugin.VirtualButton:associatedKeyVals") {
					return;
				}
				Log.Core.Write ("Converting legacy VirtualButton format.");
				FieldInfo fieldInfo = typeof(VirtualButton).GetTypeInfo ().GetDeclaredField ("positiveKeyVals");
				resolveMemberError.ResolvedMember = fieldInfo;
			}
		}
	}
}
