using System.Runtime.Serialization;

namespace Nest
{
	public class PutRoleResponse : ResponseBase
	{
		[DataMember(Name ="role")]
		public PutRoleStatus Role { get; internal set; }
	}

	public class PutRoleStatus
	{
		[DataMember(Name ="created")]
		public bool Created { get; internal set; }
	}
}
