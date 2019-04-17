using System;
using Newtonsoft.Json;

namespace Nest
{
	public interface ISecurityGetApiKeyResponse : IResponse
	{
		/// <summary>
		/// Unique id for this API key
		/// </summary>
		[JsonProperty("id")]
		string Id { get; }

		/// <summary>
		/// Name for this API key
		/// </summary>
		[JsonProperty("name")]
		string Name { get; }

		/// <summary>
		/// Optional expiration for this API key
		/// </summary>
		[JsonProperty("expiration")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		DateTimeOffset? Expiration { get; }

		/// <summary>
		/// Generated API key
		/// </summary>
		[JsonProperty("api_key")]
		string ApiKey { get; }
	}

	public class SecurityGetApiKeyResponse : ResponseBase, ISecurityGetApiKeyResponse
	{
		/// <inheritdoc />
		public string Id { get; internal set; }

		/// <inheritdoc />
		public string Name { get; internal set; }

		/// <inheritdoc />
		public DateTimeOffset? Expiration { get; internal set; }

		/// <inheritdoc />
		public string ApiKey { get; internal set; }
	}
}
