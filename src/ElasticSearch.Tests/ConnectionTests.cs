using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ElasticSearch.Client;

namespace ElasticSearch.Tests
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
	}
}
