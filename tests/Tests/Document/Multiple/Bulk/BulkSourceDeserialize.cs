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
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
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
			public string field1 { get; set; }
			public string field2 { get; set; }
		}
	}
}
