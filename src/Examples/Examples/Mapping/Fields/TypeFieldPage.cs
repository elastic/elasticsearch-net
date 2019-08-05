using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Fields
{
	public class TypeFieldPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line14()
		{
			// tag::1e867a2d4e10e70350d458a473544022[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::1e867a2d4e10e70350d458a473544022[]

			response0.MatchesExample(@"# Example documents");

			response1.MatchesExample(@"PUT my_index/_doc/1?refresh=true
			{
			  ""text"": ""Document with type 'doc'""
			}");

			response2.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""term"": {
			      ""_type"": ""_doc""  \<1>
			    }
			  },
			  ""aggs"": {
			    ""types"": {
			      ""terms"": {
			        ""field"": ""_type"", \<2>
			        ""size"": 10
			      }
			    }
			  },
			  ""sort"": [
			    {
			      ""_type"": { \<3>
			        ""order"": ""desc""
			      }
			    }
			  ],
			  ""script_fields"": {
			    ""type"": {
			      ""script"": {
			        ""lang"": ""painless"",
			        ""source"": ""doc['_type']"" \<4>
			      }
			    }
			  }
			}");

			response3.MatchesExample(@"");
		}
	}
}