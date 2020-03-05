using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class PostCalendarEventPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/post-calendar-event.asciidoc:63")]
		public void Line63()
		{
			// tag::c067182d385f59ce5952fb9a716fbf05[]
			var response0 = new SearchResponse<object>();
			// end::c067182d385f59ce5952fb9a716fbf05[]

			response0.MatchesExample(@"POST _ml/calendars/planned-outages/events
			{
			  ""events"" : [
			    {""description"": ""event 1"", ""start_time"": 1513641600000, ""end_time"": 1513728000000},
			    {""description"": ""event 2"", ""start_time"": 1513814400000, ""end_time"": 1513900800000},
			    {""description"": ""event 3"", ""start_time"": 1514160000000, ""end_time"": 1514246400000}
			  ]
			}");
		}
	}
}