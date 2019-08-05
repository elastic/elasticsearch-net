using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class CloneIndexPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line29()
		{
			// tag::f95b3b415480b2fb4d90e5e576f74c90[]
			var response0 = new SearchResponse<object>();
			// end::f95b3b415480b2fb4d90e5e576f74c90[]

			response0.MatchesExample(@"PUT my_source_index
			{
			  ""settings"": {
			    ""index.number_of_shards"" : 5
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line45()
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

		[U]
		[SkipExample]
		public void Line66()
		{
			// tag::f17b925eace96b699996ad20ae7dd3e2[]
			var response0 = new SearchResponse<object>();
			// end::f17b925eace96b699996ad20ae7dd3e2[]

			response0.MatchesExample(@"POST my_source_index/_clone/my_target_index");
		}

		[U]
		[SkipExample]
		public void Line93()
		{
			// tag::e405fe0c10af890c997d6be8d51aa940[]
			var response0 = new SearchResponse<object>();
			// end::e405fe0c10af890c997d6be8d51aa940[]

			response0.MatchesExample(@"POST my_source_index/_clone/my_target_index
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