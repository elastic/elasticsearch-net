// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Indices
{
	public class CloneIndexPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/clone-index.asciidoc:10")]
		public void Line10()
		{
			// tag::085df89b76dd980934d1dc2e59f5db1f[]
			var response0 = new SearchResponse<object>();
			// end::085df89b76dd980934d1dc2e59f5db1f[]

			response0.MatchesExample(@"POST /twitter/_clone/cloned-twitter-index");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/clone-index.asciidoc:37")]
		public void Line37()
		{
			// tag::1c5a5a8c0bdc3c577f560157bd4e2313[]
			var response0 = new SearchResponse<object>();
			// end::1c5a5a8c0bdc3c577f560157bd4e2313[]

			response0.MatchesExample(@"PUT /my_source_index/_settings
			{
			  ""settings"": {
			    ""index.blocks.write"": true
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/clone-index.asciidoc:82")]
		public void Line82()
		{
			// tag::190a21e32db2125ddaea0f634e126a84[]
			var response0 = new SearchResponse<object>();
			// end::190a21e32db2125ddaea0f634e126a84[]

			response0.MatchesExample(@"POST /my_source_index/_clone/my_target_index");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/clone-index.asciidoc:108")]
		public void Line108()
		{
			// tag::81612c2537386e031b7eb604f6756a71[]
			var response0 = new SearchResponse<object>();
			// end::81612c2537386e031b7eb604f6756a71[]

			response0.MatchesExample(@"POST /my_source_index/_clone/my_target_index
			{
			  ""settings"": {
			    ""index.number_of_shards"": 5 <1>
			  },
			  ""aliases"": {
			    ""my_search_indices"": {}
			  }
			}");
		}
	}
}
