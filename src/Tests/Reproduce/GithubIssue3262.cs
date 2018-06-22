using System;
using System.Text;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Reproduce
{
	public class GithubIssue3262
	{
		[U]
		public void CanDeserializeSingleFilterAndCharacterFilter()
		{
			var json = @"{
			  ""products_purchasing"": {
				""settings"": {
				  ""index"": {
					""number_of_shards"": ""5"",
					""provided_name"": ""products_purchasing"",
					""creation_date"": ""1510930394462"",
					""analysis"": {
					  ""normalizer"": {
						""lowercase"": {
						  ""filter"": ""lowercase"",
						  ""type"": ""custom"",
						  ""char_filter"": ""words""
						}
					  },
					  ""analyzer"": {
						""whitespace_lowercase"": {
						  ""filter"": ""lowercase"",
						  ""type"": ""custom"",
						  ""tokenizer"": ""whitespace""
						},
						""keyword_lowercase"": {
						  ""filter"": ""lowercase"",
						  ""type"": ""custom"",
						  ""tokenizer"": ""keyword""
						}
					  },
					  ""char_filter"": {
						""words"": {
						  ""pattern"": ""[(^([^A-Za-z\\s]*\\s)*|[^A-Za-z\\s])]"",
						  ""type"": ""pattern_replace"",
						  ""replacement"": """"
						}
					  }
					},
					""number_of_replicas"": ""1"",
					""uuid"": ""peH2yqVLTIOc4ScTUYTtEA"",
					""version"": {
					  ""created"": ""5050099""
					}
				  }
				}
			  }
			}";

			var bytes = Encoding.UTF8.GetBytes(json);

			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(pool, new InMemoryConnection(bytes));
			var client = new ElasticClient(connectionSettings);

			Action getIndexRequest = () => client.GetIndex(new GetIndexRequest("products_purchasing"));

			getIndexRequest.ShouldNotThrow();

			var response = client.GetIndex(new GetIndexRequest("products_purchasing"));

			var normalizer = response.Indices["products_purchasing"].Settings.Analysis.Normalizers["lowercase"] as ICustomNormalizer;
			normalizer.Should().NotBeNull();
			normalizer.Filter.Should().NotBeNull().And.HaveCount(1);
		}
	}
}
