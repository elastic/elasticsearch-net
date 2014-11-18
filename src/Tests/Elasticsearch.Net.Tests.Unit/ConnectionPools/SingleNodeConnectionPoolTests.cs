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

		private ITransport ProvideTransport(AutoFake fake)
		{
			var param = new TypedParameter(typeof(IDateTimeProvider), null);
			return fake.Provide<ITransport, Transport>(param);
		}

		[Test]
		public void Should_Not_Retry_On_IConnection_Exception()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				//set up connection configuration that holds a connection pool
				//with '_uris' (see the constructor)
				fake.Provide<IConnectionConfigurationValues>(_config);
				//prove a real HttpTransport with its unspecified dependencies
				//as fakes
				this.ProvideTransport(fake);

				//set up fake for a call on IConnection.GetSync so that it always throws 
				//an exception
				var getCall = FakeCalls.GetSyncCall(fake);
				getCall.Returns(FakeResponse.AnyWithException(_config, -1, innerException: new Exception("inner")));
				var pingCall = FakeCalls.PingAtConnectionLevel(fake);
				pingCall.Returns(_ok);
				
				//create a real ElasticsearchClient with it unspecified dependencies
				//as fakes
				var client = fake.Resolve<ElasticsearchClient>();

				//our settings dictate retrying 2 times
				client.Settings.MaxRetries.Should().Be(2);
				
				//the single node connection pool always reports 0 for maxretries
				client.Settings.ConnectionPool.MaxRetries.Should().Be(0);

				//
				var exception = Assert.Throws<Exception>(()=> client.Info());
				exception.Message.Should().Be("inner");
				//the GetSync method must in total have been called the number of nodes times.
				getCall.MustHaveHappened(Repeated.Exactly.Once);

			}
		}

	}
}
