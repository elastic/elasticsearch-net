using Newtonsoft.Json;

namespace Nest
{
	public interface IInvalidateUserAccessTokenResponse : IResponse
	{
		[JsonProperty("invalidated_tokens")]
		long InvalidatedTokens { get; }

		[JsonProperty("previously_invalidated_tokens")]
		long PreviouslyInvalidatedTokens { get; }

		[JsonProperty("error_count")]
		long ErrorCount { get; }
	}

	public class InvalidateUserAccessTokenResponse : ResponseBase, IInvalidateUserAccessTokenResponse
	{
		public long InvalidatedTokens { get; internal set;  }

		public long PreviouslyInvalidatedTokens { get; internal set;  }

		public long ErrorCount { get; internal set;  }
	}
}
