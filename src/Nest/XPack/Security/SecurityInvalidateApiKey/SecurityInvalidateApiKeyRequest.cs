using Newtonsoft.Json;

namespace Nest
{
	public partial interface ISecurityInvalidateApiKeyRequest
	{
		[JsonProperty("token")]
		string Token { get; set; }

		[JsonProperty("refresh_token")]
		string RefreshToken { get; set; }

		[JsonProperty("realm_name")]
		string RealmName { get; set; }

		[JsonProperty("username")]
		string Username { get; set; }
	}

	public partial class SecurityInvalidateApiKeyRequest
	{
		public string Token { get; set; }
		public string RefreshToken { get; set; }
		public string RealmName { get; set; }
		public string Username { get; set; }
	}

	[DescriptorFor("XpackSecuritySecurityInvalidateApiKey")]
	public partial class SecurityInvalidateApiKeyDescriptor
	{
		string ISecurityInvalidateApiKeyRequest.Token { get; set; }
		string ISecurityInvalidateApiKeyRequest.RefreshToken { get; set; }
		string ISecurityInvalidateApiKeyRequest.RealmName { get; set; }
		string ISecurityInvalidateApiKeyRequest.Username { get; set; }

		public SecurityInvalidateApiKeyDescriptor Token(string token) => Assign(token, (a, v) => a.Token = v);
		public SecurityInvalidateApiKeyDescriptor RefreshToken(string refreshToken) => Assign(refreshToken, (a, v) => a.RefreshToken = v);
		public SecurityInvalidateApiKeyDescriptor RealmName(string realmName) => Assign(realmName, (a, v) => a.RealmName = v);
		public SecurityInvalidateApiKeyDescriptor Username(string username) => Assign(username, (a, v) => a.Username = v);
	}
}
