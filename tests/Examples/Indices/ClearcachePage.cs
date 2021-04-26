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
	public class ClearcachePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/clearcache.asciidoc:11")]
		public void Line11()
		{
			// tag::486eee2c8e75520f825fec08c1fbd67e[]
			var response0 = new SearchResponse<object>();
			// end::486eee2c8e75520f825fec08c1fbd67e[]

			response0.MatchesExample(@"POST /twitter/_cache/clear");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/clearcache.asciidoc:108")]
		public void Line108()
		{
			// tag::e97b14be2c4030bfc92e5d09a27e9fc9[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::e97b14be2c4030bfc92e5d09a27e9fc9[]

			response0.MatchesExample(@"POST /twitter/_cache/clear?fielddata=true  <1>");

			response1.MatchesExample(@"POST /twitter/_cache/clear?query=true      <2>");

			response2.MatchesExample(@"POST /twitter/_cache/clear?request=true    <3>");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/clearcache.asciidoc:128")]
		public void Line128()
		{
			// tag::62069c4118d79daf9612b29659b16627[]
			var response0 = new SearchResponse<object>();
			// end::62069c4118d79daf9612b29659b16627[]

			response0.MatchesExample(@"POST /twitter/_cache/clear?fields=foo,bar   \<1>");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/clearcache.asciidoc:140")]
		public void Line140()
		{
			// tag::2f43c5e976713a5c5cd8eb4b08cfffca[]
			var response0 = new SearchResponse<object>();
			// end::2f43c5e976713a5c5cd8eb4b08cfffca[]

			response0.MatchesExample(@"POST /kimchy,elasticsearch/_cache/clear");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/clearcache.asciidoc:150")]
		public void Line150()
		{
			// tag::c4a1d03dcfb82913d0724a42b0a89f20[]
			var response0 = new SearchResponse<object>();
			// end::c4a1d03dcfb82913d0724a42b0a89f20[]

			response0.MatchesExample(@"POST /_cache/clear");
		}
	}
}
