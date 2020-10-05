// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;

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

			Action getIndexRequest = () => client.Indices.Get(new GetIndexRequest("products_purchasing"));

			getIndexRequest.Should().NotThrow();

			var response = client.Indices.Get(new GetIndexRequest("products_purchasing"));

			var normalizer = response.Indices["products_purchasing"].Settings.Analysis.Normalizers["lowercase"] as ICustomNormalizer;
			normalizer.Should().NotBeNull();
			normalizer.Filter.Should().NotBeNull().And.HaveCount(1);
		}
	}
}
