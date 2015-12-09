using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.ClientConcepts.Exceptions
{
	// TODO these tests are temporary and just serve the purpose of validating the new exception logic
	// will remove these and replace with much more robust tests
	public class ExceptionTests
	{
		[I]
		public void ServerTestWhenThrowExceptionsEnabled()
		{
			var settings = new ConnectionSettings(new Uri("http://ipv4.fiddler:9200"))
				.ThrowExceptions();
			var client = new ElasticClient(settings);
			var exception = Assert.Throws<ElasticsearchServerException>(() => client.GetMapping<Project>(s => s.Index("doesntexist")));
		}

		[I]
		public void ClientTestWhenThrowExceptionsEnabled()
		{
			var settings = new ConnectionSettings(new Uri("http://doesntexist:9200"))
				.ThrowExceptions();
			var client = new ElasticClient(settings);
			var exception = Assert.Throws<ElasticsearchClientException>(() => client.RootNodeInfo());
		}

		[I]
		public void ServerTestWhenThrowExceptionsDisabled()
		{
			var settings = new ConnectionSettings(new Uri("http://ipv4.fiddler:9200"));
			var client = new ElasticClient(settings);
			var r = client.GetMapping<Project>(s => s.Index("doesntexist"));
		}

		[I]
		public void ClientTestWhenThrowExceptionsDisabled()
		{
			var settings = new ConnectionSettings(new Uri("http://doesntexist:9200"));
			var client = new ElasticClient(settings);
			var r = client.RootNodeInfo();
		}
	}
}
