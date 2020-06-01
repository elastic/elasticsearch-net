using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Ilm
{
	public class IlmTutorialPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ilm/ilm-tutorial.asciidoc:59")]
		public void Line59()
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
		[Description("ilm/ilm-tutorial.asciidoc:114")]
		public void Line114()
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
		[Description("ilm/ilm-tutorial.asciidoc:159")]
		public void Line159()
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
		[Description("ilm/ilm-tutorial.asciidoc:195")]
		public void Line195()
		{
			// tag::2ffa953b29ed0156c9e610daf66b8e48[]
			var response0 = new SearchResponse<object>();
			// end::2ffa953b29ed0156c9e610daf66b8e48[]

			response0.MatchesExample(@"GET timeseries-*/_ilm/explain");
		}
	}
}