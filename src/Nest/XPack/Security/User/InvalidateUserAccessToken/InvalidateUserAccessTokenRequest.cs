using Newtonsoft.Json;

namespace Nest
{
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

	[DescriptorFor("XpackSecurityInvalidateToken")]
	public partial class InvalidateUserAccessTokenDescriptor
	{
		public InvalidateUserAccessTokenDescriptor(string token) => ((IInvalidateUserAccessTokenRequest)this).Token = token;

		string IInvalidateUserAccessTokenRequest.Token { get; set; }
	}
}
