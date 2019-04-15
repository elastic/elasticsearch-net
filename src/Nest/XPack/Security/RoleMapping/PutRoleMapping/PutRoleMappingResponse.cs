using System.Runtime.Serialization;

namespace Nest
{
	public class PutRoleMappingResponse : ResponseBase
	{
		public bool Created => RoleMapping?.Created ?? false;

		[DataMember(Name ="role_mapping")]
		public PutRoleMappingStatus RoleMapping { get; internal set; }
	}

	public class PutRoleMappingStatus
	{
		[DataMember(Name ="created")]
		public bool Created { get; set; }
	}
}
