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
