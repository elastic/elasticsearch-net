using System.Runtime.Serialization;

namespace Nest
{
	public interface IPutUserResponse : IResponse
	{
		[DataMember(Name ="created")]
		bool Created { get; }
	}

	public class PutUserResponse : ResponseBase, IPutUserResponse
	{
		public bool Created { get; internal set; }
	}
}
