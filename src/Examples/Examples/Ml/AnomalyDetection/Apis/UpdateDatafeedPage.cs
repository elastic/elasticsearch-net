using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class UpdateDatafeedPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line105()
		{
			// tag::df6d5b5f8e1c8785503269ccb7b34763[]
			var response0 = new SearchResponse<object>();
			// end::df6d5b5f8e1c8785503269ccb7b34763[]

			response0.MatchesExample(@"POST _ml/datafeeds/datafeed-total-requests/_update
			{
			  ""query"": {
			    ""term"": {
			      ""level"": ""error""
			    }
			  }
			}");
		}
	}
}