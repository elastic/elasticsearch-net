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
	public class RefreshPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/refresh.asciidoc:11")]
		public void Line11()
		{
			// tag::c2ac42934e4b76197032b2fc429e317d[]
			var response0 = new SearchResponse<object>();
			// end::c2ac42934e4b76197032b2fc429e317d[]

			response0.MatchesExample(@"POST /twitter/_refresh");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/refresh.asciidoc:95")]
		public void Line95()
		{
			// tag::0e98949e80e665795bc6cfc997165241[]
			var response0 = new SearchResponse<object>();
			// end::0e98949e80e665795bc6cfc997165241[]

			response0.MatchesExample(@"POST /kimchy,elasticsearch/_refresh");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/refresh.asciidoc:105")]
		public void Line105()
		{
			// tag::d7898526d239d2aea83727fb982f8f77[]
			var response0 = new SearchResponse<object>();
			// end::d7898526d239d2aea83727fb982f8f77[]

			response0.MatchesExample(@"POST /_refresh");
		}
	}
}
