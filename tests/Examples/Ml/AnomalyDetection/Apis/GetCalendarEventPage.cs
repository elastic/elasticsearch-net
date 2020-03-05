using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetCalendarEventPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/get-calendar-event.asciidoc:85")]
		public void Line85()
		{
			// tag::39d6f575c9458d9c941364dfd0493fa0[]
			var response0 = new SearchResponse<object>();
			// end::39d6f575c9458d9c941364dfd0493fa0[]

			response0.MatchesExample(@"GET _ml/calendars/planned-outages/events");
		}
	}
}