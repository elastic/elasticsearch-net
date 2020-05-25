// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenfilters
{
	public class ConditionTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/condition-tokenfilter.asciidoc:22")]
		public void Line22()
		{
			// tag::09944369863fd8666d5301d717317276[]
			var response0 = new SearchResponse<object>();
			// end::09944369863fd8666d5301d717317276[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"": ""standard"",
			  ""filter"": [
			    {
			      ""type"": ""condition"",
			      ""filter"": [ ""lowercase"" ],
			      ""script"": {
			        ""source"": ""token.getTerm().length() < 5""
			      }
			    }
			  ],
			  ""text"": ""THE QUICK BROWN FOX""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/condition-tokenfilter.asciidoc:125")]
		public void Line125()
		{
			// tag::a197076e0e74951ea88f20309ec257e2[]
			var response0 = new SearchResponse<object>();
			// end::a197076e0e74951ea88f20309ec257e2[]

			response0.MatchesExample(@"PUT /palindrome_list
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""whitespace_reverse_first_token"": {
			          ""tokenizer"": ""whitespace"",
			          ""filter"": [ ""reverse_first_token"" ]
			        }
			      },
			      ""filter"": {
			        ""reverse_first_token"": {
			          ""type"": ""condition"",
			          ""filter"": [ ""reverse"" ],
			          ""script"": {
			            ""source"": ""token.getPosition() === 0""
			          }
			        }
			      }
			    }
			  }
			}");
		}
	}
}
