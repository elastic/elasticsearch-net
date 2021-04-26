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
using Examples.Models;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class QueryFilterContextPage : ExampleBase
	{
		[U]
		[Description("query-dsl/query_filter_context.asciidoc:62")]
		public void Line62()
		{
			// tag::f29a28fffa7ec604a33a838f48f7ea79[]
			var searchResponse = client.Search<Blog>(s => s
				.AllIndices()
				.Query(q =>
					q.Match(m => m.Field(p => p.Title).Query("Search"))
					&& q.Match(m => m.Field(p => p.Content).Query("Elasticsearch"))
					&& +q.Term(m => m.Field(p => p.Status).Value(PublishStatus.Published))
					&& +q.DateRange(m => m
						.Field(p => p.PublishDate)
						.GreaterThanOrEquals("2015-01-01")
					)
				)
			);
			// end::f29a28fffa7ec604a33a838f48f7ea79[]

			searchResponse.MatchesExample(@"GET /_search
			{
			  ""query"": { \<1>
			    ""bool"": { \<2>
			      ""must"": [
			        { ""match"": { ""title"":   ""Search""        }},
			        { ""match"": { ""content"": ""Elasticsearch"" }}
			      ],
			      ""filter"": [ \<3>
			        { ""term"":  { ""status"": ""published"" }},
			        { ""range"": { ""publish_date"": { ""gte"": ""2015-01-01"" }}}
			      ]
			    }
			  }
			}", e =>
			{
				e.ApplyBodyChanges(b =>
				{
					var must = b["query"]["bool"]["must"];
					var filter = b["query"]["bool"]["filter"];
					must[0]["match"]["title"].ToLongFormQuery();
					must[1]["match"]["content"].ToLongFormQuery();
					filter[0]["term"]["status"].ToLongFormTermQuery();
				});
			});
		}
	}
}
