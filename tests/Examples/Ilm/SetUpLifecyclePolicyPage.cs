using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ilm
{
	public class SetUpLifecyclePolicyPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ilm/set-up-lifecycle-policy.asciidoc:12")]
		public void Line12()
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
		[Description("ilm/set-up-lifecycle-policy.asciidoc:59")]
		public void Line59()
		{
			// tag::3c9d99215a7020ab478bdf5c8287a14f[]
			var response0 = new SearchResponse<object>();
			// end::3c9d99215a7020ab478bdf5c8287a14f[]

			response0.MatchesExample(@"PUT _template/my_template
			{
			  ""index_patterns"": [""test-*""], \<1>
			  ""settings"": {
			    ""number_of_shards"": 1,
			    ""number_of_replicas"": 1,
			    ""index.lifecycle.name"": ""my_policy"", \<2>
			    ""index.lifecycle.rollover_alias"": ""test-alias""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/set-up-lifecycle-policy.asciidoc:90")]
		public void Line90()
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
		[Description("ilm/set-up-lifecycle-policy.asciidoc:113")]
		public void Line113()
		{
			// tag::160d259243d0800900b065c4b9d2b187[]
			var response0 = new SearchResponse<object>();
			// end::160d259243d0800900b065c4b9d2b187[]

			response0.MatchesExample(@"PUT test-index
			{
			  ""settings"": {
			    ""number_of_shards"": 1,
			    ""number_of_replicas"": 1,
			    ""index.lifecycle.name"": ""my_policy""
			  }
			}");
		}
	}
}