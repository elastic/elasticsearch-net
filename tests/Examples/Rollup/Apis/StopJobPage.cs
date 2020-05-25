// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Rollup.Apis
{
	public class StopJobPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("rollup/apis/stop-job.asciidoc:76")]
		public void Line76()
		{
			// tag::07a5fdeb7805cec1d28ba288b28f5ff5[]
			var response0 = new SearchResponse<object>();
			// end::07a5fdeb7805cec1d28ba288b28f5ff5[]

			response0.MatchesExample(@"POST _rollup/job/sensor/_stop?wait_for_completion=true&timeout=10s");
		}
	}
}
