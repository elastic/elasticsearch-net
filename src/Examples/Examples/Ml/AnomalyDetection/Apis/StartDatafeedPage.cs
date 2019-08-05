using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class StartDatafeedPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line100()
		{
			// tag::c85f09d9a0622d32788bd56b0a008592[]
			var response0 = new SearchResponse<object>();
			// end::c85f09d9a0622d32788bd56b0a008592[]

			response0.MatchesExample(@"POST _ml/datafeeds/datafeed-total-requests/_start
			{
			  ""start"": ""2017-04-07T18:22:16Z""
			}");
		}
	}
}