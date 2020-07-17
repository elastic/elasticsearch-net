// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenizers
{
	public class WhitespaceTokenizerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/whitespace-tokenizer.asciidoc:14")]
		public void Line14()
		{
			// tag::7b9dfe5857bde1bd8483ea3241656714[]
			var response0 = new SearchResponse<object>();
			// end::7b9dfe5857bde1bd8483ea3241656714[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""tokenizer"": ""whitespace"",
			  ""text"": ""The 2 QUICK Brown-Foxes jumped over the lazy dog's bone.""
			}");
		}
	}
}
