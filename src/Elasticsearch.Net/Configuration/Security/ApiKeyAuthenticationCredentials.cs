using System;
using System.Security;
using System.Text;

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
			Base64EncodedApiKey = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{id}:{apiKey.CreateString()}")).CreateSecureString();
		}

		public ApiKeyAuthenticationCredentials(string id, string apiKey)
		{
			Base64EncodedApiKey = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{id}:{apiKey}")).CreateSecureString();
		}

		public ApiKeyAuthenticationCredentials(string base64EncodedApiKey)
		{
			Base64EncodedApiKey = base64EncodedApiKey.CreateSecureString();
		}

		public ApiKeyAuthenticationCredentials(SecureString base64EncodedApiKey)
		{
			Base64EncodedApiKey = base64EncodedApiKey;
		}

		/// <summary>
		/// The Base64 encoded api key with which to authenticate
		/// Take the form, id:api_key, which is then base 64 encoded
		/// </summary>
		public SecureString Base64EncodedApiKey { get; }

		public void Dispose() => Base64EncodedApiKey?.Dispose();
	}
}
