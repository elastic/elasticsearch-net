// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenfilters
{
	public class StemmerTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/stemmer-tokenfilter.asciidoc:17")]
		public void Line17()
		{
			// tag::1ca618e7d72ec73c1064fa6eae3086d1[]
			var response0 = new SearchResponse<object>();
			// end::1ca618e7d72ec73c1064fa6eae3086d1[]

			response0.MatchesExample(@"PUT /my_index
			{
			    ""settings"": {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""my_analyzer"" : {
			                    ""tokenizer"" : ""standard"",
			                    ""filter"" : [""lowercase"", ""my_stemmer""]
			                }
			            },
			            ""filter"" : {
			                ""my_stemmer"" : {
			                    ""type"" : ""stemmer"",
			                    ""name"" : ""light_german""
			                }
			            }
			        }
			    }
			}");
		}
	}
}
