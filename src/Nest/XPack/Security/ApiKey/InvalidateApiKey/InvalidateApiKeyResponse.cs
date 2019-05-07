using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public interface IInvalidateApiKeyResponse : IResponse
	{
		/// <summary>
		/// The ids of the API keys that were invalidated as part of this request.
		/// </summary>
		[JsonProperty("invalidated_api_keys")]
		IReadOnlyCollection<string> InvalidatedApiKeys { get; }

		/// <summary>
		/// The ids of the API keys that were already invalidated.
		/// </summary>
		[JsonProperty("previously_invalidated_api_keys")]
		IReadOnlyCollection<string> PreviouslyInvalidatedApiKeys { get; }

		/// <summary>
		/// The number of errors that were encountered when invalidating the API keys.
		/// </summary>
		[JsonProperty("error_count")]
		int? ErrorCount { get; }

		/// <summary>
		/// Details about these errors. This field is not present in the response when there are no errors.
		/// </summary>
		[JsonProperty("error_details")]
		IReadOnlyCollection<ErrorCause> ErrorDetails { get; }
	}

	public class InvalidateApiKeyResponse : ResponseBase, IInvalidateApiKeyResponse
	{
		/// <inheritdoc />
		public IReadOnlyCollection<string> InvalidatedApiKeys { get; internal set; } = EmptyReadOnly<string>.Collection;

		/// <inheritdoc />
		public IReadOnlyCollection<string> PreviouslyInvalidatedApiKeys { get; internal set; } = EmptyReadOnly<string>.Collection;

		/// <inheritdoc />
		public int? ErrorCount { get; internal set; }

		/// <inheritdoc />
		public IReadOnlyCollection<ErrorCause> ErrorDetails { get; internal set; } = EmptyReadOnly<ErrorCause>.Collection;
	}
}
