// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Mapping.Types
{
	public class RangePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/types/range.asciidoc:21")]
		public void Line21()
		{
			// tag::2b371fbf0654d76436d49f5703d6c137[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::2b371fbf0654d76436d49f5703d6c137[]

			response0.MatchesExample(@"PUT range_index
			{
			  ""settings"": {
			    ""number_of_shards"": 2
			  },
			  ""mappings"": {
			    ""properties"": {
			      ""expected_attendees"": {
			        ""type"": ""integer_range""
			      },
			      ""time_frame"": {
			        ""type"": ""date_range"", \<1>
			        ""format"": ""yyyy-MM-dd HH:mm:ss||yyyy-MM-dd||epoch_millis""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT range_index/_doc/1?refresh
			{
			  ""expected_attendees"" : { \<2>
			    ""gte"" : 10,
			    ""lte"" : 20
			  },
			  ""time_frame"" : { \<3>
			    ""gte"" : ""2015-10-31 12:00:00"", \<4>
			    ""lte"" : ""2015-11-01""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/range.asciidoc:62")]
		public void Line62()
		{
			// tag::84edb44c5b74426f448b2baa101092d6[]
			var response0 = new SearchResponse<object>();
			// end::84edb44c5b74426f448b2baa101092d6[]

			response0.MatchesExample(@"GET range_index/_search
			{
			  ""query"" : {
			    ""term"" : {
			      ""expected_attendees"" : {
			        ""value"": 12
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/range.asciidoc:117")]
		public void Line117()
		{
			// tag::1572696b97822d3332be51700e09672f[]
			var response0 = new SearchResponse<object>();
			// end::1572696b97822d3332be51700e09672f[]

			response0.MatchesExample(@"GET range_index/_search
			{
			  ""query"" : {
			    ""range"" : {
			      ""time_frame"" : { \<1>
			        ""gte"" : ""2015-10-31"",
			        ""lte"" : ""2015-11-01"",
			        ""relation"" : ""within"" \<2>
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/range.asciidoc:182")]
		public void Line182()
		{
			// tag::7f514e9e785e4323d16396359cb184f2[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::7f514e9e785e4323d16396359cb184f2[]

			response0.MatchesExample(@"PUT range_index/_mapping
			{
			  ""properties"": {
			    ""ip_allowlist"": {
			      ""type"": ""ip_range""
			    }
			  }
			}");

			response1.MatchesExample(@"PUT range_index/_doc/2
			{
			  ""ip_allowlist"" : ""192.168.0.0/16""
			}");
		}
	}
}
