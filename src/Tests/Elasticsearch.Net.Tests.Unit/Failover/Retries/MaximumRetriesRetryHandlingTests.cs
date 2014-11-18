using Autofac.Extras.FakeItEasy;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Tests.Unit.Stubs;
using FakeItEasy;
using NUnit.Framework;

namespace Elasticsearch.Net.Tests.Unit.Failover.Retries
{
	[TestFixture]
	public class MaximumRetriesRetryHandlingTests
	{
		[Test]
		public void ShouldNotRetryWhenMaxRetriesIs0()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				var connectionConfiguration = new ConnectionConfiguration().MaximumRetries(0);
				fake.Provide<IConnectionConfigurationValues>(connectionConfiguration);
				FakeCalls.ProvideDefaultTransport(fake);

				var getCall = FakeCalls.GetSyncCall(fake);
				getCall.Returns(FakeResponse.Bad(connectionConfiguration));

				var client = fake.Resolve<ElasticsearchClient>();

				Assert.DoesNotThrow(() => client.Info());
				getCall.MustHaveHappened(Repeated.Exactly.Once);

			}
		}

		[Test]
		public void ShouldNotRetryWhenMaxRetriesIs0_Async()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				var connectionConfiguration = new ConnectionConfiguration().MaximumRetries(0);
				fake.Provide<IConnectionConfigurationValues>(connectionConfiguration);
				FakeCalls.ProvideDefaultTransport(fake);

				var getCall = FakeCalls.GetCall(fake);
				getCall.Returns(FakeResponse.Bad(connectionConfiguration));

				var client = fake.Resolve<ElasticsearchClient>();

				Assert.DoesNotThrow(async () => await client.InfoAsync());
				getCall.MustHaveHappened(Repeated.Exactly.Once);

			}
		}
	}
}