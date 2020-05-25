// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenfilters
{
	public class LengthTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/length-tokenfilter.asciidoc:27")]
		public void Line27()
		{
			// tag::1659420311d907d9fc024b96f4150216[]
			var response0 = new SearchResponse<object>();
			// end::1659420311d907d9fc024b96f4150216[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"": ""whitespace"",
			  ""filter"": [
			    {
			      ""type"": ""length"",
			      ""min"": 0,
			      ""max"": 4
			    }
			  ],
			  ""text"": ""the quick brown fox jumps over the lazy dog""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/length-tokenfilter.asciidoc:109")]
		public void Line109()
		{
			// tag::ea690283f301c6ce957efad93d7d5c5d[]
			var response0 = new SearchResponse<object>();
			// end::ea690283f301c6ce957efad93d7d5c5d[]

			response0.MatchesExample(@"PUT length_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""standard_length"": {
			          ""tokenizer"": ""standard"",
			          ""filter"": [ ""length"" ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/length-tokenfilter.asciidoc:149")]
		public void Line149()
		{
			// tag::d88f883ed2fb8be35cd3e72ddffcf4ef[]
			var response0 = new SearchResponse<object>();
			// end::d88f883ed2fb8be35cd3e72ddffcf4ef[]

			response0.MatchesExample(@"PUT length_custom_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""whitespace_length_2_to_10_char"": {
			          ""tokenizer"": ""whitespace"",
			          ""filter"": [ ""length_2_to_10_char"" ]
			        }
			      },
			      ""filter"": {
			        ""length_2_to_10_char"": {
			          ""type"": ""length"",
			          ""min"": 2,
			          ""max"": 10
			        }
			      }
			    }
			  }
			}");
		}
	}
}
