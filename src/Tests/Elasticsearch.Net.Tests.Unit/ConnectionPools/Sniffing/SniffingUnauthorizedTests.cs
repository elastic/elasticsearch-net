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
	public class SniffingUnauthorizedTests
	{

		[Test]
		public void ShouldThrowAndNotRetrySniffOnStartup403()
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
					.SniffOnStartup();

				var pingCall = FakeCalls.PingAtConnectionLevel(fake);
				pingCall.Returns(FakeResponse.Ok(config));

				var sniffCall = FakeCalls.Sniff(fake);
				var seenPorts = new List<int>();
				sniffCall.ReturnsLazily((Uri u, IRequestConfiguration c) =>
				{
					seenPorts.Add(u.Port);
					return FakeResponse.Any(config, 403);
				});

				var getCall = FakeCalls.GetSyncCall(fake);
				getCall.Returns(FakeResponse.Bad(config));

				fake.Provide<IConnectionConfigurationValues>(config);

				var e = Assert.Throws<ElasticsearchAuthorizationException>(() => FakeCalls.ProvideRealTranportInstance(fake));

				sniffCall.MustHaveHappened(Repeated.Exactly.Once);
				getCall.MustNotHaveHappened();
			}
		}

		[Test]
		public void ShouldNotThrowAndNotRetrySniffOnConnectionFault403()
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
					return FakeResponse.Any(config, 403);
				});

				var getCall = FakeCalls.GetSyncCall(fake);
				getCall.Returns(FakeResponse.Bad(config));

				var client = fake.Resolve<ElasticsearchClient>();

				Assert.DoesNotThrow(() => client.Info());
				sniffCall.MustHaveHappened(Repeated.Exactly.Once);
				getCall.MustHaveHappened(Repeated.Exactly.Once);
			}
		}

		[Test]
		public void ShouldNotThrowAndNotRetrySniffOnConnectionFault403_Async()
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
					return FakeResponse.Any(config, 403);
				});

				var getCall = FakeCalls.GetCall(fake);
				getCall.Returns(FakeResponse.BadAsync(config));

				var client = fake.Resolve<ElasticsearchClient>();

				Assert.DoesNotThrow(async () => await client.InfoAsync());
				sniffCall.MustHaveHappened(Repeated.Exactly.Once);
				getCall.MustHaveHappened(Repeated.Exactly.Once);
			}
		}

		[Test]
		public void ShouldThrowAndNotRetrySniffOnConnectionFault403()
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
					.SniffOnConnectionFault()
					.ThrowOnElasticsearchServerExceptions();

				fake.Provide<IConnectionConfigurationValues>(config);
				FakeCalls.ProvideDefaultTransport(fake);

				var pingCall = FakeCalls.PingAtConnectionLevel(fake);
				pingCall.Returns(FakeResponse.Ok(config));

				var sniffCall = FakeCalls.Sniff(fake);
				var seenPorts = new List<int>();
				sniffCall.ReturnsLazily((Uri u, IRequestConfiguration c) =>
				{
					seenPorts.Add(u.Port);
					return FakeResponse.Any(config, 403);
				});

				var getCall = FakeCalls.GetSyncCall(fake);
				getCall.Returns(FakeResponse.Bad(config));

				var client = fake.Resolve<ElasticsearchClient>();

				var e = Assert.Throws<ElasticsearchServerException>(() => client.Info());
				e.Status.Should().Be(403);
				sniffCall.MustHaveHappened(Repeated.Exactly.Once);
				getCall.MustHaveHappened(Repeated.Exactly.Once);
			}
		}

		[Test]
		public void ShouldThrowAndNotRetrySniffOnConnectionFault403_Async()
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
					.SniffOnConnectionFault()
					.ThrowOnElasticsearchServerExceptions();

				fake.Provide<IConnectionConfigurationValues>(config);
				FakeCalls.ProvideDefaultTransport(fake);

				var pingAsyncCall = FakeCalls.PingAtConnectionLevelAsync(fake);
				pingAsyncCall.Returns(FakeResponse.OkAsync(config));

				var pingCall = FakeCalls.PingAtConnectionLevel(fake);
				pingCall.Returns(FakeResponse.Ok(config));

				var sniffCall = FakeCalls.Sniff(fake);
				var seenPorts = new List<int>();
				sniffCall.ReturnsLazily((Uri u, IRequestConfiguration c) =>
				{
					seenPorts.Add(u.Port);
					return FakeResponse.Any(config, 403);
				});

				var getCall = FakeCalls.GetCall(fake);
				getCall.Returns(FakeResponse.BadAsync(config));

				var client = fake.Resolve<ElasticsearchClient>();

				var e = Assert.Throws<ElasticsearchServerException>(async () => await client.InfoAsync());
				e.Status.Should().Be(403);
				sniffCall.MustHaveHappened(Repeated.Exactly.Once);
				getCall.MustHaveHappened(Repeated.Exactly.Once);
			}
		}

		[Test]
		public void ShouldNotThrowAndNotRetrySniffInformationIsTooOld403()
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
					.SniffLifeSpan(new TimeSpan(1));

				fake.Provide<IConnectionConfigurationValues>(config);
				FakeCalls.ProvideDefaultTransport(fake);

				var pingCall = FakeCalls.PingAtConnectionLevel(fake);
				pingCall.Returns(FakeResponse.Ok(config));

				var sniffCall = FakeCalls.Sniff(fake);
				var seenPorts = new List<int>();
				sniffCall.ReturnsLazily((Uri u, IRequestConfiguration c) =>
				{
					seenPorts.Add(u.Port);
					return FakeResponse.Any(config, 403);
				});

				var getCall = FakeCalls.GetSyncCall(fake);
				getCall.Returns(FakeResponse.Bad(config));

				var client = fake.Resolve<ElasticsearchClient>();

				Assert.DoesNotThrow(() => client.Info());
				sniffCall.MustHaveHappened(Repeated.Exactly.Once);
				getCall.MustNotHaveHappened();
			}
		}

		[Test]
		public void ShouldNotThrowAndNotRetrySniffInformationIsTooOld403_Async()
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
					.SniffLifeSpan(new TimeSpan(1));

				fake.Provide<IConnectionConfigurationValues>(config);
				FakeCalls.ProvideDefaultTransport(fake);

				var pingAsyncCall = FakeCalls.PingAtConnectionLevelAsync(fake);
				pingAsyncCall.Returns(FakeResponse.OkAsync(config));

				var pingCall = FakeCalls.PingAtConnectionLevel(fake);
				pingCall.Returns(FakeResponse.Ok(config));

				var sniffCall = FakeCalls.Sniff(fake);
				var seenPorts = new List<int>();
				sniffCall.ReturnsLazily((Uri u, IRequestConfiguration c) =>
				{
					seenPorts.Add(u.Port);
					return FakeResponse.Any(config, 403);
				});

				var getCall = FakeCalls.GetCall(fake);
				getCall.Returns(FakeResponse.BadAsync(config));

				var client = fake.Resolve<ElasticsearchClient>();

				Assert.DoesNotThrow(async () => await client.InfoAsync());
				sniffCall.MustHaveHappened(Repeated.Exactly.Once);
				getCall.MustNotHaveHappened();
			}
		}

		[Test]
		public void ShouldThrowAndNotRetrySniffInformationIsTooOld403()
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
					.SniffLifeSpan(new TimeSpan(1))
					.ThrowOnElasticsearchServerExceptions();

				fake.Provide<IConnectionConfigurationValues>(config);
				FakeCalls.ProvideDefaultTransport(fake);

				var pingCall = FakeCalls.PingAtConnectionLevel(fake);
				pingCall.Returns(FakeResponse.Ok(config));

				var sniffCall = FakeCalls.Sniff(fake);
				var seenPorts = new List<int>();
				sniffCall.ReturnsLazily((Uri u, IRequestConfiguration c) =>
				{
					seenPorts.Add(u.Port);
					return FakeResponse.Any(config, 403);
				});

				var getCall = FakeCalls.GetSyncCall(fake);
				getCall.Returns(FakeResponse.Bad(config));

				var client = fake.Resolve<ElasticsearchClient>();

				var e = Assert.Throws<ElasticsearchServerException>(() => client.Info());
				e.Status.Should().Be(403);
				sniffCall.MustHaveHappened(Repeated.Exactly.Once);
				getCall.MustNotHaveHappened();
			}
		}

		[Test]
		public void ShouldThrowAndNotRetrySniffInformationIsTooOld403_Async()
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
					.SniffLifeSpan(new TimeSpan(1))
					.ThrowOnElasticsearchServerExceptions();

				fake.Provide<IConnectionConfigurationValues>(config);
				FakeCalls.ProvideDefaultTransport(fake);

				var pingAsyncCall = FakeCalls.PingAtConnectionLevelAsync(fake);
				pingAsyncCall.Returns(FakeResponse.OkAsync(config));

				var pingCall = FakeCalls.PingAtConnectionLevel(fake);
				pingCall.Returns(FakeResponse.Ok(config));

				var sniffCall = FakeCalls.Sniff(fake);
				var seenPorts = new List<int>();
				sniffCall.ReturnsLazily((Uri u, IRequestConfiguration c) =>
				{
					seenPorts.Add(u.Port);
					return FakeResponse.Any(config, 403);
				});

				var getCall = FakeCalls.GetCall(fake);
				getCall.Returns(FakeResponse.BadAsync(config));

				var client = fake.Resolve<ElasticsearchClient>();

				var e = Assert.Throws<ElasticsearchServerException>(async () => await client.InfoAsync());
				sniffCall.MustHaveHappened(Repeated.Exactly.Once);
				getCall.MustNotHaveHappened();
			}
		}
	}
}