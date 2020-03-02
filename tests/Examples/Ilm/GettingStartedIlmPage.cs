using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ilm
{
	public class GettingStartedIlmPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line46()
		{
			// tag::f2dc1a1a2a6ba3c7c4273ce41ada4207[]
			var response0 = new SearchResponse<object>();
			// end::f2dc1a1a2a6ba3c7c4273ce41ada4207[]

			response0.MatchesExample(@"PUT _ilm/policy/datastream_policy
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
		public void Line93()
		{
			// tag::e3d7b19f993382750719cdfaad2fdd90[]
			var response0 = new SearchResponse<object>();
			// end::e3d7b19f993382750719cdfaad2fdd90[]

			response0.MatchesExample(@"PUT _template/datastream_template
			{
			  ""index_patterns"": [""datastream-*""],                 \<1>
			  ""settings"": {
			    ""number_of_shards"": 1,
			    ""number_of_replicas"": 1,
			    ""index.lifecycle.name"": ""datastream_policy"",      \<2>
			    ""index.lifecycle.rollover_alias"": ""datastream""    \<3>
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line135()
		{
			// tag::55ee835d7c28e933ad8fcb9e45af2bf2[]
			var response0 = new SearchResponse<object>();
			// end::55ee835d7c28e933ad8fcb9e45af2bf2[]

			response0.MatchesExample(@"PUT datastream-000001
			{
			  ""aliases"": {
			    ""datastream"": {
			      ""is_write_index"": true
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line173()
		{
			// tag::a1dbaff15cf8166f74c443ca58258d7e[]
			var response0 = new SearchResponse<object>();
			// end::a1dbaff15cf8166f74c443ca58258d7e[]

			response0.MatchesExample(@"GET datastream-*/_ilm/explain");
		}
	}
}