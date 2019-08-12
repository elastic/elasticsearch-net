using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class ShrinkIndexPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line38()
		{
			// tag::5e93f806cfd459149222b443b7992a51[]
			var response0 = new SearchResponse<object>();
			// end::5e93f806cfd459149222b443b7992a51[]

			response0.MatchesExample(@"PUT /my_source_index/_settings
			{
			  ""settings"": {
			    ""index.routing.allocation.require._name"": ""shrink_node_name"", \<1>
			    ""index.blocks.write"": true \<2>
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line67()
		{
			// tag::0c44dc55c06e882de2947b5e9fa78acc[]
			var response0 = new SearchResponse<object>();
			// end::0c44dc55c06e882de2947b5e9fa78acc[]

			response0.MatchesExample(@"POST my_source_index/_shrink/my_target_index
			{
			  ""settings"": {
			    ""index.routing.allocation.require._name"": null, \<1>
			    ""index.blocks.write"": null \<2>
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line111()
		{
			// tag::324a946abc2c86b5a71dd5cec6c765b3[]
			var response0 = new SearchResponse<object>();
			// end::324a946abc2c86b5a71dd5cec6c765b3[]

			response0.MatchesExample(@"POST my_source_index/_shrink/my_target_index
			{
			  ""settings"": {
			    ""index.number_of_replicas"": 1,
			    ""index.number_of_shards"": 1, \<1>
			    ""index.codec"": ""best_compression"" \<2>
			  },
			  ""aliases"": {
			    ""my_search_indices"": {}
			  }
			}");
		}
	}
}