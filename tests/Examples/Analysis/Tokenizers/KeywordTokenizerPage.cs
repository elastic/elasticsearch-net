// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenizers
{
	public class KeywordTokenizerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/keyword-tokenizer.asciidoc:12")]
		public void Line12()
		{
			// tag::09a44b619a99f6bf3f01bd5e258fd22d[]
			var response0 = new SearchResponse<object>();
			// end::09a44b619a99f6bf3f01bd5e258fd22d[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""tokenizer"": ""keyword"",
			  ""text"": ""New York""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/keyword-tokenizer.asciidoc:58")]
		public void Line58()
		{
			// tag::c95d5317525c2ff625e6971c277247af[]
			var response0 = new SearchResponse<object>();
			// end::c95d5317525c2ff625e6971c277247af[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""tokenizer"": ""keyword"",
			  ""filter"": [ ""lowercase"" ],
			  ""text"": ""john.SMITH@example.COM""
			}");
		}
	}
}
