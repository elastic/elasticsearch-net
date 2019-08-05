using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class DeleteCalendarPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line41()
		{
			// tag::63893e7e9479a9b60db71dcddcc79aaf[]
			var response0 = new SearchResponse<object>();
			// end::63893e7e9479a9b60db71dcddcc79aaf[]

			response0.MatchesExample(@"DELETE _ml/calendars/planned-outages");
		}
	}
}