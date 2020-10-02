// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class MultiMatchQueryPage : ExampleBase
	{
		[U]
		[Description("query-dsl/multi-match-query.asciidoc:11")]
		public void Line11()
		{
			// tag::53b908c3432118c5a6e460f74d32006b[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q =>
					q.MultiMatch(c => c
						.Query("this is a test")
						.Fields(new[] { "subject", "message" })
					)
				)
			);
			// end::53b908c3432118c5a6e460f74d32006b[]

			searchResponse.MatchesExample(@"GET /_search
			{
			  ""query"": {
			    ""multi_match"" : {
			      ""query"":    ""this is a test"", \<1>
			      ""fields"": [ ""subject"", ""message"" ] \<2>
			    }
			  }
			}");
		}

		[U]
		[Description("query-dsl/multi-match-query.asciidoc:33")]
		public void Line33()
		{
			// tag::6a1702dd50690cae833572e48a0ddf25[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q =>
					q.MultiMatch(c => c
						.Query("Will Smith")
						.Fields("title, *_name")
					)
				)
			);
			// end::6a1702dd50690cae833572e48a0ddf25[]

			searchResponse.MatchesExample(@"GET /_search
			{
			  ""query"": {
			    ""multi_match"" : {
			      ""query"":    ""Will Smith"",
			      ""fields"": [ ""title"", ""*_name"" ] \<1>
			    }
			  }
			}");
		}

		[U]
		[Description("query-dsl/multi-match-query.asciidoc:50")]
		public void Line50()
		{
			// tag::e30ea6e3823a139d7693d8cce1920a06[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q =>
					q.MultiMatch(c => c
						.Query("this is a test")
						.Fields(new[] { "subject^3", "message" })
					)
				)
			);
			// end::e30ea6e3823a139d7693d8cce1920a06[]

			searchResponse.MatchesExample(@"GET /_search
			{
			  ""query"": {
			    ""multi_match"" : {
			      ""query"" : ""this is a test"",
			      ""fields"" : [ ""subject^3"", ""message"" ] \<1>
			    }
			  }
			}");
		}

		[U]
		[Description("query-dsl/multi-match-query.asciidoc:113")]
		public void Line113()
		{
			// tag::5da6efd5b038ada64c9e853c88c1ec47[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q =>
					q.MultiMatch(c => c
						.Query("brown fox")
						.Type(TextQueryType.BestFields)
						.Fields(new[] { "subject", "message" })
						.TieBreaker(0.3)
					)
				)
			);
			// end::5da6efd5b038ada64c9e853c88c1ec47[]

			searchResponse.MatchesExample(@"GET /_search
			{
			  ""query"": {
			    ""multi_match"" : {
			      ""query"":      ""brown fox"",
			      ""type"":       ""best_fields"",
			      ""fields"":     [ ""subject"", ""message"" ],
			      ""tie_breaker"": 0.3
			    }
			  }
			}");
		}

		[U]
		[Description("query-dsl/multi-match-query.asciidoc:130")]
		public void Line130()
		{
			// tag::b0eaf67e5cce24ef8889bf20951ccec1[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q =>
					q.DisMax(c => c
						.Queries(
							qs => qs.Match(m => m.Field("subject").Query("brown fox")),
							qs => qs.Match(m => m.Field("message").Query("brown fox"))
						)
						.TieBreaker(0.3)
					)
				)
			);
			// end::b0eaf67e5cce24ef8889bf20951ccec1[]

			searchResponse.MatchesExample(@"GET /_search
			{
			  ""query"": {
			    ""dis_max"": {
			      ""queries"": [
			        { ""match"": { ""subject"": ""brown fox"" }},
			        { ""match"": { ""message"": ""brown fox"" }}
			      ],
			      ""tie_breaker"": 0.3
			    }
			  }
			}", (e, body) =>
			{
				body["query"]["dis_max"]["queries"][0]["match"]["subject"].ToLongFormQuery();
				body["query"]["dis_max"]["queries"][1]["match"]["message"].ToLongFormQuery();
			});
		}

		[U]
		[Description("query-dsl/multi-match-query.asciidoc:170")]
		public void Line170()
		{
			// tag::e270f3f721a5712cd11a5ca03554f5b0[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q =>
					q.MultiMatch(c => c
						.Query("Will Smith")
						.Type(TextQueryType.BestFields)
						.Fields(new[] { "first_name", "last_name" })
						.Operator(Operator.And)
					)
				)
			);
			// end::e270f3f721a5712cd11a5ca03554f5b0[]

			searchResponse.MatchesExample(@"GET /_search
			{
			  ""query"": {
			    ""multi_match"" : {
			      ""query"":      ""Will Smith"",
			      ""type"":       ""best_fields"",
			      ""fields"":     [ ""first_name"", ""last_name"" ],
			      ""operator"":   ""and"" \<1>
			    }
			  }
			}");
		}

		[U]
		[Description("query-dsl/multi-match-query.asciidoc:212")]
		public void Line212()
		{
			// tag::7b908b1189f076942de8cd497ff1fa59[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q =>
					q.MultiMatch(c => c
						.Query("quick brown fox")
						.Type(TextQueryType.MostFields)
						.Fields(new[] { "title", "title.original", "title.shingles" })
					)
				)
			);
			// end::7b908b1189f076942de8cd497ff1fa59[]

			searchResponse.MatchesExample(@"GET /_search
			{
			  ""query"": {
			    ""multi_match"" : {
			      ""query"":      ""quick brown fox"",
			      ""type"":       ""most_fields"",
			      ""fields"":     [ ""title"", ""title.original"", ""title.shingles"" ]
			    }
			  }
			}");
		}

		[U]
		[Description("query-dsl/multi-match-query.asciidoc:228")]
		public void Line228()
		{
			// tag::6bbc613bd4f9aec1bbdbabf5db021d28[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q =>
					q.Bool(c => c
						.Should(
							qs => qs.Match(m => m.Field("title").Query("quick brown fox")),
							qs => qs.Match(m => m.Field("title.original").Query("quick brown fox")),
							qs => qs.Match(m => m.Field("title.shingles").Query("quick brown fox"))
						)
					)
				)
			);
			// end::6bbc613bd4f9aec1bbdbabf5db021d28[]

			searchResponse.MatchesExample(@"GET /_search
			{
			  ""query"": {
			    ""bool"": {
			      ""should"": [
			        { ""match"": { ""title"":          ""quick brown fox"" }},
			        { ""match"": { ""title.original"": ""quick brown fox"" }},
			        { ""match"": { ""title.shingles"": ""quick brown fox"" }}
			      ]
			    }
			  }
			}", (e, body) =>
			{
				body["query"]["bool"]["should"][0]["match"]["title"].ToLongFormQuery();
				body["query"]["bool"]["should"][1]["match"]["title.original"].ToLongFormQuery();
				body["query"]["bool"]["should"][2]["match"]["title.shingles"].ToLongFormQuery();
			});
		}

		[U]
		[Description("query-dsl/multi-match-query.asciidoc:259")]
		public void Line259()
		{
			// tag::0e118857b815b62118a30c042f079db1[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q =>
					q.MultiMatch(c => c
						.Query("quick brown f")
						.Type(TextQueryType.PhrasePrefix)
						.Fields(new[] { "subject", "message" })
					)
				)
			);
			// end::0e118857b815b62118a30c042f079db1[]

			searchResponse.MatchesExample(@"GET /_search
			{
			  ""query"": {
			    ""multi_match"" : {
			      ""query"":      ""quick brown f"",
			      ""type"":       ""phrase_prefix"",
			      ""fields"":     [ ""subject"", ""message"" ]
			    }
			  }
			}");
		}

		[U]
		[Description("query-dsl/multi-match-query.asciidoc:275")]
		public void Line275()
		{
			// tag::33f148e3d8676de6cc52f58749898a13[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q =>
					q.DisMax(c => c
						.Queries(
							qs => qs.MatchPhrasePrefix(m => m.Field("subject").Query("quick brown f")),
							qs => qs.MatchPhrasePrefix(m => m.Field("message").Query("quick brown f"))
						)
					)
				)
			);
			// end::33f148e3d8676de6cc52f58749898a13[]

			searchResponse.MatchesExample(@"GET /_search
			{
			  ""query"": {
			    ""dis_max"": {
			      ""queries"": [
			        { ""match_phrase_prefix"": { ""subject"": ""quick brown f"" }},
			        { ""match_phrase_prefix"": { ""message"": ""quick brown f"" }}
			      ]
			    }
			  }
			}", (e, body) =>
			{
				body["query"]["dis_max"]["queries"][0]["match_phrase_prefix"]["subject"].ToLongFormQuery();
				body["query"]["dis_max"]["queries"][1]["match_phrase_prefix"]["message"].ToLongFormQuery();
			});
		}

		[U]
		[Description("query-dsl/multi-match-query.asciidoc:341")]
		public void Line341()
		{
			// tag::047266b0d20fdb62ebc72d51952c8f6d[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q =>
					q.MultiMatch(c => c
						.Query("Will Smith")
						.Type(TextQueryType.CrossFields)
						.Fields(new[] { "first_name", "last_name" })
						.Operator(Operator.And)
					)
				)
			);
			// end::047266b0d20fdb62ebc72d51952c8f6d[]

			searchResponse.MatchesExample(@"GET /_search
			{
			  ""query"": {
			    ""multi_match"" : {
			      ""query"":      ""Will Smith"",
			      ""type"":       ""cross_fields"",
			      ""fields"":     [ ""first_name"", ""last_name"" ],
			      ""operator"":   ""and""
			    }
			  }
			}");
		}

		[U]
		[Description("query-dsl/multi-match-query.asciidoc:400")]
		public void Line400()
		{
			// tag::ad0dcbc7fc619e952c8825b8f307b7b2[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q =>
					q.MultiMatch(c => c
						.Query("Jon")
						.Type(TextQueryType.CrossFields)
						.Fields(new[] { "first", "first.edge", "last", "last.edge" })
					)
				)
			);
			// end::ad0dcbc7fc619e952c8825b8f307b7b2[]

			searchResponse.MatchesExample(@"GET /_search
			{
			  ""query"": {
			    ""multi_match"" : {
			      ""query"":      ""Jon"",
			      ""type"":       ""cross_fields"",
			      ""fields"":     [
			        ""first"", ""first.edge"",
			        ""last"",  ""last.edge""
			      ]
			    }
			  }
			}");
		}

		[U]
		[Description("query-dsl/multi-match-query.asciidoc:438")]
		public void Line438()
		{
			// tag::3cd50a789b8e1f0ebbbc53a8d7ecf656[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q =>
					q.Bool(b =>
						b.Should(
							sh => sh.MultiMatch(c => c
								.Query("Will Smith")
								.Type(TextQueryType.CrossFields)
								.Fields(new[] { "first", "last" })
								.MinimumShouldMatch(MinimumShouldMatch.Percentage(50))
							),
							sh => sh.MultiMatch(c => c
								.Query("Will Smith")
								.Type(TextQueryType.CrossFields)
								.Fields("*.edge")
							)
						)
					)
				)
			);
			// end::3cd50a789b8e1f0ebbbc53a8d7ecf656[]

			searchResponse.MatchesExample(@"GET /_search
			{
			  ""query"": {
			    ""bool"": {
			      ""should"": [
			        {
			          ""multi_match"" : {
			            ""query"":      ""Will Smith"",
			            ""type"":       ""cross_fields"",
			            ""fields"":     [ ""first"", ""last"" ],
			            ""minimum_should_match"": ""50%"" \<1>
			          }
			        },
			        {
			          ""multi_match"" : {
			            ""query"":      ""Will Smith"",
			            ""type"":       ""cross_fields"",
			            ""fields"":     [ ""*.edge"" ]
			          }
			        }
			      ]
			    }
			  }
			}");
		}

		[U]
		[Description("query-dsl/multi-match-query.asciidoc:472")]
		public void Line472()
		{
			// tag::179f0a3e84ff4bbac18787a018eabf89[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q =>
					q.MultiMatch(c => c
						.Query("Jon")
						.Type(TextQueryType.CrossFields)
						.Analyzer("standard")
						.Fields(new[] { "first", "last", "*.edge" })
					)
				)
			);
			// end::179f0a3e84ff4bbac18787a018eabf89[]

			searchResponse.MatchesExample(@"GET /_search
			{
			  ""query"": {
			   ""multi_match"" : {
			      ""query"":      ""Jon"",
			      ""type"":       ""cross_fields"",
			      ""analyzer"":   ""standard"", \<1>
			      ""fields"":     [ ""first"", ""last"", ""*.edge"" ]
			    }
			  }
			}");
		}

		[U]
		[Description("query-dsl/multi-match-query.asciidoc:524")]
		public void Line524()
		{
			// tag::68721288dc9ad8aa1b55099b4d303051[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q =>
					q.MultiMatch(c => c
						.Query("quick brown f")
						.Type(TextQueryType.BoolPrefix)
						.Fields(new[] { "subject", "message" })
					)
				)
			);
			// end::68721288dc9ad8aa1b55099b4d303051[]

			searchResponse.MatchesExample(@"GET /_search
			{
			  ""query"": {
			    ""multi_match"" : {
			      ""query"":      ""quick brown f"",
			      ""type"":       ""bool_prefix"",
			      ""fields"":     [ ""subject"", ""message"" ]
			    }
			  }
			}");
		}
	}
}
