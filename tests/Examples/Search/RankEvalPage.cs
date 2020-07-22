// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search
{
	public class RankEvalPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/rank-eval.asciidoc:232")]
		public void Line232()
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
		[Description("search/rank-eval.asciidoc:287")]
		public void Line287()
		{
			// tag::b0b86fd5ac0d4814fc70cb0642cee258[]
			var response0 = new SearchResponse<object>();
			// end::b0b86fd5ac0d4814fc70cb0642cee258[]

			response0.MatchesExample(@"GET /twitter/_rank_eval
			{
			    ""requests"": [
			    {
			        ""id"": ""JFK query"",
			        ""request"": { ""query"": { ""match_all"": {}}},
			        ""ratings"": []
			    }],
			    ""metric"": {
			      ""recall"": {
			        ""k"" : 20,
			        ""relevant_rating_threshold"": 1
			      }
			   }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/rank-eval.asciidoc:328")]
		public void Line328()
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
		[Description("search/rank-eval.asciidoc:372")]
		public void Line372()
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
		[Description("search/rank-eval.asciidoc:428")]
		public void Line428()
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
