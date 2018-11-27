using System.Runtime.Serialization;

namespace Nest
{
	public interface IPutUserResponse : IResponse
	{
		[DataMember(Name ="user")]
		PutUserStatus User { get; }
	}

	public class PutUserResponse : ResponseBase, IPutUserResponse
	{
		public PutUserStatus User { get; internal set; }
	}

	public class PutUserStatus
	{
		[DataMember(Name ="created")]
		public bool Created { get; internal set; }
	}
}
