using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.FakeItEasy;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Exceptions;
using Elasticsearch.Net.Providers;
using Elasticsearch.Net.Tests.Unit.Stubs;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace Elasticsearch.Net.Tests.Unit.Connection
{
	[TestFixture]
	public class RetryTests
	{
		private static readonly int _retries = 4;

		//we do not pass a Uri or IConnectionPool so this config
		//defaults to SingleNodeConnectionPool()
		private readonly ConnectionConfiguration _connectionConfig = new ConnectionConfiguration()
			.MaximumRetries(_retries);

		[Test]
		public void ThrowsOutOfNodesException_AndRetriesTheSpecifiedTimes()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				fake.Provide<IConnectionConfigurationValues>(_connectionConfig);
				FakeCalls.ProvideDefaultTransport(fake);

				var getCall = FakeCalls.GetSyncCall(fake);
				getCall.Throws<Exception>();
				
				var client = fake.Resolve<ElasticsearchClient>();

				client.Settings.MaxRetries.Should().Be(_retries);

				Assert.Throws<OutOfNodesException>(()=> client.Info());
				getCall.MustHaveHappened(Repeated.Exactly.Times(_retries + 1));

			}
		}
		
		[Test]
		public async void ThrowsOutOfNodesException_AndRetriesTheSpecifiedTimes_Async()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				fake.Provide<IConnectionConfigurationValues>(_connectionConfig);
				FakeCalls.ProvideDefaultTransport(fake);
				var getCall = FakeCalls.GetCall(fake); 

				//return a started task that throws
				Func<ElasticsearchResponse<Dictionary<string, object>>> badTask = () => { throw new Exception(); };
				var t = new Task<ElasticsearchResponse<Dictionary<string, object>>>(badTask);
				t.Start();
				getCall.Returns(t);
				
				var client = fake.Resolve<ElasticsearchClient>();

				client.Settings.MaxRetries.Should().Be(_retries);
				try
				{
					var result = await client.InfoAsync();
				}
				catch (Exception e)
				{
					Assert.AreEqual(e.GetType(), typeof(OutOfNodesException));
				}
				getCall.MustHaveHappened(Repeated.Exactly.Times(_retries + 1));

			}
		}

		[Test]
		public void ShouldNotRetryOn400()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				fake.Provide<IConnectionConfigurationValues>(_connectionConfig);
				FakeCalls.ProvideDefaultTransport(fake);

				var getCall = FakeCalls.GetSyncCall(fake); 
				getCall.Returns(FakeResponse.Any(_connectionConfig, 400));
				
				var client = fake.Resolve<ElasticsearchClient>();

				Assert.DoesNotThrow(()=> client.Info());
				getCall.MustHaveHappened(Repeated.Exactly.Once);

			}
		}
		[Test]
		public async void ShouldNotRetryOn400_Async()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				fake.Provide<IConnectionConfigurationValues>(_connectionConfig);
				FakeCalls.ProvideDefaultTransport(fake);
				
				var getCall = FakeCalls.GetCall(fake); 
				getCall.Returns(Task.FromResult(FakeResponse.Any(_connectionConfig, 400)));
				
				var client = fake.Resolve<ElasticsearchClient>();

				var result = await client.InfoAsync();
				getCall.MustHaveHappened(Repeated.Exactly.Once);

			}
		}
		[Test]
		public void ShouldNotRetryOn500()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				fake.Provide<IConnectionConfigurationValues>(_connectionConfig);
				FakeCalls.ProvideDefaultTransport(fake);

				var getCall = FakeCalls.GetSyncCall(fake);
				getCall.Returns(FakeResponse.Any(_connectionConfig, 400));
				
				var client = fake.Resolve<ElasticsearchClient>();

				Assert.DoesNotThrow(()=> client.Info());
				getCall.MustHaveHappened(Repeated.Exactly.Once);

			}
		}
		
		[Test]
		public void ShouldNotRetryOn201()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				fake.Provide<IConnectionConfigurationValues>(_connectionConfig);
				FakeCalls.ProvideDefaultTransport(fake);
				
				var getCall = FakeCalls.GetSyncCall(fake);
				getCall.Returns(FakeResponse.Any(_connectionConfig, 201));
				
				var client = fake.Resolve<ElasticsearchClient>();

				Assert.DoesNotThrow(()=> client.Info());
				getCall.MustHaveHappened(Repeated.Exactly.Once);

			}
		}
		
		[Test]
		public void ShouldRetryOn503()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				fake.Provide<IConnectionConfigurationValues>(_connectionConfig);
				FakeCalls.ProvideDefaultTransport(fake);
				
				var getCall = FakeCalls.GetSyncCall(fake);
				getCall.Returns(FakeResponse.Bad(_connectionConfig));
				
				var client = fake.Resolve<ElasticsearchClient>();

				Assert.Throws<OutOfNodesException>(()=> client.Info());
				getCall.MustHaveHappened(Repeated.Exactly.Times(_retries + 1));

			}
		}
		
		[Test]
		public async void ShouldRetryOn503_Async()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				fake.Provide<IConnectionConfigurationValues>(_connectionConfig);
				FakeCalls.ProvideDefaultTransport(fake);
				
				var getCall = FakeCalls.GetCall(fake);
				getCall.Returns(Task.FromResult(FakeResponse.Bad(_connectionConfig)));
				
				var client = fake.Resolve<ElasticsearchClient>();
				try
				{
					var result = await client.InfoAsync();
				}
				catch (Exception e)
				{
					Assert.AreEqual(e.GetType(), typeof(OutOfNodesException));
				}
				getCall.MustHaveHappened(Repeated.Exactly.Times(_retries + 1));

			}
		}
	}
}
