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

namespace Examples.Indices
{
	public class ClosePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/close.asciidoc:10")]
		public void Line10()
		{
			// tag::34107944bca50a003cda9fca934b2011[]
			var response0 = new SearchResponse<object>();
			// end::34107944bca50a003cda9fca934b2011[]

			response0.MatchesExample(@"POST /twitter/_close");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/close.asciidoc:66")]
		public void Line66()
		{
			// tag::3a6b9143f3de6258d44ff7e0eb38d953[]
			var response0 = new SearchResponse<object>();
			// end::3a6b9143f3de6258d44ff7e0eb38d953[]

			response0.MatchesExample(@"POST /my_index/_close");
		}
	}
}
