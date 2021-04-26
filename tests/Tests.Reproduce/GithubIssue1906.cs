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
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;

namespace Tests.Reproduce
{
	public class GithubIssue1906
	{
		[U] public void SearchDoesNotTakeDefaultIndexIntoAccount()
		{
			var node = new Uri("http://localhost:9200");
			var connectionPool = new SingleNodeConnectionPool(node);
			var connectionSettings = new ConnectionSettings(connectionPool, new InMemoryConnection())
				.DefaultIndex("logstash-*")
				.DefaultFieldNameInferrer(p => p)
				.OnRequestCompleted(info =>
				{
					// info.Uri is /_search/ without the default index
					// my ES instance throws an error on the .kibana index (@timestamp field not mapped because I sort on @timestamp)
				});

			var client = new ElasticClient(connectionSettings);
			var response = client.Search<ESLogEvent>(s => s);

			response.ApiCall.Uri.AbsolutePath.Should().Be("/logstash-%2A/_search");

			response = client.Search<ESLogEvent>(new SearchRequest<ESLogEvent>());
			response.ApiCall.Uri.AbsolutePath.Should().Be("/logstash-%2A/_search");

			response = client.Search<ESLogEvent>(new SearchRequest());
			response.ApiCall.Uri.AbsolutePath.Should().Be("/_search");
		}

		private class ESLogEvent { }
	}
}
