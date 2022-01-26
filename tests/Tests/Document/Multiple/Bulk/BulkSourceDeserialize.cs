// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Text;
using Tests.Core.Extensions;

namespace Tests.Document.Multiple;

public class BulkSourceDeserialize
{
	[U]
	public void CanDeserialize()
	{
		var json = @"{
					""took"": 61,
					""errors"": false,
					""items"": [{
						""update"": {
							""_index"": ""test"",
							""_type"": ""_doc"",
							""_id"": ""1"",
							""_version"": 2,
							""result"": ""updated"",
							""_shards"": {
								""total"": 2,
								""successful"": 1,
								""failed"": 0
							},
							""_seq_no"": 3,
							""_primary_term"": 1,
							""get"": {
								""_seq_no"": 3,
								""_primary_term"": 1,
								""found"": true,
								""_source"": {
									""field1"": ""value1"",
									""field2"": ""value2""
								}
							},
							""status"": 200
						}
					}]
				}";

		var pool = new SingleNodePool(new Uri("http://localhost:9200"));

		var connection = new InMemoryConnection(Encoding.UTF8.GetBytes(json));
		var settings = new ElasticsearchClientSettings(pool, connection);
		var client = new ElasticsearchClient(settings);

		var bulkResponse = client.Bulk(new BulkRequest());

		bulkResponse.ShouldBeValid();

		var item1 = bulkResponse.Items[0].Get;
		item1.SeqNo.Should().Be(3);
		item1.PrimaryTerm.Should().Be(1);
		item1.Found.Should().Be(true);

		var simpleObject = bulkResponse.Items[0].Get.Source.As<SimpleObject>();
		simpleObject.Field1.Should().Be("value1");
		simpleObject.Field2.Should().Be("value2");
	}

	private class SimpleObject
	{
		public string Field1 { get; set; }
		public string Field2 { get; set; }
	}
}
