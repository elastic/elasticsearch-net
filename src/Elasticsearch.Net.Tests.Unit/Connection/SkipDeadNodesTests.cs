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
		public void DeadNodesAreNotVisited()
		{
			using (var fake = new AutoFake())
			{
				var now = DateTime.UtcNow;
				var dateTimeProvider = fake.Resolve<IDateTimeProvider>();
				var nowCall = A.CallTo(()=>dateTimeProvider.Now());
				nowCall.ReturnsNextFromSequence(
					DateTime.UtcNow, //info 1
					DateTime.UtcNow, //info 2 
					DateTime.UtcNow, //info 2 retry
					DateTime.UtcNow, //info 3
					DateTime.UtcNow, //info 4
					DateTime.UtcNow, //info 5 pass over node 3
					DateTime.UtcNow, //info 5
					DateTime.UtcNow, //info 6
					DateTime.UtcNow.AddMinutes(2), //info 7
					DateTime.UtcNow.AddMinutes(2), //info 8
					DateTime.UtcNow.AddMinutes(2) //info 9
				);
				A.CallTo(()=>dateTimeProvider.AliveTime(A<Uri>._, A<int>._))
					.Returns(new DateTime());
				A.CallTo(() => dateTimeProvider.DeadTime(A<Uri>._, A<int>._))
					.Returns(DateTime.UtcNow.AddMinutes(1));
				//make sure the transport layer uses a different datetimeprovider
				fake.Provide<IDateTimeProvider>(new DateTimeProvider());
				var connectionPool = new StaticConnectionPool(new[]
				{
					new Uri("http://localhost:9204"),
					new Uri("http://localhost:9203"),
					new Uri("http://localhost:9202"),
					new Uri("http://localhost:9201")
				}, randomizeOnStartup: false, dateTimeProvider: dateTimeProvider);
				var config = new ConnectionConfiguration(connectionPool);
				fake.Provide<IConnectionConfigurationValues>(config);
				fake.Provide<ITransport>(fake.Resolve<Transport>());
				var connection = fake.Resolve<IConnection>();

				var seenNodes = new List<Uri>();
				var getCall = A.CallTo(() => connection.GetSync(A<Uri>._));
				getCall.ReturnsNextFromSequence(
					ElasticsearchResponse.Create(config, 200, "GET", "/", null, null), //info 1 - 9204
					ElasticsearchResponse.Create(config, 503, "GET", "/", null, null), //info 2 - 9203 DEAD
					ElasticsearchResponse.Create(config, 200, "GET", "/", null, null), //info 2 retry - 9202
					ElasticsearchResponse.Create(config, 200, "GET", "/", null, null), //info 3 - 9201
					ElasticsearchResponse.Create(config, 200, "GET", "/", null, null), //info 4 - 9204
					ElasticsearchResponse.Create(config, 200, "GET", "/", null, null), //info 5 - 9202
					ElasticsearchResponse.Create(config, 200, "GET", "/", null, null), //info 6 - 9201
					ElasticsearchResponse.Create(config, 200, "GET", "/", null, null), //info 7 - 9204
					ElasticsearchResponse.Create(config, 200, "GET", "/", null, null), //info 8 - 9203 (Now > Timeout)
					ElasticsearchResponse.Create(config, 200, "GET", "/", null, null) //info 9 - 9202
				);
				getCall.Invokes((Uri u) => seenNodes.Add(u));

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

				//var nowCall = A.CallTo(() => fake.Resolve<IDateTimeProvider>().Sniff(A<Uri>._, A<int>._));
			}
		}
	}
}
