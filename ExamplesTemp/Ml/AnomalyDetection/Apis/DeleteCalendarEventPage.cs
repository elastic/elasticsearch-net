using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class DeleteCalendarEventPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line47()
		{
			// tag::f6982ff80b9a64cd5fcac5b20908c906[]
			var response0 = new SearchResponse<object>();
			// end::f6982ff80b9a64cd5fcac5b20908c906[]

			response0.MatchesExample(@"DELETE _ml/calendars/planned-outages/events/LS8LJGEBMTCMA-qz49st");
		}
	}
}