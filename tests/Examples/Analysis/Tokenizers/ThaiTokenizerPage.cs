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
	public class ThaiTokenizerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/thai-tokenizer.asciidoc:20")]
		public void Line20()
		{
			// tag::a1e5f3956f9a697e79478fc9a6e30e1f[]
			var response0 = new SearchResponse<object>();
			// end::a1e5f3956f9a697e79478fc9a6e30e1f[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""tokenizer"": ""thai"",
			  ""text"": ""การที่ได้ต้องแสดงว่างานดี""
			}");
		}
	}
}
