using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetApiKeyResponse : IResponse
	{
		/// <summary>
		/// The list of API keys that were retrieved for this request.
		/// </summary>
		[JsonProperty("api_keys")]
		IReadOnlyCollection<ApiKeys> ApiKeys { get; }
	}

	public class GetApiKeyResponse : ResponseBase, IGetApiKeyResponse
	{
		/// <inheritdoc />
		public IReadOnlyCollection<ApiKeys> ApiKeys { get; internal set; } = EmptyReadOnly<ApiKeys>.Collection;
	}

	public class ApiKeys
	{
		/// <summary>
		/// Id for the API key
		/// </summary>
		[JsonProperty("id")]
		public string Id { get; internal set; }

		/// <summary>
		/// Name of the API key
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; internal set; }

		/// <summary>
		/// Creation time for the API key
		/// </summary>
		[JsonProperty("creation")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset Creation { get; internal set; }

		/// <summary>
		/// Optional expiration time for the API key in milliseconds
		/// </summary>
		[JsonProperty("expiration")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset? Expiration { get; internal set; }

		/// <summary>
		/// Invalidation status for the API key. If the key has been invalidated, it has a value of true. Otherwise, it is false.
		/// </summary>
		[JsonProperty("invalidated")]
		public bool Invalidated { get; internal set; }

		/// <summary>
		/// Principal for which this API key was created
		/// </summary>
		[JsonProperty("username")]
		public string Username { get; internal set; }
		/// <summary>
		/// Realm name of the principal for which this API key was created
		/// </summary>
		[JsonProperty("realm")]
		public string Realm { get; internal set; }
	}
}
