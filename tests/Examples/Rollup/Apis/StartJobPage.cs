// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Rollup.Apis
{
	public class StartJobPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("rollup/apis/start-job.asciidoc:51")]
		public void Line51()
		{
			// tag::618c9d42284c067891fb57034a4fd834[]
			var response0 = new SearchResponse<object>();
			// end::618c9d42284c067891fb57034a4fd834[]

			response0.MatchesExample(@"POST _rollup/job/sensor/_start");
		}
	}
}
