using System.Reflection;
using Duality.Serialization;

namespace MFEP.Duality.Plugins.InputPlugin.Serialization
{
	public class MappingSerializeErrorHandler : SerializeErrorHandler
	{
		public override void HandleError (SerializeError error)
		{
			var resolveMemberError = error as ResolveMemberError;
			if (resolveMemberError != null) {
				if (resolveMemberError.MemberId != @"F:MFEP.Duality.Plugins.InputPlugin.VirtualButton:associatedKeyVals") {
					return;
				}
				var virtualButtonType = typeof(VirtualButton);
				FieldInfo fieldInfo = virtualButtonType.GetTypeInfo ().GetDeclaredField ("positiveKeyVals");
				resolveMemberError.ResolvedMember = fieldInfo;
			}
		}
	}
}
