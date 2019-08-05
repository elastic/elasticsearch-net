using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class DeleteDatafeedPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line46()
		{
			// tag::8a12cd824404d74f098d854716a26899[]
			var response0 = new SearchResponse<object>();
			// end::8a12cd824404d74f098d854716a26899[]

			response0.MatchesExample(@"DELETE _ml/datafeeds/datafeed-total-requests");
		}
	}
}