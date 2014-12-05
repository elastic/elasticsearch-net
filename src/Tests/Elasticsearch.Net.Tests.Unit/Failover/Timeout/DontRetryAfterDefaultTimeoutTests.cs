using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.FakeItEasy;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Connection.Configuration;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Exceptions;
using Elasticsearch.Net.Providers;
using Elasticsearch.Net.Tests.Unit.Stubs;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace Elasticsearch.Net.Tests.Unit.Failover.Timeout
{
	[TestFixture]
	public class DontRetryAfterDefaultTimeoutTests
	{
		[Test]
		public void FailEarlyIfTimeoutIsExhausted()
		{
			using (var fake = new AutoFake())
			{
				var dateTimeProvider = ProvideDateTimeProvider(fake);
				var config = ProvideConfiguration(dateTimeProvider);
				var connection = ProvideConnection(fake, config, dateTimeProvider);

				var getCall = FakeCalls.GetSyncCall(fake);
				var ok = FakeResponse.Ok(config);
				var bad = FakeResponse.Bad(config);
				getCall.ReturnsNextFromSequence(
					bad,  //info 1 - 9204
					bad, //info 2 - 9203 DEAD
					ok  //info 2 retry - 9202
				);
				
				var seenNodes = new List<Uri>();
				getCall.Invokes((Uri u, IRequestConfiguration o) => seenNodes.Add(u));

				var pingCall = FakeCalls.PingAtConnectionLevel(fake);
				pingCall.Returns(ok);

				var client1 = fake.Resolve<ElasticsearchClient>();
				
				//event though the third node should have returned ok, the first 2 calls took a minute
				var e = Assert.Throws<MaxRetryException>(() => client1.Info());
				e.Message.Should()
					.StartWith("Retry timeout 00:01:00 was hit after retrying 1 times:");

				IElasticsearchResponse response = null;
				Assert.DoesNotThrow(() => response = client1.Info() );
				response.Should().NotBeNull();
				response.Success.Should().BeTrue();

			}
		}

		[Test]
		public void FailEarlyIfTimeoutIsExhausted_Async()
		{
			using (var fake = new AutoFake())
			{
				var dateTimeProvider = ProvideDateTimeProvider(fake);
				var config = ProvideConfiguration(dateTimeProvider);
				var connection = ProvideConnection(fake, config, dateTimeProvider);
				
				var getCall = FakeCalls.GetCall(fake);
				var ok = Task.FromResult(FakeResponse.Ok(config));
				var bad = Task.FromResult(FakeResponse.Bad(config));
				getCall.ReturnsNextFromSequence(
					bad,  
					bad,  
					ok 
				);
				
				var seenNodes = new List<Uri>();
				getCall.Invokes((Uri u, IRequestConfiguration o) => seenNodes.Add(u));

				var pingCall = FakeCalls.PingAtConnectionLevelAsync(fake);
				pingCall.Returns(ok);

				var client1 = fake.Resolve<ElasticsearchClient>();
				//event though the third node should have returned ok, the first 2 calls took a minute
				var e = Assert.Throws<MaxRetryException>(async () => await client1.InfoAsync());
				e.Message.Should()
					.StartWith("Retry timeout 00:01:00 was hit after retrying 1 times:");

				IElasticsearchResponse response = null;
				Assert.DoesNotThrow(async () => response = await client1.InfoAsync() );
				response.Should().NotBeNull();
				response.Success.Should().BeTrue();
			}
		}
		
		private static IConnection ProvideConnection(AutoFake fake, ConnectionConfiguration config, IDateTimeProvider dateTimeProvider)
		{
			fake.Provide<IConnectionConfigurationValues>(config);
			var param = new TypedParameter(typeof(IDateTimeProvider), dateTimeProvider);
			var transport = fake.Provide<ITransport, Transport>(param);
			var connection = fake.Resolve<IConnection>();
			return connection;
		}

		private static ConnectionConfiguration ProvideConfiguration(IDateTimeProvider dateTimeProvider)
		{
			var connectionPool = new StaticConnectionPool(new[]
			{
				new Uri("http://localhost:9204"),
				new Uri("http://localhost:9203"),
				new Uri("http://localhost:9202"),
				new Uri("http://localhost:9201")
			}, randomizeOnStartup: false, dateTimeProvider: dateTimeProvider);
			var config = new ConnectionConfiguration(connectionPool).EnableMetrics();
			return config;
		}

		private static IDateTimeProvider ProvideDateTimeProvider(AutoFake fake)
		{
			var now = DateTime.UtcNow;
			var dateTimeProvider = fake.Resolve<IDateTimeProvider>();
			var nowCall = A.CallTo(() => dateTimeProvider.Now());
			nowCall.ReturnsNextFromSequence(
				now, //initital sniff now from constructor
				now, //pool select next node
				now.AddSeconds(30), //info 1 took to long? 
				now.AddSeconds(30), //pool select next node? 
				now.AddMinutes(1) //info 2 took to long? 
			);
			A.CallTo(() => dateTimeProvider.AliveTime(A<Uri>._, A<int>._)).Returns(new DateTime());
			//dead time will return a fixed timeout of 1 minute
			A.CallTo(() => dateTimeProvider.DeadTime(A<Uri>._, A<int>._, A<int?>._, A<int?>._))
				.Returns(DateTime.UtcNow.AddMinutes(1));
			//make sure the transport layer uses a different datetimeprovider
			fake.Provide<IDateTimeProvider>(new DateTimeProvider());
			return dateTimeProvider;
		}
	}
}
