// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;
using Elastic.Transport;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Examples.Root
{
	public class ApiConventionsPage : ExampleBase
	{
		[U]
		[Description("api-conventions.asciidoc:88")]
		public void Line88()
		{
			// tag::978088f989d45dd09339582e9cbc60e0[]
			var searchResponse = client.Search<object>(s => s
				.Index("<logstash-{now/d}>")
				.Query(q => q
					.Match(m => m
						.Field("test")
						.Query("data")
					)
				)
			);
			// end::978088f989d45dd09339582e9cbc60e0[]

			searchResponse.MatchesExample(@"GET /%3Clogstash-%7Bnow%2Fd%7D%3E/_search
			{
			  ""query"" : {
			    ""match"": {
			      ""test"": ""data""
			    }
			  }
			}", (e, b) => b["query"]["match"]["test"].ToLongFormQuery());
		}

		[U]
		[Description("api-conventions.asciidoc:142")]
		public void Line142()
		{
			// tag::a34d70d7022eb4ba48909d440c80390f[]
			var searchResponse = client.Search<object>(s => s
				.Index("<logstash-{now/d-2d}>,<logstash-{now/d-1d}>,<logstash-{now/d}>")
				.Query(q => q
					.Match(m => m
						.Field("test")
						.Query("data")
					)
				)
			);
			// end::a34d70d7022eb4ba48909d440c80390f[]

			searchResponse.MatchesExample(@"GET /%3Clogstash-%7Bnow%2Fd-2d%7D%3E%2C%3Clogstash-%7Bnow%2Fd-1d%7D%3E%2C%3Clogstash-%7Bnow%2Fd%7D%3E/_search
			{
			  ""query"" : {
			    ""match"": {
			      ""test"": ""data""
			    }
			  }
			}", (e, b) => b["query"]["match"]["test"].ToLongFormQuery());
		}

		[U]
		[Description("api-conventions.asciidoc:231")]
		public void Line231()
		{
			// tag::09dbd90c5e22ea4a17b4cf9aa72e08ae[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.QueryOnQueryString("elasticsearch")
				.FilterPath("took","hits.hits._id","hits.hits._score") // <1> Using filter path can result in a response that cannot be parsed by the client's serializer. In these cases, using the low level client and parsing the JSON response may be preferred.
			);
			// end::09dbd90c5e22ea4a17b4cf9aa72e08ae[]

			searchResponse.MatchesExample(@"GET /_search?q=elasticsearch&filter_path=took,hits.hits._id,hits.hits._score");
		}

		[U]
		[Description("api-conventions.asciidoc:259")]
		public void Line259()
		{
			// tag::1dbb8cf17fbc45c87c7d2f75f15f9778[]
			var stateResponse = client.Cluster.State(selector: c => c
				.FilterPath("metadata.indices.*.stat*") // <1> Using filter path can result in a response that cannot be parsed by the client's serializer. In these cases, using the low level client and parsing the JSON response may be preferred.
			);
			// end::1dbb8cf17fbc45c87c7d2f75f15f9778[]

			stateResponse.MatchesExample(@"GET /_cluster/state?filter_path=metadata.indices.*.stat*");
		}

		[U]
		[Description("api-conventions.asciidoc:282")]
		public void Line282()
		{
			// tag::1252fa45847edba5ec2b2f33da70ec5b[]
			var stateResponse = client.Cluster.State(selector: c => c
				.FilterPath("routing_table.indices.**.state") // <1> Using filter path can result in a response that cannot be parsed by the client's serializer. In these cases, using the low level client and parsing the JSON response may be preferred.
			);
			// end::1252fa45847edba5ec2b2f33da70ec5b[]

			stateResponse.MatchesExample(@"GET /_cluster/state?filter_path=routing_table.indices.**.state");
		}

		[U]
		[Description("api-conventions.asciidoc:307")]
		public void Line307()
		{
			// tag::621665fdbd7fc103c09bfeed28b67b1a[]
			var countResponse = client.Count<object>(c => c
				.AllIndices()
				.FilterPath("-_shards") // <1> Using filter path can result in a response that cannot be parsed by the client's serializer. In these cases, using the low level client and parsing the JSON response may be preferred.
			);
			// end::621665fdbd7fc103c09bfeed28b67b1a[]

			countResponse.MatchesExample(@"GET /_count?filter_path=-_shards", e =>
			{
				e.Uri.Path = "/_all" + e.Uri.Path;
			});
		}

		[U]
		[Description("api-conventions.asciidoc:326")]
		public void Line326()
		{
			// tag::1e18a67caf8f06ff2710ec4a8b30f625[]
			var stateResponse = client.Cluster.State(selector: c => c
				.FilterPath("metadata.indices.*.state,-metadata.indices.logstash-*") // <1> Using filter path can result in a response that cannot be parsed by the client's serializer. In these cases, using the low level client and parsing the JSON response may be preferred.
			);
			// end::1e18a67caf8f06ff2710ec4a8b30f625[]

			stateResponse.MatchesExample(@"GET /_cluster/state?filter_path=metadata.indices.*.state,-metadata.indices.logstash-*");
		}

		[U]
		[Description("api-conventions.asciidoc:353")]
		public void Line353()
		{
			// tag::6464124d1677f4552ddddd95a340ca3a[]
			var indexResponse1 = client.Index(new
			{
				title = "Book #1",
				rating = 200.1
			}, i => i.Index("library").Refresh(Refresh.True));

			var indexResponse2 = client.Index(new
			{
				title = "Book #2",
				rating = 1.7
			}, i => i.Index("library").Refresh(Refresh.True));

			var indexResponse3 = client.Index(new
			{
				title = "Book #3",
				rating = 0.1
			}, i => i.Index("library").Refresh(Refresh.True));

			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.FilterPath("hits.hits._source") // <1> Using filter path can result in a response that cannot be parsed by the client's serializer. In these cases, using the low level client and parsing the JSON response may be preferred.
				.Source(so => so
					.Includes(f => f.Field("title"))
				)
				.Sort(so => so
					.Field("rating", SortOrder.Descending)
				)
			);
			// end::6464124d1677f4552ddddd95a340ca3a[]

			indexResponse1.MatchesExample(@"POST /library/_doc?refresh
			{""title"": ""Book #1"", ""rating"": 200.1}", e => e.Uri.Query = e.Uri.Query.Replace("refresh", "refresh=true"));

			indexResponse2.MatchesExample(@"POST /library/_doc?refresh
			{""title"": ""Book #2"", ""rating"": 1.7}", e => e.Uri.Query = e.Uri.Query.Replace("refresh", "refresh=true"));

			indexResponse3.MatchesExample(@"POST /library/_doc?refresh
			{""title"": ""Book #3"", ""rating"": 0.1}", e => e.Uri.Query = e.Uri.Query.Replace("refresh", "refresh=true"));

			searchResponse.MatchesExample(@"GET /_search?filter_path=hits.hits._source&_source=title&sort=rating:desc", e =>
			{
				e.Method = HttpMethod.POST;
				e.Uri.Query = e.Uri.Query.Replace("&sort=rating:desc", string.Empty);
				e.Uri.Query = e.Uri.Query.Replace("&_source=title", string.Empty);
				e.ApplyBodyChanges(b =>
				{
					b["_source"] = new JObject
					{
						["includes"] = new JArray("title")
					};
					b["sort"] = new JArray(new JObject
					{
						["rating"] = new JObject
						{
							["order"] = "desc"
						}
					});
				});
			});
		}

		[U]
		[Description("api-conventions.asciidoc:386")]
		public void Line386()
		{
			// tag::b9a153725b28fdd0a5aabd7f17a8c2d7[]
			var settingsResponse = client.Indices.GetSettings("twitter", s => s
				.FlatSettings()
			);
			// end::b9a153725b28fdd0a5aabd7f17a8c2d7[]

			settingsResponse.MatchesExample(@"GET twitter/_settings?flat_settings=true");
		}

		[U]
		[Description("api-conventions.asciidoc:416")]
		public void Line416()
		{
			// tag::5925c23a173a63bdb30b458248d1df76[]
			var settingsResponse = client.Indices.GetSettings("twitter", s => s
				.FlatSettings(false)
			);
			// end::5925c23a173a63bdb30b458248d1df76[]

			settingsResponse.MatchesExample(@"GET twitter/_settings?flat_settings=false");
		}

		[U]
		[Description("api-conventions.asciidoc:580")]
		public void Line580()
		{
			// tag::a6f8636b03cc5f677b7d89e750328612[]
			var searchResponse = client.Search<object>(s =>
			{
				IRequest request = s;
				request.RequestParameters.SetQueryString("size", "surprise_me"); // <1> The high level client provides a strongly typed method to set "size" which does not allow a string value to be set. This can be circumvented by accessing the underlying request interface
				return s.Index("twitter");
			});
			// end::a6f8636b03cc5f677b7d89e750328612[]

			searchResponse.MatchesExample(@"POST /twitter/_search?size=surprise_me");
		}

		[U]
		[Description("api-conventions.asciidoc:612")]
		public void Line612()
		{
			// tag::6d1e75312a28a5ba23837abf768f2510[]
			var searchResponse = client.Search<object>(s =>
			{
				IRequest request = s;
				request.RequestParameters.SetQueryString("size", "surprise_me"); // <1> The high level client provides a strongly typed method to set "size" which does not allow a string value to be set. This can be circumvented by accessing the underlying request interface
				return s.Index("twitter").ErrorTrace();
			});
			// end::6d1e75312a28a5ba23837abf768f2510[]

			searchResponse.MatchesExample(@"POST /twitter/_search?size=surprise_me&error_trace=true");
		}
	}
}
