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
	public class SimplepatternTokenizerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/simplepattern-tokenizer.asciidoc:39")]
		public void Line39()
		{
			// tag::9ffc049d5c5a570b90d913e92f910ee4[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::9ffc049d5c5a570b90d913e92f910ee4[]

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
			          ""type"": ""simple_pattern"",
			          ""pattern"": ""[0123456789]{3}""
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"POST my_index/_analyze
			{
			  ""analyzer"": ""my_analyzer"",
			  ""text"": ""fd-786-335-514-x""
			}");
		}
	}
}
