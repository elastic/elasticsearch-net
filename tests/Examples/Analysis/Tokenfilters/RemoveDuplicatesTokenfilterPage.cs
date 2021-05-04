// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class RemoveDuplicatesTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/remove-duplicates-tokenfilter.asciidoc:24")]
		public void Line24()
		{
			// tag::15d948d593d2624ac5e2b155052048f0[]
			var response0 = new SearchResponse<object>();
			// end::15d948d593d2624ac5e2b155052048f0[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"": ""whitespace"",
			  ""filter"": [
			    ""keyword_repeat"",
			    ""stemmer""
			  ],
			  ""text"": ""jumping dog""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/remove-duplicates-tokenfilter.asciidoc:79")]
		public void Line79()
		{
			// tag::bab4c3b22c1768fcc7153345e4096dfb[]
			var response0 = new SearchResponse<object>();
			// end::bab4c3b22c1768fcc7153345e4096dfb[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"": ""whitespace"",
			  ""filter"": [
			    ""keyword_repeat"",
			    ""stemmer"",
			    ""remove_duplicates""
			  ],
			  ""text"": ""jumping dog""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/remove-duplicates-tokenfilter.asciidoc:136")]
		public void Line136()
		{
			// tag::198d39435b00b938cc2fa8f98c92e135[]
			var response0 = new SearchResponse<object>();
			// end::198d39435b00b938cc2fa8f98c92e135[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_custom_analyzer"": {
			          ""tokenizer"": ""standard"",
			          ""filter"": [
			            ""keyword_repeat"",
			            ""stemmer"",
			            ""remove_duplicates""
			          ]
			        }
			      }
			    }
			  }
			}");
		}
	}
}
