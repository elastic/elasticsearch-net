using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis
{
	public class NormalizersPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line25()
		{
			// tag::966ff3a4c5b61ed1a36d44c17ce06157[]
			var response0 = new SearchResponse<object>();
			// end::966ff3a4c5b61ed1a36d44c17ce06157[]

			response0.MatchesExample(@"PUT index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""char_filter"": {
			        ""quote"": {
			          ""type"": ""mapping"",
			          ""mappings"": [
			            ""« => \"""",
			            ""» => \""""
			          ]
			        }
			      },
			      ""normalizer"": {
			        ""my_normalizer"": {
			          ""type"": ""custom"",
			          ""char_filter"": [""quote""],
			          ""filter"": [""lowercase"", ""asciifolding""]
			        }
			      }
			    }
			  },
			  ""mappings"": {
			    ""properties"": {
			      ""foo"": {
			        ""type"": ""keyword"",
			        ""normalizer"": ""my_normalizer""
			      }
			    }
			  }
			}");
		}
	}
}