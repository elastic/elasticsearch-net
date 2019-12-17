using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ingest
{
	public class EnrichPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line72()
		{
			// tag::927dd38daa489175a5008799452e870a[]
			var response0 = new SearchResponse<object>();
			// end::927dd38daa489175a5008799452e870a[]

			response0.MatchesExample(@"PUT /users/_doc/1?refresh=wait_for
			{
			    ""email"": ""mardy.brown@asciidocsmith.com"",
			    ""first_name"": ""Mardy"",
			    ""last_name"": ""Brown"",
			    ""city"": ""New Orleans"",
			    ""county"": ""Orleans"",
			    ""state"": ""LA"",
			    ""zip"": 70116,
			    ""web"": ""mardy.asciidocsmith.com""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line103()
		{
			// tag::9ab4e8a564e13475cb3a0376be56bb8e[]
			var response0 = new SearchResponse<object>();
			// end::9ab4e8a564e13475cb3a0376be56bb8e[]

			response0.MatchesExample(@"PUT /_enrich/policy/users-policy
			{
			    ""match"": {
			        ""indices"": ""users"",
			        ""match_field"": ""email"",
			        ""enrich_fields"": [""first_name"", ""last_name"", ""city"", ""zip"", ""state""]
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line130()
		{
			// tag::af4f82ce86672a9bafd834f334c8e1c9[]
			var response0 = new SearchResponse<object>();
			// end::af4f82ce86672a9bafd834f334c8e1c9[]

			response0.MatchesExample(@"POST /_enrich/policy/users-policy/_execute");
		}

		[U(Skip = "Example not implemented")]
		public void Line161()
		{
			// tag::958b661d89b2beeb0cfefe8edbe3e408[]
			var response0 = new SearchResponse<object>();
			// end::958b661d89b2beeb0cfefe8edbe3e408[]

			response0.MatchesExample(@"PUT /_ingest/pipeline/user_lookup
			{
			  ""description"" : ""Enriching user details to messages"",
			  ""processors"" : [
			    {
			      ""enrich"" : {
			        ""policy_name"": ""users-policy"",
			        ""field"" : ""email"",
			        ""target_field"": ""user"",
			        ""max_matches"": ""1""
			      }
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line217()
		{
			// tag::7495d7e8d99e4f5ac8034988b706e09d[]
			var response0 = new SearchResponse<object>();
			// end::7495d7e8d99e4f5ac8034988b706e09d[]

			response0.MatchesExample(@"PUT /my_index/_doc/my_id?pipeline=user_lookup
			{
			  ""email"": ""mardy.brown@asciidocsmith.com""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line230()
		{
			// tag::ce20ab8067b6e4ad68e8ad7a5a0b73fd[]
			var response0 = new SearchResponse<object>();
			// end::ce20ab8067b6e4ad68e8ad7a5a0b73fd[]

			response0.MatchesExample(@"GET /my_index/_doc/my_id");
		}
	}
}