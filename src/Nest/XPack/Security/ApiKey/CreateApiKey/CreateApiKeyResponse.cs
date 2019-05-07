using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface ICreateApiKeyResponse : IResponse
	{
		/// <summary>
		/// Id for the API key
		/// </summary>
		[JsonProperty("id")]
		string Id { get; }

		/// <summary>
		/// Name of the API key
		/// </summary>
		[JsonProperty("name")]
		string Name { get; }

		/// <summary>
		/// Optional expiration time for the API key in milliseconds
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

	public class CreateApiKeyResponse : ResponseBase, ICreateApiKeyResponse
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
