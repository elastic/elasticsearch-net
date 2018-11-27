using System.Runtime.Serialization;

namespace Nest
{
	public interface IDeleteUserResponse : IResponse
	{
		[DataMember(Name ="found")]
		bool Found { get; }
	}

	public class DeleteUserResponse : ResponseBase, IDeleteUserResponse
	{
		public bool Found { get; internal set; }
	}
}
