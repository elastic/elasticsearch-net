using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class DeleteCalendarEventPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/delete-calendar-event.asciidoc:45")]
		public void Line45()
		{
			// tag::f6982ff80b9a64cd5fcac5b20908c906[]
			var response0 = new SearchResponse<object>();
			// end::f6982ff80b9a64cd5fcac5b20908c906[]

			response0.MatchesExample(@"DELETE _ml/calendars/planned-outages/events/LS8LJGEBMTCMA-qz49st");
		}
	}
}