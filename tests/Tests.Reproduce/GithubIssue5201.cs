// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Text;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
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
