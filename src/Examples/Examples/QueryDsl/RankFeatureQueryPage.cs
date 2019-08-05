using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class RankFeatureQueryPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line57()
		{
			// tag::e2750d69bcb6d4c7e16e704cd0fb3530[]
			var response0 = new SearchResponse<object>();
			// end::e2750d69bcb6d4c7e16e704cd0fb3530[]

			response0.MatchesExample(@"PUT /test
			{
			  ""mappings"": {
			    ""properties"": {
			      ""pagerank"": {
			        ""type"": ""rank_feature""
			      },
			      ""url_length"": {
			        ""type"": ""rank_feature"",
			        ""positive_score_impact"": false
			      },
			      ""topics"": {
			        ""type"": ""rank_features""
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line83()
		{
			// tag::c786505cf972dd41bd0cbb6ebcf939e9[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::c786505cf972dd41bd0cbb6ebcf939e9[]

			response0.MatchesExample(@"PUT /test/_doc/1?refresh
			{
			  ""url"": ""http://en.wikipedia.org/wiki/2016_Summer_Olympics"",
			  ""content"": ""Rio 2016"",
			  ""pagerank"": 50.3,
			  ""url_length"": 42,
			  ""topics"": {
			    ""sports"": 50,
			    ""brazil"": 30
			  }
			}");

			response1.MatchesExample(@"PUT /test/_doc/2?refresh
			{
			  ""url"": ""http://en.wikipedia.org/wiki/2016_Brazilian_Grand_Prix"",
			  ""content"": ""Formula One motor race held on 13 November 2016"",
			  ""pagerank"": 50.3,
			  ""url_length"": 47,
			  ""topics"": {
			    ""sports"": 35,
			    ""formula one"": 65,
			    ""brazil"": 20
			  }
			}");

			response2.MatchesExample(@"PUT /test/_doc/3?refresh
			{
			  ""url"": ""http://en.wikipedia.org/wiki/Deadpool_(film)"",
			  ""content"": ""Deadpool is a 2016 American superhero film"",
			  ""pagerank"": 50.3,
			  ""url_length"": 37,
			  ""topics"": {
			    ""movies"": 60,
			    ""super hero"": 65
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line130()
		{
			// tag::fd0cd8ecd03468726b59a605eea06d75[]
			var response0 = new SearchResponse<object>();
			// end::fd0cd8ecd03468726b59a605eea06d75[]

			response0.MatchesExample(@"GET /test/_search
			{
			  ""query"": {
			    ""bool"": {
			      ""must"": [
			        {
			          ""match"": {
			            ""content"": ""2016""
			          }
			        }
			      ],
			      ""should"": [
			        {
			          ""rank_feature"": {
			            ""field"": ""pagerank""
			          }
			        },
			        {
			          ""rank_feature"": {
			            ""field"": ""url_length"",
			            ""boost"": 0.1
			          }
			        },
			        {
			          ""rank_feature"": {
			            ""field"": ""topics.sports"",
			            ""boost"": 0.4
			          }
			        }
			      ]
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line236()
		{
			// tag::309f0721145b5c656338a02459c3ff1e[]
			var response0 = new SearchResponse<object>();
			// end::309f0721145b5c656338a02459c3ff1e[]

			response0.MatchesExample(@"GET /test/_search
			{
			  ""query"": {
			    ""rank_feature"": {
			      ""field"": ""pagerank"",
			      ""saturation"": {
			        ""pivot"": 8
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line257()
		{
			// tag::0c05c66cfe3a2169b1ec1aba77e26db2[]
			var response0 = new SearchResponse<object>();
			// end::0c05c66cfe3a2169b1ec1aba77e26db2[]

			response0.MatchesExample(@"GET /test/_search
			{
			  ""query"": {
			    ""rank_feature"": {
			      ""field"": ""pagerank"",
			      ""saturation"": {}
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line279()
		{
			// tag::e89bf0d893b7bf43c2d9b44db6cfe21b[]
			var response0 = new SearchResponse<object>();
			// end::e89bf0d893b7bf43c2d9b44db6cfe21b[]

			response0.MatchesExample(@"GET /test/_search
			{
			  ""query"": {
			    ""rank_feature"": {
			      ""field"": ""pagerank"",
			      ""log"": {
			        ""scaling_factor"": 4
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line306()
		{
			// tag::9e3c28d5820c38ea117eb2e9a5061089[]
			var response0 = new SearchResponse<object>();
			// end::9e3c28d5820c38ea117eb2e9a5061089[]

			response0.MatchesExample(@"GET /test/_search
			{
			  ""query"": {
			    ""rank_feature"": {
			      ""field"": ""pagerank"",
			      ""sigmoid"": {
			        ""pivot"": 7,
			        ""exponent"": 0.6
			      }
			    }
			  }
			}");
		}
	}
}