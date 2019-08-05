using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Aggregations
{
	public class MiscPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line19()
		{
			// tag::0827fcf75228b6d0206a1ffe6bf7d263[]
			var response0 = new SearchResponse<object>();
			// end::0827fcf75228b6d0206a1ffe6bf7d263[]

			response0.MatchesExample(@"GET /twitter/_search
			{
			  ""size"": 0,
			  ""aggregations"": {
			    ""my_agg"": {
			      ""terms"": {
			        ""field"": ""text""
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line46()
		{
			// tag::2d39331333f64fcc31fa298ac59b161f[]
			var response0 = new SearchResponse<object>();
			// end::2d39331333f64fcc31fa298ac59b161f[]

			response0.MatchesExample(@"GET /twitter/_search
			{
			  ""size"": 0,
			  ""aggs"": {
			    ""titles"": {
			      ""terms"": {
			        ""field"": ""title""
			      },
			      ""meta"": {
			        ""color"": ""blue""
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line98()
		{
			// tag::ea447f43ebd5f72c65de699904474d0d[]
			var response0 = new SearchResponse<object>();
			// end::ea447f43ebd5f72c65de699904474d0d[]

			response0.MatchesExample(@"GET /twitter/_search?typed_keys
			{
			  ""aggregations"": {
			    ""tweets_over_time"": {
			      ""date_histogram"": {
			        ""field"": ""date"",
			        ""calendar_interval"": ""year""
			      },
			      ""aggregations"": {
			        ""top_users"": {
			            ""top_hits"": {
			                ""size"": 1
			            }
			        }
			      }
			    }
			  }
			}");
		}
	}
}