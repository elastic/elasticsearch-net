using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Search
{
	public class RankEvalPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line151()
		{
			// tag::2a989679e4b71569e17e02db8865b685[]
			var response0 = new SearchResponse<object>();
			// end::2a989679e4b71569e17e02db8865b685[]

			response0.MatchesExample(@"GET /twitter/_rank_eval
			{
			    ""requests"": [
			    {
			        ""id"": ""JFK query"",
			        ""request"": { ""query"": { ""match_all"": {}}},
			        ""ratings"": []
			    }],
			    ""metric"": {
			      ""precision"": {
			        ""k"" : 20,
			        ""relevant_rating_threshold"": 1,
			        ""ignore_unlabeled"": false
			      }
			   }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line194()
		{
			// tag::351775a1f73e47025463bd937948f7b4[]
			var response0 = new SearchResponse<object>();
			// end::351775a1f73e47025463bd937948f7b4[]

			response0.MatchesExample(@"GET /twitter/_rank_eval
			{
			    ""requests"": [
			    {
			        ""id"": ""JFK query"",
			        ""request"": { ""query"": { ""match_all"": {}}},
			        ""ratings"": []
			    }],
			    ""metric"": {
			        ""mean_reciprocal_rank"": {
			            ""k"" : 20,
			            ""relevant_rating_threshold"" : 1
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line233()
		{
			// tag::c4f013ff1a8b80c87c0265a91ed12648[]
			var response0 = new SearchResponse<object>();
			// end::c4f013ff1a8b80c87c0265a91ed12648[]

			response0.MatchesExample(@"GET /twitter/_rank_eval
			{
			    ""requests"": [
			    {
			        ""id"": ""JFK query"",
			        ""request"": { ""query"": { ""match_all"": {}}},
			        ""ratings"": []
			    }],
			    ""metric"": {
			       ""dcg"": {
			            ""k"" : 20,
			            ""normalize"": false
			       }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line282()
		{
			// tag::12c4a9be9ffc26cdc0e9343d53c1fd5d[]
			var response0 = new SearchResponse<object>();
			// end::12c4a9be9ffc26cdc0e9343d53c1fd5d[]

			response0.MatchesExample(@"GET /twitter/_rank_eval
			{
			    ""requests"": [
			    {
			        ""id"": ""JFK query"",
			        ""request"": { ""query"": { ""match_all"": {}}},
			        ""ratings"": []
			    }],
			    ""metric"": {
			       ""expected_reciprocal_rank"": {
			            ""maximum_relevance"" : 3,
			            ""k"" : 20
			       }
			    }
			}");
		}
	}
}