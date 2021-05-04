// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenizers
{
	public class ChargroupTokenizerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/chargroup-tokenizer.asciidoc:33")]
		public void Line33()
		{
			// tag::f8cafb1a08bc9b2dd5239f99d4e93f4c[]
			var response0 = new SearchResponse<object>();
			// end::f8cafb1a08bc9b2dd5239f99d4e93f4c[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""tokenizer"": {
			    ""type"": ""char_group"",
			    ""tokenize_on_chars"": [
			      ""whitespace"",
			      ""-"",
			      ""\n""
			    ]
			  },
			  ""text"": ""The QUICK brown-fox""
			}");
		}
	}
}
