using System;
using System.IO;
using System.Threading.Tasks;
using Autofac.Extras.FakeItEasy;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Exceptions;
using Elasticsearch.Net.Tests.Unit.Stubs;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace Elasticsearch.Net.Tests.Unit.Failover.Retries
{
	/// <summary>
	/// When an exception happens on the actual call to elasticsearch we should retry
	/// If we are using connection pooling, but no more then the actual configured maximum.
	/// 
	/// Since we are retrying the exception that bubbles out of the client should be a 
	/// MaxRetry exception that journals our journey
	/// </summary>
	[TestFixture]
	public class ClientExceptionsUsingPoolingTests
	{
		private static readonly int _retries = 4;

		private readonly ConnectionConfiguration _connectionPoolConfig =
			new ConnectionConfiguration(new StaticConnectionPool(new[]
			{
				new Uri("http://localhost:9200"),
				new Uri("http://localhost:9201"),
				new Uri("http://localhost:9202"),
				new Uri("http://localhost:9203"),
				new Uri("http://localhost:9204"),
				new Uri("http://localhost:9205"),
				new Uri("http://localhost:9206"),
				new Uri("http://localhost:9208"),
			}))
				.DisablePing()
				.MaximumRetries(_retries);

		private void AssertMaxRetryException(MaxRetryException maxRetryException)
		{
			maxRetryException.InnerException.Should().NotBeNull();
			var ae = maxRetryException.InnerException as AggregateException;
			ae.Should().NotBeNull();
			var flattened = ae.Flatten();
			flattened.InnerExceptions.Should().NotBeEmpty().And.HaveCount(_retries + 1);
			foreach (var e in flattened.InnerExceptions)
				e.Message.Should().Be("inner");

		}

		[Test]
		public void SyncThrowsMaxRetry_RetriesSpecifiedNumberOfTimes()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				fake.Provide<IConnectionConfigurationValues>(_connectionPoolConfig);
				FakeCalls.ProvideDefaultTransport(fake);

				var getCall = FakeCalls.GetSyncCall(fake);
				getCall.Throws((o) => new Exception("inner"));

				var client = fake.Resolve<ElasticsearchClient>();

				client.Settings.MaxRetries.Should().Be(_retries);

				var e = Assert.Throws<MaxRetryException>(() => client.Info());
				this.AssertMaxRetryException(e);
				getCall.MustHaveHappened(Repeated.Exactly.Times(_retries + 1));
			}
		}

		[Test]
		public void Hard_IConnectionException_OnAsync_ThrowsMaxRetry_AndRetries()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				fake.Provide<IConnectionConfigurationValues>(_connectionPoolConfig);
				FakeCalls.ProvideDefaultTransport(fake);
				var getCall = FakeCalls.GetCall(fake);

				//return a started task that throws
				getCall.Throws((o) => new Exception("inner"));

				var client = fake.Resolve<ElasticsearchClient>();
				client.Settings.MaxRetries.Should().Be(_retries);

				var e = Assert.Throws<MaxRetryException>(async () => await client.InfoAsync());
				this.AssertMaxRetryException(e);
				getCall.MustHaveHappened(Repeated.Exactly.Times(_retries + 1));
			}
		}

		[Test]
		public void Soft_IConnectionException_OnAsync_ThrowsMaxRetry_AndRetries()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				fake.Provide<IConnectionConfigurationValues>(_connectionPoolConfig);
				FakeCalls.ProvideDefaultTransport(fake);
				var getCall = FakeCalls.GetCall(fake);

				//return a started task that throws
				Func<ElasticsearchResponse<Stream>> badTask = () => { throw new Exception("inner"); };
				var t = new Task<ElasticsearchResponse<Stream>>(badTask);
				t.Start();
				getCall.Returns(t);

				var client = fake.Resolve<ElasticsearchClient>();

				client.Settings.MaxRetries.Should().Be(_retries);

				var e = Assert.Throws<MaxRetryException>(async () => await client.InfoAsync());
				this.AssertMaxRetryException(e);
				getCall.MustHaveHappened(Repeated.Exactly.Times(_retries + 1));
			}
		}
	}
}
