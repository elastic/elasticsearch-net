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
