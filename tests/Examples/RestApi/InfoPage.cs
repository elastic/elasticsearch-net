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

namespace Examples.RestApi
{
	public class InfoPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("rest-api/info.asciidoc:45")]
		public void Line45()
		{
			// tag::9054187cbab5c9e1c4ca2a4dba6a5db0[]
			var response0 = new SearchResponse<object>();
			// end::9054187cbab5c9e1c4ca2a4dba6a5db0[]

			response0.MatchesExample(@"GET /_xpack");
		}

		[U(Skip = "Example not implemented")]
		[Description("rest-api/info.asciidoc:167")]
		public void Line167()
		{
			// tag::b11a0675e49df0709be693297ca73a2c[]
			var response0 = new SearchResponse<object>();
			// end::b11a0675e49df0709be693297ca73a2c[]

			response0.MatchesExample(@"GET /_xpack?categories=build,features");
		}

		[U(Skip = "Example not implemented")]
		[Description("rest-api/info.asciidoc:174")]
		public void Line174()
		{
			// tag::4ed946065faa92f9950f04e402676a97[]
			var response0 = new SearchResponse<object>();
			// end::4ed946065faa92f9950f04e402676a97[]

			response0.MatchesExample(@"GET /_xpack?human=false");
		}
	}
}
