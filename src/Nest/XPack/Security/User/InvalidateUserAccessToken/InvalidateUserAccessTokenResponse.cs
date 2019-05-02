using System.Runtime.Serialization;

namespace Nest
{
	public class InvalidateUserAccessTokenResponse : ResponseBase
	{
		[DataMember(Name = "invalidated_tokens")]
		public long InvalidatedTokens { get; internal set;  }

		[DataMember(Name = "previously_invalidated_tokens")]
		public long PreviouslyInvalidatedTokens { get; internal set;  }

		[DataMember(Name = "error_count")]
		public long ErrorCount { get; internal set;  }
	}
}
