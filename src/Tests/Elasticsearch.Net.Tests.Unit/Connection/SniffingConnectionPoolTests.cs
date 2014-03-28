using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.FakeItEasy;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Exceptions;
using Elasticsearch.Net.Providers;
using Elasticsearch.Net.Tests.Unit.Stubs;
using FakeItEasy;
using FakeItEasy.Configuration;
using FluentAssertions;
using NUnit.Framework;

namespace Elasticsearch.Net.Tests.Unit.Connection
{
	[TestFixture]
	public class SniffingConnectionPoolTests
	{
		[Test]
		public void SniffOnStartupCallsSniffOnlyOnce()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				//It's recommended to only have on instance of your connection pool
				//Be sure to register it as Singleton in your IOC
				var connectionPool = new SniffingConnectionPool(new[] { new Uri("http://localhost:9200") });
				var config = new ConnectionConfiguration(connectionPool)
					.SniffOnStartup();
				fake.Provide<IConnectionConfigurationValues>(config);
				var param = new TypedParameter(typeof(IDateTimeProvider), null);
				fake.Provide<ITransport, Transport>(param);
				var connection = fake.Resolve<IConnection>();
				var sniffCall = A.CallTo(() => connection.Sniff(A<Uri>._));
				var client1 = fake.Resolve<ElasticsearchClient>(); 
				var client2 = fake.Resolve<ElasticsearchClient>(); 
				var client3 = fake.Resolve<ElasticsearchClient>(); 
				var client4 = fake.Resolve<ElasticsearchClient>(); 

				sniffCall.MustHaveHappened(Repeated.Exactly.Once);
			}
		}

		[Test]
		public void SniffIsCalledAfterItHasGoneOutOfDate()
		{
			using (var fake = new AutoFake())
			{
				var dateTimeProvider = fake.Resolve<IDateTimeProvider>();
				var nowCall = A.CallTo(()=>dateTimeProvider.Now());
				nowCall.ReturnsNextFromSequence(
					DateTime.UtcNow, //initial sniff time (set even if not sniff_on_startup
					DateTime.UtcNow, //info call 1
					DateTime.UtcNow, //info call 2
					DateTime.UtcNow.AddMinutes(10), //info call 3
					DateTime.UtcNow.AddMinutes(10), //set now after sniff 3
					DateTime.UtcNow.AddMinutes(20), //info call 4
					DateTime.UtcNow.AddMinutes(20), //set now after sniff 4
					DateTime.UtcNow.AddMinutes(22) //info call 5
				);
				var connectionPool = new SniffingConnectionPool(new[] { new Uri("http://localhost:9200") });
				var config = new ConnectionConfiguration(connectionPool)
					.SniffLifeSpan(TimeSpan.FromMinutes(4));
				fake.Provide<IConnectionConfigurationValues>(config);
				fake.Provide<ITransport>(fake.Resolve<Transport>());
				var connection = fake.Resolve<IConnection>();
				var sniffCall = A.CallTo(() => connection.Sniff(A<Uri>._));
				
				var getCall = FakeCalls.GetSyncCall(fake);
				getCall.Returns(FakeResponse.Ok(config));

				var client1 = fake.Resolve<ElasticsearchClient>();
				client1.Info(); //info call 1
				client1.Info(); //info call 2
				client1.Info(); //info call 3
				client1.Info(); //info call 4
				client1.Info(); //info call 5

				sniffCall.MustHaveHappened(Repeated.Exactly.Twice);
				nowCall.MustHaveHappened(Repeated.Exactly.Times(8));

				//var nowCall = A.CallTo(() => fake.Resolve<IDateTimeProvider>().Sniff(A<Uri>._, A<int>._));
			}
		}
		
		[Test]
		public void SniffIsCalledAfterItHasGoneOutOfDate_NotWhenItSeesA503()
		{
			using (var fake = new AutoFake())
			{
				var dateTimeProvider = fake.Resolve<IDateTimeProvider>();
				var nowCall = A.CallTo(()=>dateTimeProvider.Now());
				nowCall.ReturnsNextFromSequence(
					DateTime.UtcNow, //initial sniff time (set even if not sniff_on_startup
					DateTime.UtcNow, //info call 1
					DateTime.UtcNow, //info call 2
					DateTime.UtcNow.AddMinutes(10), //info call 3
					DateTime.UtcNow.AddMinutes(10), //set now after sniff 3
					DateTime.UtcNow.AddMinutes(10), //info call 4
					DateTime.UtcNow.AddMinutes(12) //info call 5
				);
				var connectionPool = new SniffingConnectionPool(new[] { new Uri("http://localhost:9200") });
				var config = new ConnectionConfiguration(connectionPool)
					.SniffLifeSpan(TimeSpan.FromMinutes(4));
				fake.Provide<IConnectionConfigurationValues>(config);
				fake.Provide<ITransport>(fake.Resolve<Transport>());
				var connection = fake.Resolve<IConnection>();
				var sniffCall = A.CallTo(() => connection.Sniff(A<Uri>._));
				var getCall = FakeCalls.GetSyncCall(fake);
				getCall.ReturnsNextFromSequence(
					FakeResponse.Ok(config), //info 1
					FakeResponse.Ok(config), //info 2
					FakeResponse.Ok(config), //info 3
					FakeResponse.Ok(config), //sniff
					FakeResponse.Ok(config), //info 4
					FakeResponse.Bad(config) //info 5
				);

				var client1 = fake.Resolve<ElasticsearchClient>();
				client1.Info(); //info call 1
				client1.Info(); //info call 2
				client1.Info(); //info call 3
				client1.Info(); //info call 4
				client1.Info(); //info call 5

				sniffCall.MustHaveHappened(Repeated.Exactly.Once);
				nowCall.MustHaveHappened(Repeated.Exactly.Times(7));

				//var nowCall = A.CallTo(() => fake.Resolve<IDateTimeProvider>().Sniff(A<Uri>._, A<int>._));
			}
		}
		
		[Test]
		public void SniffOnConnectionFaultCausesSniffOn503()
		{
			using (var fake = new AutoFake())
			{
				var dateTimeProvider = fake.Resolve<IDateTimeProvider>();
				var nowCall = A.CallTo(()=>dateTimeProvider.Now());
				nowCall.Returns(DateTime.UtcNow);

				var connectionPool = new SniffingConnectionPool(new[] { new Uri("http://localhost:9200") });
				var config = new ConnectionConfiguration(connectionPool)
					.SniffOnConnectionFault();
				fake.Provide<IConnectionConfigurationValues>(config);
				fake.Provide<ITransport>(fake.Resolve<Transport>());
				var connection = fake.Resolve<IConnection>();
				var sniffCall = A.CallTo(() => connection.Sniff(A<Uri>._));
				var getCall = FakeCalls.GetSyncCall(fake);
				getCall.ReturnsNextFromSequence(
					
					FakeResponse.Ok(config), //info 1
					FakeResponse.Ok(config), //info 2
					FakeResponse.Ok(config), //info 3
					FakeResponse.Ok(config), //info 4
					FakeResponse.Bad(config) //info 5
				);

				var client1 = fake.Resolve<ElasticsearchClient>();
				client1.Info(); //info call 1
				client1.Info(); //info call 2
				client1.Info(); //info call 3
				client1.Info(); //info call 4
				Assert.Throws<MaxRetryException>(()=>client1.Info()); //info call 5

				sniffCall.MustHaveHappened(Repeated.Exactly.Once);
				nowCall.MustHaveHappened(Repeated.Exactly.Times(7));

				//var nowCall = A.CallTo(() => fake.Resolve<IDateTimeProvider>().Sniff(A<Uri>._, A<int>._));
			}
		}
		[Test]
		public void HostsReturnedBySniffAreVisited()
		{
			using (var fake = new AutoFake())
			{
				var dateTimeProvider = fake.Resolve<IDateTimeProvider>();
				var nowCall = A.CallTo(()=>dateTimeProvider.Now());
				nowCall.Returns(DateTime.UtcNow);

				var connectionPool = new SniffingConnectionPool(new[]
				{
					new Uri("http://localhost:9200"),
					new Uri("http://localhost:9201")
				}, randomizeOnStartup: false);
				var config = new ConnectionConfiguration(connectionPool)
					.SniffOnConnectionFault();
				fake.Provide<IConnectionConfigurationValues>(config);
				var connection = fake.Resolve<IConnection>();
				var sniffCall = A.CallTo(() => connection.Sniff(A<Uri>._));
				sniffCall.Returns(new List<Uri>()
				{
					new Uri("http://localhost:9204"),
					new Uri("http://localhost:9203"),
					new Uri("http://localhost:9202"),
					new Uri("http://localhost:9201")
				});

				var seenNodes = new List<Uri>();
				//var getCall =  FakeResponse.GetSyncCall(fake);
				var getCall = A.CallTo(() => connection.GetSync(A<Uri>._, A<IConnectionConfigurationOverrides>._));
				getCall.ReturnsNextFromSequence(
					FakeResponse.Ok(config), //info 1
					FakeResponse.Bad(config), //info 2
					FakeResponse.Ok(config), //info 2 retry
					FakeResponse.Ok(config), //info 3
					FakeResponse.Ok(config), //info 4
					FakeResponse.Ok(config), //info 5
					FakeResponse.Ok(config), //info 6
					FakeResponse.Ok(config), //info 7
					FakeResponse.Ok(config), //info 8
					FakeResponse.Ok(config) //info 9
				);
				getCall.Invokes((Uri u, object o) => seenNodes.Add(u));

				fake.Provide<ITransport>(fake.Resolve<Transport>());
				var client1 = fake.Resolve<ElasticsearchClient>();
				client1.Info(); //info call 1
				client1.Info(); //info call 2
				client1.Info(); //info call 3
				client1.Info(); //info call 4
				client1.Info(); //info call 5
				client1.Info(); //info call 6
				client1.Info(); //info call 7
				client1.Info(); //info call 8
				client1.Info(); //info call 9

				sniffCall.MustHaveHappened(Repeated.Exactly.Once);
				seenNodes.Should().NotBeEmpty().And.HaveCount(10);
				seenNodes[0].Port.Should().Be(9200);
				seenNodes[1].Port.Should().Be(9201);
				//after sniff
				seenNodes[2].Port.Should().Be(9202);
				seenNodes[3].Port.Should().Be(9204, string.Join(",", seenNodes.Select(n=>n.Port)));
				seenNodes[4].Port.Should().Be(9203);
				seenNodes[5].Port.Should().Be(9202);
				seenNodes[6].Port.Should().Be(9201);

				//var nowCall = A.CallTo(() => fake.Resolve<IDateTimeProvider>().Sniff(A<Uri>._, A<int>._));
			}
		}
	}
}