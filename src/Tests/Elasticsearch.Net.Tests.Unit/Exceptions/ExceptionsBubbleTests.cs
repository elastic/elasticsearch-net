using System;
using System.IO;
using System.Threading.Tasks;
using Autofac.Extras.FakeItEasy;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Tests.Unit.Stubs;
using FakeItEasy;
using NUnit.Framework;

namespace Elasticsearch.Net.Tests.Unit.Exceptions
{
	[TestFixture]
	public class ExceptionsBubbleTests
	{
		//we do not pass a Uri or IConnectionPool so this config
		//defaults to SingleNodeConnectionPool()
		private readonly ConnectionConfiguration _connectionConfig = new ConnectionConfiguration()
			.MaximumRetries(0);

		[Test]
		public void CallThrowsHardException_ShouldBubbleToCallee()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				fake.Provide<IConnectionConfigurationValues>(_connectionConfig);
				FakeCalls.ProvideDefaultTransport(fake);

				var getCall = FakeCalls.GetSyncCall(fake);
				getCall.Throws<Exception>();

				var client = fake.Resolve<ElasticsearchClient>();

				Assert.Throws<Exception>(() => client.Info());
				getCall.MustHaveHappened(Repeated.Exactly.Once);

			}
		}

		[Test]
		public void Async_CallThrowsSoftException_ShouldBubbleToCallee()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				fake.Provide<IConnectionConfigurationValues>(_connectionConfig);
				FakeCalls.ProvideDefaultTransport(fake);
				var getCall = FakeCalls.GetCall(fake);

				//return a started task that throws
				Func<ElasticsearchResponse<Stream>> badTask = () => { throw new Exception(); };
				var t = new Task<ElasticsearchResponse<Stream>>(badTask);
				t.Start();
				getCall.Returns(t);

				var client = fake.Resolve<ElasticsearchClient>();

				Assert.Throws<Exception>(async () => await client.InfoAsync());
				getCall.MustHaveHappened(Repeated.Exactly.Once);

			}
		}
	}
}