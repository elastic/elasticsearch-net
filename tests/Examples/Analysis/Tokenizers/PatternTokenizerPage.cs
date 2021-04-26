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
	public class PatternTokenizerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/pattern-tokenizer.asciidoc:32")]
		public void Line32()
		{
			// tag::1a6dbe5df488c4a16e2f1101ba8a25d9[]
			var response0 = new SearchResponse<object>();
			// end::1a6dbe5df488c4a16e2f1101ba8a25d9[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""tokenizer"": ""pattern"",
			  ""text"": ""The foo_bar_size's default is 5.""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/pattern-tokenizer.asciidoc:128")]
		public void Line128()
		{
			// tag::448339a39d847c4cac57a325e23c2a5a[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::448339a39d847c4cac57a325e23c2a5a[]

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
			          ""type"": ""pattern"",
			          ""pattern"": "",""
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"POST my_index/_analyze
			{
			  ""analyzer"": ""my_analyzer"",
			  ""text"": ""comma,separated,values""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/pattern-tokenizer.asciidoc:216")]
		public void Line216()
		{
			// tag::fa8d64d622b4d7fe3234924b4de4f0bf[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::fa8d64d622b4d7fe3234924b4de4f0bf[]

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
			          ""type"": ""pattern"",
			          ""pattern"": ""\""((?:\\\\\""|[^\""]|\\\\\"")+)\"""",
			          ""group"": 1
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"POST my_index/_analyze
			{
			  ""analyzer"": ""my_analyzer"",
			  ""text"": ""\""value\"", \""value with embedded \\\"" quote\""""
			}");
		}
	}
}
