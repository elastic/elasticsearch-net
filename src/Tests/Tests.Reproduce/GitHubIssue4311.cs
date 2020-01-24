using System;
using System.Text;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;

namespace Tests.Reproduce
{
	public class GithubIssue4311
	{
		[U]
		public void CanDeserializeIndexResponseWithSingleFilterItem()
		{
			var json = @"{
						  "".es-test-repro"": {
									""aliases"": {
										"".es-test-repro-alias1"": {
											""filter"": {
												""bool"": {
													""must_not"": {
														""exists"": {
															""field"": ""field1""
														}
													}
												}
											}
										}
									},
									""mappings"": {
										""user"": {
											""properties"": {
												""email"": {
													""type"": ""text"",
													""fields"": {
														""keyword"": {
															""type"": ""keyword"",
															""ignore_above"": 256
														}
													}
												},
												""firstName"": {
													""type"": ""text"",
													""fields"": {
														""keyword"": {
															""type"": ""keyword"",
															""ignore_above"": 256
														}
													}
												},
												""isActive"": {
													""type"": ""boolean""
												},
												""lastName"": {
													""type"": ""text"",
													""fields"": {
														""keyword"": {
															""type"": ""keyword"",
															""ignore_above"": 256
														}
													}
												},
												""state"": {
													""type"": ""text"",
													""fields"": {
														""keyword"": {
															""type"": ""keyword"",
															""ignore_above"": 256
														}
													}
												}
											}
										}
									},
									""settings"": {
										""index"": {
											""creation_date"": ""1579594498360"",
											""number_of_shards"": ""5"",
											""number_of_replicas"": ""1"",
											""uuid"": ""wqYY9c4xSH6nyilffGobmw"",
											""version"": {
												""created"": ""6080399""
											},
											""provided_name"": "".es-test-repro""
										}
									}
								}
							}";

			var bytes = Encoding.UTF8.GetBytes(json);
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(pool, new InMemoryConnection(bytes));
			var client = new ElasticClient(connectionSettings);

			var indexResponse = client.GetIndex(".es-test-repro");
			indexResponse.IsValid.Should().BeTrue();
		}
	}
}
