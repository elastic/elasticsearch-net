// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Bucket
{
	public class FilterAggregationPage : ExampleBase
	{
		[U]
		[Description("aggregations/bucket/filter-aggregation.asciidoc:9")]
		public void Line9()
		{
			// tag::b93ed4ef309819734f0eeea82e8b0f1f[]
			var searchResponse = client.Search<object>(s => s
				.Index("sales")
				.Size(0)
				.Aggregations(a => a
					.Filter("t_shirts", f => f
						.Filter(q => q
							.Term("type", "t-shirt")
						)
						.Aggregations(aa => aa
							.Average("avg_price", av => av
								.Field("price")
							)
						)
					)
				)
			);
			// end::b93ed4ef309819734f0eeea82e8b0f1f[]

			searchResponse.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""t_shirts"" : {
			            ""filter"" : { ""term"": { ""type"": ""t-shirt"" } },
			            ""aggs"" : {
			                ""avg_price"" : { ""avg"" : { ""field"" : ""price"" } }
			            }
			        }
			    }
			}", (e, b) =>
			{
				e.Uri.Query = e.Uri.Query.Replace("size=0", string.Empty);
				b["size"] = 0;
				b["aggs"]["t_shirts"]["filter"]["term"]["type"].ToLongFormTermQuery();
			});
		}
	}
}
