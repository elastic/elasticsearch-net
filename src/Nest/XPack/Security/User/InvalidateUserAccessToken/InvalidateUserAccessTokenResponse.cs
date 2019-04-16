using System.Runtime.Serialization;

namespace Nest
{
	public interface IInvalidateUserAccessTokenResponse : IResponse
	{
		[DataMember(Name = "invalidated_tokens")]
		long InvalidatedTokens { get; }

		[DataMember(Name = "previously_invalidated_tokens")]
		long PreviouslyInvalidatedTokens { get; }

		[DataMember(Name = "error_count")]
		long ErrorCount { get; }
	}

	public class InvalidateUserAccessTokenResponse : ResponseBase, IInvalidateUserAccessTokenResponse
	{
		public long InvalidatedTokens { get; internal set;  }

		public long PreviouslyInvalidatedTokens { get; internal set;  }

		public long ErrorCount { get; internal set;  }
	}
}
