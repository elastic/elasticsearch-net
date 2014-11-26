using System;
using System.IO;
using System.Threading.Tasks;
using Autofac.Extras.FakeItEasy;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Exceptions;
using Elasticsearch.Net.Tests.Unit.Stubs;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace Elasticsearch.Net.Tests.Unit.Failover.Retries
{
	[TestFixture]
	public class ClientExceptionRetryHandlingTests
	{
		private static readonly int _retries = 4;

		//we do not pass a Uri or IConnectionPool so this config
		//defaults to SingleNodeConnectionPool()
		private readonly ConnectionConfiguration _connectionConfig = new ConnectionConfiguration()
			.MaximumRetries(_retries);

		[Test]
		public void OnConnectionException_WithoutPooling_DoNotRetry()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				fake.Provide<IConnectionConfigurationValues>(_connectionConfig);
				FakeCalls.ProvideDefaultTransport(fake);

				var getCall = FakeCalls.GetSyncCall(fake);
				getCall.Throws((o)=> new Exception("inner"));

				var client = fake.Resolve<ElasticsearchClient>();

				client.Settings.MaxRetries.Should().Be(_retries);

				var e = Assert.Throws<Exception>(() => client.Info());
				e.Message.Should().Be("inner");
				getCall.MustHaveHappened(Repeated.Exactly.Once);
			}
		}
		
		[Test]
		public void Hard_IConnectionException_AsyncCall_WithoutPooling_DoesNot_Retry_AndRethrows()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				fake.Provide<IConnectionConfigurationValues>(_connectionConfig);
				FakeCalls.ProvideDefaultTransport(fake);
				var getCall = FakeCalls.GetCall(fake);

				//return a started task that throws
				getCall.Throws((o)=> new Exception("inner"));

				var client = fake.Resolve<ElasticsearchClient>();

				client.Settings.MaxRetries.Should().Be(_retries);

				var e = Assert.Throws<Exception>(async () => await client.InfoAsync());
				e.Message.Should().Be("inner");
				getCall.MustHaveHappened(Repeated.Exactly.Once);
			}
		}

		[Test]
		public void Soft_IConnectionException_AsyncCall_WithoutPooling_DoesNot_Retry_AndRethrows()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				fake.Provide<IConnectionConfigurationValues>(_connectionConfig);
				FakeCalls.ProvideDefaultTransport(fake);
				var getCall = FakeCalls.GetCall(fake);

				//return a started task that throws
				Func<ElasticsearchResponse<Stream>> badTask = () => { throw new Exception("inner"); };
				var t = new Task<ElasticsearchResponse<Stream>>(badTask);
				t.Start();
				getCall.Returns(t);

				var client = fake.Resolve<ElasticsearchClient>();

				client.Settings.MaxRetries.Should().Be(_retries);

				var e = Assert.Throws<Exception>(async () => await client.InfoAsync());
				e.Message.Should().Be("inner");
				getCall.MustHaveHappened(Repeated.Exactly.Once);
			}
		}
	}
}
