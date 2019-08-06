using System;
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
		public void Line209()
		{
			// tag::f8cc4b331a19ff4df8e4a490f906ee69[]
			var catResponse = client.Cat.Health(h => h.Verbose());
			// end::f8cc4b331a19ff4df8e4a490f906ee69[]

			catResponse.MatchesExample(@"GET /_cat/health?v");
		}

		[U]
		public void Line240()
		{
			// tag::db20adb70a8e8d0709d15ba0daf18d23[]
			var catResponse = client.Cat.Nodes(h => h.Verbose());
			// end::db20adb70a8e8d0709d15ba0daf18d23[]

			catResponse.MatchesExample(@"GET /_cat/nodes?v");
		}

		[U]
		public void Line263()
		{
			// tag::c3fa04519df668d6c27727a12ab09648[]
			var catResponse = client.Cat.Indices(h => h.Verbose());
			// end::c3fa04519df668d6c27727a12ab09648[]

			catResponse.MatchesExample(@"GET /_cat/indices?v");
		}

		[U]
		public void Line284()
		{
			// tag::0caf6b6b092ecbcf6f996053678a2384[]
			var createIndexResponse = client.Indices.Create("customer", c => c
				.Pretty()
			);

			var catResponse = client.Cat.Indices(h => h.Verbose());
			// end::0caf6b6b092ecbcf6f996053678a2384[]

			createIndexResponse.MatchesExample(@"PUT /customer?pretty");

			catResponse.MatchesExample(@"GET /_cat/indices?v");
		}

		[U]
		public void Line311()
		{
			// tag::21fe98843fe0b5473f515d21803db409[]
			var indexResponse = client.Index(new Customer
				{
					Name = "John Doe"
				}, c => c
				.Index("customer")
				.Id(1)
				.Pretty()
			);
			// end::21fe98843fe0b5473f515d21803db409[]

			indexResponse.MatchesExample(@"PUT /customer/_doc/1?pretty
			{
			  ""name"": ""John Doe""
			}");
		}

		[U]
		public void Line347()
		{
			// tag::37a3b66b555aed55618254f50d572cd6[]
			var getResponse = client.Get<Customer>(1, g => g
				.Index("customer")
				.Pretty()
			);
			// end::37a3b66b555aed55618254f50d572cd6[]

			getResponse.MatchesExample(@"GET /customer/_doc/1?pretty");
		}

		[U]
		public void Line378()
		{
			// tag::92e3c75133dc4fdb2cc65f149001b40b[]
			var deleteResponse = client.Indices.Delete("customer", d => d.Pretty());

			var catResponse = client.Cat.Indices(h => h.Verbose());
			// end::92e3c75133dc4fdb2cc65f149001b40b[]

			deleteResponse.MatchesExample(@"DELETE /customer?pretty");

			catResponse.MatchesExample(@"GET /_cat/indices?v");
		}

		[U]
		public void Line398()
		{
			// tag::fa5f618ced25ed2e67a1439bb77ba340[]
			var createIndexResponse = client.Indices.Create("customer");

			var indexResponse = client.Index(new Customer { Name = "John Doe" }, i => i
				.Index("customer")
				.Id(1)
			);

			var getResponse = client.Get<Customer>(1, g => g.Index("customer"));

			var deleteIndexResponse = client.Indices.Delete("customer");
			// end::fa5f618ced25ed2e67a1439bb77ba340[]

			createIndexResponse.MatchesExample(@"PUT /customer");

			indexResponse.MatchesExample(@"PUT /customer/_doc/1
			{
			  ""name"": ""John Doe""
			}");

			getResponse.MatchesExample(@"GET /customer/_doc/1");

			deleteIndexResponse.MatchesExample(@"DELETE /customer");
		}

		[U]
		public void Line442()
		{
			// tag::75bda7da7fefff2c16f0423a9aa41c6e[]
			var indexResponse = client.Index(new Customer
				{
					Name = "Jane Doe"
				}, i => i
				.Index("customer")
				.Id(1)
				.Pretty()
			);
			// end::75bda7da7fefff2c16f0423a9aa41c6e[]

			indexResponse.MatchesExample(@"PUT /customer/_doc/1?pretty
			{
			  ""name"": ""Jane Doe""
			}");
		}

		[U]
		public void Line454()
		{
			// tag::37c778346eb67042df5e8edf2485e40a[]
			var indexResponse = client.Index(new Customer
				{
					Name = "Jane Doe"
				}, i => i
				.Index("customer")
				.Id(2)
				.Pretty()
			);
			// end::37c778346eb67042df5e8edf2485e40a[]

			indexResponse.MatchesExample(@"PUT /customer/_doc/2?pretty
			{
			  ""name"": ""Jane Doe""
			}");
		}

		[U]
		public void Line470()
		{
			// tag::1c0f3b0bc4b7e53b53755fd3bda5b8cf[]
			var indexResponse = client.Index(new Customer
				{
					Name = "Jane Doe"
				}, i => i
				.Index("customer")
				.Pretty()
			);
			// end::1c0f3b0bc4b7e53b53755fd3bda5b8cf[]

			indexResponse.MatchesExample(@"POST /customer/_doc?pretty
			{
			  ""name"": ""Jane Doe""
			}");
		}

		[U]
		public void Line489()
		{
			// tag::6a8d7b34f2b42d5992aaa1ff147062d9[]
			var updateResponse = client.Update<Customer>(1, u => u
				.Doc(new Customer
				{
					Name = "Jane Doe"
				})
				.Index("customer")
				.Pretty()
			);
			// end::6a8d7b34f2b42d5992aaa1ff147062d9[]

			updateResponse.MatchesExample(@"POST /customer/_update/1?pretty
			{
			  ""doc"": { ""name"": ""Jane Doe"" }
			}");
		}

		[U]
		public void Line501()
		{
			// tag::731621af937d66170347b9cc6b4a3c48[]
			var updateResponse = client.Update<Customer>(1, u => u
				.Doc(new Customer
				{
					Name = "Jane Doe",
					Age = 20
				})
				.Index("customer")
				.Pretty()
			);
			// end::731621af937d66170347b9cc6b4a3c48[]

			updateResponse.MatchesExample(@"POST /customer/_update/1?pretty
			{
			  ""doc"": { ""name"": ""Jane Doe"", ""age"": 20 }
			}");
		}

		[U]
		public void Line513()
		{
			// tag::38dfa309717488362d0f784e17ebd1b5[]
			var updateResponse = client.Update<Customer>(1, u => u
				.Script(s => s
					.Source("ctx._source.age += 5")
				)
				.Index("customer")
				.Pretty()
			);
			// end::38dfa309717488362d0f784e17ebd1b5[]

			updateResponse.MatchesExample(@"POST /customer/_update/1?pretty
			{
			  ""script"" : ""ctx._source.age += 5""
			}");
		}

		[U]
		public void Line532()
		{
			// tag::9c5ef83db886840355ff662b6e9ae8ab[]
			var deleteResponse = client.Delete<Customer>(2, d => d
				.Index("customer")
				.Pretty()
			);
			// end::9c5ef83db886840355ff662b6e9ae8ab[]

			deleteResponse.MatchesExample(@"DELETE /customer/_doc/2?pretty");
		}

		[U]
		public void Line550()
		{
			// tag::7d32a32357b5ea8819b72608fcc6fd07[]
			var bulkResponse = client.Bulk(b => b
				.Index("customer")
				.Index<Customer>(i => i.Document(new Customer { Name = "John Doe" }).Id("1"))
				.Index<Customer>(i => i.Document(new Customer { Name = "Jane Doe" }).Id("2"))
				.Pretty()
			);
			// end::7d32a32357b5ea8819b72608fcc6fd07[]

			bulkResponse.MatchesExample(@"POST /customer/_bulk?pretty
			{""index"":{""_id"":""1""}}
			{""name"": ""John Doe"" }
			{""index"":{""_id"":""2""}}
			{""name"": ""Jane Doe"" }");
		}

		[U]
		public void Line562()
		{
			// tag::193864342d9f0a36ec84a91ca325f5ec[]
			var bulkResponse = client.Bulk(b => b
				.Index("customer")
				.Update<Customer>(i => i.Doc(new Customer { Name = "John Doe becomes Jane Doe" }).Id("1"))
				.Delete<Customer>(i => i.Id("2"))
				.Pretty()
			);
			// end::193864342d9f0a36ec84a91ca325f5ec[]

			bulkResponse.MatchesExample(@"POST /customer/_bulk?pretty
			{""update"":{""_id"":""1""}}
			{""doc"": { ""name"": ""John Doe becomes Jane Doe"" } }
			{""delete"":{""_id"":""2""}}");
		}

		[U]
		public void Line647()
		{
			// tag::c181969ef91c3b4a2513c1885be98e26[]
			var searchResponse = client.Search<Account>(s => s
				.Index("bank")
				.Query(q => q.QueryString(qs => qs.Query("*")))
				.Sort(so => so.Ascending(f => f.AccountNumber))
				.Pretty()
			);
			// end::c181969ef91c3b4a2513c1885be98e26[]

			searchResponse.MatchesExample(@"GET /bank/_search?q=*&sort=account_number:asc&pretty", e =>
			{
				// client does not support sort and query in query string. Remove
				// and create in body
				var uri = e.Uri.ToString();
				uri = uri.Replace("q=*&sort=account_number:asc&", string.Empty);
				e.Uri = new Uri(uri);

				e.Body = JsonConvert.SerializeObject(new
				{
					query = new { query_string = new { query = "*" } },
					sort = new[] { new { account_number = "asc" } }
				});
				return e;
			});
		}

		[U]
		public void Line720()
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
			}");
		}

		[U]
		public void Line789()
		{
			// tag::345ea7e9cb5af9e052ce0cf6f1f52c23[]
			var searchResponse = client.Search<Account>(s => s
				.Index("bank")
				.MatchAll()
			);
			// end::345ea7e9cb5af9e052ce0cf6f1f52c23[]

			searchResponse.MatchesExample(@"GET /bank/_search
			{
			  ""query"": { ""match_all"": {} }
			}");
		}

		[U]
		public void Line805()
		{
			// tag::3d7527bb7ac3b0e1f97b22bdfeb99070[]
			var searchResponse = client.Search<Account>(s => s
				.Index("bank")
				.MatchAll()
				.Size(1)
			);
			// end::3d7527bb7ac3b0e1f97b22bdfeb99070[]

			searchResponse.MatchesExample(@"GET /bank/_search
			{
			  ""query"": { ""match_all"": {} },
			  ""size"": 1
			}");
		}

		[U]
		public void Line820()
		{
			// tag::3c31f9eb032861bff64abd8b14758991[]
			var searchResponse = client.Search<Account>(s => s
				.Index("bank")
				.MatchAll()
				.From(10)
				.Size(10)
			);
			// end::3c31f9eb032861bff64abd8b14758991[]

			searchResponse.MatchesExample(@"GET /bank/_search
			{
			  ""query"": { ""match_all"": {} },
			  ""from"": 10,
			  ""size"": 10
			}");
		}

		[U]
		public void Line836()
		{
			// tag::e8035a7476601ad4b136edb250f92d53[]
			var searchResponse = client.Search<Account>(s => s
				.Index("bank")
				.MatchAll()
				.Sort(so => so
					.Field(f => f.Balance, SortOrder.Descending)
				)
			);
			// end::e8035a7476601ad4b136edb250f92d53[]

			searchResponse.MatchesExample(@"GET /bank/_search
			{
			  ""query"": { ""match_all"": {} },
			  ""sort"": { ""balance"": { ""order"": ""desc"" } }
			}");
		}

		[U]
		public void Line854()
		{
			// tag::b8459547da50aebddbcdd1aaaac02b5f[]
			var searchResponse = client.Search<Account>(s => s
				.Index("bank")
				.MatchAll()
				.Source(sf => sf
					.Includes(ff => ff
						.Field(f => f.AccountNumber)
						.Field(f => f.Balance)
					)
				)
			);
			// end::b8459547da50aebddbcdd1aaaac02b5f[]

			searchResponse.MatchesExample(@"GET /bank/_search
			{
			  ""query"": { ""match_all"": {} },
			  ""_source"": [""account_number"", ""balance""]
			}");
		}

		[U]
		public void Line873()
		{
			// tag::2e6bfd38c9bcb728227f0d4dd11c09a2[]
			var searchResponse = client.Search<Account>(s => s
				.Index("bank")
				.Query(q => q
					.Match(m => m
						.Field(f => f.AccountNumber)
						.Query("20")
					)
				)
			);
			// end::2e6bfd38c9bcb728227f0d4dd11c09a2[]

			searchResponse.MatchesExample(@"GET /bank/_search
			{
			  ""query"": { ""match"": { ""account_number"": 20 } }
			}", e =>
			{
				// client does not support shorthand match query syntax. Expand out
				var body = JObject.Parse(e.Body);
				body["query"]["match"]["account_number"] = new JObject{{ "query", "20" }};
				e.Body = body.ToString();
				return e;
			});
		}

		[U]
		public void Line885()
		{
			// tag::b8eab60f6441edf314306d8194c7cd56[]
			var searchResponse = client.Search<Account>(s => s
				.Index("bank")
				.Query(q => q
					.Match(m => m
						.Field(f => f.Address)
						.Query("mill")
					)
				)
			);
			// end::b8eab60f6441edf314306d8194c7cd56[]

			searchResponse.MatchesExample(@"GET /bank/_search
			{
			  ""query"": { ""match"": { ""address"": ""mill"" } }
			}", e =>
			{
				// client does not support shorthand match query syntax. Expand out
				var body = JObject.Parse(e.Body);
				body["query"]["match"]["address"] = new JObject{{ "query", "mill" }};
				e.Body = body.ToString();
				return e;
			});
		}

		[U]
		public void Line897()
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
				var body = JObject.Parse(e.Body);
				body["query"]["match"]["address"] = new JObject{{ "query", "mill lane" }};
				e.Body = body.ToString();
				return e;
			});
		}

		[U]
		public void Line909()
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
				var body = JObject.Parse(e.Body);
				body["query"]["match_phrase"]["address"] = new JObject{{ "query", "mill lane" }};
				e.Body = body.ToString();
				return e;
			});
		}

		[U]
		public void Line923()
		{
			// tag::2de2349b7010652ca6104fb60f531a80[]
			var searchResponse = client.Search<Account>(s => s
				.Index("bank")
				.Query(q => q
					.Bool(b => b
						.Must(mu => mu
							.Match(m => m
								.Field(f => f.Address)
								.Query("mill")
							), mu => mu
							.Match(m => m
								.Field(f => f.Address)
								.Query("lane")
							)
						)
					)
				)
			);
			// end::2de2349b7010652ca6104fb60f531a80[]

			searchResponse.MatchesExample(@"GET /bank/_search
			{
			  ""query"": {
			    ""bool"": {
			      ""must"": [
			        { ""match"": { ""address"": ""mill"" } },
			        { ""match"": { ""address"": ""lane"" } }
			      ]
			    }
			  }
			}", e =>
			{
				// client does not support shorthand match query syntax. Expand out
				var body = JObject.Parse(e.Body);
				body["query"]["bool"]["must"][0]["match"]["address"] = new JObject{{ "query", "mill" }};
				body["query"]["bool"]["must"][1]["match"]["address"] = new JObject{{ "query", "lane" }};
				e.Body = body.ToString();
				return e;
			});
		}

		[U]
		public void Line944()
		{
			// tag::171d3a3af2d0f46cae5896c5bd3da4b5[]
			var searchResponse = client.Search<Account>(s => s
				.Index("bank")
				.Query(q => q
					.Bool(b => b
						.Should(sh => sh
								.Match(m => m
									.Field(f => f.Address)
									.Query("mill")
								), sh => sh
								.Match(m => m
									.Field(f => f.Address)
									.Query("lane")
								)
						)
					)
				)
			);
			// end::171d3a3af2d0f46cae5896c5bd3da4b5[]

			searchResponse.MatchesExample(@"GET /bank/_search
			{
			  ""query"": {
			    ""bool"": {
			      ""should"": [
			        { ""match"": { ""address"": ""mill"" } },
			        { ""match"": { ""address"": ""lane"" } }
			      ]
			    }
			  }
			}", e =>
			{
				// client does not support shorthand match query syntax. Expand out
				var body = JObject.Parse(e.Body);
				body["query"]["bool"]["should"][0]["match"]["address"] = new JObject{{ "query", "mill" }};
				body["query"]["bool"]["should"][1]["match"]["address"] = new JObject{{ "query", "lane" }};
				e.Body = body.ToString();
				return e;
			});
		}

		[U]
		public void Line965()
		{
			// tag::5d38d4da86157b897e4876674bd169ef[]
			var searchResponse = client.Search<Account>(s => s
				.Index("bank")
				.Query(q => q
					.Bool(b => b
						.MustNot(mn => mn
								.Match(m => m
									.Field(f => f.Address)
									.Query("mill")
								), mn => mn
								.Match(m => m
									.Field(f => f.Address)
									.Query("lane")
								)
						)
					)
				)
			);
			// end::5d38d4da86157b897e4876674bd169ef[]

			searchResponse.MatchesExample(@"GET /bank/_search
			{
			  ""query"": {
			    ""bool"": {
			      ""must_not"": [
			        { ""match"": { ""address"": ""mill"" } },
			        { ""match"": { ""address"": ""lane"" } }
			      ]
			    }
			  }
			}", e =>
			{
				// client does not support shorthand match query syntax. Expand out
				var body = JObject.Parse(e.Body);
				body["query"]["bool"]["must_not"][0]["match"]["address"] = new JObject{{ "query", "mill" }};
				body["query"]["bool"]["must_not"][1]["match"]["address"] = new JObject{{ "query", "lane" }};
				e.Body = body.ToString();
				return e;
			});
		}

		[U]
		public void Line988()
		{
			// tag::47bb632c6091ad0cd94bc660bdd309a5[]
			var searchResponse = client.Search<Account>(s => s
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
				var body = JObject.Parse(e.Body);
				body["query"]["bool"]["must"][0]["match"]["age"] = new JObject{{ "query", "40" }};
				body["query"]["bool"]["must_not"][0]["match"]["state"] = new JObject{{ "query", "ID" }};
				e.Body = body.ToString();
				return e;
			});
		}

		[U]
		public void Line1018()
		{
			// tag::251ea12c1248385ab409906ac64d9ee9[]
			var searchResponse = client.Search<Account>(s => s
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
			}");
		}

		[U]
		public void Line1051()
		{
			// tag::feefeb68144002fd1fff57b77b95b85e[]
			var searchResponse = client.Search<Account>(s => s
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
		public void Line1144()
		{
			// tag::cfbaea6f0df045c5d940bbb6a9c69cd8[]
			var searchResponse = client.Search<Account>(s => s
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
		public void Line1172()
		{
			// tag::645796e8047967ca4a7635a22a876f4c[]
			var searchResponse = client.Search<Account>(s => s
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
			}");
		}

		[U]
		public void Line1201()
		{
			// tag::c84b5f9c6528f84a08c5318b3385d55c[]
			var searchResponse = client.Search<Account>(s => s
				.Size(0)
				.Aggregations(a => a
					.Range("group_by_age", ra => ra
						.Field(f => f.Age)
						.Ranges(
							r => r.From(20).To(30),
							r => r.From(30).To(40),
							r => r.From(40).To(50)
						)
						.Aggregations(aa => aa
							.Terms("group_by_gender", t => t
								.Field(f => f.Gender.Suffix("keyword"))
								.Aggregations(aaa => aaa
									.Average("average_balance", av => av
										.Field(f => f.Balance)
									)
								)
							)
						)
					)
				)
			);
			// end::c84b5f9c6528f84a08c5318b3385d55c[]

			searchResponse.MatchesExample(@"GET /bank/_search
			{
			  ""size"": 0,
			  ""aggs"": {
			    ""group_by_age"": {
			      ""range"": {
			        ""field"": ""age"",
			        ""ranges"": [
			          {
			            ""from"": 20,
			            ""to"": 30
			          },
			          {
			            ""from"": 30,
			            ""to"": 40
			          },
			          {
			            ""from"": 40,
			            ""to"": 50
			          }
			        ]
			      },
			      ""aggs"": {
			        ""group_by_gender"": {
			          ""terms"": {
			            ""field"": ""gender.keyword""
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
			    }
			  }
			}");
		}
	}
}
