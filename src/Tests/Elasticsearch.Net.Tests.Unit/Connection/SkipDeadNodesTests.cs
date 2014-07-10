using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.FakeItEasy;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Connection.Configuration;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Providers;
using Elasticsearch.Net.Tests.Unit.Stubs;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace Elasticsearch.Net.Tests.Unit.Connection
{
	[TestFixture]
	public class SkipDeadNodesTests
	{
		[Test]
		public void DeadNodesAreNotVisited_AndPingedAppropiately()
		{
			using (var fake = new AutoFake())
			{
				var dateTimeProvider = ProvideDateTimeProvider(fake);
				var config = ProvideConfiguration(dateTimeProvider);
				var connection = ProvideConnection(fake, config);

				var getCall = FakeCalls.GetSyncCall(fake);
				var ok = FakeResponse.Ok(config);
				var bad = FakeResponse.Bad(config);
				getCall.ReturnsNextFromSequence(
					ok,  //info 1 - 9204
					bad, //info 2 - 9203 DEAD
					ok,  //info 2 retry - 9202
					ok,  //info 3 - 9201
					ok,  //info 4 - 9204
					ok,  //info 5 - 9202
					ok,  //info 6 - 9201
					ok,  //info 7 - 9204
					ok,  //info 8 - 9203 (Now > Timeout)
					ok   //info 9 - 9202
				);
				
				var seenNodes = new List<Uri>();
				getCall.Invokes((Uri u, IRequestConfiguration o) => seenNodes.Add(u));

				var pingCall = FakeCalls.PingAtConnectionLevel(fake);
				pingCall.Returns(ok);

				var client1 = fake.Resolve<ElasticsearchClient>();
				var result = client1.Info(); //info call 1//first time node is used so a ping is sent first
				result.Metrics.Requests.Count.Should().Be(2);
				result.Metrics.Requests.First().RequestType.Should().Be(RequestType.Ping);
				result.Metrics.Requests.First().Node.Port.Should().Be(9204);
				result.Metrics.Requests.Last().RequestType.Should().Be(RequestType.ElasticsearchCall);
				result.Metrics.Requests.Last().Node.Port.Should().Be(9204);

				result = client1.Info(); //info call 2
				//using 9203 for the first time ping succeeds
				result.Metrics.Requests.First().RequestType.Should().Be(RequestType.Ping);
				result.Metrics.Requests.First().Node.Port.Should().Be(9203);
				result.Metrics.Requests.First().Success.Should().BeTrue();

				//call on 9203 fails
				result.Metrics.Requests[1].RequestType.Should().Be(RequestType.ElasticsearchCall);
				result.Metrics.Requests[1].Node.Port.Should().Be(9203);
				result.Metrics.Requests[1].Success.Should().BeFalse();
				result.Metrics.Requests[1].HttpStatusCode.Should().Be(503);

				//using 9202 for the first time ping succeeds
				result.Metrics.Requests[2].RequestType.Should().Be(RequestType.Ping);
				result.Metrics.Requests[2].Node.Port.Should().Be(9202);

				//call on 9203 fails
				result.Metrics.Requests[3].RequestType.Should().Be(RequestType.ElasticsearchCall);
				result.Metrics.Requests[3].Node.Port.Should().Be(9202);
				result.Metrics.Requests[3].Success.Should().BeTrue();
				result.Metrics.Requests[3].HttpStatusCode.Should().Be(200);
				result = client1.Info(); //info call 3
				result = client1.Info(); //info call 4
				result = client1.Info(); //info call 5
				result = client1.Info(); //info call 6
				result = client1.Info(); //info call 7
				result = client1.Info(); //info call 8
				result = client1.Info(); //info call 9

				AssertSeenNodesAreInExpectedOrder(seenNodes);

				//4 nodes first time usage + 1 time after the first time 9203 came back to live
				pingCall.MustHaveHappened(Repeated.Exactly.Times(5));

			}
		}


		[Test]
		public async void DeadNodesAreNotVisited_AndPingedAppropiately_Async()
		{
			using (var fake = new AutoFake())
			{
				var dateTimeProvider = ProvideDateTimeProvider(fake);
				var config = ProvideConfiguration(dateTimeProvider);
				var connection = ProvideConnection(fake, config);
				
				var getCall = FakeCalls.GetCall(fake);
				var ok = Task.FromResult(FakeResponse.Ok(config));
				var bad = Task.FromResult(FakeResponse.Bad(config));
				getCall.ReturnsNextFromSequence(
					ok,  //info 1 - 9204
					bad, //info 2 - 9203 DEAD
					ok,  //info 2 retry - 9202
					ok,  //info 3 - 9201
					ok,  //info 4 - 9204
					ok,  //info 5 - 9202
					ok,  //info 6 - 9201
					ok,  //info 7 - 9204
					ok,  //info 8 - 9203 (Now > Timeout)
					ok   //info 9 - 9202
				);
				
				var seenNodes = new List<Uri>();
				getCall.Invokes((Uri u, IRequestConfiguration o) => seenNodes.Add(u));

				var pingCall = FakeCalls.PingAtConnectionLevelAsync(fake);
				pingCall.Returns(ok);

				var client1 = fake.Resolve<ElasticsearchClient>();
				var result = await client1.InfoAsync(); //info call 1
				//first time node is used so a ping is sent first
				result.Metrics.Requests.Count.Should().Be(2);
				result.Metrics.Requests.First().RequestType.Should().Be(RequestType.Ping);
				result.Metrics.Requests.First().Node.Port.Should().Be(9204);
				result.Metrics.Requests.Last().RequestType.Should().Be(RequestType.ElasticsearchCall);
				result.Metrics.Requests.Last().Node.Port.Should().Be(9204);
				

				result = await client1.InfoAsync(); //info call 2
				result.Metrics.Requests.Count.Should().Be(4);

				//using 9203 for the first time ping succeeds
				result.Metrics.Requests.First().RequestType.Should().Be(RequestType.Ping);
				result.Metrics.Requests.First().Node.Port.Should().Be(9203);
				result.Metrics.Requests.First().Success.Should().BeTrue();

				//call on 9203 fails
				result.Metrics.Requests[1].RequestType.Should().Be(RequestType.ElasticsearchCall);
				result.Metrics.Requests[1].Node.Port.Should().Be(9203);
				result.Metrics.Requests[1].Success.Should().BeFalse();
				result.Metrics.Requests[1].HttpStatusCode.Should().Be(503);

				//using 9202 for the first time ping succeeds
				result.Metrics.Requests[2].RequestType.Should().Be(RequestType.Ping);
				result.Metrics.Requests[2].Node.Port.Should().Be(9202);

				//call on 9203 fails
				result.Metrics.Requests[3].RequestType.Should().Be(RequestType.ElasticsearchCall);
				result.Metrics.Requests[3].Node.Port.Should().Be(9202);
				result.Metrics.Requests[3].Success.Should().BeTrue();
				result.Metrics.Requests[3].HttpStatusCode.Should().Be(200);

				result = await client1.InfoAsync(); //info call 3
				result = await client1.InfoAsync(); //info call 4
				result = await client1.InfoAsync(); //info call 5
				result = await client1.InfoAsync(); //info call 6
				result = await client1.InfoAsync(); //info call 7
				result = await client1.InfoAsync(); //info call 8
				result = await client1.InfoAsync(); //info call 9

				AssertSeenNodesAreInExpectedOrder(seenNodes);

				//4 nodes first time usage + 1 time after the first time 9203 came back to live
				pingCall.MustHaveHappened(Repeated.Exactly.Times(5));
			}
		}
		
		private static IConnection ProvideConnection(AutoFake fake, ConnectionConfiguration config)
		{
			fake.Provide<IConnectionConfigurationValues>(config);
			var param = new TypedParameter(typeof(IDateTimeProvider), null);
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
			var config = new ConnectionConfiguration(connectionPool)
				.EnableMetrics();
			return config;
		}
		private static IDateTimeProvider ProvideDateTimeProvider(AutoFake fake)
		{
			var now = DateTime.UtcNow;
			var dateTimeProvider = fake.Resolve<IDateTimeProvider>();
			var nowCall = A.CallTo(() => dateTimeProvider.Now());
			nowCall.ReturnsNextFromSequence(
				now, //info 1
				now, //info 2 
				now, //info 2 retry
				now, //info 3
				now, //info 4
				now, //info 5 pass over node 3
				now, //info 5
				now, //info 6
				now.AddMinutes(2), //info 7
				now.AddMinutes(2), //info 8
				now.AddMinutes(2) //info 9
				);
			A.CallTo(() => dateTimeProvider.AliveTime(A<Uri>._, A<int>._)).Returns(new DateTime());
			//dead time will return a fixed timeout of 1 minute
			A.CallTo(() => dateTimeProvider.DeadTime(A<Uri>._, A<int>._, A<int?>._, A<int?>._))
				.Returns(DateTime.UtcNow.AddMinutes(1));
			//make sure the transport layer uses a different datetimeprovider
			fake.Provide<IDateTimeProvider>(new DateTimeProvider());
			return dateTimeProvider;
		}
		private static void AssertSeenNodesAreInExpectedOrder(List<Uri> seenNodes)
		{
			seenNodes.Should().NotBeEmpty().And.HaveCount(10);
			seenNodes[0].Port.Should().Be(9204);
			seenNodes[1].Port.Should().Be(9203);
			//after sniff
			seenNodes[2].Port.Should().Be(9202);
			seenNodes[3].Port.Should().Be(9201);
			seenNodes[4].Port.Should().Be(9204);
			seenNodes[5].Port.Should().Be(9202);
			seenNodes[6].Port.Should().Be(9201);
			seenNodes[7].Port.Should().Be(9204);
			seenNodes[8].Port.Should().Be(9203);
		}
	}
}
