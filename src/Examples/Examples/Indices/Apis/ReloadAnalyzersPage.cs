using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices.Apis
{
	public class ReloadAnalyzersPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line15()
		{
			// tag::fd25ae98c5c8be66fdd5e6ef32815ff5[]
			var response0 = new SearchResponse<object>();
			// end::fd25ae98c5c8be66fdd5e6ef32815ff5[]

			response0.MatchesExample(@"PUT /my_index
			{
			    ""settings"": {
			        ""index"" : {
			            ""analysis"" : {
			                ""analyzer"" : {
			                    ""my_synonyms"" : {
			                        ""tokenizer"" : ""whitespace"",
			                        ""filter"" : [""synonym""]
			                    }
			                },
			                ""filter"" : {
			                    ""synonym"" : {
			                        ""type"" : ""synonym"",
			                        ""synonyms_path"" : ""analysis/synonym.txt"",
			                        ""updateable"" : true \<1>
			                    }
			                }
			            }
			        }
			    },
			    ""mappings"": {
			        ""properties"": {
			            ""text"": {
			                ""type"": ""text"",
			                ""analyzer"" : ""standard"",
			                ""search_analyzer"": ""my_synonyms"" \<2>
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line68()
		{
			// tag::7554da505cc27f6bd0d028b66e85f4a5[]
			var response0 = new SearchResponse<object>();
			// end::7554da505cc27f6bd0d028b66e85f4a5[]

			response0.MatchesExample(@"POST /my_index/_reload_search_analyzers");
		}
	}
}