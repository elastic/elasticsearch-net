// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ilm.Apis
{
	public class DeleteLifecyclePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ilm/apis/delete-lifecycle.asciidoc:77")]
		public void Line77()
		{
			// tag::af517b6936fa41d124d68b107b2efdc3[]
			var response0 = new SearchResponse<object>();
			// end::af517b6936fa41d124d68b107b2efdc3[]

			response0.MatchesExample(@"DELETE _ilm/policy/my_policy");
		}
	}
}
