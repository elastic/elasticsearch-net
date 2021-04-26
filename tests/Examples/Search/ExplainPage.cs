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

namespace Examples.Search
{
	public class ExplainPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/explain.asciidoc:8")]
		public void Line8()
		{
			// tag::abfec22fbe7d571711cc65661ca887ee[]
			var response0 = new SearchResponse<object>();
			// end::abfec22fbe7d571711cc65661ca887ee[]

			response0.MatchesExample(@"GET /twitter/_explain/0
			{
			      ""query"" : {
			        ""match"" : { ""message"" : ""elasticsearch"" }
			      }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/explain.asciidoc:182")]
		public void Line182()
		{
			// tag::5032518611d928d1f802e215cf79c550[]
			var response0 = new SearchResponse<object>();
			// end::5032518611d928d1f802e215cf79c550[]

			response0.MatchesExample(@"GET /twitter/_explain/0?q=message:search");
		}
	}
}
