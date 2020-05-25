// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenfilters
{
	public class ClassicTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/classic-tokenfilter.asciidoc:21")]
		public void Line21()
		{
			// tag::c8bbf362f06a0d8dab33ec0d99743343[]
			var response0 = new SearchResponse<object>();
			// end::c8bbf362f06a0d8dab33ec0d99743343[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"" : ""classic"",
			  ""filter"" : [""classic""],
			  ""text"" : ""The 2 Q.U.I.C.K. Brown-Foxes jumped over the lazy dog's bone.""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/classic-tokenfilter.asciidoc:132")]
		public void Line132()
		{
			// tag::50952b8040f875ce3719c71ca1c3bc8f[]
			var response0 = new SearchResponse<object>();
			// end::50952b8040f875ce3719c71ca1c3bc8f[]

			response0.MatchesExample(@"PUT /classic_example
			{
			    ""settings"" : {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""classic_analyzer"" : {
			                    ""tokenizer"" : ""classic"",
			                    ""filter"" : [""classic""]
			                }
			            }
			        }
			    }
			}");
		}
	}
}
