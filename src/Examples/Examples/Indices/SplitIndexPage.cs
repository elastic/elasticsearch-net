using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class SplitIndexPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line89()
		{
			// tag::76bcb71590ce6acebc8427c4ebcf9521[]
			var response0 = new SearchResponse<object>();
			// end::76bcb71590ce6acebc8427c4ebcf9521[]

			response0.MatchesExample(@"PUT my_source_index
			{
			  ""settings"": {
			    ""index.number_of_shards"" : 1
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line105()
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
		public void Line126()
		{
			// tag::290a366536875db313d1cbbed61cb9b6[]
			var response0 = new SearchResponse<object>();
			// end::290a366536875db313d1cbbed61cb9b6[]

			response0.MatchesExample(@"POST my_source_index/_split/my_target_index
			{
			  ""settings"": {
			    ""index.number_of_shards"": 2
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line161()
		{
			// tag::d1a84808a9bca68c9bd7ede0a55a5a9f[]
			var response0 = new SearchResponse<object>();
			// end::d1a84808a9bca68c9bd7ede0a55a5a9f[]

			response0.MatchesExample(@"POST my_source_index/_split/my_target_index
			{
			  ""settings"": {
			    ""index.number_of_shards"": 5 \<1>
			  },
			  ""aliases"": {
			    ""my_search_indices"": {}
			  }
			}");
		}
	}
}