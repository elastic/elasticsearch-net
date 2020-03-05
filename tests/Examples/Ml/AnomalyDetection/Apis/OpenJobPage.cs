using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class OpenJobPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/open-job.asciidoc:54")]
		public void Line54()
		{
			// tag::a6204edaa0bcf7b82a89ab4f6bda0914[]
			var response0 = new SearchResponse<object>();
			// end::a6204edaa0bcf7b82a89ab4f6bda0914[]

			response0.MatchesExample(@"POST _ml/anomaly_detectors/low_request_rate/_open
			{
			  ""timeout"": ""35m""
			}");
		}
	}
}