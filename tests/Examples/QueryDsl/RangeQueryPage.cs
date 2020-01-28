using System;
using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class RangeQueryPage : ExampleBase
	{
		[U]
		public void Line16()
		{
			// tag::97bcd92ef148312d41e69f0d18284327[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.LongRange(r => r
						.Field("age")
						.GreaterThanOrEquals(10)
						.LessThanOrEquals(20)
						.Boost(2)
					)
				)
			);
			// end::97bcd92ef148312d41e69f0d18284327[]

			searchResponse.MatchesExample(@"GET _search
			{
			    ""query"": {
			        ""range"" : {
			            ""age"" : {
			                ""gte"" : 10,
			                ""lte"" : 20,
			                ""boost"" : 2.0
			            }
			        }
			    }
			}");
		}

		[U]
		public void Line152()
		{
			// tag::4466d410e06712c63328de4db249e6da[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.DateRange(r => r
						.Field("timestamp")
						.GreaterThanOrEquals("now-1d/d")
						.LessThan("now/d")
					)
				)
			);
			// end::4466d410e06712c63328de4db249e6da[]

			searchResponse.MatchesExample(@"GET _search
			{
			    ""query"": {
			        ""range"" : {
			            ""timestamp"" : {
			                ""gte"" : ""now-1d/d"",
			                ""lt"" :  ""now/d""
			            }
			        }
			    }
			}");
		}

		[U]
		public void Line214()
		{
			// tag::5d13a71fa7fda73b15111803b1c7cfd3[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.DateRange(r => r
						.Field("timestamp")
						.TimeZone("+01:00")
						.GreaterThanOrEquals("2015-01-01 00:00:00")
						.LessThanOrEquals("now")
					)
				)
			);
			// end::5d13a71fa7fda73b15111803b1c7cfd3[]

			searchResponse.MatchesExample(@"GET _search
			{
			    ""query"": {
			        ""range"" : {
			            ""timestamp"" : {
			                ""time_zone"": ""+01:00"", \<1>
			                ""gte"": ""2015-01-01 00:00:00"", \<2>
			                ""lte"": ""now"" \<3>
			            }
			        }
			    }
			}");
		}
	}
}
