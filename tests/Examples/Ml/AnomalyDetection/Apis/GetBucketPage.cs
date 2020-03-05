using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetBucketPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/get-bucket.asciidoc:174")]
		public void Line174()
		{
			// tag::f96d4614f2fc294339fef325b794355f[]
			var response0 = new SearchResponse<object>();
			// end::f96d4614f2fc294339fef325b794355f[]

			response0.MatchesExample(@"GET _ml/anomaly_detectors/low_request_rate/results/buckets
			{
			  ""anomaly_score"": 80,
			  ""start"": ""1454530200001""
			}");
		}
	}
}