using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class StopDatafeedPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line86()
		{
			// tag::9b1e808ac4cca788990b497fb59ba455[]
			var response0 = new SearchResponse<object>();
			// end::9b1e808ac4cca788990b497fb59ba455[]

			response0.MatchesExample(@"POST _ml/datafeeds/datafeed-total-requests/_stop
			{
			  ""timeout"": ""30s""
			}");
		}
	}
}