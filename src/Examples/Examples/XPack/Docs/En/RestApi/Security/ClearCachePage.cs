using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class ClearCachePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line42()
		{
			// tag::a5e2b3588258430f2e595abda98e3943[]
			var response0 = new SearchResponse<object>();
			// end::a5e2b3588258430f2e595abda98e3943[]

			response0.MatchesExample(@"POST /_security/realm/default_file/_clear_cache");
		}

		[U(Skip = "Example not implemented")]
		public void Line50()
		{
			// tag::c1409f591a01589638d9b00436ce42c0[]
			var response0 = new SearchResponse<object>();
			// end::c1409f591a01589638d9b00436ce42c0[]

			response0.MatchesExample(@"POST /_security/realm/default_file/_clear_cache?usernames=rdeniro,alpacino");
		}

		[U(Skip = "Example not implemented")]
		public void Line59()
		{
			// tag::00272f75a6afea91f8554ef7cda0c1f2[]
			var response0 = new SearchResponse<object>();
			// end::00272f75a6afea91f8554ef7cda0c1f2[]

			response0.MatchesExample(@"POST /_security/realm/default_file,ldap1/_clear_cache");
		}
	}
}