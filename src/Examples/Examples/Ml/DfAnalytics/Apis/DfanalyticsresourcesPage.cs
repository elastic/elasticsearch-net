using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.DfAnalytics.Apis
{
	public class DfanalyticsresourcesPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line23()
		{
			// tag::7b8afc2612fb2cdf2263cff1dead852c[]
			var response0 = new SearchResponse<object>();
			// end::7b8afc2612fb2cdf2263cff1dead852c[]

			response0.MatchesExample(@"PUT _ml/data_frame/analytics/loganalytics
			{
			  ""source"": {
			    ""index"": ""logdata""
			  },
			  ""dest"": {
			    ""index"": ""logdata_out""
			  },
			  ""analysis"": {
			    ""outlier_detection"": {
			    }
			  },
			  ""analyzed_fields"": {
			        ""includes"": [ ""request.bytes"", ""response.counts.error"" ],
			        ""excludes"": [ ""source.geo"" ]
			  }
			}");
		}
	}
}