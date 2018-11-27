using System.Runtime.Serialization;

namespace Nest
{
	public interface IPutRoleMappingResponse : IResponse
	{
		[DataMember(Name ="role_mapping")]
		PutRoleMappingStatus RoleMapping { get; }
	}

	public class PutRoleMappingResponse : ResponseBase, IPutRoleMappingResponse
	{
		public bool Created => RoleMapping?.Created ?? false;
		public PutRoleMappingStatus RoleMapping { get; internal set; }
	}

	public class PutRoleMappingStatus
	{
		[DataMember(Name ="created")]
		public bool Created { get; set; }
	}
}
