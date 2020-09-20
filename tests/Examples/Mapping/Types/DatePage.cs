// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Newtonsoft.Json.Linq;

namespace Examples.Mapping.Types
{
	public class DatePage : ExampleBase
	{
		[U]
		[Description("mapping/types/date.asciidoc:35")]
		public void Line35()
		{
			// tag::645136747d37368a14ab34de8bd046c6[]
			var createIndexResponse = client.Indices.Create("my_index", c => c
				.Map(m => m
					.Properties(p => p
						.Date(d => d
							.Name("date")
						)
					)
				)
			);

			var indexResponse = client.Index(
				new { date = "2015-01-01" },
				i => i.Id(1).Index("my_index"));

			var indexResponse2 = client.Index(
				new { date = "2015-01-01T12:10:30Z" },
				i => i.Id(2).Index("my_index"));

			var indexResponse3 = client.Index(
				new { date = 1420070400001 },
				i => i.Id(3).Index("my_index"));

			var searchResponse = client.Search<object>(s => s
				.Index("my_index")
				.Sort(so => so
					.Ascending("date")
				)
			);
			// end::645136747d37368a14ab34de8bd046c6[]

			createIndexResponse.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""date"": {
			        ""type"": ""date"" \<1>
			      }
			    }
			  }
			}");

			indexResponse.MatchesExample(@"PUT my_index/_doc/1
			{ ""date"": ""2015-01-01"" } \<2>");

			indexResponse2.MatchesExample(@"PUT my_index/_doc/2
			{ ""date"": ""2015-01-01T12:10:30Z"" } \<3>");

			indexResponse3.MatchesExample(@"PUT my_index/_doc/3
			{ ""date"": 1420070400001 } \<4>");

			searchResponse.MatchesExample(@"GET my_index/_search
			{
			  ""sort"": { ""date"": ""asc""} \<5>
			}", e => e.ApplyBodyChanges(json =>
			{
				json["sort"] = new JArray(new JObject
				{
					{ "date", new JObject { { "order", "asc" } }}
				});
			}));
		}

		[U]
		[Description("mapping/types/date.asciidoc:77")]
		public void Line77()
		{
			// tag::e2a042c629429855c3bcaefffb26b7fa[]
			var createIndexResponse = client.Indices.Create("my_index", c => c
				.Map(m => m
					.Properties(p => p
						.Date(d => d
							.Name("date")
							.Format("yyyy-MM-dd HH:mm:ss||yyyy-MM-dd||epoch_millis")
						)
					)
				)
			);
			// end::e2a042c629429855c3bcaefffb26b7fa[]

			createIndexResponse.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""date"": {
			        ""type"":   ""date"",
			        ""format"": ""yyyy-MM-dd HH:mm:ss||yyyy-MM-dd||epoch_millis""
			      }
			    }
			  }
			}");
		}
	}
}
