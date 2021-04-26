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
using System.ComponentModel;
using Nest;

namespace Examples.Search
{
	public class ScrollApiPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/scroll-api.asciidoc:25")]
		public void Line25()
		{
			// tag::9ee6b38bc04ee4e73339b986ee09c30a[]
			var response0 = new SearchResponse<object>();
			// end::9ee6b38bc04ee4e73339b986ee09c30a[]

			response0.MatchesExample(@"GET /_search/scroll
			{
			    ""scroll_id"" : ""DXF1ZXJ5QW5kRmV0Y2gBAAAAAAAAAD4WYm9laVYtZndUQlNsdDcwakFMNjU1QQ==""
			}");
		}
	}
}