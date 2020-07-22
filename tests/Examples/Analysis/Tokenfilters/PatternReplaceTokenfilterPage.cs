using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class PatternReplaceTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/pattern_replace-tokenfilter.asciidoc:39")]
		public void Line39()
		{
			// tag::2ec8d757188349a4630e120ba2c98c3b[]
			var response0 = new SearchResponse<object>();
			// end::2ec8d757188349a4630e120ba2c98c3b[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"": ""whitespace"",
			  ""filter"": [
			    {
			      ""type"": ""pattern_replace"",
			      ""pattern"": ""(dog)"",
			      ""replacement"": ""watch$1""
			    }
			  ],
			  ""text"": ""foxes jump lazy dogs""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/pattern_replace-tokenfilter.asciidoc:136")]
		public void Line136()
		{
			// tag::194b56d9f00c05352bc571d1e371a4d7[]
			var response0 = new SearchResponse<object>();
			// end::194b56d9f00c05352bc571d1e371a4d7[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_analyzer"": {
			          ""tokenizer"": ""keyword"",
			          ""filter"": [
			            ""my_pattern_replace_filter""
			          ]
			        }
			      },
			      ""filter"": {
			        ""my_pattern_replace_filter"": {
			          ""type"": ""pattern_replace"",
			          ""pattern"": ""[£|€]"",
			          ""replacement"": """",
			          ""all"": false
			        }
			      }
			    }
			  }
			}");
		}
	}
}