using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IGetUserAccessTokenRequest
	{
		[JsonProperty("username")]
		string Username { get; set; }
		[JsonProperty("password")]
		string Password { get; set; }
		[JsonProperty("scope")]
		string Scope { get; set; }
		[JsonProperty("grant_type")]
		AccessTokenGrantType? GrantType { get; set; }
	}

	public partial class GetUserAccessTokenRequest
	{
		[JsonProperty("username")]
		string IGetUserAccessTokenRequest.Username { get; set; }
		[JsonProperty("password")]
		string IGetUserAccessTokenRequest.Password { get; set; }

		public GetUserAccessTokenRequest(string username, string password)
		{
			var self = (IGetUserAccessTokenRequest) this;
			self.Username = username;
			self.Password = password;
		}

		public AccessTokenGrantType? GrantType { get; set; } = AccessTokenGrantType.Password;

		public string Scope { get; set; }
	}

	[DescriptorFor("XpackSecurityGetToken")]
	public partial class GetUserAccessTokenDescriptor
	{
		AccessTokenGrantType? IGetUserAccessTokenRequest.GrantType { get; set; } = AccessTokenGrantType.Password;
		string IGetUserAccessTokenRequest.Username { get; set; }
		string IGetUserAccessTokenRequest.Password { get; set; }
		string IGetUserAccessTokenRequest.Scope { get; set; }

		public GetUserAccessTokenDescriptor(string username, string password)
		{
			var self = (IGetUserAccessTokenRequest) this;
			self.Username = username;
			self.Password = password;
		}

		public GetUserAccessTokenDescriptor GrantType(AccessTokenGrantType? type) => Assign(a=>a.GrantType = type);
		public GetUserAccessTokenDescriptor Scope(string scope) => Assign(a=>a.Scope = scope);
	}
}
