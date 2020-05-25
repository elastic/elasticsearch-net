// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.DfAnalytics.Apis
{
	public class ExplainDfanalyticsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/df-analytics/apis/explain-dfanalytics.asciidoc:124")]
		public void Line124()
		{
			// tag::8aa17bd25a3f2d634e5253b4b72fec4c[]
			var response0 = new SearchResponse<object>();
			// end::8aa17bd25a3f2d634e5253b4b72fec4c[]

			response0.MatchesExample(@"POST _ml/data_frame/analytics/_explain
			{
			  ""source"": {
			    ""index"": ""houses_sold_last_10_yrs""
			  },
			  ""analysis"": {
			    ""regression"": {
			      ""dependent_variable"": ""price""
			    }
			  }
			}");
		}
	}
}
