using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class DistanceFeatureQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line37()
		{
			// tag::b81a7b5f5ef19553f9cd49196f31018c[]
			var response0 = new SearchResponse<object>();
			// end::b81a7b5f5ef19553f9cd49196f31018c[]

			response0.MatchesExample(@"PUT /items
			{
			  ""mappings"": {
			    ""properties"": {
			      ""name"": {
			        ""type"": ""keyword""
			      },
			      ""production_date"": {
			        ""type"": ""date""
			      },
			      ""location"": {
			        ""type"": ""geo_point""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line63()
		{
			// tag::b0d3f839237fabf8cdc2221734c668ad[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::b0d3f839237fabf8cdc2221734c668ad[]

			response0.MatchesExample(@"PUT /items/_doc/1?refresh
			{
			  ""name"" : ""chocolate"",
			  ""production_date"": ""2018-02-01"",
			  ""location"": [-71.34, 41.12]
			}");

			response1.MatchesExample(@"PUT /items/_doc/2?refresh
			{
			  ""name"" : ""chocolate"",
			  ""production_date"": ""2018-01-01"",
			  ""location"": [-71.3, 41.15]
			}");

			response2.MatchesExample(@"");

			response3.MatchesExample(@"PUT /items/_doc/3?refresh
			{
			  ""name"" : ""chocolate"",
			  ""production_date"": ""2017-12-01"",
			  ""location"": [-71.3, 41.12]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line100()
		{
			// tag::1e2c5cef7a3f254c71a33865eb4d7569[]
			var response0 = new SearchResponse<object>();
			// end::1e2c5cef7a3f254c71a33865eb4d7569[]

			response0.MatchesExample(@"GET /items/_search
			{
			  ""query"": {
			    ""bool"": {
			      ""must"": {
			        ""match"": {
			          ""name"": ""chocolate""
			        }
			      },
			      ""should"": {
			        ""distance_feature"": {
			          ""field"": ""production_date"",
			          ""pivot"": ""7d"",
			          ""origin"": ""now""
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line130()
		{
			// tag::57a3e8d2ca64e37e90d658c4cd935399[]
			var response0 = new SearchResponse<object>();
			// end::57a3e8d2ca64e37e90d658c4cd935399[]

			response0.MatchesExample(@"GET /items/_search
			{
			  ""query"": {
			    ""bool"": {
			      ""must"": {
			        ""match"": {
			          ""name"": ""chocolate""
			        }
			      },
			      ""should"": {
			        ""distance_feature"": {
			          ""field"": ""location"",
			          ""pivot"": ""1000m"",
			          ""origin"": [-71.3, 41.15]
			        }
			      }
			    }
			  }
			}");
		}
	}
}