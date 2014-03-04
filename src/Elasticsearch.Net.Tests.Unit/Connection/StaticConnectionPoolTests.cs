using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.FakeItEasy;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Exceptions;
using Elasticsearch.Net.Tests.Unit.Stubs;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace Elasticsearch.Net.Tests.Unit.Connection
{
	[TestFixture]
	public class StaticConnectionPoolTests
	{
		private static Uri[] _uris = new[]
		{
			new Uri("http://localhost:9200"),
			new Uri("http://localhost:9201"),
			new Uri("http://localhost:9202"),
			new Uri("http://localhost:9203"),
		};

		private static readonly int _retries = _uris.Count() - 1;

		[Test]
		public void ThrowsOutOfNodesException_AndRetriesTheSpecifiedTimes()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				var connectionPool = new StaticConnectionPool(_uris);

				fake.Provide<IConnectionConfigurationValues>(new ConnectionConfiguration(connectionPool));
				
				var getCall = A.CallTo(() => fake.Resolve<IConnection>().GetSync(A<Uri>._));
				getCall.Throws<Exception>();
				
				var client = fake.Resolve<ElasticsearchClient>();

				//we don't specify our own value so it should be up to the connection pool
				client.Settings.MaxRetries.Should().Be(null);

				client.Settings.ConnectionPool.MaxRetries.Should().Be(_retries);

				Assert.Throws<OutOfNodesException>(()=> client.Info());
				getCall.MustHaveHappened(Repeated.Exactly.Times(_retries + 1));

			}
		}

		[Test]
		public void AllNodesMustBeTriedOnce()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				var connectionPool = new StaticConnectionPool(_uris);
				fake.Provide<IConnectionConfigurationValues>(new ConnectionConfiguration(connectionPool));
				var connection = fake.Resolve<NoopConnection>();
				
				var calls = _uris.Select(u =>
					A.CallTo(()=> fake.Resolve<IUriObserver>().Observe(A<Uri>.That.Matches(uu=>uu.Port == u.Port)))
				).ToList();

				fake.Provide<IConnection>(connection);

				foreach (var c in calls)
					c.Throws<Exception>();

				fake.Provide<IHttpTransport>(fake.Resolve<HttpTransport>());
				var client = fake.Resolve<ElasticsearchClient>();

				Assert.Throws<OutOfNodesException>(()=> client.Info());

				foreach (var call in calls)
					call.MustHaveHappened(Repeated.Exactly.Once);

			}
		}
		
		[Test]
		public void HardRetryLimitTakesPrecedence()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				var connectionPool = new StaticConnectionPool(_uris);
				fake.Provide<IConnectionConfigurationValues>(new ConnectionConfiguration(connectionPool)
					.SetMaxRetries(7));
				var connection = fake.Resolve<NoopConnection>();
				
				var calls = _uris.Select(u =>
					A.CallTo(()=> fake.Resolve<IUriObserver>().Observe(A<Uri>.That.Matches(uu=>uu.Port == u.Port)))
				).ToList();

				fake.Provide<IConnection>(connection);

				foreach (var c in calls)
					c.Throws<Exception>();

				var client = fake.Resolve<ElasticsearchClient>();

				Assert.Throws<OutOfNodesException>(()=> client.Info());

				//twice because we say we want the retry the call 7 times (original + retry == 8)
				foreach (var call in calls)
					call.MustHaveHappened(Repeated.Exactly.Twice);

			}
		}
	}
}
