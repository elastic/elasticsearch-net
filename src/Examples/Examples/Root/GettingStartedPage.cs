using System;
using System.Globalization;
using Elastic.Xunit.XunitPlumbing;
using Examples.Models;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Examples.Root
{
	public class GettingStartedPage : ExampleBase
	{
		[U]
		public void Line169()
		{
			// tag::f8cc4b331a19ff4df8e4a490f906ee69[]
			var catResponse = client.Cat.Health(h => h.Verbose());
			// end::f8cc4b331a19ff4df8e4a490f906ee69[]

			catResponse.MatchesExample(@"GET /_cat/health?v");
		}

		[U]
		public void Line214()
		{
			// tag::311c4b632a29b9ead63b02d01f10096b[]
			var indexResponse = client.Index(new Customer
			{
				Name = "John Doe"
			}, i => i
			.Index("customer")
			.Id(1)
			);
			// end::311c4b632a29b9ead63b02d01f10096b[]

			indexResponse.MatchesExample(@"PUT /customer/_doc/1
			{
			  ""name"": ""John Doe""
			}");
		}

		[U]
		public void Line253()
		{
			// tag::3f3b3e207f79303ce6f86e03e928e062[]
			var getResponse = client.Get<Customer>(1, g => g
				.Index("customer")
			);
			// end::3f3b3e207f79303ce6f86e03e928e062[]

			getResponse.MatchesExample(@"GET /customer/_doc/1");
		}

		[U]
		public void Line355()
		{
			// tag::506844befdc5691d835771bcbb1c1a60[]
			var searchResponse = client.Search<Account>(s => s
				.Index("bank")
				.MatchAll()
				.Sort(so => so.Ascending(f => f.AccountNumber))
			);
			// end::506844befdc5691d835771bcbb1c1a60[]

			searchResponse.MatchesExample(@"GET /bank/_search
			{
			  ""query"": { ""match_all"": {} },
			  ""sort"": [
			    { ""account_number"": ""asc"" }
			  ]
			}", e =>
			{
				// client always generates long form for sort order
				e.ApplyBodyChanges(body =>
				{
					var asc = body["sort"][0]["account_number"];
					body["sort"][0]["account_number"] = new JObject { { "order", asc } };
				});
				return e;
			});
		}

		[U]
		public void Line424()
		{
			// tag::4b90feb9d5d3dbfce424dac0341320b7[]
			var response0 = client.Search<Account>(s => s
				.Index("bank")
				.Query(q => q.MatchAll())
				.Sort(so => so
					.Field(f => f.AccountNumber, SortOrder.Ascending)
				)
				.From(10)
				.Size(10)
			);
			// end::4b90feb9d5d3dbfce424dac0341320b7[]

			response0.MatchesExample(@"GET /bank/_search
			{
			  ""query"": { ""match_all"": {} },
			  ""sort"": [
			    { ""account_number"": ""asc"" }
			  ],
			  ""from"": 10,
			  ""size"": 10
			}", e =>
			{
				e.ApplyBodyChanges(body =>
				{
					// only long form of sort it supported by the client
					var sort = body["sort"][0]["account_number"].Value<string>();
					body["sort"][0]["account_number"] = new JObject { { "order", sort } };
				});
				return e;
			});
		}

		[U]
		public void Line445()
		{
			// tag::cd247f267968aa0927bfdad56852f8f5[]
			var searchResponse = client.Search<Account>(s => s
				.Index("bank")
				.Query(q => q
					.Match(m => m
						.Field(f => f.Address)
						.Query("mill lane")
					)
				)
			);
			// end::cd247f267968aa0927bfdad56852f8f5[]

			searchResponse.MatchesExample(@"GET /bank/_search
			{
			  ""query"": { ""match"": { ""address"": ""mill lane"" } }
			}", e =>
			{
				// client does not support shorthand match query syntax. Expand out
				e.ApplyBodyChanges(body =>
				{
					body["query"]["match"]["address"] = new JObject { { "query", "mill lane" } };
				});
				return e;
			});
		}

		[U]
		public void Line458()
		{
			// tag::231aa0bb39c35fe199d28fe0e4a62b2e[]
			var searchResponse = client.Search<Account>(s => s
				.Index("bank")
				.Query(q => q
					.MatchPhrase(m => m
						.Field(f => f.Address)
						.Query("mill lane")
					)
				)
			);
			// end::231aa0bb39c35fe199d28fe0e4a62b2e[]

			searchResponse.MatchesExample(@"GET /bank/_search
			{
			  ""query"": { ""match_phrase"": { ""address"": ""mill lane"" } }
			}", e =>
			{
				// client does not support shorthand match query syntax. Expand out
				e.ApplyBodyChanges(body =>
				{
					body["query"]["match_phrase"]["address"] = new JObject { { "query", "mill lane" } };
				});
				return e;
			});
		}

		[U]
		public void Line475()
		{
			// tag::47bb632c6091ad0cd94bc660bdd309a5[]
			var searchResponse = client.Search<Account>(s => s
				.Index("bank")
				.Query(q => q
					.Bool(b => b
						.Must(mu => mu
							.Match(m => m
								.Field(f => f.Age)
								.Query("40")
							)
						)
						.MustNot(mn => mn
							.Match(m => m
								.Field(ff => ff.State)
								.Query("ID")
							)
						)
					)
				)
			);
			// end::47bb632c6091ad0cd94bc660bdd309a5[]

			searchResponse.MatchesExample(@"GET /bank/_search
			{
			  ""query"": {
			    ""bool"": {
			      ""must"": [
			        { ""match"": { ""age"": ""40"" } }
			      ],
			      ""must_not"": [
			        { ""match"": { ""state"": ""ID"" } }
			      ]
			    }
			  }
			}", e =>
			{
				// client does not support shorthand match query syntax. Expand out
				e.ApplyBodyChanges(body =>
				{
					body["query"]["bool"]["must"][0]["match"]["age"] = new JObject { { "query", "40" } };
					body["query"]["bool"]["must_not"][0]["match"]["state"] = new JObject { { "query", "ID" } };
				});
				return e;
			});
		}

		[U]
		public void Line507()
		{
			// tag::251ea12c1248385ab409906ac64d9ee9[]
			var searchResponse = client.Search<Account>(s => s
				.Index("bank")
				.Query(q => q
					.Bool(b => b
						.Must(mu => mu
							.MatchAll()
						)
						.Filter(f => f
							.Range(r => r
								.Field(ff => ff.Balance)
								.GreaterThanOrEquals(20000)
								.LessThanOrEquals(30000)
							)
						)
					)
				)
			);
			// end::251ea12c1248385ab409906ac64d9ee9[]

			searchResponse.MatchesExample(@"GET /bank/_search
			{
			  ""query"": {
			    ""bool"": {
			      ""must"": { ""match_all"": {} },
			      ""filter"": {
			        ""range"": {
			          ""balance"": {
			            ""gte"": 20000,
			            ""lte"": 30000
			          }
			        }
			      }
			    }
			  }
			}", e =>
			{
				// bool must and filter are always an array and numeric range queries always use doubles
				e.ApplyBodyChanges(body =>
				{
					var must = body["query"]["bool"]["must"];
					body["query"]["bool"]["must"] = new JArray(must);
					var filter = body["query"]["bool"]["filter"];
					filter["range"]["balance"]["gte"] = 20000d;
					filter["range"]["balance"]["lte"] = 30000d;
					body["query"]["bool"]["filter"] = new JArray(filter);
				});
				return e;
			});
		}

		[U]
		public void Line541()
		{
			// tag::feefeb68144002fd1fff57b77b95b85e[]
			var searchResponse = client.Search<Account>(s => s
				.Index("bank")
				.Size(0)
				.Aggregations(a => a
					.Terms("group_by_state", ra => ra
						.Field(f => f.State.Suffix("keyword"))
					)
				)
			);
			// end::feefeb68144002fd1fff57b77b95b85e[]

			searchResponse.MatchesExample(@"GET /bank/_search
			{
			  ""size"": 0,
			  ""aggs"": {
			    ""group_by_state"": {
			      ""terms"": {
			        ""field"": ""state.keyword""
			      }
			    }
			  }
			}");
		}

		[U]
		public void Line628()
		{
			// tag::cfbaea6f0df045c5d940bbb6a9c69cd8[]
			var searchResponse = client.Search<Account>(s => s
				.Index("bank")
				.Size(0)
				.Aggregations(a => a
					.Terms("group_by_state", ra => ra
						.Field(f => f.State.Suffix("keyword"))
						.Aggregations(aa => aa
							.Average("average_balance", av => av
								.Field(f => f.Balance)
							)
						)
					)
				)
			);
			// end::cfbaea6f0df045c5d940bbb6a9c69cd8[]

			searchResponse.MatchesExample(@"GET /bank/_search
			{
			  ""size"": 0,
			  ""aggs"": {
			    ""group_by_state"": {
			      ""terms"": {
			        ""field"": ""state.keyword""
			      },
			      ""aggs"": {
			        ""average_balance"": {
			          ""avg"": {
			            ""field"": ""balance""
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U]
		public void Line654()
		{
			// tag::645796e8047967ca4a7635a22a876f4c[]
			var searchResponse = client.Search<Account>(s => s
				.Index("bank")
				.Size(0)
				.Aggregations(a => a
					.Terms("group_by_state", ra => ra
						.Field(f => f.State.Suffix("keyword"))
						.Order(o => o
							.Descending("average_balance")
						)
						.Aggregations(aa => aa
							.Average("average_balance", av => av
								.Field(f => f.Balance)
							)
						)
					)
				)
			);
			// end::645796e8047967ca4a7635a22a876f4c[]

			searchResponse.MatchesExample(@"GET /bank/_search
			{
			  ""size"": 0,
			  ""aggs"": {
			    ""group_by_state"": {
			      ""terms"": {
			        ""field"": ""state.keyword"",
			        ""order"": {
			          ""average_balance"": ""desc""
			        }
			      },
			      ""aggs"": {
			        ""average_balance"": {
			          ""avg"": {
			            ""field"": ""balance""
			          }
			        }
			      }
			    }
			  }
			}", e =>
			{
				// Terms aggs order is always an array
				e.ApplyBodyChanges(body =>
				{
					var order = body["aggs"]["group_by_state"]["terms"]["order"];
					body["aggs"]["group_by_state"]["terms"]["order"] = new JArray(order);
				});
				return e;
			});
		}
	}
}
