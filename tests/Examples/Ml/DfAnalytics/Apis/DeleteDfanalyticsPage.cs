// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.DfAnalytics.Apis
{
	public class DeleteDfanalyticsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/df-analytics/apis/delete-dfanalytics.asciidoc:59")]
		public void Line59()
		{
			// tag::1c8b6768c4eefc76fcb38708152f561b[]
			var response0 = new SearchResponse<object>();
			// end::1c8b6768c4eefc76fcb38708152f561b[]

			response0.MatchesExample(@"DELETE _ml/data_frame/analytics/loganalytics");
		}
	}
}
