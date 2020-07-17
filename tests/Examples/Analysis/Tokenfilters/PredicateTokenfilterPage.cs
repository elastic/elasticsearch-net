// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenfilters
{
	public class PredicateTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/predicate-tokenfilter.asciidoc:20")]
		public void Line20()
		{
			// tag::a159143bb578403bb9c7ff37d635d7ad[]
			var response0 = new SearchResponse<object>();
			// end::a159143bb578403bb9c7ff37d635d7ad[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"": ""whitespace"",
			  ""filter"": [
			    {
			      ""type"": ""predicate_token_filter"",
			      ""script"": {
			        ""source"": """"""
			          token.term.length() > 3
			        """"""
			      }
			    }
			  ],
			  ""text"": ""the fox jumps the lazy dog""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/predicate-tokenfilter.asciidoc:102")]
		public void Line102()
		{
			// tag::a2861e628545fd2b8ee2c747b19ac628[]
			var response0 = new SearchResponse<object>();
			// end::a2861e628545fd2b8ee2c747b19ac628[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_analyzer"": {
			          ""tokenizer"": ""standard"",
			          ""filter"": [
			            ""my_script_filter""
			          ]
			        }
			      },
			      ""filter"": {
			        ""my_script_filter"": {
			          ""type"": ""predicate_token_filter"",
			          ""script"": {
			            ""source"": """"""
			              token.type.contains(""ALPHANUM"")
			            """"""
			          }
			        }
			      }
			    }
			  }
			}");
		}
	}
}
