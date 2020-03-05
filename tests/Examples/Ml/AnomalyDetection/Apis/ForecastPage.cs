using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class ForecastPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/forecast.asciidoc:61")]
		public void Line61()
		{
			// tag::591c7fb7451069829a14bba593136f1f[]
			var response0 = new SearchResponse<object>();
			// end::591c7fb7451069829a14bba593136f1f[]

			response0.MatchesExample(@"POST _ml/anomaly_detectors/low_request_rate/_forecast
			{
			  ""duration"": ""10d""
			}");
		}
	}
}