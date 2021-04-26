/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenizers
{
	public class KeywordTokenizerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/keyword-tokenizer.asciidoc:15")]
		public void Line15()
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
		[Description("analysis/tokenizers/keyword-tokenizer.asciidoc:61")]
		public void Line61()
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
