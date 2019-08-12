using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class ForecastPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line62()
		{
			// tag::5bed6929ccc86ef27f9468cf844169c8[]
			var response0 = new SearchResponse<object>();
			// end::5bed6929ccc86ef27f9468cf844169c8[]

			response0.MatchesExample(@"POST _ml/anomaly_detectors/total-requests/_forecast
			{
			  ""duration"": ""10d""
			}");
		}
	}
}