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

namespace Examples.Ilm
{
	public class UpdateLifecyclePolicyPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ilm/update-lifecycle-policy.asciidoc:29")]
		public void Line29()
		{
			// tag::0c44088f251488432966131135f1bd1c[]
			var response0 = new SearchResponse<object>();
			// end::0c44088f251488432966131135f1bd1c[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""hot"": {
			        ""actions"": {
			          ""rollover"": {
			            ""max_size"": ""25GB""
			          }
			        }
			      },
			      ""delete"": {
			        ""min_age"": ""30d"",
			        ""actions"": {
			          ""delete"": {}
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/update-lifecycle-policy.asciidoc:60")]
		public void Line60()
		{
			// tag::2c37ed0b33658d73a712e7942ea7433a[]
			var response0 = new SearchResponse<object>();
			// end::2c37ed0b33658d73a712e7942ea7433a[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""hot"": {
			        ""actions"": {
			          ""rollover"": {
			            ""max_size"": ""25GB""
			          }
			        }
			      },
			      ""delete"": {
			        ""min_age"": ""10d"", \<1>
			        ""actions"": {
			          ""delete"": {}
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/update-lifecycle-policy.asciidoc:144")]
		public void Line144()
		{
			// tag::fc541f5741c1fe052439ededa84ffe8a[]
			var response0 = new SearchResponse<object>();
			// end::fc541f5741c1fe052439ededa84ffe8a[]

			response0.MatchesExample(@"PUT _ilm/policy/my_executing_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""hot"": {
			        ""actions"": {
			          ""rollover"": {
			            ""max_docs"": 1
			          }
			        }
			      },
			      ""delete"": {
			        ""min_age"": ""10d"",
			        ""actions"": {
			          ""delete"": {}
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/update-lifecycle-policy.asciidoc:184")]
		public void Line184()
		{
			// tag::0f6fa3a706a7c17858d3dbe329839ea6[]
			var response0 = new SearchResponse<object>();
			// end::0f6fa3a706a7c17858d3dbe329839ea6[]

			response0.MatchesExample(@"GET my_index/_ilm/explain");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/update-lifecycle-policy.asciidoc:227")]
		public void Line227()
		{
			// tag::f94601bc9cd640adb939af67116a40c8[]
			var response0 = new SearchResponse<object>();
			// end::f94601bc9cd640adb939af67116a40c8[]

			response0.MatchesExample(@"PUT _ilm/policy/my_executing_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""hot"": {
			        ""min_age"": ""1d"", \<1>
			        ""actions"": {
			          ""rollover"": {
			            ""max_docs"": 1
			          }
			        }
			      },
			      ""delete"": {
			        ""min_age"": ""10d"",
			        ""actions"": {
			          ""delete"": {}
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/update-lifecycle-policy.asciidoc:306")]
		public void Line306()
		{
			// tag::416c65c55a53d0161426cc09ae999c72[]
			var response0 = new SearchResponse<object>();
			// end::416c65c55a53d0161426cc09ae999c72[]

			response0.MatchesExample(@"PUT _ilm/policy/my_executing_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""warm"": {
			        ""min_age"": ""1d"",
			        ""actions"": {
			          ""forcemerge"": {
			            ""max_num_segments"": 1
			          }
			        }
			      },
			      ""delete"": {
			        ""min_age"": ""10d"",
			        ""actions"": {
			          ""delete"": {}
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/update-lifecycle-policy.asciidoc:496")]
		public void Line496()
		{
			// tag::552b6761ef052efa1e83f8a3c30d6f78[]
			var response0 = new SearchResponse<object>();
			// end::552b6761ef052efa1e83f8a3c30d6f78[]

			response0.MatchesExample(@"PUT my_index/_settings
			{
			  ""lifecycle.name"": ""my_other_policy""
			}");
		}
	}
}
