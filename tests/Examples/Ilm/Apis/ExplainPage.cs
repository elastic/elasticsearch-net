// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ilm.Apis
{
	public class ExplainPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ilm/apis/explain.asciidoc:103")]
		public void Line103()
		{
			// tag::0f6fa3a706a7c17858d3dbe329839ea6[]
			var response0 = new SearchResponse<object>();
			// end::0f6fa3a706a7c17858d3dbe329839ea6[]

			response0.MatchesExample(@"GET my_index/_ilm/explain");
		}
	}
}
