// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;
using Newtonsoft.Json.Linq;

namespace Examples.Mapping.Params
{
	public class MultiFieldsPage : ExampleBase
	{
		[U]
		[Description("mapping/params/multi-fields.asciidoc:10")]
		public void Line10()
		{
			// tag::5271f4ff29bb48838396e5a674664ee0[]
			var createIndexResponse = client.Indices.Create("my_index", c => c
				.Map(m => m
					.Properties(p => p
						.Text(t => t
							.Name("city")
							.Fields(f => f
								.Keyword(k => k
									.Name("raw")
								)
							)
						)
					)
				)
			);

			var indexResponse1 = client.Index(new
			{
				city = "New York"
			}, i => i.Index("my_index").Id(1));

			var indexResponse2 = client.Index(new
			{
				city = "York"
			}, i => i.Index("my_index").Id(2));

			var searchResponse = client.Search<object>(s => s
				.Index("my_index")
				.Query(q => q
					.Match(m => m
						.Field("city")
						.Query("york")
					)
				)
				.Sort(so => so
					.Field("city.raw", SortOrder.Ascending)
				)
				.Aggregations(a => a
					.Terms("Cities", t => t
						.Field("city.raw")
					)
				)
			);
			// end::5271f4ff29bb48838396e5a674664ee0[]

			createIndexResponse.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""city"": {
			        ""type"": ""text"",
			        ""fields"": {
			          ""raw"": { \<1>
			            ""type"":  ""keyword""
			          }
			        }
			      }
			    }
			  }
			}");

			indexResponse1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""city"": ""New York""
			}");

			indexResponse2.MatchesExample(@"PUT my_index/_doc/2
			{
			  ""city"": ""York""
			}");

			searchResponse.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""match"": {
			      ""city"": ""york"" \<2>
			    }
			  },
			  ""sort"": {
			    ""city.raw"": ""asc"" \<3>
			  },
			  ""aggs"": {
			    ""Cities"": {
			      ""terms"": {
			        ""field"": ""city.raw"" \<3>
			      }
			    }
			  }
			}", (e,b) =>
			{

				b["query"]["match"]["city"].ToLongFormQuery();
				b["sort"] = new JArray(new JObject
				{
					["city.raw"] = new JObject
					{
						["order"] = "asc"
					}
				});
			});
		}

		[U]
		[Description("mapping/params/multi-fields.asciidoc:75")]
		public void Line75()
		{
			// tag::fc8097bdfb6f3a4017bf4186ccca8a84[]
			var createIndexResponse = client.Indices.Create("my_index", c => c
				.Map(m => m
					.Properties(p => p
						.Text(t => t
							.Name("text")
							.Fields(f => f
								.Text(k => k
									.Name("english")
									.Analyzer("english")
								)
							)
						)
					)
				)
			);

			var indexResponse1 = client.Index(new
			{
				text = "quick brown fox"
			}, i => i.Index("my_index").Id(1));

			var indexResponse2 = client.Index(new
			{
				text = "quick brown foxes"
			}, i => i.Index("my_index").Id(2));

			var searchResponse = client.Search<object>(s => s
				.Index("my_index")
				.Query(q => q
					.MultiMatch(m => m
						.Fields(new [] { "text", "text.english" })
						.Query("quick brown foxes")
						.Type(TextQueryType.MostFields)
					)
				)
			);
			// end::fc8097bdfb6f3a4017bf4186ccca8a84[]

			createIndexResponse.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""text"": { \<1>
			        ""type"": ""text"",
			        ""fields"": {
			          ""english"": { \<2>
			            ""type"":     ""text"",
			            ""analyzer"": ""english""
			          }
			        }
			      }
			    }
			  }
			}");

			indexResponse1.MatchesExample(@"PUT my_index/_doc/1
			{ ""text"": ""quick brown fox"" } \<3>");

			indexResponse2.MatchesExample(@"PUT my_index/_doc/2
			{ ""text"": ""quick brown foxes"" } \<3>");

			searchResponse.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""multi_match"": {
			      ""query"": ""quick brown foxes"",
			      ""fields"": [ \<4>
			        ""text"",
			        ""text.english""
			      ],
			      ""type"": ""most_fields"" \<4>
			    }
			  }
			}");
		}
	}
}
