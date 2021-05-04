// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Search
{
	public class RunASearchPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/run-a-search.asciidoc:42")]
		public void Line42()
		{
			// tag::07f136f15a9021d6dcb1e1598a9be91d[]
			var response0 = new SearchResponse<object>();
			// end::07f136f15a9021d6dcb1e1598a9be91d[]

			response0.MatchesExample(@"PUT /user_logs_000001/_bulk?refresh
			{""index"":{""_index"" : ""user_logs_000001"", ""_id"" : ""1""}}
			{ ""@timestamp"": ""2020-12-06T11:04:05.000Z"", ""user"": { ""id"": ""vlb44hny"" }, ""message"": ""Login attempt failed"" }
			{""index"":{""_index"" : ""user_logs_000001"", ""_id"" : ""2""}}
			{ ""@timestamp"": ""2020-12-07T11:06:07.000Z"", ""user"": { ""id"": ""8a4f500d"" }, ""message"": ""Login successful"" }
			{""index"":{""_index"" : ""user_logs_000001"", ""_id"" : ""3""}}
			{ ""@timestamp"": ""2020-12-07T11:07:08.000Z"", ""user"": { ""id"": ""l7gk7f82"" }, ""message"": ""Logout successful"" }");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/run-a-search.asciidoc:58")]
		public void Line58()
		{
			// tag::8d841549879130ec46be9c2a56ab3e8b[]
			var response0 = new SearchResponse<object>();
			// end::8d841549879130ec46be9c2a56ab3e8b[]

			response0.MatchesExample(@"GET /user_logs_000001/_search?q=user.id:8a4f500d");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/run-a-search.asciidoc:119")]
		public void Line119()
		{
			// tag::8c711ea9d86aee29cadcf556e3ad1615[]
			var response0 = new SearchResponse<object>();
			// end::8c711ea9d86aee29cadcf556e3ad1615[]

			response0.MatchesExample(@"GET /user_logs_000001/_search
			{
			  ""query"": {
			    ""match"": {
			      ""message"": ""login successful""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/run-a-search.asciidoc:212")]
		public void Line212()
		{
			// tag::52429c9be200a725ef6a088ebc7d64bc[]
			var response0 = new SearchResponse<object>();
			// end::52429c9be200a725ef6a088ebc7d64bc[]

			response0.MatchesExample(@"GET /user_logs_000001,user_logs_000002/_search
			{
			  ""query"": {
			    ""match"": {
			      ""message"": ""login successful""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/run-a-search.asciidoc:236")]
		public void Line236()
		{
			// tag::efc9883d5b4bb2244487de1089220844[]
			var response0 = new SearchResponse<object>();
			// end::efc9883d5b4bb2244487de1089220844[]

			response0.MatchesExample(@"GET /user_logs*/_search
			{
			  ""query"": {
			    ""match"": {
			      ""message"": ""login successful""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/run-a-search.asciidoc:258")]
		public void Line258()
		{
			// tag::20361b63921d2ac82e1e22c1bcf68de5[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::20361b63921d2ac82e1e22c1bcf68de5[]

			response0.MatchesExample(@"GET /_search
			{
			  ""query"": {
			    ""match"": {
			      ""message"": ""login successful""
			    }
			  }
			}");

			response1.MatchesExample(@"GET /_all/_search
			{
			  ""query"": {
			    ""match"": {
			      ""message"": ""login successful""
			    }
			  }
			}");

			response2.MatchesExample(@"GET /*/_search
			{
			    ""query"" : {
			        ""match"" : { ""message"" : ""login"" }
			    }
			}");
		}
	}
}