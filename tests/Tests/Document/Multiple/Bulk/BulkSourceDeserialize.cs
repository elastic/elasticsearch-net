// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;

namespace Tests.Document.Multiple.Bulk
{
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

			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));

			var connection = new InMemoryConnection(Encoding.UTF8.GetBytes(json));
			var settings = new ConnectionSettings(pool, connection);
			var client = new ElasticClient(settings);

			var bulkResponse = client.Bulk(r => r);

			bulkResponse.ShouldBeValid();

			var simpleObject = bulkResponse.Items[0].GetResponse<SimpleObject>();

			simpleObject.Found.Should().BeTrue();
			simpleObject.Source.field1.Should().Be("value1");
			simpleObject.Source.field2.Should().Be("value2");
		}

		private class SimpleObject
		{
			// ReSharper disable InconsistentNaming
			// ReSharper disable UnusedAutoPropertyAccessor.Local
			public string field1 { get; set; }
			public string field2 { get; set; }
			// ReSharper restore InconsistentNaming
			// ReSharper restore UnusedAutoPropertyAccessor.Local
		}
	}
}
