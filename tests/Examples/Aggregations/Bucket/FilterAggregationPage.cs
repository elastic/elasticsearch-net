/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
