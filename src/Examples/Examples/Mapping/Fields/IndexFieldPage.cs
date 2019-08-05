using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Fields
{
	public class IndexFieldPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line17()
		{
			// tag::e8146b1dda248705f7fb1fb6306d9d86[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::e8146b1dda248705f7fb1fb6306d9d86[]

			response0.MatchesExample(@"# Example documents");

			response1.MatchesExample(@"PUT index_1/_doc/1
			{
			  ""text"": ""Document in index 1""
			}");

			response2.MatchesExample(@"PUT index_2/_doc/2?refresh=true
			{
			  ""text"": ""Document in index 2""
			}");

			response3.MatchesExample(@"GET index_1,index_2/_search
			{
			  ""query"": {
			    ""terms"": {
			      ""_index"": [""index_1"", ""index_2""] \<1>
			    }
			  },
			  ""aggs"": {
			    ""indices"": {
			      ""terms"": {
			        ""field"": ""_index"", \<2>
			        ""size"": 10
			      }
			    }
			  },
			  ""sort"": [
			    {
			      ""_index"": { \<3>
			        ""order"": ""asc""
			      }
			    }
			  ],
			  ""script_fields"": {
			    ""index_name"": {
			      ""script"": {
			        ""lang"": ""painless"",
			        ""source"": ""doc['_index']"" \<4>
			      }
			    }
			  }
			}");
		}
	}
}