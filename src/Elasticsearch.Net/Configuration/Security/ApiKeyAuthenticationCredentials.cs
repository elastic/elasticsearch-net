using System;
using System.Security;

namespace Elasticsearch.Net
{
	/// <summary>
	/// Credentials for Api Key Authentication
	/// </summary>
	public class ApiKeyAuthenticationCredentials : IDisposable
	{
		public ApiKeyAuthenticationCredentials()
		{
		}

		public ApiKeyAuthenticationCredentials(string id, SecureString apiKey)
		{
			Id = id;
			ApiKey = apiKey;
		}

		public ApiKeyAuthenticationCredentials(string id, string apiKey)
		{
			Id = id;
			ApiKey = apiKey.CreateSecureString();
		}

		/// <summary>
		/// The api_key with which to authenticate
		/// </summary>
		public SecureString ApiKey { get; set; }

		/// <summary>
		/// The id with which to authenticate
		/// </summary>
		public string Id { get; set; }

		public void Dispose() => ApiKey?.Dispose();
	}
}
