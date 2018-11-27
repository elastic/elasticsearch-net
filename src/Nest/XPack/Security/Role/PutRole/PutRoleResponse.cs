using System.Runtime.Serialization;

namespace Nest
{
	public interface IPutRoleResponse : IResponse
	{
		[DataMember(Name ="role")]
		PutRoleStatus Role { get; }
	}

	public class PutRoleResponse : ResponseBase, IPutRoleResponse
	{
		public PutRoleStatus Role { get; internal set; }
	}

	public class PutRoleStatus
	{
		[DataMember(Name ="created")]
		public bool Created { get; internal set; }
	}
}
