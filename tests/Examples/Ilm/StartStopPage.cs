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

namespace Examples.Ilm
{
	public class StartStopPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ilm/start-stop.asciidoc:59")]
		public void Line59()
		{
			// tag::182df084f028479ecbe8d7648ddad892[]
			var response0 = new SearchResponse<object>();
			// end::182df084f028479ecbe8d7648ddad892[]

			response0.MatchesExample(@"GET _ilm/status");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/start-stop.asciidoc:82")]
		public void Line82()
		{
			// tag::585a34ad79aee16678b37da785933ac8[]
			var response0 = new SearchResponse<object>();
			// end::585a34ad79aee16678b37da785933ac8[]

			response0.MatchesExample(@"POST _ilm/stop");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/start-stop.asciidoc:135")]
		public void Line135()
		{
			// tag::72ae3851160fcf02b8e2cdfd4e57d238[]
			var response0 = new SearchResponse<object>();
			// end::72ae3851160fcf02b8e2cdfd4e57d238[]

			response0.MatchesExample(@"POST _ilm/start");
		}
	}
}