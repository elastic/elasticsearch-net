// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Rollup.Apis
{
	public class DeleteJobPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("rollup/apis/delete-job.asciidoc:80")]
		public void Line80()
		{
			// tag::94246f45025ed394cd6415ed8d7a0588[]
			var response0 = new SearchResponse<object>();
			// end::94246f45025ed394cd6415ed8d7a0588[]

			response0.MatchesExample(@"DELETE _rollup/job/sensor");
		}
	}
}
