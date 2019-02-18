using Newtonsoft.Json;

namespace Nest
{
	[MapsApi("security.invalidate_token.json")]
	public partial interface IInvalidateUserAccessTokenRequest
	{
		[JsonProperty("token")]
		string Token { get; set; }
	}

	public partial class InvalidateUserAccessTokenRequest
	{
		public InvalidateUserAccessTokenRequest(string token) => ((IInvalidateUserAccessTokenRequest)this).Token = token;

		[JsonProperty("token")]
		string IInvalidateUserAccessTokenRequest.Token { get; set; }
	}

	public partial class InvalidateUserAccessTokenDescriptor
	{
		public InvalidateUserAccessTokenDescriptor(string token) => ((IInvalidateUserAccessTokenRequest)this).Token = token;

		string IInvalidateUserAccessTokenRequest.Token { get; set; }
	}
}
