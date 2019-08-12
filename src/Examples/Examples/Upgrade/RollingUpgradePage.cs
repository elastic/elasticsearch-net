using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Upgrade
{
	public class RollingUpgradePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line35()
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

		[U(Skip = "Example not implemented")]
		public void Line37()
		{
			// tag::31b4eec9ac4c2c3fdfbaeee8d2f83513[]
			var response0 = new SearchResponse<object>();
			// end::31b4eec9ac4c2c3fdfbaeee8d2f83513[]

			response0.MatchesExample(@"POST _flush/synced");
		}

		[U(Skip = "Example not implemented")]
		public void Line70()
		{
			// tag::a21a7bf052b41f5b996dc58f7b69770f[]
			var response0 = new SearchResponse<object>();
			// end::a21a7bf052b41f5b996dc58f7b69770f[]

			response0.MatchesExample(@"POST _ml/set_upgrade_mode?enabled=true");
		}

		[U(Skip = "Example not implemented")]
		public void Line77()
		{
			// tag::7e49705769c42895fb7b1e2ca028ff47[]
			var response0 = new SearchResponse<object>();
			// end::7e49705769c42895fb7b1e2ca028ff47[]

			response0.MatchesExample(@"GET _cat/nodes");
		}

		[U(Skip = "Example not implemented")]
		public void Line91()
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

		[U(Skip = "Example not implemented")]
		public void Line110()
		{
			// tag::5c53944aec2ce3e55854e315f0482029[]
			var response0 = new SearchResponse<object>();
			// end::5c53944aec2ce3e55854e315f0482029[]

			response0.MatchesExample(@"GET _cat/health?v");
		}

		[U(Skip = "Example not implemented")]
		public void Line141()
		{
			// tag::6b74ff6df5d7583add837b34a6c80a43[]
			var response0 = new SearchResponse<object>();
			// end::6b74ff6df5d7583add837b34a6c80a43[]

			response0.MatchesExample(@"GET _cat/recovery");
		}

		[U(Skip = "Example not implemented")]
		public void Line168()
		{
			// tag::3c5d5a5c34a62724942329658c688f5e[]
			var response0 = new SearchResponse<object>();
			// end::3c5d5a5c34a62724942329658c688f5e[]

			response0.MatchesExample(@"POST _ml/set_upgrade_mode?enabled=false");
		}
	}
}