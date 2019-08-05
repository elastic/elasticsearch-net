using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class SetUpgradeModePage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line78()
		{
			// tag::ae4aa368617637a390074535df86e64b[]
			var response0 = new SearchResponse<object>();
			// end::ae4aa368617637a390074535df86e64b[]

			response0.MatchesExample(@"POST _ml/set_upgrade_mode?enabled=true&timeout=10m");
		}

		[U]
		[SkipExample]
		public void Line103()
		{
			// tag::8e9e7dc5fad2b2b8e74ab4dc225d9c53[]
			var response0 = new SearchResponse<object>();
			// end::8e9e7dc5fad2b2b8e74ab4dc225d9c53[]

			response0.MatchesExample(@"POST _ml/set_upgrade_mode?enabled=false&timeout=10m");
		}
	}
}