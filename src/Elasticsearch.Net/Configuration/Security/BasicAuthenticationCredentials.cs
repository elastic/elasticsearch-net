// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Security;

namespace Elasticsearch.Net
{
	/// <summary>
	/// Credentials for Basic Authentication
	/// </summary>
	public class BasicAuthenticationCredentials : IDisposable
	{
		//TODO remove this constructor in 8.0
		public BasicAuthenticationCredentials() { }

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
