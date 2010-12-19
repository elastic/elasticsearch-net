using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using ElasticSearch.Client;

namespace ElasticSearch.Tests
{
	public class ConnectionTests : BaseElasticSearchTests
	{
		[Fact]
		public void TestSettings()
		{
			Assert.Equal(this.Settings.Host, Test.Default.Host);
			Assert.Equal(this.Settings.Port, Test.Default.Port);
			Assert.Equal(this.Settings.DefaultIndex, Test.Default.DefaultIndex);
			Assert.Equal(this.Settings.MaximumAsyncConnections, Test.Default.MaximumAsyncConnections);
		}
		[Fact]
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
