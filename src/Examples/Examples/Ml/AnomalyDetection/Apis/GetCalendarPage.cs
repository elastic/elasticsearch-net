using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetCalendarPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line72()
		{
			// tag::5fca6671bc8eaddc44ac488d1c3c6909[]
			var response0 = new SearchResponse<object>();
			// end::5fca6671bc8eaddc44ac488d1c3c6909[]

			response0.MatchesExample(@"GET _ml/calendars/planned-outages");
		}
	}
}