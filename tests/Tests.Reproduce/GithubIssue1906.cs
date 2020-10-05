// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
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
