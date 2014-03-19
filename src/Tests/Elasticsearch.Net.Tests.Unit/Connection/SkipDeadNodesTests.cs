using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core.Activators.Reflection;
using Autofac.Extras.FakeItEasy;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Exceptions;
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
				getCall.Invokes((Uri u, object o) => seenNodes.Add(u));

				var pingCall = FakeCalls.Ping(fake);
				pingCall.Returns(true);

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
				getCall.Invokes((Uri u, object o) => seenNodes.Add(u));

				var pingCall = FakeCalls.Ping(fake);
				pingCall.Returns(true);

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

				AssertSeenNodesAreInExpectedOrder(seenNodes);

				//4 nodes first time usage + 1 time after the first time 9203 came back to live
				pingCall.MustHaveHappened(Repeated.Exactly.Times(5));
			}
		}
		
		private static IConnection ProvideConnection(AutoFake fake, ConnectionConfiguration config)
		{
			fake.Provide<IConnectionConfigurationValues>(config);
			fake.Provide<ITransport>(fake.Resolve<Transport>());
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
			var config = new ConnectionConfiguration(connectionPool);
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
