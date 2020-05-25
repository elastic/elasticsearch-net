// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ilm
{
	public class GettingStartedIlmPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ilm/getting-started-ilm.asciidoc:46")]
		public void Line46()
		{
			// tag::080b3362db1fa14e1ca4e290d6e6447d[]
			var response0 = new SearchResponse<object>();
			// end::080b3362db1fa14e1ca4e290d6e6447d[]

			response0.MatchesExample(@"PUT _ilm/policy/timeseries_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""hot"": {                      <1>
			        ""actions"": {
			          ""rollover"": {
			            ""max_size"": ""50GB"",     <2>
			            ""max_age"": ""30d""
			          }
			        }
			      },
			      ""delete"": {
			        ""min_age"": ""90d"",           <3>
			        ""actions"": {
			          ""delete"": {}              <4>
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/getting-started-ilm.asciidoc:93")]
		public void Line93()
		{
			// tag::4d01c243f7406c98546311ec1ff6b7e6[]
			var response0 = new SearchResponse<object>();
			// end::4d01c243f7406c98546311ec1ff6b7e6[]

			response0.MatchesExample(@"PUT _template/timeseries_template
			{
			  ""index_patterns"": [""timeseries-*""],                 <1>
			  ""settings"": {
			    ""number_of_shards"": 1,
			    ""number_of_replicas"": 1,
			    ""index.lifecycle.name"": ""timeseries_policy"",      <2>
			    ""index.lifecycle.rollover_alias"": ""timeseries""    <3>
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/getting-started-ilm.asciidoc:135")]
		public void Line135()
		{
			// tag::7148c8512079d378af70302e65502dd2[]
			var response0 = new SearchResponse<object>();
			// end::7148c8512079d378af70302e65502dd2[]

			response0.MatchesExample(@"PUT timeseries-000001
			{
			  ""aliases"": {
			    ""timeseries"": {
			      ""is_write_index"": true
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/getting-started-ilm.asciidoc:173")]
		public void Line173()
		{
			// tag::2ffa953b29ed0156c9e610daf66b8e48[]
			var response0 = new SearchResponse<object>();
			// end::2ffa953b29ed0156c9e610daf66b8e48[]

			response0.MatchesExample(@"GET timeseries-*/_ilm/explain");
		}
	}
}
