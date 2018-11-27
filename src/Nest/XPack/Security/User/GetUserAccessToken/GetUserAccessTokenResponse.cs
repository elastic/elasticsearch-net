using System.Runtime.Serialization;

namespace Nest
{
	public interface IGetUserAccessTokenResponse : IResponse
	{
		[DataMember(Name ="access_token")]
		string AccessToken { get; set; }

		[DataMember(Name ="expires_in")]
		long ExpiresIn { get; set; }

		[DataMember(Name ="scope")]
		string Scope { get; set; }

		[DataMember(Name ="type")]
		string Type { get; set; }
	}

	public class GetUserAccessTokenResponse : ResponseBase, IGetUserAccessTokenResponse
	{
		public string AccessToken { get; set; }
		public long ExpiresIn { get; set; }
		public string Scope { get; set; }
		public string Type { get; set; }
	}
}
