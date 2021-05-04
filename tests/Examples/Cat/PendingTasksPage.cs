// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Cat
{
	public class PendingTasksPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cat/pending_tasks.asciidoc:39")]
		public void Line39()
		{
			// tag::dc2e9e499c7037eb9327cc84a942c5e9[]
			var response0 = new SearchResponse<object>();
			// end::dc2e9e499c7037eb9327cc84a942c5e9[]

			response0.MatchesExample(@"GET /_cat/pending_tasks?v");
		}
	}
}
