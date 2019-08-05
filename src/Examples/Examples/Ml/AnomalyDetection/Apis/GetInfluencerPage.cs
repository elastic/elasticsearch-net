using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetInfluencerPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line77()
		{
			// tag::c644b2026414830b6265f7a14dd37ce9[]
			var response0 = new SearchResponse<object>();
			// end::c644b2026414830b6265f7a14dd37ce9[]

			response0.MatchesExample(@"GET _ml/anomaly_detectors/it_ops_new_kpi/results/influencers
			{
			  ""sort"": ""influencer_score"",
			  ""desc"": true
			}");
		}
	}
}