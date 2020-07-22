// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ilm
{
	public class SetUpLifecyclePolicyPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ilm/set-up-lifecycle-policy.asciidoc:44")]
		public void Line44()
		{
			// tag::7ecf197610e30c20f7206513ce393822[]
			var response0 = new SearchResponse<object>();
			// end::7ecf197610e30c20f7206513ce393822[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""hot"": {
			        ""actions"": {
			          ""rollover"": {
			            ""max_size"": ""25GB"" \<1>
			          }
			        }
			      },
			      ""delete"": {
			        ""min_age"": ""30d"",
			        ""actions"": {
			          ""delete"": {} \<2>
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/set-up-lifecycle-policy.asciidoc:91")]
		public void Line91()
		{
			// tag::f29c02d259065033bd557519d1b21481[]
			var response0 = new SearchResponse<object>();
			// end::f29c02d259065033bd557519d1b21481[]

			response0.MatchesExample(@"PUT _template/my_template
			{
			  ""index_patterns"": [""test-*""], <1>
			  ""settings"": {
			    ""number_of_shards"": 1,
			    ""number_of_replicas"": 1,
			    ""index.lifecycle.name"": ""my_policy"", <2>
			    ""index.lifecycle.rollover_alias"": ""test-alias"" <3>
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/set-up-lifecycle-policy.asciidoc:138")]
		public void Line138()
		{
			// tag::25737fd456fd317cc4cc2db76b6cf28e[]
			var response0 = new SearchResponse<object>();
			// end::25737fd456fd317cc4cc2db76b6cf28e[]

			response0.MatchesExample(@"PUT test-000001
			{
			  ""aliases"": {
			    ""test-alias"":{
			      ""is_write_index"": true \<1>
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/set-up-lifecycle-policy.asciidoc:175")]
		public void Line175()
		{
			// tag::d690a6af462c70a783625a323e11c72c[]
			var response0 = new SearchResponse<object>();
			// end::d690a6af462c70a783625a323e11c72c[]

			response0.MatchesExample(@"PUT test-index
			{
			  ""settings"": {
			    ""number_of_shards"": 1,
			    ""number_of_replicas"": 1,
			    ""index.lifecycle.name"": ""my_policy"" <1>
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/set-up-lifecycle-policy.asciidoc:251")]
		public void Line251()
		{
			// tag::ec195297eb804cba1cb19c9926773059[]
			var response0 = new SearchResponse<object>();
			// end::ec195297eb804cba1cb19c9926773059[]

			response0.MatchesExample(@"PUT mylogs-pre-ilm*/_settings <1>
			{
			  ""index"": {
			    ""lifecycle"": {
			      ""name"": ""mylogs_policy_existing""
			    }
			  }
			}");
		}
	}
}
