using System;
using System.Collections.Generic;
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
		[JsonProperty("token")]
		string IInvalidateUserAccessTokenRequest.Token { get; set; }

		public InvalidateUserAccessTokenRequest(string token)
		{
			((IInvalidateUserAccessTokenRequest) this).Token = token;
		}
	}

	[DescriptorFor("XpackSecurityInvalidateToken")]
	public partial class InvalidateUserAccessTokenDescriptor
	{
		string IInvalidateUserAccessTokenRequest.Token { get; set; }

		public InvalidateUserAccessTokenDescriptor(string token)
		{
			((IInvalidateUserAccessTokenRequest) this).Token = token;
		}

	}
}
