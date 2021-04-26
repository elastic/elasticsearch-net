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

namespace Examples.Modules
{
	public class RemoteClustersPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("modules/remote-clusters.asciidoc:133")]
		public void Line133()
		{
			// tag::318d8096ad47c1c939776c2ce12758f9[]
			var response0 = new SearchResponse<object>();
			// end::318d8096ad47c1c939776c2ce12758f9[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			  ""persistent"": {
			    ""cluster"": {
			      ""remote"": {
			        ""cluster_one"": {
			          ""seeds"": [
			            ""127.0.0.1:9300""
			          ],
			          ""transport.ping_schedule"": ""30s""
			        },
			        ""cluster_two"": {
			          ""mode"": ""sniff"",
			          ""seeds"": [
			            ""127.0.0.1:9301""
			          ],
			          ""transport.compress"": true,
			          ""skip_unavailable"": true
			        },
			        ""cluster_three"": {
			          ""mode"": ""proxy"",
			          ""proxy_address"": ""127.0.0.1:9302""
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("modules/remote-clusters.asciidoc:170")]
		public void Line170()
		{
			// tag::0b1aad9e990ae831f392f816dfbd4528[]
			var response0 = new SearchResponse<object>();
			// end::0b1aad9e990ae831f392f816dfbd4528[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			  ""persistent"": {
			    ""cluster"": {
			      ""remote"": {
			        ""cluster_one"": {
			          ""seeds"": [
			            ""127.0.0.1:9300""
			          ],
			          ""transport.ping_schedule"": ""60s""
			        },
			        ""cluster_two"": {
			          ""mode"": ""sniff"",
			          ""seeds"": [
			            ""127.0.0.1:9301""
			          ],
			          ""transport.compress"": false
			        },
			        ""cluster_three"": {
			          ""mode"": ""proxy"",
			          ""proxy_address"": ""127.0.0.1:9302"",
			          ""transport.compress"": true
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("modules/remote-clusters.asciidoc:208")]
		public void Line208()
		{
			// tag::466bb2bc027f33a611e32fd7a9540eef[]
			var response0 = new SearchResponse<object>();
			// end::466bb2bc027f33a611e32fd7a9540eef[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			  ""persistent"": {
			    ""cluster"": {
			      ""remote"": {
			        ""cluster_two"": { <1>
			          ""mode"": null,
			          ""seeds"": null,
			          ""skip_unavailable"": null,
			          ""transport"": {
			            ""compress"": null
			          }
			        }
			      }
			    }
			  }
			}");
		}
	}
}
