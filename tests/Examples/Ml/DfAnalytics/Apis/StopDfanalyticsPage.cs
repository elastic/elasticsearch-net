// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.DfAnalytics.Apis
{
	public class StopDfanalyticsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/df-analytics/apis/stop-dfanalytics.asciidoc:76")]
		public void Line76()
		{
			// tag::db19cc7a26ca80106d86d688f4be67a8[]
			var response0 = new SearchResponse<object>();
			// end::db19cc7a26ca80106d86d688f4be67a8[]

			response0.MatchesExample(@"POST _ml/data_frame/analytics/loganalytics/_stop");
		}
	}
}
