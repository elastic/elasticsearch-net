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

namespace Examples.Licensing
{
	public class StartBasicPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("licensing/start-basic.asciidoc:43")]
		public void Line43()
		{
			// tag::8699d35269a47ba867fa8cc766287413[]
			var response0 = new SearchResponse<object>();
			// end::8699d35269a47ba867fa8cc766287413[]

			response0.MatchesExample(@"POST /_license/start_basic");
		}

		[U(Skip = "Example not implemented")]
		[Description("licensing/start-basic.asciidoc:63")]
		public void Line63()
		{
			// tag::f58fd031597e2c3df78bf0efd07206e3[]
			var response0 = new SearchResponse<object>();
			// end::f58fd031597e2c3df78bf0efd07206e3[]

			response0.MatchesExample(@"POST /_license/start_basic?acknowledge=true");
		}
	}
}
