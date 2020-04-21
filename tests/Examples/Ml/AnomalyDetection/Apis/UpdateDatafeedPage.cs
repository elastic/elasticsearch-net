using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class UpdateDatafeedPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/update-datafeed.asciidoc:119")]
		public void Line119()
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