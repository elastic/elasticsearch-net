// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("security.grant_api_key.json")]
	[ReadAs(typeof(GrantApiKeyRequest))]
	public partial interface IGrantApiKeyRequest
	{
		/// <summary>
		/// The user’s access token. If you specify the access_token grant type,
		/// this parameter is required. It is not valid with other grant types.
		/// </summary>
		[DataMember(Name = "access_token")]
		string AccessToken { get; set; }

		/// <summary>
		/// The type of grant. Supported grant types are: access_token,password.
		/// </summary>
		[DataMember(Name = "grant_type")]
		GrantType? GrantType { get; set; }

		/// <summary>
		/// The user’s password. If you specify the password grant type,
		/// this parameter is required. It is not valid with other grant types.
		/// </summary>
		[DataMember(Name = "password")]
		string Password { get; set; }

		/// <summary>
		/// The user name that identifies the user. If you specify the password grant type, 
		/// this parameter is required. It is not valid with other grant types.
		/// </summary>
		[DataMember(Name = "username")]
		string Username { get; set; }

		/// <summary>
		/// Defines the API key.
		/// </summary>
		[DataMember(Name = "api_key")]
		IApiKey ApiKey { get; set; }
	}

	public partial class GrantApiKeyRequest
	{
		/// <inheritdoc cref="IGrantApiKeyRequest.AccessToken" />
		public string AccessToken { get; set; }

		/// <inheritdoc cref="IGrantApiKeyRequest.GrantType" />
		public GrantType? GrantType { get; set; }

		/// <inheritdoc cref="IGrantApiKeyRequest.Password" />
		public string Password { get; set; }

		/// <inheritdoc cref="IGrantApiKeyRequest.Username" />
		public string Username { get; set; }

		/// <inheritdoc cref="IGrantApiKeyRequest.ApiKey" />
		public IApiKey ApiKey { get; set; }
	}

	public partial class GrantApiKeyDescriptor
	{
		/// <inheritdoc cref="IGrantApiKeyRequest.AccessToken" />
		string IGrantApiKeyRequest.AccessToken { get; set; }

		/// <inheritdoc cref="IGrantApiKeyRequest.GrantType" />
		GrantType? IGrantApiKeyRequest.GrantType { get; set; } = Nest.GrantType.AccessToken;

		/// <inheritdoc cref="IGrantApiKeyRequest.Password" />
		string IGrantApiKeyRequest.Password { get; set; }

		/// <inheritdoc cref="IGrantApiKeyRequest.Username" />
		string IGrantApiKeyRequest.Username { get; set; }

		/// <inheritdoc cref="IGrantApiKeyRequest.ApiKey" />
		IApiKey IGrantApiKeyRequest.ApiKey { get; set; }

		/// <inheritdoc cref="IGrantApiKeyRequest.AccessToken" />
		public GrantApiKeyDescriptor AccessToken(string accessToken) => Assign(accessToken, (a, v) => a.AccessToken = v);

		/// <inheritdoc cref="IGrantApiKeyRequest.GrantType" />
		public GrantApiKeyDescriptor GrantType(GrantType? type) => Assign(type, (a, v) => a.GrantType = v);

		/// <inheritdoc cref="IGrantApiKeyRequest.Password" />
		public GrantApiKeyDescriptor Password(string password) => Assign(password, (a, v) => a.Password = v);

		/// <inheritdoc cref="IGrantApiKeyRequest.Username" />
		public GrantApiKeyDescriptor Username(string username) => Assign(username, (a, v) => a.Username = v);

		/// <inheritdoc cref="IGrantApiKeyRequest.ApiKey" />
		public GrantApiKeyDescriptor ApiKey(Func<ApiKeyDescriptor, IApiKey> selector) =>
			Assign(selector, (a, v) => a.ApiKey = v?.Invoke(new ApiKeyDescriptor()));
	}
}
