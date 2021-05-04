// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenizers
{
	public class LowercaseTokenizerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/lowercase-tokenizer.asciidoc:20")]
		public void Line20()
		{
			// tag::a99bc141066ef673e35f306157750ec9[]
			var response0 = new SearchResponse<object>();
			// end::a99bc141066ef673e35f306157750ec9[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""tokenizer"": ""lowercase"",
			  ""text"": ""The 2 QUICK Brown-Foxes jumped over the lazy dog's bone.""
			}");
		}
	}
}
