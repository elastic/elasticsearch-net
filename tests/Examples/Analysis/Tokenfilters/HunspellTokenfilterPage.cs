// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenfilters
{
	public class HunspellTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/hunspell-tokenfilter.asciidoc:47")]
		public void Line47()
		{
			// tag::0af002734dd884f9385da6c3a4ca87a1[]
			var response0 = new SearchResponse<object>();
			// end::0af002734dd884f9385da6c3a4ca87a1[]

			response0.MatchesExample(@"PUT /hunspell_example
			{
			    ""settings"": {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""en"" : {
			                    ""tokenizer"" : ""standard"",
			                    ""filter"" : [ ""lowercase"", ""en_US"" ]
			                }
			            },
			            ""filter"" : {
			                ""en_US"" : {
			                    ""type"" : ""hunspell"",
			                    ""locale"" : ""en_US"",
			                    ""dedup"" : true
			                }
			            }
			        }
			    }
			}");
		}
	}
}
