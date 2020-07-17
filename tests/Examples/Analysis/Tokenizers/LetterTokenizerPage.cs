// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenizers
{
	public class LetterTokenizerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/letter-tokenizer.asciidoc:16")]
		public void Line16()
		{
			// tag::76448aaaaa2c352bb6e09d2f83a3fbb3[]
			var response0 = new SearchResponse<object>();
			// end::76448aaaaa2c352bb6e09d2f83a3fbb3[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""tokenizer"": ""letter"",
			  ""text"": ""The 2 QUICK Brown-Foxes jumped over the lazy dog's bone.""
			}");
		}
	}
}
