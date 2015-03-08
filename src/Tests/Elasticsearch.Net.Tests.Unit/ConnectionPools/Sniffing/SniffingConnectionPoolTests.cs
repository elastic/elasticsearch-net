using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Elasticsearch.Net.Tests.Unit.ConnectionPools.Sniffing
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
				var uris = new[] { new Uri("http://localhost:9200") };
				var connectionPool = new SniffingConnectionPool(uris);
				var config = new ConnectionConfiguration(connectionPool)
					.DisablePing()
					.SniffOnStartup();
				fake.Provide<IConnectionConfigurationValues>(config);
				var sniffCall = FakeCalls.Sniff(fake, config, uris);
				var transport = FakeCalls.ProvideDefaultTransport(fake);
				var client1 = new ElasticsearchClient(config, transport: transport); 
				var client2 = new ElasticsearchClient(config, transport: transport); 
				var client3 = new ElasticsearchClient(config, transport: transport); 
				var client4 = new ElasticsearchClient(config, transport: transport); 

				sniffCall.MustHaveHappened(Repeated.Exactly.Once);
			}
		}

		[Test]
		public void SniffCalledOnceAndEachEnpointPingedOnce()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				//It's recommended to only have on instance of your connection pool
				//Be sure to register it as Singleton in your IOC
				var uris = new[] { new Uri("http://localhost:9200"), new Uri("http://localhost:9201") };
				var connectionPool = new SniffingConnectionPool(uris);
				var config = new ConnectionConfiguration(connectionPool)
					.SniffOnStartup();
				fake.Provide<IConnectionConfigurationValues>(config);

				var pingCall = FakeCalls.PingAtConnectionLevel(fake);
				pingCall.Returns(FakeResponse.Ok(config));
				var sniffCall = FakeCalls.Sniff(fake, config, uris);
				var getCall = FakeCalls.GetSyncCall(fake);
				getCall.Returns(FakeResponse.Ok(config));

				var transport1 = FakeCalls.ProvideRealTranportInstance(fake);
				var transport2 = FakeCalls.ProvideRealTranportInstance(fake);
				var transport3 = FakeCalls.ProvideRealTranportInstance(fake);
				var transport4 = FakeCalls.ProvideRealTranportInstance(fake);

				transport1.Should().NotBe(transport2);

				var client1 = new ElasticsearchClient(config, transport: transport1);
				client1.Info();
				client1.Info();
				client1.Info();
				client1.Info();
				var client2 = new ElasticsearchClient(config, transport: transport2); 
				client2.Info();
				client2.Info();
				client2.Info();
				client2.Info();
				var client3 = new ElasticsearchClient(config, transport: transport3); 
				client3.Info();
				client3.Info();
				client3.Info();
				client3.Info();
				var client4 = new ElasticsearchClient(config, transport: transport4); 
				client4.Info();
				client4.Info();
				client4.Info();
				client4.Info();

				sniffCall.MustHaveHappened(Repeated.Exactly.Once);
				//sniff validates first node, one new node should be pinged before usage.
				pingCall.MustHaveHappened(Repeated.Exactly.Once);
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
				var uris = new[] { new Uri("http://localhost:9200") };
				var connectionPool = new SniffingConnectionPool(uris);
				var config = new ConnectionConfiguration(connectionPool)
					.SniffLifeSpan(TimeSpan.FromMinutes(4));
				fake.Provide<IConnectionConfigurationValues>(config);
				var transport = FakeCalls.ProvideDefaultTransport(fake, dateTimeProvider);
				var connection = fake.Resolve<IConnection>();
				var sniffCall = FakeCalls.Sniff(fake, config, uris);
				
				var pingCall = FakeCalls.PingAtConnectionLevel(fake);
				pingCall.Returns(FakeResponse.Ok(config));

				var getCall = FakeCalls.GetSyncCall(fake);
				getCall.Returns(FakeResponse.Ok(config));

				var client1 = fake.Resolve<ElasticsearchClient>();
				var result = client1.Info(); //info call 1
				result = client1.Info(); //info call 2
				result = client1.Info(); //info call 3
				result = client1.Info(); //info call 4
				result = client1.Info(); //info call 5

				sniffCall.MustHaveHappened(Repeated.Exactly.Twice);
				nowCall.MustHaveHappened(Repeated.Exactly.Times(8));
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
				var uris = new[] { new Uri("http://localhost:9200") };
				var connectionPool = new SniffingConnectionPool(uris);
				var config = new ConnectionConfiguration(connectionPool)
					.SniffLifeSpan(TimeSpan.FromMinutes(4))
					.ExposeRawResponse();
				fake.Provide<IConnectionConfigurationValues>(config);
				var transport = FakeCalls.ProvideDefaultTransport(fake, dateTimeProvider);

				var pingCall = FakeCalls.PingAtConnectionLevel(fake);
				pingCall.Returns(FakeResponse.Ok(config));

				var sniffCall = FakeCalls.Sniff(fake, config, uris);
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
				var result = client1.Info(); //info call 1
				result = client1.Info(); //info call 2
				result = client1.Info(); //info call 3
				result = client1.Info(); //info call 4
				result = client1.Info(); //info call 5

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
				nowCall.Invokes(() =>
				{

				});
				nowCall.Returns(DateTime.UtcNow);
				var nodes = new[] { new Uri("http://localhost:9200") };
				var connectionPool = new SniffingConnectionPool(nodes);
				var config = new ConnectionConfiguration(connectionPool)
					.SniffOnConnectionFault();
				fake.Provide<IConnectionConfigurationValues>(config);
				var transport = FakeCalls.ProvideDefaultTransport(fake, dateTimeProvider);
				var connection = fake.Resolve<IConnection>();

				var sniffNewNodes = new[]
				{
					new Uri("http://localhost:9200"),
					new Uri("http://localhost:9201")
				};
				var pingCall = FakeCalls.PingAtConnectionLevel(fake);
				pingCall.Returns(FakeResponse.Ok(config));

				var sniffCall = FakeCalls.Sniff(fake, config, sniffNewNodes);
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
				nowCall.MustHaveHappened(Repeated.Exactly.Times(10));

			}
		}

		[Test]
		public async void HostsReturnedBySniffAreVisited_Async()
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
				FakeCalls.ProvideDefaultTransport(fake);
				FakeCalls.PingAtConnectionLevelAsync(fake)
					.ReturnsLazily(()=>
						FakeResponse.OkAsync(config)
					);
				
				var sniffCall = FakeCalls.Sniff(fake, config, new List<Uri>()
				{
					new Uri("http://localhost:9204"),
					new Uri("http://localhost:9203"),
					new Uri("http://localhost:9202"),
					new Uri("http://localhost:9201")
				});

				var connection = fake.Resolve<IConnection>();
				var seenNodes = new List<Uri>();
				//var getCall =  FakeResponse.GetSyncCall(fake);
				var getCall = A.CallTo(() => connection.Get(
					A<Uri>.That.Not.Matches(u=>u.AbsolutePath.StartsWith("/_nodes")), 
					A<IRequestConfiguration>._));
				getCall.ReturnsNextFromSequence(
					FakeResponse.OkAsync(config), //info 1
					FakeResponse.BadAsync(config), //info 2
					FakeResponse.OkAsync(config), //info 2 retry
					FakeResponse.OkAsync(config), //info 3
					FakeResponse.OkAsync(config), //info 4
					FakeResponse.OkAsync(config), //info 5
					FakeResponse.OkAsync(config), //info 6
					FakeResponse.OkAsync(config), //info 7
					FakeResponse.OkAsync(config), //info 8
					FakeResponse.OkAsync(config) //info 9
				);
				getCall.Invokes((Uri u, IRequestConfiguration o) => seenNodes.Add(u));

				var client1 = fake.Resolve<ElasticsearchClient>();
				await client1.InfoAsync(); //info call 1
				await client1.InfoAsync(); //info call 2
				await client1.InfoAsync(); //info call 3
				await client1.InfoAsync(); //info call 4
				await client1.InfoAsync(); //info call 5
				await client1.InfoAsync(); //info call 6
				await client1.InfoAsync(); //info call 7
				await client1.InfoAsync(); //info call 8
				await client1.InfoAsync(); //info call 9

				sniffCall.MustHaveHappened(Repeated.Exactly.Once);
				seenNodes.Should().NotBeEmpty().And.HaveCount(10);
				seenNodes[0].Port.Should().Be(9200);
				seenNodes[1].Port.Should().Be(9201);
				//after sniff
				seenNodes[2].Port.Should().Be(9202, string.Join(",", seenNodes.Select(n=>n.Port)));
				seenNodes[3].Port.Should().Be(9204);
				seenNodes[4].Port.Should().Be(9203);
				seenNodes[5].Port.Should().Be(9202);
				seenNodes[6].Port.Should().Be(9201);
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
				FakeCalls.ProvideDefaultTransport(fake);
				FakeCalls.PingAtConnectionLevel(fake)
					.Returns(FakeResponse.Ok(config));
				
				var sniffCall = FakeCalls.Sniff(fake, config, new List<Uri>()
				{
					new Uri("http://localhost:9204"),
					new Uri("http://localhost:9203"),
					new Uri("http://localhost:9202"),
					new Uri("http://localhost:9201")
				});

				var connection = fake.Resolve<IConnection>();
				var seenNodes = new List<Uri>();
				//var getCall =  FakeResponse.GetSyncCall(fake);
				var getCall = A.CallTo(() => connection.GetSync(
					A<Uri>.That.Not.Matches(u=>u.AbsolutePath.StartsWith("/_nodes")), 
					A<IRequestConfiguration>._));
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
				getCall.Invokes((Uri u, IRequestConfiguration o) => seenNodes.Add(u));

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
				seenNodes[2].Port.Should().Be(9202, string.Join(",", seenNodes.Select(n=>n.Port)));
				seenNodes[3].Port.Should().Be(9204);
				seenNodes[4].Port.Should().Be(9203);
				seenNodes[5].Port.Should().Be(9202);
				seenNodes[6].Port.Should().Be(9201);
			}
		}
		
		[Test]
		public void ShouldRetryOnSniffConnectionException_Async()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{	
				var uris = new[]
				{
					new Uri("http://localhost:9200"),
					new Uri("http://localhost:9201"),
					new Uri("http://localhost:9202")
				};
				var connectionPool = new SniffingConnectionPool(uris, randomizeOnStartup: false);
				var config = new ConnectionConfiguration(connectionPool)
					.SniffOnConnectionFault();
				
				fake.Provide<IConnectionConfigurationValues>(config);

				var pingAsyncCall = FakeCalls.PingAtConnectionLevelAsync(fake);
				pingAsyncCall.Returns(FakeResponse.OkAsync(config));

				//sniffing is always synchronous and in turn will issue synchronous pings
				var pingCall = FakeCalls.PingAtConnectionLevel(fake);
				pingCall.Returns(FakeResponse.Ok(config));

				var sniffCall = FakeCalls.Sniff(fake);
				var seenPorts = new List<int>();
				sniffCall.ReturnsLazily((Uri u, IRequestConfiguration c) =>
				{
					seenPorts.Add(u.Port);
					throw new Exception("Something bad happened");
				});

				var getCall = FakeCalls.GetCall(fake);
				getCall.Returns(FakeResponse.BadAsync(config));

				FakeCalls.ProvideDefaultTransport(fake);
				
				var client = fake.Resolve<ElasticsearchClient>();

				var e = Assert.Throws<MaxRetryException>(async () => await client.NodesHotThreadsAsync("nodex"));

				//all nodes must be tried to sniff for more information
				sniffCall.MustHaveHappened(Repeated.Exactly.Times(uris.Count()));
				//make sure we only saw one call to hot threads (the one that failed initially)
				getCall.MustHaveHappened(Repeated.Exactly.Once);
				
				//make sure the sniffs actually happened on all the individual nodes
				seenPorts.ShouldAllBeEquivalentTo(uris.Select(u=>u.Port));
				e.InnerException.Message.Should().Contain("Sniffing known nodes");
			}
		}
		
		[Test]
		public void ShouldRetryOnSniffConnectionException()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{	
				var uris = new[]
				{
					new Uri("http://localhost:9200"),
					new Uri("http://localhost:9201")
				};
				var connectionPool = new SniffingConnectionPool(uris, randomizeOnStartup: false);
				var config = new ConnectionConfiguration(connectionPool)
					.SniffOnConnectionFault();
				
				fake.Provide<IConnectionConfigurationValues>(config);
				FakeCalls.ProvideDefaultTransport(fake);

				var pingCall = FakeCalls.PingAtConnectionLevel(fake);
				pingCall.Returns(FakeResponse.Ok(config));

				var sniffCall = FakeCalls.Sniff(fake);
				var seenPorts = new List<int>();
				sniffCall.ReturnsLazily((Uri u, IRequestConfiguration c) =>
				{
					seenPorts.Add(u.Port);
					throw new Exception("Something bad happened");
				});

				var getCall = FakeCalls.GetSyncCall(fake);
				getCall.Returns(FakeResponse.Bad(config));

				var client = fake.Resolve<ElasticsearchClient>();

				var e = Assert.Throws<MaxRetryException>(() => client.Info());
				sniffCall.MustHaveHappened(Repeated.Exactly.Times(uris.Count()));
				getCall.MustHaveHappened(Repeated.Exactly.Once);
				
				//make sure that if a ping throws an exception it wont
				//keep retrying to ping the same node but failover to the next
				seenPorts.ShouldAllBeEquivalentTo(uris.Select(u=>u.Port));

				var sniffException = e.InnerException as SniffException;
				sniffException.Should().NotBeNull();
			}
		}

		[Test]
		public void ShouldRetryOnSniff500()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				var uris = new[]
				{
					new Uri("http://localhost:9200"),
					new Uri("http://localhost:9201"),
					new Uri("http://localhost:9202")
				};
				var connectionPool = new SniffingConnectionPool(uris, randomizeOnStartup: false);
				var config = new ConnectionConfiguration(connectionPool)
					.SniffOnConnectionFault();

				fake.Provide<IConnectionConfigurationValues>(config);
				FakeCalls.ProvideDefaultTransport(fake);

				var pingCall = FakeCalls.PingAtConnectionLevel(fake);
				pingCall.Returns(FakeResponse.Ok(config));

				var sniffCall = FakeCalls.Sniff(fake);
				var seenPorts = new List<int>();
				sniffCall.ReturnsLazily((Uri u, IRequestConfiguration c) =>
				{
					seenPorts.Add(u.Port);
					return FakeResponse.Bad(config);
				});

				var getCall = FakeCalls.GetSyncCall(fake);
				getCall.Returns(FakeResponse.Bad(config));

				var client = fake.Resolve<ElasticsearchClient>();

				var e = Assert.Throws<MaxRetryException>(() => client.Info());
				sniffCall.MustHaveHappened(Repeated.Exactly.Times(uris.Count()));
				getCall.MustHaveHappened(Repeated.Exactly.Once);

				//make sure that if a ping throws an exception it wont
				//keep retrying to ping the same node but failover to the next
				seenPorts.ShouldAllBeEquivalentTo(uris.Select(u => u.Port));

				var sniffException = e.InnerException as SniffException;
				sniffException.Should().NotBeNull();
			}
		}
		

	}
}