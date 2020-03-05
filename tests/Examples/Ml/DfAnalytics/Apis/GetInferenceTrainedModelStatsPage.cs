using Elastic.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Ml.DfAnalytics.Apis
{
	public class GetInferenceTrainedModelStatsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/df-analytics/apis/get-inference-trained-model-stats.asciidoc:85")]
		public void Line85()
		{
			// tag::2dde95ba98c5d5e19725fbb10435d283[]
			var response0 = new SearchResponse<object>();
			// end::2dde95ba98c5d5e19725fbb10435d283[]

			response0.MatchesExample(@"GET _ml/inference/_stats");
		}
	}
}