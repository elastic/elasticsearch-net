using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using System;
using Tests.Framework;
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
			var exception = Assert.Throws<ElasticsearchClientException>(() => client.GetMapping<Project>(s => s.Index("doesntexist")));
			exception.InnerException.Should().NotBeNull();
			exception.Response.Should().NotBeNull();
			exception.Response.ServerError.Should().NotBeNull();
			exception.Response.ServerError.Status.Should().BeGreaterThan(0);
		}

		[I]
		public void ClientTestWhenThrowExceptionsEnabled()
		{
			var settings = new ConnectionSettings(new Uri("http://doesntexist:9200"))
				.ThrowExceptions();
			var client = new ElasticClient(settings);
			var exception = Assert.Throws<ElasticsearchClientException>(() => client.RootNodeInfo());
			var inner = exception.InnerException;
			inner.Should().NotBeNull();
		}

		[I]
		public void ServerTestWhenThrowExceptionsDisabled()
		{
			var settings = new ConnectionSettings(new Uri("http://ipv4.fiddler:9200"));
			var client = new ElasticClient(settings);
			var response = client.GetMapping<Project>(s => s.Index("doesntexist"));
			response.CallDetails.OriginalException.Should().NotBeNull();
			response.CallDetails.ServerError.Should().NotBeNull();
			response.CallDetails.ServerError.Status.Should().BeGreaterThan(0);
		}

		[I]
		public void ClientTestWhenThrowExceptionsDisabled()
		{
			var settings = new ConnectionSettings(new Uri("http://doesntexist:9200"));
			var client = new ElasticClient(settings);
			var response = client.RootNodeInfo();
			response.CallDetails.OriginalException.Should().NotBeNull();
			response.CallDetails.ServerError.Should().BeNull();
		}
	}
}
