// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Root
{
	public class ApiConventionsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("api-conventions.asciidoc:78")]
		public void Line78()
		{
			// tag::978088f989d45dd09339582e9cbc60e0[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::978088f989d45dd09339582e9cbc60e0[]

			response0.MatchesExample(@"# GET /<logstash-{now/d}>/_search");

			response1.MatchesExample(@"GET /%3Clogstash-%7Bnow%2Fd%7D%3E/_search
			{
			  ""query"" : {
			    ""match"": {
			      ""test"": ""data""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("api-conventions.asciidoc:132")]
		public void Line132()
		{
			// tag::a34d70d7022eb4ba48909d440c80390f[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::a34d70d7022eb4ba48909d440c80390f[]

			response0.MatchesExample(@"# GET /<logstash-{now/d-2d}>,<logstash-{now/d-1d}>,<logstash-{now/d}>/_search");

			response1.MatchesExample(@"GET /%3Clogstash-%7Bnow%2Fd-2d%7D%3E%2C%3Clogstash-%7Bnow%2Fd-1d%7D%3E%2C%3Clogstash-%7Bnow%2Fd%7D%3E/_search
			{
			  ""query"" : {
			    ""match"": {
			      ""test"": ""data""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("api-conventions.asciidoc:219")]
		public void Line219()
		{
			// tag::09dbd90c5e22ea4a17b4cf9aa72e08ae[]
			var response0 = new SearchResponse<object>();
			// end::09dbd90c5e22ea4a17b4cf9aa72e08ae[]

			response0.MatchesExample(@"GET /_search?q=elasticsearch&filter_path=took,hits.hits._id,hits.hits._score");
		}

		[U(Skip = "Example not implemented")]
		[Description("api-conventions.asciidoc:247")]
		public void Line247()
		{
			// tag::1dbb8cf17fbc45c87c7d2f75f15f9778[]
			var response0 = new SearchResponse<object>();
			// end::1dbb8cf17fbc45c87c7d2f75f15f9778[]

			response0.MatchesExample(@"GET /_cluster/state?filter_path=metadata.indices.*.stat*");
		}

		[U(Skip = "Example not implemented")]
		[Description("api-conventions.asciidoc:270")]
		public void Line270()
		{
			// tag::1252fa45847edba5ec2b2f33da70ec5b[]
			var response0 = new SearchResponse<object>();
			// end::1252fa45847edba5ec2b2f33da70ec5b[]

			response0.MatchesExample(@"GET /_cluster/state?filter_path=routing_table.indices.**.state");
		}

		[U(Skip = "Example not implemented")]
		[Description("api-conventions.asciidoc:295")]
		public void Line295()
		{
			// tag::621665fdbd7fc103c09bfeed28b67b1a[]
			var response0 = new SearchResponse<object>();
			// end::621665fdbd7fc103c09bfeed28b67b1a[]

			response0.MatchesExample(@"GET /_count?filter_path=-_shards");
		}

		[U(Skip = "Example not implemented")]
		[Description("api-conventions.asciidoc:314")]
		public void Line314()
		{
			// tag::1e18a67caf8f06ff2710ec4a8b30f625[]
			var response0 = new SearchResponse<object>();
			// end::1e18a67caf8f06ff2710ec4a8b30f625[]

			response0.MatchesExample(@"GET /_cluster/state?filter_path=metadata.indices.*.state,-metadata.indices.logstash-*");
		}

		[U(Skip = "Example not implemented")]
		[Description("api-conventions.asciidoc:341")]
		public void Line341()
		{
			// tag::6464124d1677f4552ddddd95a340ca3a[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::6464124d1677f4552ddddd95a340ca3a[]

			response0.MatchesExample(@"POST /library/_doc?refresh
			{""title"": ""Book #1"", ""rating"": 200.1}");

			response1.MatchesExample(@"POST /library/_doc?refresh
			{""title"": ""Book #2"", ""rating"": 1.7}");

			response2.MatchesExample(@"POST /library/_doc?refresh
			{""title"": ""Book #3"", ""rating"": 0.1}");

			response3.MatchesExample(@"GET /_search?filter_path=hits.hits._source&_source=title&sort=rating:desc");
		}

		[U(Skip = "Example not implemented")]
		[Description("api-conventions.asciidoc:374")]
		public void Line374()
		{
			// tag::b9a153725b28fdd0a5aabd7f17a8c2d7[]
			var response0 = new SearchResponse<object>();
			// end::b9a153725b28fdd0a5aabd7f17a8c2d7[]

			response0.MatchesExample(@"GET twitter/_settings?flat_settings=true");
		}

		[U(Skip = "Example not implemented")]
		[Description("api-conventions.asciidoc:405")]
		public void Line405()
		{
			// tag::5925c23a173a63bdb30b458248d1df76[]
			var response0 = new SearchResponse<object>();
			// end::5925c23a173a63bdb30b458248d1df76[]

			response0.MatchesExample(@"GET twitter/_settings?flat_settings=false");
		}

		[U(Skip = "Example not implemented")]
		[Description("api-conventions.asciidoc:570")]
		public void Line570()
		{
			// tag::a6f8636b03cc5f677b7d89e750328612[]
			var response0 = new SearchResponse<object>();
			// end::a6f8636b03cc5f677b7d89e750328612[]

			response0.MatchesExample(@"POST /twitter/_search?size=surprise_me");
		}

		[U(Skip = "Example not implemented")]
		[Description("api-conventions.asciidoc:602")]
		public void Line602()
		{
			// tag::6d1e75312a28a5ba23837abf768f2510[]
			var response0 = new SearchResponse<object>();
			// end::6d1e75312a28a5ba23837abf768f2510[]

			response0.MatchesExample(@"POST /twitter/_search?size=surprise_me&error_trace=true");
		}
	}
}