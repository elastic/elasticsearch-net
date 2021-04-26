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

namespace Examples.XPack.Docs.En.Security.SecuringCommunications
{
	public class TutorialTlsAddnodesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/securing-communications/tutorial-tls-addnodes.asciidoc:156")]
		public void Line156()
		{
			// tag::b02e4907c9936c1adc16ccce9d49900d[]
			var response0 = new SearchResponse<object>();
			// end::b02e4907c9936c1adc16ccce9d49900d[]

			response0.MatchesExample(@"GET _cluster/health");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/securing-communications/tutorial-tls-addnodes.asciidoc:166")]
		public void Line166()
		{
			// tag::9296d687ad779f8c57896edff2791c0d[]
			var response0 = new SearchResponse<object>();
			// end::9296d687ad779f8c57896edff2791c0d[]

			response0.MatchesExample(@"GET _cat/nodes?v");
		}
	}
}
