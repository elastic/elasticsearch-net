using System;
using System.Net;
using System.Runtime.Remoting.Channels;
using Elasticsearch.Net;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Exceptions;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Exceptions
{
	[TestFixture]
	public class ElasticsearchExceptionTests 
	{
		[Test]
		public void ConnectionException_WithClientThatDoesNotThrow_StillThrows()
		{
			var uri = ElasticsearchConfiguration.CreateBaseUri(9492);
			var client = new ElasticClient(new ConnectionSettings(uri).SetTimeout(500));
			var e = Assert.Throws<WebException>(() => client.RootNodeInfo());
		}

		[Test]
		public void ConnectionException_WithThrowingClient()
		{
			var uri = ElasticsearchConfiguration.CreateBaseUri(9494);
			var client = new ElasticClient(new ConnectionSettings(uri)
				.SetTimeout(500)
				.ThrowOnElasticsearchServerExceptions());
			var e = Assert.Throws<WebException>(() => client.RootNodeInfo());
		}
		
		[Test]
		public void ConnectionException_WithClientThatDoesNotThrow_StillThrows_Async()
		{
			var uri = ElasticsearchConfiguration.CreateBaseUri(9492);
			var client = new ElasticClient(new ConnectionSettings(uri).SetTimeout(500));
			Assert.Throws<WebException>(async () => await client.RootNodeInfoAsync());
		}

		[Test]
		public void ConnectionException_WithThrowingClient_Async()
		{
			var uri = ElasticsearchConfiguration.CreateBaseUri(9494);
			var client = new ElasticClient(new ConnectionSettings(uri)
				.SetTimeout(500)
				.ThrowOnElasticsearchServerExceptions());
			Assert.Throws<WebException>(async () => await client.RootNodeInfoAsync());
		}
		
		[Test]
		public async void ServerError_Is_Set_ClientThat_DoesNotThow_AndDoesNotExposeRawResponse_Async()
		{
			var uri = ElasticsearchConfiguration.CreateBaseUri();
			var client = new ElasticClient(new ConnectionSettings(uri).ExposeRawResponse(false));
			Assert.DoesNotThrow(async () =>
			{
				var result = await client.SearchAsync<ElasticsearchProject>(s => s.QueryRaw(@"{ ""badjson"": {}  }"));
				result.IsValid.Should().BeFalse();
				result.ConnectionStatus.HttpStatusCode.Should().Be(400);
				var e = result.ServerError;
				e.Should().NotBeNull();
				e.ExceptionType.Should().Contain("SearchPhaseExecutionException");
			});
		}
		
		[Test]
		public void ServerError_Is_Set_ClientThat_DoesNotThow_AndDoesNotExposeRawResponse()
		{
			var uri = ElasticsearchConfiguration.CreateBaseUri();
			var client = new ElasticClient(new ConnectionSettings(uri).ExposeRawResponse(false));
			Assert.DoesNotThrow(() =>
			{
				var result = client.Search<ElasticsearchProject>(s => s.QueryRaw(@"{ ""badjson"": {}  }"));
				result.IsValid.Should().BeFalse();
				result.ConnectionStatus.HttpStatusCode.Should().Be(400);
				var e = result.ServerError;
				e.Should().NotBeNull();
				e.ExceptionType.Should().Contain("SearchPhaseExecutionException");
			});
		}

		[Test]
		public void ServerError_Is_Set_ClientThat_DoesNotThow()
		{
			var uri = ElasticsearchConfiguration.CreateBaseUri();
			var client = new ElasticClient(new ConnectionSettings(uri).ExposeRawResponse(true));
			Assert.DoesNotThrow(() =>
			{
				var result = client.Search<ElasticsearchProject>(s => s.QueryRaw(@"{ ""badjson"": {}  }"));
				result.IsValid.Should().BeFalse();
				result.ConnectionStatus.HttpStatusCode.Should().Be(400);
				var e = result.ServerError;
				e.Should().NotBeNull();
				e.ExceptionType.Should().Contain("SearchPhaseExecutionException");
			});
		}
		
		[Test]
		public void WebException_WithThrowingClient_ThrowsMappedException()
		{
			var uri = ElasticsearchConfiguration.CreateBaseUri();
			var client = new ElasticClient(new ConnectionSettings(uri).ThrowOnElasticsearchServerExceptions());
			var e = Assert.Throws<ElasticsearchServerException>(() => client.Search<ElasticsearchProject>(s => s.QueryRaw(@"{ ""badjson"" : {} }")));
			e.ExceptionType.Should().Contain("SearchPhaseExecutionException");
		}
		
		[Test]
		public void ConnectionPool_SingleNode_PingExceptionThrowsMaxRetry()
		{
			var uris = new []
			{
				ElasticsearchConfiguration.CreateBaseUri(9201),
			};
			var connectionPool = new StaticConnectionPool(uris);
			var client = new ElasticClient(new ConnectionSettings(connectionPool)
				.SetTimeout(1000)
			);
			var e = Assert.Throws<MaxRetryException>(() =>
			{
				var result = client.Search<ElasticsearchProject>(s => s.MatchAll());
				result.IsValid.Should().BeFalse();
			});
			e.Should().NotBeNull();
			Assert.Pass(e.ToString());
		}
	
		[Test]
		public void ConnectionPool_SingleNode_PingExceptionThrowsMaxRetry_Async()
		{
			var uris = new []
			{
				ElasticsearchConfiguration.CreateBaseUri(9201),
			};
			var connectionPool = new StaticConnectionPool(uris);
			var client = new ElasticClient(new ConnectionSettings(connectionPool)
				.SetTimeout(1000)
			);
			var e = Assert.Throws<MaxRetryException>(async () =>
			{
				var result = await client.SearchAsync<ElasticsearchProject>(s => s
					.MatchAll()
				);
				result.IsValid.Should().BeFalse();
			});
			e.Should().NotBeNull();
		}
		[Test]
		public void ConnectionPool_DoesNotThrowOnServerExceptions_ThrowsMaxRetryException_OnDeadNodes()
		{
			var uris = new []
			{
				ElasticsearchConfiguration.CreateBaseUri(9201),
				ElasticsearchConfiguration.CreateBaseUri(9202),
				ElasticsearchConfiguration.CreateBaseUri(9203),
			};
			var connectionPool = new StaticConnectionPool(uris);
			var client = new ElasticClient(new ConnectionSettings(connectionPool)
				.SetTimeout(1000)
			);
			var e = Assert.Throws<MaxRetryException>(() =>
			{
				var result = client.Search<ElasticsearchProject>(s => s.MatchAll());
				result.IsValid.Should().BeFalse();
			});
			e.Should().NotBeNull();
		}
	
		[Test]
		public async void ConnectionPool_DoesNotThrowOnServerExceptions_ThrowsMaxRetryException_OnDeadNodes_Async()
		{
			var uris = new []
			{
				ElasticsearchConfiguration.CreateBaseUri(9201),
				ElasticsearchConfiguration.CreateBaseUri(9202),
				ElasticsearchConfiguration.CreateBaseUri(9203),
			};
			var connectionPool = new StaticConnectionPool(uris);
			var client = new ElasticClient(new ConnectionSettings(connectionPool)
				.SetTimeout(1000)
			);

			try
			{
				var result = await client.SearchAsync<ElasticsearchProject>(s => s.MatchAll());
				result.IsValid.Should().BeFalse();
			}
			catch (MaxRetryException e)
			{
				Assert.Pass("MaxRetryException caught");
			}
			catch (Exception e)
			{
				Assert.Fail("Did not expect exception of type {0} to be caught", e.GetType().Name);
			}
		}
		
		[Test]
		public void ConnectionPool_ThrowOnServerExceptions_ThrowsElasticsearchServerException()
		{
			var uris = new []
			{
				ElasticsearchConfiguration.CreateBaseUri(9200),
				ElasticsearchConfiguration.CreateBaseUri(9200),
				ElasticsearchConfiguration.CreateBaseUri(9200),
			};
			var connectionPool = new StaticConnectionPool(uris);
			var client = new ElasticClient(new ConnectionSettings(connectionPool)
				.ThrowOnElasticsearchServerExceptions()
				.SetTimeout(1000)
			);
			var e = Assert.Throws<ElasticsearchServerException>(() =>
			{
				var index = ElasticsearchConfiguration.NewUniqueIndexName();
				var create = client.CreateIndex(index);
				var close = client.CloseIndex(index);
				var result = client.Search<ElasticsearchProject>(s => s.Index(index));
			});
			e.Should().NotBeNull();
		}
		
		[Test]
		public async void ConnectionPool_ThrowOnServerExceptions_ThrowsElasticsearchServerException_Async()
		{
			var uris = new []
			{
				ElasticsearchConfiguration.CreateBaseUri(9200),
				ElasticsearchConfiguration.CreateBaseUri(9200),
				ElasticsearchConfiguration.CreateBaseUri(9200),
			};
			var connectionPool = new StaticConnectionPool(uris);
			var client = new ElasticClient(new ConnectionSettings(connectionPool)
				.ThrowOnElasticsearchServerExceptions()
				.SetTimeout(1000)
			);
			try
			{

				var index = ElasticsearchConfiguration.NewUniqueIndexName();
				var create = await client.CreateIndexAsync(i=>i.Index(index));
				var close = await client.CloseIndexAsync(i=>i.Index(index));
				var result = await client.SearchAsync<ElasticsearchProject>(s => s.Index(index));
			}
			catch (ElasticsearchServerException e)
			{
				Assert.Pass("ElasticearchServerException caught");
			}
			catch (Exception e)
			{
				Assert.Fail("Did not expect exception of type {0} to be caught", e.GetType().Name);
			}
		}
	}
}