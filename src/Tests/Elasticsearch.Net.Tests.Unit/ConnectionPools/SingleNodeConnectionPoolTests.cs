using System;
using System.IO;
using Autofac;
using Autofac.Extras.FakeItEasy;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Providers;
using Elasticsearch.Net.Tests.Unit.Stubs;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using Elasticsearch.Net.ConnectionPool;

namespace Elasticsearch.Net.Tests.Unit.ConnectionPools
{
	[TestFixture]
	public class SingleNodeConnectionPoolTests
	{
		private readonly ConnectionConfiguration _config;
		private ElasticsearchResponse<Stream> _ok;
		private ElasticsearchResponse<Stream> _bad;

		public SingleNodeConnectionPoolTests()
		{
			_config = new ConnectionConfiguration(new Uri("http://localhost:9200"))
				.MaximumRetries(2);

			_ok = FakeResponse.Ok(_config);
			_bad = FakeResponse.Any(_config, -1);


		}

		[Test]
		public void HttpsUri_UsingSsl_IsTrue()
		{
			Assert.IsTrue(new SingleNodeConnectionPool(new Uri("https://test1")).UsingSsl);
		}

		[Test]
		public void HttpUri_UsingSsl_IsFalse()
		{
			Assert.IsFalse(new SingleNodeConnectionPool(new Uri("http://test1")).UsingSsl);
		}
	}
}
