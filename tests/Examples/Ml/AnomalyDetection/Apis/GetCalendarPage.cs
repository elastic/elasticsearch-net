using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetCalendarPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/get-calendar.asciidoc:68")]
		public void Line68()
		{
			// tag::5fca6671bc8eaddc44ac488d1c3c6909[]
			var response0 = new SearchResponse<object>();
			// end::5fca6671bc8eaddc44ac488d1c3c6909[]

			response0.MatchesExample(@"GET _ml/calendars/planned-outages");
		}
	}
}