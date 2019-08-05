using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Root
{
	public class GettingStartedPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line209()
		{
			// tag::f8cc4b331a19ff4df8e4a490f906ee69[]
			var response0 = new SearchResponse<object>();
			// end::f8cc4b331a19ff4df8e4a490f906ee69[]

			response0.MatchesExample(@"GET /_cat/health?v");
		}

		[U]
		[SkipExample]
		public void Line240()
		{
			// tag::db20adb70a8e8d0709d15ba0daf18d23[]
			var response0 = new SearchResponse<object>();
			// end::db20adb70a8e8d0709d15ba0daf18d23[]

			response0.MatchesExample(@"GET /_cat/nodes?v");
		}

		[U]
		[SkipExample]
		public void Line263()
		{
			// tag::c3fa04519df668d6c27727a12ab09648[]
			var response0 = new SearchResponse<object>();
			// end::c3fa04519df668d6c27727a12ab09648[]

			response0.MatchesExample(@"GET /_cat/indices?v");
		}

		[U]
		[SkipExample]
		public void Line284()
		{
			// tag::0caf6b6b092ecbcf6f996053678a2384[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::0caf6b6b092ecbcf6f996053678a2384[]

			response0.MatchesExample(@"PUT /customer?pretty");

			response1.MatchesExample(@"GET /_cat/indices?v");
		}

		[U]
		[SkipExample]
		public void Line311()
		{
			// tag::21fe98843fe0b5473f515d21803db409[]
			var response0 = new SearchResponse<object>();
			// end::21fe98843fe0b5473f515d21803db409[]

			response0.MatchesExample(@"PUT /customer/_doc/1?pretty
			{
			  ""name"": ""John Doe""
			}");
		}

		[U]
		[SkipExample]
		public void Line347()
		{
			// tag::37a3b66b555aed55618254f50d572cd6[]
			var response0 = new SearchResponse<object>();
			// end::37a3b66b555aed55618254f50d572cd6[]

			response0.MatchesExample(@"GET /customer/_doc/1?pretty");
		}

		[U]
		[SkipExample]
		public void Line378()
		{
			// tag::92e3c75133dc4fdb2cc65f149001b40b[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::92e3c75133dc4fdb2cc65f149001b40b[]

			response0.MatchesExample(@"DELETE /customer?pretty");

			response1.MatchesExample(@"GET /_cat/indices?v");
		}

		[U]
		[SkipExample]
		public void Line398()
		{
			// tag::fa5f618ced25ed2e67a1439bb77ba340[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::fa5f618ced25ed2e67a1439bb77ba340[]

			response0.MatchesExample(@"PUT /customer");

			response1.MatchesExample(@"PUT /customer/_doc/1
			{
			  ""name"": ""John Doe""
			}");

			response2.MatchesExample(@"GET /customer/_doc/1");

			response3.MatchesExample(@"DELETE /customer");
		}

		[U]
		[SkipExample]
		public void Line442()
		{
			// tag::75bda7da7fefff2c16f0423a9aa41c6e[]
			var response0 = new SearchResponse<object>();
			// end::75bda7da7fefff2c16f0423a9aa41c6e[]

			response0.MatchesExample(@"PUT /customer/_doc/1?pretty
			{
			  ""name"": ""Jane Doe""
			}");
		}

		[U]
		[SkipExample]
		public void Line454()
		{
			// tag::37c778346eb67042df5e8edf2485e40a[]
			var response0 = new SearchResponse<object>();
			// end::37c778346eb67042df5e8edf2485e40a[]

			response0.MatchesExample(@"PUT /customer/_doc/2?pretty
			{
			  ""name"": ""Jane Doe""
			}");
		}

		[U]
		[SkipExample]
		public void Line470()
		{
			// tag::1c0f3b0bc4b7e53b53755fd3bda5b8cf[]
			var response0 = new SearchResponse<object>();
			// end::1c0f3b0bc4b7e53b53755fd3bda5b8cf[]

			response0.MatchesExample(@"POST /customer/_doc?pretty
			{
			  ""name"": ""Jane Doe""
			}");
		}

		[U]
		[SkipExample]
		public void Line489()
		{
			// tag::6a8d7b34f2b42d5992aaa1ff147062d9[]
			var response0 = new SearchResponse<object>();
			// end::6a8d7b34f2b42d5992aaa1ff147062d9[]

			response0.MatchesExample(@"POST /customer/_update/1?pretty
			{
			  ""doc"": { ""name"": ""Jane Doe"" }
			}");
		}

		[U]
		[SkipExample]
		public void Line501()
		{
			// tag::731621af937d66170347b9cc6b4a3c48[]
			var response0 = new SearchResponse<object>();
			// end::731621af937d66170347b9cc6b4a3c48[]

			response0.MatchesExample(@"POST /customer/_update/1?pretty
			{
			  ""doc"": { ""name"": ""Jane Doe"", ""age"": 20 }
			}");
		}

		[U]
		[SkipExample]
		public void Line513()
		{
			// tag::38dfa309717488362d0f784e17ebd1b5[]
			var response0 = new SearchResponse<object>();
			// end::38dfa309717488362d0f784e17ebd1b5[]

			response0.MatchesExample(@"POST /customer/_update/1?pretty
			{
			  ""script"" : ""ctx._source.age += 5""
			}");
		}

		[U]
		[SkipExample]
		public void Line532()
		{
			// tag::9c5ef83db886840355ff662b6e9ae8ab[]
			var response0 = new SearchResponse<object>();
			// end::9c5ef83db886840355ff662b6e9ae8ab[]

			response0.MatchesExample(@"DELETE /customer/_doc/2?pretty");
		}

		[U]
		[SkipExample]
		public void Line550()
		{
			// tag::7d32a32357b5ea8819b72608fcc6fd07[]
			var response0 = new SearchResponse<object>();
			// end::7d32a32357b5ea8819b72608fcc6fd07[]

			response0.MatchesExample(@"POST /customer/_bulk?pretty
			{""index"":{""_id"":""1""}}
			{""name"": ""John Doe"" }
			{""index"":{""_id"":""2""}}
			{""name"": ""Jane Doe"" }");
		}

		[U]
		[SkipExample]
		public void Line562()
		{
			// tag::193864342d9f0a36ec84a91ca325f5ec[]
			var response0 = new SearchResponse<object>();
			// end::193864342d9f0a36ec84a91ca325f5ec[]

			response0.MatchesExample(@"POST /customer/_bulk?pretty
			{""update"":{""_id"":""1""}}
			{""doc"": { ""name"": ""John Doe becomes Jane Doe"" } }
			{""delete"":{""_id"":""2""}}");
		}

		[U]
		[SkipExample]
		public void Line647()
		{
			// tag::c181969ef91c3b4a2513c1885be98e26[]
			var response0 = new SearchResponse<object>();
			// end::c181969ef91c3b4a2513c1885be98e26[]

			response0.MatchesExample(@"GET /bank/_search?q=*&sort=account_number:asc&pretty");
		}

		[U]
		[SkipExample]
		public void Line720()
		{
			// tag::506844befdc5691d835771bcbb1c1a60[]
			var response0 = new SearchResponse<object>();
			// end::506844befdc5691d835771bcbb1c1a60[]

			response0.MatchesExample(@"GET /bank/_search
			{
			  ""query"": { ""match_all"": {} },
			  ""sort"": [
			    { ""account_number"": ""asc"" }
			  ]
			}");
		}

		[U]
		[SkipExample]
		public void Line789()
		{
			// tag::345ea7e9cb5af9e052ce0cf6f1f52c23[]
			var response0 = new SearchResponse<object>();
			// end::345ea7e9cb5af9e052ce0cf6f1f52c23[]

			response0.MatchesExample(@"GET /bank/_search
			{
			  ""query"": { ""match_all"": {} }
			}");
		}

		[U]
		[SkipExample]
		public void Line805()
		{
			// tag::3d7527bb7ac3b0e1f97b22bdfeb99070[]
			var response0 = new SearchResponse<object>();
			// end::3d7527bb7ac3b0e1f97b22bdfeb99070[]

			response0.MatchesExample(@"GET /bank/_search
			{
			  ""query"": { ""match_all"": {} },
			  ""size"": 1
			}");
		}

		[U]
		[SkipExample]
		public void Line820()
		{
			// tag::3c31f9eb032861bff64abd8b14758991[]
			var response0 = new SearchResponse<object>();
			// end::3c31f9eb032861bff64abd8b14758991[]

			response0.MatchesExample(@"GET /bank/_search
			{
			  ""query"": { ""match_all"": {} },
			  ""from"": 10,
			  ""size"": 10
			}");
		}

		[U]
		[SkipExample]
		public void Line836()
		{
			// tag::e8035a7476601ad4b136edb250f92d53[]
			var response0 = new SearchResponse<object>();
			// end::e8035a7476601ad4b136edb250f92d53[]

			response0.MatchesExample(@"GET /bank/_search
			{
			  ""query"": { ""match_all"": {} },
			  ""sort"": { ""balance"": { ""order"": ""desc"" } }
			}");
		}

		[U]
		[SkipExample]
		public void Line854()
		{
			// tag::b8459547da50aebddbcdd1aaaac02b5f[]
			var response0 = new SearchResponse<object>();
			// end::b8459547da50aebddbcdd1aaaac02b5f[]

			response0.MatchesExample(@"GET /bank/_search
			{
			  ""query"": { ""match_all"": {} },
			  ""_source"": [""account_number"", ""balance""]
			}");
		}

		[U]
		[SkipExample]
		public void Line873()
		{
			// tag::2e6bfd38c9bcb728227f0d4dd11c09a2[]
			var response0 = new SearchResponse<object>();
			// end::2e6bfd38c9bcb728227f0d4dd11c09a2[]

			response0.MatchesExample(@"GET /bank/_search
			{
			  ""query"": { ""match"": { ""account_number"": 20 } }
			}");
		}

		[U]
		[SkipExample]
		public void Line885()
		{
			// tag::b8eab60f6441edf314306d8194c7cd56[]
			var response0 = new SearchResponse<object>();
			// end::b8eab60f6441edf314306d8194c7cd56[]

			response0.MatchesExample(@"GET /bank/_search
			{
			  ""query"": { ""match"": { ""address"": ""mill"" } }
			}");
		}

		[U]
		[SkipExample]
		public void Line897()
		{
			// tag::cd247f267968aa0927bfdad56852f8f5[]
			var response0 = new SearchResponse<object>();
			// end::cd247f267968aa0927bfdad56852f8f5[]

			response0.MatchesExample(@"GET /bank/_search
			{
			  ""query"": { ""match"": { ""address"": ""mill lane"" } }
			}");
		}

		[U]
		[SkipExample]
		public void Line909()
		{
			// tag::231aa0bb39c35fe199d28fe0e4a62b2e[]
			var response0 = new SearchResponse<object>();
			// end::231aa0bb39c35fe199d28fe0e4a62b2e[]

			response0.MatchesExample(@"GET /bank/_search
			{
			  ""query"": { ""match_phrase"": { ""address"": ""mill lane"" } }
			}");
		}

		[U]
		[SkipExample]
		public void Line923()
		{
			// tag::2de2349b7010652ca6104fb60f531a80[]
			var response0 = new SearchResponse<object>();
			// end::2de2349b7010652ca6104fb60f531a80[]

			response0.MatchesExample(@"GET /bank/_search
			{
			  ""query"": {
			    ""bool"": {
			      ""must"": [
			        { ""match"": { ""address"": ""mill"" } },
			        { ""match"": { ""address"": ""lane"" } }
			      ]
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line944()
		{
			// tag::171d3a3af2d0f46cae5896c5bd3da4b5[]
			var response0 = new SearchResponse<object>();
			// end::171d3a3af2d0f46cae5896c5bd3da4b5[]

			response0.MatchesExample(@"GET /bank/_search
			{
			  ""query"": {
			    ""bool"": {
			      ""should"": [
			        { ""match"": { ""address"": ""mill"" } },
			        { ""match"": { ""address"": ""lane"" } }
			      ]
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line965()
		{
			// tag::5d38d4da86157b897e4876674bd169ef[]
			var response0 = new SearchResponse<object>();
			// end::5d38d4da86157b897e4876674bd169ef[]

			response0.MatchesExample(@"GET /bank/_search
			{
			  ""query"": {
			    ""bool"": {
			      ""must_not"": [
			        { ""match"": { ""address"": ""mill"" } },
			        { ""match"": { ""address"": ""lane"" } }
			      ]
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line988()
		{
			// tag::47bb632c6091ad0cd94bc660bdd309a5[]
			var response0 = new SearchResponse<object>();
			// end::47bb632c6091ad0cd94bc660bdd309a5[]

			response0.MatchesExample(@"GET /bank/_search
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
			}");
		}

		[U]
		[SkipExample]
		public void Line1018()
		{
			// tag::251ea12c1248385ab409906ac64d9ee9[]
			var response0 = new SearchResponse<object>();
			// end::251ea12c1248385ab409906ac64d9ee9[]

			response0.MatchesExample(@"GET /bank/_search
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
		[SkipExample]
		public void Line1051()
		{
			// tag::feefeb68144002fd1fff57b77b95b85e[]
			var response0 = new SearchResponse<object>();
			// end::feefeb68144002fd1fff57b77b95b85e[]

			response0.MatchesExample(@"GET /bank/_search
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
		[SkipExample]
		public void Line1144()
		{
			// tag::cfbaea6f0df045c5d940bbb6a9c69cd8[]
			var response0 = new SearchResponse<object>();
			// end::cfbaea6f0df045c5d940bbb6a9c69cd8[]

			response0.MatchesExample(@"GET /bank/_search
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
		[SkipExample]
		public void Line1172()
		{
			// tag::645796e8047967ca4a7635a22a876f4c[]
			var response0 = new SearchResponse<object>();
			// end::645796e8047967ca4a7635a22a876f4c[]

			response0.MatchesExample(@"GET /bank/_search
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
		[SkipExample]
		public void Line1201()
		{
			// tag::c84b5f9c6528f84a08c5318b3385d55c[]
			var response0 = new SearchResponse<object>();
			// end::c84b5f9c6528f84a08c5318b3385d55c[]

			response0.MatchesExample(@"GET /bank/_search
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