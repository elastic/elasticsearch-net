using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class OpenJobPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line56()
		{
			// tag::72cb058a415b56a8964c05195114b5c0[]
			var response0 = new SearchResponse<object>();
			// end::72cb058a415b56a8964c05195114b5c0[]

			response0.MatchesExample(@"POST _ml/anomaly_detectors/total-requests/_open
			{
			  ""timeout"": ""35m""
			}");
		}
	}
}