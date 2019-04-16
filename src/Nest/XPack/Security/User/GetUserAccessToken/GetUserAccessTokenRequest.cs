using System.Runtime.Serialization;

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

		public GetUserAccessTokenDescriptor GrantType(AccessTokenGrantType? type) => Assign(a => a.GrantType = type);

		public GetUserAccessTokenDescriptor Scope(string scope) => Assign(a => a.Scope = scope);
	}
}
