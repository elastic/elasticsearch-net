// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ingest.Processors
{
	public class GeoipPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ingest/processors/geoip.asciidoc:46")]
		public void Line46()
		{
			// tag::0b6aa8f2d6916951959d6186b25d2b54[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::0b6aa8f2d6916951959d6186b25d2b54[]

			response0.MatchesExample(@"PUT _ingest/pipeline/geoip
			{
			  ""description"" : ""Add geoip info"",
			  ""processors"" : [
			    {
			      ""geoip"" : {
			        ""field"" : ""ip""
			      }
			    }
			  ]
			}");

			response1.MatchesExample(@"PUT my_index/_doc/my_id?pipeline=geoip
			{
			  ""ip"": ""8.8.8.8""
			}");

			response2.MatchesExample(@"GET my_index/_doc/my_id");
		}

		[U(Skip = "Example not implemented")]
		[Description("ingest/processors/geoip.asciidoc:93")]
		public void Line93()
		{
			// tag::573a466d7a3a8e31194666e2ecc1d92a[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::573a466d7a3a8e31194666e2ecc1d92a[]

			response0.MatchesExample(@"PUT _ingest/pipeline/geoip
			{
			  ""description"" : ""Add geoip info"",
			  ""processors"" : [
			    {
			      ""geoip"" : {
			        ""field"" : ""ip"",
			        ""target_field"" : ""geo"",
			        ""database_file"" : ""GeoLite2-Country.mmdb""
			      }
			    }
			  ]
			}");

			response1.MatchesExample(@"PUT my_index/_doc/my_id?pipeline=geoip
			{
			  ""ip"": ""8.8.8.8""
			}");

			response2.MatchesExample(@"GET my_index/_doc/my_id");
		}

		[U(Skip = "Example not implemented")]
		[Description("ingest/processors/geoip.asciidoc:144")]
		public void Line144()
		{
			// tag::c5681f52305e065ef13c3e0ad5393263[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::c5681f52305e065ef13c3e0ad5393263[]

			response0.MatchesExample(@"PUT _ingest/pipeline/geoip
			{
			  ""description"" : ""Add geoip info"",
			  ""processors"" : [
			    {
			      ""geoip"" : {
			        ""field"" : ""ip""
			      }
			    }
			  ]
			}");

			response1.MatchesExample(@"PUT my_index/_doc/my_id?pipeline=geoip
			{
			  ""ip"": ""80.231.5.0""
			}");

			response2.MatchesExample(@"GET my_index/_doc/my_id");
		}

		[U(Skip = "Example not implemented")]
		[Description("ingest/processors/geoip.asciidoc:193")]
		public void Line193()
		{
			// tag::0737ebaea33631f001fb3f4226948492[]
			var response0 = new SearchResponse<object>();
			// end::0737ebaea33631f001fb3f4226948492[]

			response0.MatchesExample(@"PUT my_ip_locations
			{
			  ""mappings"": {
			    ""properties"": {
			      ""geoip"": {
			        ""properties"": {
			          ""location"": { ""type"": ""geo_point"" }
			        }
			      }
			    }
			  }
			}");
		}
	}
}
