using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using FluentAssertions;
using Tests.Framework;

namespace Tests.Reproduce
{
	public class GithubIssue1906
	{
		private class ESLogEvent { }

		[U] public void SearchDoesNotTakeDefaultIndexIntoAccount()
		{
			var node = new Uri("http://localhost:9200");
			var connectionPool = new SingleNodeConnectionPool(node);
			var connectionSettings = new ConnectionSettings(connectionPool, connection: new InMemoryConnection())
				.DefaultIndex("logstash-*")
				.DefaultFieldNameInferrer(p => p)
				.OnRequestCompleted(info =>
				{
					// info.Uri is /_search/ without the default index
					// my ES instance throws an error on the .kibana index (@timestamp field not mapped because I sort on @timestamp)
				});

			var client = new ElasticClient(connectionSettings);
			var response = client.Search<ESLogEvent>(s=>s);

			response.ApiCall.Uri.AbsolutePath.Should().Be("/logstash-%2A/eslogevent/_search");

			response = client.Search<ESLogEvent>(new SearchRequest<ESLogEvent>{ });
			response.ApiCall.Uri.AbsolutePath.Should().Be("/logstash-%2A/eslogevent/_search");

			response = client.Search<ESLogEvent>(new SearchRequest { });
			response.ApiCall.Uri.AbsolutePath.Should().Be("/_search");

		}
	}
}
