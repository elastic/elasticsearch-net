using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Nest;

namespace Nest.Tests.Integration
{
	[TestFixture]
	public class ConnectionTests : BaseElasticSearchTests
	{
		[Test]
		public void TestSettings()
		{
			Assert.AreEqual(this.Settings.Host, Test.Default.Host);
			Assert.AreEqual(this.Settings.Port, Test.Default.Port);
			Assert.AreEqual(this.Settings.DefaultIndex, Test.Default.DefaultIndex);
			Assert.AreEqual(this.Settings.MaximumAsyncConnections, Test.Default.MaximumAsyncConnections);
		}
		[Test]
		public void TestConnectSuccess()
		{
			var client = this.CreateClient();
			ConnectionStatus status;
			client.TryConnect(out status);
			Assert.True(client.IsValid);
			Assert.True(status.Success);
			Assert.Null(status.Error);
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
			Assert.Throws<ArgumentNullException>(() =>
			{
				string host = null;
				var settings = new ConnectionSettings(host, 80);
			});
			Assert.Throws<UriFormatException>(() =>
			{
				var settings = new ConnectionSettings(":asda:asdasd", 80);
			});
		}
		[Test]
		public void construct_client_with_invalid_hostname()
		{
			Assert.Throws<UriFormatException>(() =>
			{
				var settings = new ConnectionSettings("some mangled hostname", 80);
			});
			
		}
		[Test]
		public void connect_to_unknown_hostname()
		{
			Assert.DoesNotThrow(() =>
			{
				var settings = new ConnectionSettings("youdontownthis.domain.do.you", 80);
				var client = new ElasticClient(settings);
				ConnectionStatus connectionStatus;
				client.TryConnect(out connectionStatus);

				Assert.False(client.IsValid);
				Assert.True(connectionStatus != null);
				Assert.True(connectionStatus.Error.HttpStatusCode == System.Net.HttpStatusCode.BadGateway
					|| connectionStatus.Error.ExceptionMessage.StartsWith("The remote name could not be resolved"));
			});
		}
		[Test]
		public void TestConnectSuccessWithUri()
		{
			var settings = new ConnectionSettings(Test.Default.Uri);
			var client = new ElasticClient(settings);
			ConnectionStatus status;
			client.TryConnect(out status);
			Assert.True(client.IsValid);
			Assert.True(status.Success);
			Assert.Null(status.Error);
		}
		[Test]
		public void construct_client_with_null_uri()
		{
			Assert.Throws<ArgumentNullException>(() =>
			{
				Uri uri = null;
				var settings = new ConnectionSettings(uri);
			});
		}
	}
}
