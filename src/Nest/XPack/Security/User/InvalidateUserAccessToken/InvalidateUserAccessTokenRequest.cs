/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Invalidates one or more access tokens or refresh tokens.
	/// </summary>
	[MapsApi("security.invalidate_token.json")]
	public partial interface IInvalidateUserAccessTokenRequest
	{
		/// <summary>
		/// An access token.
		/// This parameter cannot be used if any of <see cref="RefreshToken"/>, <see cref="RealmName"/> or <see cref="Username"/> are used.
		/// </summary>
		[DataMember(Name ="token")]
		string Token { get; set; }

		/// <summary>
		/// A refresh token.
		/// This parameter cannot be used any of <see cref="RefreshToken"/>, <see cref="RealmName"/> or <see cref="Username"/> are used.
		/// </summary>
		[DataMember(Name = "refresh_token")]
		string RefreshToken { get; set; }

		/// <summary>
		/// The name of an authentication realm.
		/// This parameter cannot be used with either <see cref="RefreshToken"/> or <see cref="Token"/>.
		/// </summary>
		[DataMember(Name = "realm_name")]
		string RealmName { get; set; }

		/// <summary>
		/// The username of a user.
		/// This parameter cannot be used with either <see cref="RefreshToken"/> or <see cref="Token"/>.
		/// </summary>
		[DataMember(Name = "username")]
		string Username { get; set; }
	}


	public partial class InvalidateUserAccessTokenRequest
	{
		public InvalidateUserAccessTokenRequest(string token) => ((IInvalidateUserAccessTokenRequest)this).Token = token;

		/// <inheritdoc cref="IInvalidateUserAccessTokenRequest.Token"/>
		string IInvalidateUserAccessTokenRequest.Token { get; set; }

		/// <inheritdoc cref="IInvalidateUserAccessTokenRequest.RefreshToken"/>
		string IInvalidateUserAccessTokenRequest.RefreshToken { get; set; }

		/// <inheritdoc cref="IInvalidateUserAccessTokenRequest.RealmName"/>
		string IInvalidateUserAccessTokenRequest.RealmName { get; set; }

		/// <inheritdoc cref="IInvalidateUserAccessTokenRequest.Username"/>
		string IInvalidateUserAccessTokenRequest.Username { get; set; }
	}

	public partial class InvalidateUserAccessTokenDescriptor
	{
		/// <inheritdoc cref="IInvalidateUserAccessTokenRequest.Token" />
		public InvalidateUserAccessTokenDescriptor(string token) => ((IInvalidateUserAccessTokenRequest)this).Token = token;

		/// <inheritdoc cref="IInvalidateUserAccessTokenRequest.RefreshToken" />
		public InvalidateUserAccessTokenDescriptor RefreshToken(string refreshToken) => Assign(refreshToken, (a, v) => a.RefreshToken = v);

		/// <inheritdoc cref="IInvalidateUserAccessTokenRequest.RealmName" />
		public InvalidateUserAccessTokenDescriptor RealmName(string realmName) => Assign(realmName, (a, v) => a.RealmName = v);

		/// <inheritdoc cref="IInvalidateUserAccessTokenRequest.Username" />
		public InvalidateUserAccessTokenDescriptor Username(string username) => Assign(username, (a, v) => a.Username = v);

		/// <inheritdoc cref="IInvalidateUserAccessTokenRequest.Token" />
		string IInvalidateUserAccessTokenRequest.Token { get; set; }

		/// <inheritdoc cref="IInvalidateUserAccessTokenRequest.RefreshToken" />
		string IInvalidateUserAccessTokenRequest.RefreshToken { get; set; }

		/// <inheritdoc cref="IInvalidateUserAccessTokenRequest.RealmName" />
		string IInvalidateUserAccessTokenRequest.RealmName { get; set; }

		/// <inheritdoc cref="IInvalidateUserAccessTokenRequest.Username" />
		string IInvalidateUserAccessTokenRequest.Username { get; set; }
	}
}
