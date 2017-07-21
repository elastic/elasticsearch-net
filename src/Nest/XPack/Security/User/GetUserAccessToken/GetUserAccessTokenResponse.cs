using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetUserAccessTokenResponse : IResponse
	{
		[JsonProperty("access_token")]
		string AccessToken { get; set; }
		[JsonProperty("type")]
		string Type { get; set; }
		[JsonProperty("expires_in")]
		long ExpiresIn { get; set; }
		[JsonProperty("scope")]
		string Scope { get; set; }
	}

	public class GetUserAccessTokenResponse : ResponseBase, IGetUserAccessTokenResponse
	{
		public string AccessToken { get; set; }
		public string Type { get; set; }
		public long ExpiresIn { get; set; }
		public string Scope { get; set; }
	}
}
