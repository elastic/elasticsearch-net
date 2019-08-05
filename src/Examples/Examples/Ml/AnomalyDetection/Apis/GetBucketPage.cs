using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetBucketPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line93()
		{
			// tag::2c7fce4025e8e429e1ae8d50f5eb4b88[]
			var response0 = new SearchResponse<object>();
			// end::2c7fce4025e8e429e1ae8d50f5eb4b88[]

			response0.MatchesExample(@"GET _ml/anomaly_detectors/it-ops-kpi/results/buckets
			{
			  ""anomaly_score"": 80,
			  ""start"": ""1454530200001""
			}");
		}
	}
}