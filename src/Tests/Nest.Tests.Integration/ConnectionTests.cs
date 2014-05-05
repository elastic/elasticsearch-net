using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Elasticsearch.Net.Exceptions;
using NUnit.Framework;
using Nest;

namespace Nest.Tests.Integration
{
	[TestFixture]
	public class ConnectionTests : IntegrationTests
	{
		[Test]
		public void TestSettings()
		{
			Assert.AreEqual(ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex);
			Assert.AreEqual(this._settings.MaximumAsyncConnections, Test.Default.MaximumAsyncConnections);
		}
		
		[Test]
		public void TestConnectSuccess()
		{
			var rootNodeInfo = _client.RootNodeInfo();
			Assert.True(rootNodeInfo.IsValid);
			Assert.True(rootNodeInfo.ConnectionStatus.Success);
		}
		[Test]
		public void construct_client_with_null()
		{
			Assert.Throws<ArgumentNullException>(() =>
			{
				var client = new ElasticClient(null);
			});
		}
		[Test]
		public void construct_client_with_null_or_empy_settings()
		{
			Assert.Throws<UriFormatException>(() =>
			{
				var settings = new ConnectionSettings(new Uri("http://:80"), "index");
			});
			Assert.Throws<UriFormatException>(() =>
			{
				var settings = new ConnectionSettings(new Uri(":asda:asdasd:80"), "index");
			});
		}
		[Test]
		public void construct_client_with_invalid_hostname()
		{
			Assert.Throws<UriFormatException>(() =>
			{
				var settings = new ConnectionSettings(new Uri("some mangled hostname:80"), "index");
			});

		}
		[Test]
		public void connect_to_unknown_hostname()
		{
			IRootInfoResponse result = null;

			//this test will fail if fiddler is enabled since the proxy 
			//will report a statuscode of 502 instead of -1
			Assert.Throws<MaxRetryException>(() =>
			{
				var settings = new ConnectionSettings(new Uri("http://youdontownthis.domain.do.you"), "index");
				var client = new ElasticClient(settings);
				result = client.RootNodeInfo();
				Assert.False(result.IsValid);
				Assert.NotNull(result.ConnectionStatus);
			});
		
		}
		[Test]
		public void TestConnectSuccessWithUri()
		{
			var settings = new ConnectionSettings(Test.Default.Uri, "index");
			var client = new ElasticClient(settings);
			var result = client.RootNodeInfo();

			Assert.True(result.IsValid);
			Assert.NotNull(result.ConnectionStatus.HttpStatusCode);
		}
		[Test]
		public void construct_client_with_null_uri()
		{
			Assert.Throws<ArgumentNullException>(() =>
			{
				Uri uri = null;
				var settings = new ConnectionSettings(uri, "index");
			});
		}

		[Test]
		public void ConnectUsingRawClient()
		{
			var result = this._client.Raw.Info();
			Assert.IsTrue(result.Success);
			StringAssert.EndsWith(":9200/?pretty=true", result.RequestUrl);


			var resultWithQueryString = this._client.Raw.Info(qs => qs.AddQueryString("hello", "world"));
			Assert.IsTrue(resultWithQueryString.Success);

			StringAssert.EndsWith(":9200/?hello=world&pretty=true", resultWithQueryString.RequestUrl);
		}

		[Test]
		public void ConnectUsingRawClientComplexCall()
		{
			var result = this._client.Raw.ClusterHealth(s => s
				.Level(LevelOptions.Indices)
				.Local(true)
				.WaitForActiveShards(1)
			);
			Assert.IsTrue(result.Success);
			StringAssert.EndsWith(":9200/_cluster/health?level=indices&local=true&wait_for_active_shards=1&pretty=true", result.RequestUrl);

		}
	}
}
