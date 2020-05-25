// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenfilters
{
	public class SnowballTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/snowball-tokenfilter.asciidoc:17")]
		public void Line17()
		{
			// tag::e776311ef67c972f322b669dc4ab9926[]
			var response0 = new SearchResponse<object>();
			// end::e776311ef67c972f322b669dc4ab9926[]

			response0.MatchesExample(@"PUT /my_index
			{
			    ""settings"": {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""my_analyzer"" : {
			                    ""tokenizer"" : ""standard"",
			                    ""filter"" : [""lowercase"", ""my_snow""]
			                }
			            },
			            ""filter"" : {
			                ""my_snow"" : {
			                    ""type"" : ""snowball"",
			                    ""language"" : ""Lovins""
			                }
			            }
			        }
			    }
			}");
		}
	}
}
