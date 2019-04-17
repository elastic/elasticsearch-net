using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public interface ISecurityInvalidateApiKeyResponse : IResponse
	{
		[JsonProperty("created")]
		bool Created { get; }

		[JsonProperty("invalidated_tokens")]
		int InvalidatedTokens { get; }

		[JsonProperty("previously_invalidated_tokens")]
		int PreviousInvalidatedTokens { get; }

		[JsonProperty("error_count")]
		int? ErrorCount { get; }

		[JsonProperty("error_details")]
		IReadOnlyCollection<ErrorCause> ErrorDetails { get; }
	}

	public class SecurityInvalidateApiKeyResponse : ResponseBase, ISecurityInvalidateApiKeyResponse
	{
		public bool Created { get; internal set; }

		public int InvalidatedTokens { get; internal set; }

		public int PreviousInvalidatedTokens { get; internal set; }

		public int? ErrorCount { get; internal set; }

		public IReadOnlyCollection<ErrorCause> ErrorDetails { get; internal set;  } = EmptyReadOnly<ErrorCause>.Collection;
	}
}
