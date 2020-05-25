// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Cat
{
	public class TasksPage : ExampleBase
	{

		[U(Skip = "Example not implemented")]
		[Description("cat/tasks.asciidoc:57")]
		public void Line57()
		{
			// tag::f3422381d36398fcb2612692b11b1e96[]
			var response0 = new SearchResponse<object>();
			// end::f3422381d36398fcb2612692b11b1e96[]

			response0.MatchesExample(@"GET _cat/tasks?v");
		}
	}
}
