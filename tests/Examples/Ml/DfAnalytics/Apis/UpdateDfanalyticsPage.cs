using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Ml.DfAnalytics.Apis
{
	public class UpdateDfanalyticsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/df-analytics/apis/update-dfanalytics.asciidoc:100")]
		public void Line100()
		{
			// tag::d19956ce4b751d1909ec2d257c4ecaa4[]
			var response0 = new SearchResponse<object>();
			// end::d19956ce4b751d1909ec2d257c4ecaa4[]

			response0.MatchesExample(@"POST _ml/data_frame/analytics/model-flight-delays/_update
			{
			  ""model_memory_limit"": ""200mb""
			}");
		}
	}
}