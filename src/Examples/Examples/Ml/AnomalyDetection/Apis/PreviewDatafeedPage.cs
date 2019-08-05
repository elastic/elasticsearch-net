using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class PreviewDatafeedPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line52()
		{
			// tag::27b20b345329fc72d8a0c7d440596ea1[]
			var response0 = new SearchResponse<object>();
			// end::27b20b345329fc72d8a0c7d440596ea1[]

			response0.MatchesExample(@"GET _ml/datafeeds/datafeed-farequote/_preview");
		}
	}
}