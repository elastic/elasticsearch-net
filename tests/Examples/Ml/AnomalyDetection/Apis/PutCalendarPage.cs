using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class PutCalendarPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line45()
		{
			// tag::e61b5abe85000cc954a42e2cd74f3a26[]
			var response0 = new SearchResponse<object>();
			// end::e61b5abe85000cc954a42e2cd74f3a26[]

			response0.MatchesExample(@"PUT _ml/calendars/planned-outages");
		}
	}
}