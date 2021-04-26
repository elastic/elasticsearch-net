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

namespace Examples.Cat
{
	public class NodeattrsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cat/nodeattrs.asciidoc:69")]
		public void Line69()
		{
			// tag::e20e2e6f949ac660a77840a9263fadef[]
			var response0 = new SearchResponse<object>();
			// end::e20e2e6f949ac660a77840a9263fadef[]

			response0.MatchesExample(@"GET /_cat/nodeattrs?v");
		}

		[U(Skip = "Example not implemented")]
		[Description("cat/nodeattrs.asciidoc:100")]
		public void Line100()
		{
			// tag::0c69c638073cc8518187b678dd33443c[]
			var response0 = new SearchResponse<object>();
			// end::0c69c638073cc8518187b678dd33443c[]

			response0.MatchesExample(@"GET /_cat/nodeattrs?v&h=name,pid,attr,value");
		}
	}
}
