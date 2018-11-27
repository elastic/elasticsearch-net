using System.Runtime.Serialization;

namespace Nest
{
	public interface IInvalidateUserAccessTokenResponse : IResponse
	{
		[DataMember(Name ="created")]
		bool Created { get; }
	}

	public class InvalidateUserAccessTokenResponse : ResponseBase, IInvalidateUserAccessTokenResponse
	{
		public bool Created { get; internal set; }
	}
}
