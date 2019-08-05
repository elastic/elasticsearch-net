using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetDatafeedStatsPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line93()
		{
			// tag::62ef8873988dc63f37ed93114072e4a8[]
			var response0 = new SearchResponse<object>();
			// end::62ef8873988dc63f37ed93114072e4a8[]

			response0.MatchesExample(@"GET _ml/datafeeds/datafeed-total-requests/_stats");
		}
	}
}