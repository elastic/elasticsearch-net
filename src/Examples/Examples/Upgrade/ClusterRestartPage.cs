using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Upgrade
{
	public class ClusterRestartPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line27()
		{
			// tag::1cd3b9d65576a9212eef898eb3105758[]
			var response0 = new SearchResponse<object>();
			// end::1cd3b9d65576a9212eef898eb3105758[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			  ""persistent"": {
			    ""cluster.routing.allocation.enable"": ""primaries""
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line28()
		{
			// tag::31b4eec9ac4c2c3fdfbaeee8d2f83513[]
			var response0 = new SearchResponse<object>();
			// end::31b4eec9ac4c2c3fdfbaeee8d2f83513[]

			response0.MatchesExample(@"POST _flush/synced");
		}

		[U]
		[SkipExample]
		public void Line60()
		{
			// tag::a21a7bf052b41f5b996dc58f7b69770f[]
			var response0 = new SearchResponse<object>();
			// end::a21a7bf052b41f5b996dc58f7b69770f[]

			response0.MatchesExample(@"POST _ml/set_upgrade_mode?enabled=true");
		}

		[U]
		[SkipExample]
		public void Line83()
		{
			// tag::c0a4b0c1c6eff14da8b152ceb19c1c31[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::c0a4b0c1c6eff14da8b152ceb19c1c31[]

			response0.MatchesExample(@"GET _cat/health");

			response1.MatchesExample(@"GET _cat/nodes");
		}

		[U]
		[SkipExample]
		public void Line117()
		{
			// tag::45ef5156dbd2d3fd4fd22b8d99f7aad4[]
			var response0 = new SearchResponse<object>();
			// end::45ef5156dbd2d3fd4fd22b8d99f7aad4[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			  ""persistent"": {
			    ""cluster.routing.allocation.enable"": null
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line137()
		{
			// tag::2d9b30acd6b5683f39d53494c0dd779c[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::2d9b30acd6b5683f39d53494c0dd779c[]

			response0.MatchesExample(@"GET _cat/health");

			response1.MatchesExample(@"GET _cat/recovery");
		}

		[U]
		[SkipExample]
		public void Line154()
		{
			// tag::3c5d5a5c34a62724942329658c688f5e[]
			var response0 = new SearchResponse<object>();
			// end::3c5d5a5c34a62724942329658c688f5e[]

			response0.MatchesExample(@"POST _ml/set_upgrade_mode?enabled=false");
		}
	}
}