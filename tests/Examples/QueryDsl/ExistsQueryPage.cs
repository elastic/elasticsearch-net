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
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class ExistsQueryPage : ExampleBase
	{
		[U]
		[Description("query-dsl/exists-query.asciidoc:20")]
		public void Line20()
		{
			// tag::3342c69b2c2303247217532956fcce85[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.Exists(e => e
						.Field("user")
					)
				)
			);
			// end::3342c69b2c2303247217532956fcce85[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""exists"": {
			            ""field"": ""user""
			        }
			    }
			}");
		}

		[U]
		[Description("query-dsl/exists-query.asciidoc:56")]
		public void Line56()
		{
			// tag::43af86de5e49aa06070092fffc138208[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => !q
					.Exists(e => e
						.Field("user")
					)
				)
			);
			// end::43af86de5e49aa06070092fffc138208[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""bool"": {
			            ""must_not"": {
			                ""exists"": {
			                    ""field"": ""user""
			                }
			            }
			        }
			    }
			}", (e, body) =>
			{
				body["query"]["bool"]["must_not"].ToJArray();
			});
		}
	}
}
