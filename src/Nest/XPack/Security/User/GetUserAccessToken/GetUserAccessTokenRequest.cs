// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("security.get_token.json")]
	public partial interface IGetUserAccessTokenRequest
	{
		[DataMember(Name ="grant_type")]
		AccessTokenGrantType? GrantType { get; set; }

		[DataMember(Name ="password")]
		string Password { get; set; }

		[DataMember(Name ="scope")]
		string Scope { get; set; }

		[DataMember(Name ="username")]
		string Username { get; set; }
	}

	public partial class GetUserAccessTokenRequest
	{
		public GetUserAccessTokenRequest(string username, string password)
		{
			var self = (IGetUserAccessTokenRequest)this;
			self.Username = username;
			self.Password = password;
		}

		public AccessTokenGrantType? GrantType { get; set; } = AccessTokenGrantType.Password;

		public string Scope { get; set; }

		[DataMember(Name ="password")]
		string IGetUserAccessTokenRequest.Password { get; set; }

		[DataMember(Name ="username")]
		string IGetUserAccessTokenRequest.Username { get; set; }
	}

	public partial class GetUserAccessTokenDescriptor
	{
		public GetUserAccessTokenDescriptor(string username, string password)
		{
			var self = (IGetUserAccessTokenRequest)this;
			self.Username = username;
			self.Password = password;
		}

		AccessTokenGrantType? IGetUserAccessTokenRequest.GrantType { get; set; } = AccessTokenGrantType.Password;
		string IGetUserAccessTokenRequest.Password { get; set; }
		string IGetUserAccessTokenRequest.Scope { get; set; }
		string IGetUserAccessTokenRequest.Username { get; set; }

		public GetUserAccessTokenDescriptor GrantType(AccessTokenGrantType? type) => Assign(type, (a, v) => a.GrantType = v);

		public GetUserAccessTokenDescriptor Scope(string scope) => Assign(scope, (a, v) => a.Scope = v);
	}
}
