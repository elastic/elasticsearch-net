using System;
using System.Security;

namespace Elasticsearch.Net
{
	/// <summary>
	/// Credentials for Basic Authentication
	/// </summary>
	public class BasicAuthenticationCredentials : IDisposable
	{
		public BasicAuthenticationCredentials()
		{
		}

		public BasicAuthenticationCredentials(string username, string password)
		{
			Username = username;
			Password = password.CreateSecureString();
		}

		public BasicAuthenticationCredentials(string username, SecureString password)
		{
			Username = username;
			Password = password;
		}

		/// <summary>
		/// The password with which to authenticate
		/// </summary>
		public SecureString Password { get; set; }

		/// <summary>
		/// The username with which to authenticate
		/// </summary>
		public string Username { get; set; }

		public void Dispose() => Password?.Dispose();
	}
}
