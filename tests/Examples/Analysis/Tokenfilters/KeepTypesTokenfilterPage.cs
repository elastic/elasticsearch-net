// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenfilters
{
	public class KeepTypesTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/keep-types-tokenfilter.asciidoc:38")]
		public void Line38()
		{
			// tag::83cd4eb89818b4c32f654d370eafa920[]
			var response0 = new SearchResponse<object>();
			// end::83cd4eb89818b4c32f654d370eafa920[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"": ""standard"",
			  ""filter"": [
			    {
			      ""type"": ""keep_types"",
			      ""types"": [ ""<NUM>"" ]
			    }
			  ],
			  ""text"": ""1 quick fox 2 lazy dogs""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/keep-types-tokenfilter.asciidoc:91")]
		public void Line91()
		{
			// tag::d94f666616dea141dcb7aaf08a35bc10[]
			var response0 = new SearchResponse<object>();
			// end::d94f666616dea141dcb7aaf08a35bc10[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"": ""standard"",
			  ""filter"": [
			    {
			      ""type"": ""keep_types"",
			      ""types"": [ ""<NUM>"" ],
			      ""mode"": ""exclude""
			    }
			  ],
			  ""text"": ""1 quick fox 2 lazy dogs""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/keep-types-tokenfilter.asciidoc:182")]
		public void Line182()
		{
			// tag::13b02da42d3afe7f0b649e1c98ac9549[]
			var response0 = new SearchResponse<object>();
			// end::13b02da42d3afe7f0b649e1c98ac9549[]

			response0.MatchesExample(@"PUT keep_types_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_analyzer"": {
			          ""tokenizer"": ""standard"",
			          ""filter"": [ ""extract_alpha"" ]
			        }
			      },
			      ""filter"": {
			        ""extract_alpha"": {
			          ""type"": ""keep_types"",
			          ""types"": [ ""<ALPHANUM>"" ]
			        }
			      }
			    }
			  }
			}");
		}
	}
}
