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

using System;
using System.Text;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;

namespace Tests.Reproduce
{
	public class GitHubIssue5201
	{
		private static readonly byte[] ResponseBytes = Encoding.UTF8.GetBytes(@"{
		          ""size"" : 0,
		          ""_source"" : false,
		          ""stored_fields"" : ""_none_"",
		          ""aggregations"" : {
		            ""groupby"" : {
		                ""composite"" : {
		                    ""size"" : 1000,
		                    ""sources"" : [
		                        {
		                            ""ccf51bfa"" : {
		                                ""terms"" : {
		                                    ""field"" : ""id"",
		                                    ""missing_bucket"" : true,
		                                    ""order"" : ""asc""
		                                }
		                            }
		                        }
		                    ]
		                }
		            }
		        }
		    }");

		[U] public async Task DeserializeAggregations()
		{
			var pool = new SingleNodeConnectionPool(new Uri($"http://localhost:9200"));
			var settings = new ConnectionSettings(pool, new InMemoryConnection(ResponseBytes));
			var client = new ElasticClient(settings);

			var translateResponseAsync = await client.Sql.TranslateAsync(t => t.Query("select UDFvarchar1, count(1) from interactions group by UDFvarchar1 order by UDFvarchar1"));
			translateResponseAsync.Result.Aggregations.Should().NotBeNull();

			var translateResponse = client.Sql.Translate(t => t.Query("select UDFvarchar1, count(1) from interactions group by UDFvarchar1 order by UDFvarchar1"));
			translateResponse.Result.Aggregations.Should().NotBeNull();
		}
	}
}
