using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class MultiMatchQueryPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line11()
		{
			// tag::53b908c3432118c5a6e460f74d32006b[]
			var response0 = new SearchResponse<object>();
			// end::53b908c3432118c5a6e460f74d32006b[]

			response0.MatchesExample(@"GET /_search
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
		[SkipExample]
		public void Line33()
		{
			// tag::6a1702dd50690cae833572e48a0ddf25[]
			var response0 = new SearchResponse<object>();
			// end::6a1702dd50690cae833572e48a0ddf25[]

			response0.MatchesExample(@"GET /_search
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
		[SkipExample]
		public void Line50()
		{
			// tag::e30ea6e3823a139d7693d8cce1920a06[]
			var response0 = new SearchResponse<object>();
			// end::e30ea6e3823a139d7693d8cce1920a06[]

			response0.MatchesExample(@"GET /_search
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
		[SkipExample]
		public void Line114()
		{
			// tag::5da6efd5b038ada64c9e853c88c1ec47[]
			var response0 = new SearchResponse<object>();
			// end::5da6efd5b038ada64c9e853c88c1ec47[]

			response0.MatchesExample(@"GET /_search
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
		[SkipExample]
		public void Line132()
		{
			// tag::b0eaf67e5cce24ef8889bf20951ccec1[]
			var response0 = new SearchResponse<object>();
			// end::b0eaf67e5cce24ef8889bf20951ccec1[]

			response0.MatchesExample(@"GET /_search
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
			}");
		}

		[U]
		[SkipExample]
		public void Line173()
		{
			// tag::e270f3f721a5712cd11a5ca03554f5b0[]
			var response0 = new SearchResponse<object>();
			// end::e270f3f721a5712cd11a5ca03554f5b0[]

			response0.MatchesExample(@"GET /_search
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
		[SkipExample]
		public void Line216()
		{
			// tag::7b908b1189f076942de8cd497ff1fa59[]
			var response0 = new SearchResponse<object>();
			// end::7b908b1189f076942de8cd497ff1fa59[]

			response0.MatchesExample(@"GET /_search
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
		[SkipExample]
		public void Line233()
		{
			// tag::6bbc613bd4f9aec1bbdbabf5db021d28[]
			var response0 = new SearchResponse<object>();
			// end::6bbc613bd4f9aec1bbdbabf5db021d28[]

			response0.MatchesExample(@"GET /_search
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
			}");
		}

		[U]
		[SkipExample]
		public void Line264()
		{
			// tag::0e118857b815b62118a30c042f079db1[]
			var response0 = new SearchResponse<object>();
			// end::0e118857b815b62118a30c042f079db1[]

			response0.MatchesExample(@"GET /_search
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
		[SkipExample]
		public void Line281()
		{
			// tag::33f148e3d8676de6cc52f58749898a13[]
			var response0 = new SearchResponse<object>();
			// end::33f148e3d8676de6cc52f58749898a13[]

			response0.MatchesExample(@"GET /_search
			{
			  ""query"": {
			    ""dis_max"": {
			      ""queries"": [
			        { ""match_phrase_prefix"": { ""subject"": ""quick brown f"" }},
			        { ""match_phrase_prefix"": { ""message"": ""quick brown f"" }}
			      ]
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line348()
		{
			// tag::047266b0d20fdb62ebc72d51952c8f6d[]
			var response0 = new SearchResponse<object>();
			// end::047266b0d20fdb62ebc72d51952c8f6d[]

			response0.MatchesExample(@"GET /_search
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
		[SkipExample]
		public void Line408()
		{
			// tag::ad0dcbc7fc619e952c8825b8f307b7b2[]
			var response0 = new SearchResponse<object>();
			// end::ad0dcbc7fc619e952c8825b8f307b7b2[]

			response0.MatchesExample(@"GET /_search
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
		[SkipExample]
		public void Line447()
		{
			// tag::3cd50a789b8e1f0ebbbc53a8d7ecf656[]
			var response0 = new SearchResponse<object>();
			// end::3cd50a789b8e1f0ebbbc53a8d7ecf656[]

			response0.MatchesExample(@"GET /_search
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
		[SkipExample]
		public void Line482()
		{
			// tag::179f0a3e84ff4bbac18787a018eabf89[]
			var response0 = new SearchResponse<object>();
			// end::179f0a3e84ff4bbac18787a018eabf89[]

			response0.MatchesExample(@"GET /_search
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
		[SkipExample]
		public void Line535()
		{
			// tag::68721288dc9ad8aa1b55099b4d303051[]
			var response0 = new SearchResponse<object>();
			// end::68721288dc9ad8aa1b55099b4d303051[]

			response0.MatchesExample(@"GET /_search
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