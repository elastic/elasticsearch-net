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

namespace Examples.Slm
{
	public class GettingStartedSlmPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("slm/getting-started-slm.asciidoc:34")]
		public void Line34()
		{
			// tag::89b72dd7f747f6297c2b089e8bc807be[]
			var response0 = new SearchResponse<object>();
			// end::89b72dd7f747f6297c2b089e8bc807be[]

			response0.MatchesExample(@"PUT /_snapshot/my_repository
			{
			  ""type"": ""fs"",
			  ""settings"": {
			    ""location"": ""my_backup_location""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("slm/getting-started-slm.asciidoc:66")]
		public void Line66()
		{
			// tag::0da01289c561448254d521504d5122dd[]
			var response0 = new SearchResponse<object>();
			// end::0da01289c561448254d521504d5122dd[]

			response0.MatchesExample(@"PUT /_slm/policy/nightly-snapshots
			{
			  ""schedule"": ""0 30 2 * * ?"", <1>
			  ""name"": ""<nightly-snap-{now/d}>"", <2>
			  ""repository"": ""my_repository"", <3>
			  ""config"": { <4>
			    ""indices"": [""*""] <5>
			  },
			  ""retention"": { <6>
			    ""expire_after"": ""30d"", <7>
			    ""min_count"": 5, <8>
			    ""max_count"": 50 <9>
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("slm/getting-started-slm.asciidoc:119")]
		public void Line119()
		{
			// tag::af5ff39759d3af0525d941634a6cdb82[]
			var response0 = new SearchResponse<object>();
			// end::af5ff39759d3af0525d941634a6cdb82[]

			response0.MatchesExample(@"POST /_slm/policy/nightly-snapshots/_execute");
		}

		[U(Skip = "Example not implemented")]
		[Description("slm/getting-started-slm.asciidoc:129")]
		public void Line129()
		{
			// tag::f1b545d3c3eeedf8ae09c56070c26053[]
			var response0 = new SearchResponse<object>();
			// end::f1b545d3c3eeedf8ae09c56070c26053[]

			response0.MatchesExample(@"GET /_slm/policy/nightly-snapshots?human");
		}
	}
}
