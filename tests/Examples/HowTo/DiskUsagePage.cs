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

namespace Examples.HowTo
{
	public class DiskUsagePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("how-to/disk-usage.asciidoc:14")]
		public void Line14()
		{
			// tag::e273060a675c959fd5f3cde27c8aff07[]
			var response0 = new SearchResponse<object>();
			// end::e273060a675c959fd5f3cde27c8aff07[]

			response0.MatchesExample(@"PUT index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""foo"": {
			        ""type"": ""integer"",
			        ""index"": false
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("how-to/disk-usage.asciidoc:34")]
		public void Line34()
		{
			// tag::c8568f4f02f75db9afd669880db98a16[]
			var response0 = new SearchResponse<object>();
			// end::c8568f4f02f75db9afd669880db98a16[]

			response0.MatchesExample(@"PUT index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""foo"": {
			        ""type"": ""text"",
			        ""norms"": false
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("how-to/disk-usage.asciidoc:54")]
		public void Line54()
		{
			// tag::1a5cd30017368fe4888454a13c6e8561[]
			var response0 = new SearchResponse<object>();
			// end::1a5cd30017368fe4888454a13c6e8561[]

			response0.MatchesExample(@"PUT index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""foo"": {
			        ""type"": ""text"",
			        ""index_options"": ""freqs""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("how-to/disk-usage.asciidoc:74")]
		public void Line74()
		{
			// tag::ae3ae58724c413734b67a90a6ddb319f[]
			var response0 = new SearchResponse<object>();
			// end::ae3ae58724c413734b67a90a6ddb319f[]

			response0.MatchesExample(@"PUT index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""foo"": {
			        ""type"": ""text"",
			        ""norms"": false,
			        ""index_options"": ""freqs""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("how-to/disk-usage.asciidoc:106")]
		public void Line106()
		{
			// tag::597d456edfcb3d410954a3e9b5babf9a[]
			var response0 = new SearchResponse<object>();
			// end::597d456edfcb3d410954a3e9b5babf9a[]

			response0.MatchesExample(@"PUT index
			{
			  ""mappings"": {
			    ""dynamic_templates"": [
			      {
			        ""strings"": {
			          ""match_mapping_type"": ""string"",
			          ""mapping"": {
			            ""type"": ""keyword""
			          }
			        }
			      }
			    ]
			  }
			}");
		}
	}
}
