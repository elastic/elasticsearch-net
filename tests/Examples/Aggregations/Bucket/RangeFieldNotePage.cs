// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Bucket
{
	public class RangeFieldNotePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/range-field-note.asciidoc:12")]
		public void Line12()
		{
			// tag::dbcd8892dd01c43d5a60c94173574faf[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::dbcd8892dd01c43d5a60c94173574faf[]

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
			        ""type"": ""date_range"",
			        ""format"": ""yyyy-MM-dd||epoch_millis""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT range_index/_doc/1?refresh
			{
			  ""expected_attendees"" : {
			    ""gte"" : 10,
			    ""lte"" : 20
			  },
			  ""time_frame"" : {
			    ""gte"" : ""2019-10-28"",
			    ""lte"" : ""2019-11-04""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/range-field-note.asciidoc:49")]
		public void Line49()
		{
			// tag::cf1872035b9acf6a214310badae345f7[]
			var response0 = new SearchResponse<object>();
			// end::cf1872035b9acf6a214310badae345f7[]

			response0.MatchesExample(@"POST /range_index/_search?size=0
			{
			    ""aggs"" : {
			        ""range_histo"" : {
			            ""histogram"" : {
			                ""field"" : ""expected_attendees"",
			                ""interval"" : 5
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/range-field-note.asciidoc:105")]
		public void Line105()
		{
			// tag::620ffff06483f13e5143dfddaa8b7b30[]
			var response0 = new SearchResponse<object>();
			// end::620ffff06483f13e5143dfddaa8b7b30[]

			response0.MatchesExample(@"POST /range_index/_search?size=0
			{
			    ""query"": {
			      ""range"": {
			        ""time_frame"": {
			          ""gte"": ""2019-11-01"",
			          ""format"": ""yyyy-MM-dd""
			        }
			      }
			    },
			    ""aggs"" : {
			        ""november_data"" : {
			            ""date_histogram"" : {
			                ""field"" : ""time_frame"",
			                ""calendar_interval"" : ""day"",
			                ""format"": ""yyyy-MM-dd""
			              }
			        }
			    }
			}");
		}
	}
}
