// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Indices
{
	public class SplitIndexPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/split-index.asciidoc:10")]
		public void Line10()
		{
			// tag::1a19b7db5485cd814e1f76f7cd7d2923[]
			var response0 = new SearchResponse<object>();
			// end::1a19b7db5485cd814e1f76f7cd7d2923[]

			response0.MatchesExample(@"POST /twitter/_split/split-twitter-index
			{
			  ""settings"": {
			    ""index.number_of_shards"": 2
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/split-index.asciidoc:42")]
		public void Line42()
		{
			// tag::01c0e302f4fd5118faf5e34f4a010ebf[]
			var response0 = new SearchResponse<object>();
			// end::01c0e302f4fd5118faf5e34f4a010ebf[]

			response0.MatchesExample(@"PUT /my_source_index/_settings
			{
			  ""settings"": {
			    ""index.blocks.write"": true \<1>
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/split-index.asciidoc:151")]
		public void Line151()
		{
			// tag::2e796e5ca59768d4426abbf9a049db3e[]
			var response0 = new SearchResponse<object>();
			// end::2e796e5ca59768d4426abbf9a049db3e[]

			response0.MatchesExample(@"POST /my_source_index/_split/my_target_index
			{
			  ""settings"": {
			    ""index.number_of_shards"": 2
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/split-index.asciidoc:185")]
		public void Line185()
		{
			// tag::f2f1cae094855a45fd8f73478bec8e70[]
			var response0 = new SearchResponse<object>();
			// end::f2f1cae094855a45fd8f73478bec8e70[]

			response0.MatchesExample(@"POST /my_source_index/_split/my_target_index
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
