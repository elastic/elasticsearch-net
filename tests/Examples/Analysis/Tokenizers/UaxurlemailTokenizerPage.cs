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
	public class UaxurlemailTokenizerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/uaxurlemail-tokenizer.asciidoc:14")]
		public void Line14()
		{
			// tag::d12df43ffcdcd937bae9b26fb475e239[]
			var response0 = new SearchResponse<object>();
			// end::d12df43ffcdcd937bae9b26fb475e239[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""tokenizer"": ""uax_url_email"",
			  ""text"": ""Email me at john.smith@global-international.com""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/uaxurlemail-tokenizer.asciidoc:95")]
		public void Line95()
		{
			// tag::1125986e8e55028ff4c10b5e6c7bbebb[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::1125986e8e55028ff4c10b5e6c7bbebb[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_analyzer"": {
			          ""tokenizer"": ""my_tokenizer""
			        }
			      },
			      ""tokenizer"": {
			        ""my_tokenizer"": {
			          ""type"": ""uax_url_email"",
			          ""max_token_length"": 5
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"POST my_index/_analyze
			{
			  ""analyzer"": ""my_analyzer"",
			  ""text"": ""john.smith@global-international.com""
			}");
		}
	}
}
