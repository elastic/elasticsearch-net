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

namespace Examples.Docs
{
	public class RefreshPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("docs/refresh.asciidoc:87")]
		public void Line87()
		{
			// tag::92d343eb755971c44a939d0660bf5ac2[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::92d343eb755971c44a939d0660bf5ac2[]

			response0.MatchesExample(@"PUT /test/_doc/1?refresh
			{""test"": ""test""}");

			response1.MatchesExample(@"PUT /test/_doc/2?refresh=true
			{""test"": ""test""}");
		}

		[U(Skip = "Example not implemented")]
		[Description("docs/refresh.asciidoc:98")]
		public void Line98()
		{
			// tag::1070e59ba144cdf309fd9b2591612b95[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::1070e59ba144cdf309fd9b2591612b95[]

			response0.MatchesExample(@"PUT /test/_doc/3
			{""test"": ""test""}");

			response1.MatchesExample(@"PUT /test/_doc/4?refresh=false
			{""test"": ""test""}");
		}

		[U(Skip = "Example not implemented")]
		[Description("docs/refresh.asciidoc:108")]
		public void Line108()
		{
			// tag::e4b2b5e0aaedf3cbbcde3d61eb1f13fc[]
			var response0 = new SearchResponse<object>();
			// end::e4b2b5e0aaedf3cbbcde3d61eb1f13fc[]

			response0.MatchesExample(@"PUT /test/_doc/4?refresh=wait_for
			{""test"": ""test""}");
		}
	}
}
