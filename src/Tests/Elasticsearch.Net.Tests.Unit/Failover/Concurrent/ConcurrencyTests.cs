using System;
using System.Linq;
using System.Threading;
using Autofac.Extras.FakeItEasy;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Tests.Unit.Stubs;
using FluentAssertions;
using NUnit.Framework;

namespace Elasticsearch.Net.Tests.Unit.Failover.Concurrent
{
	[TestFixture]
	public class ConcurrencyTests
	{
		private static Uri[] _uris = new[]
		{
			new Uri("http://localhost:9200"),
			new Uri("http://localhost:9201"),
			new Uri("http://localhost:9202"),
			new Uri("http://localhost:9203"),
		};
		private readonly StaticConnectionPool _connectionPool;
		private readonly ConnectionConfiguration _config;

		public ConcurrencyTests()
		{
			_connectionPool = new SniffingConnectionPool(_uris);
			_config = new ConnectionConfiguration(_connectionPool)
				.SniffOnConnectionFault()
				.SniffOnStartup()
				.MaximumRetries(5);
		}
		/// <summary>
		/// This test calls a cluster which is configured to throw randomly but never on node 9202
		/// We use 4 threads to do concurrent calls on a single client instance
		/// Failover should always discover the live node 9202 and none of our calls should throw an exception.
		/// </summary>
		[Test]
		public void ClusterWithOnlyOneGoodNode_MustWithstandThousandsOfConcurrentCalls()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				//set up connection configuration that holds a connection pool
				//with '_uris' (see the constructor)
				fake.Provide<IConnectionConfigurationValues>(_config);
				//we want to use our special concurrencytestconnection
				//this randonly throws on any node but 9200 and sniffing will represent a different 
				//view of the cluster each time but always holding node 9200
				fake.Provide<IConnection>(new ConcurrencyTestConnection(this._config));
				//prove a real Transport with its unspecified dependencies
				//as fakes
				FakeCalls.ProvideDefaultTransport(fake);
				
				//create a real ElasticsearchClient with it unspecified dependencies as fakes
				var client = fake.Resolve<ElasticsearchClient>();
				int seen = 0;

				//We'll call Info() 10.000 times on 4 threads
				//This should not throw any exceptions even if connections sometime fail at a node level
				//because node 9200 is always up and running
				Assert.DoesNotThrow(()=>
				{
					Action a = () =>
					{
						for(var i=0;i<10000;i++)
						{
							client.Info<VoidResponse>();
							Interlocked.Increment(ref seen);
						}
					};
					var thread1 = new Thread(()=>a());
					var thread2 = new Thread(()=>a());
					var thread3 = new Thread(()=>a());
					var thread4 = new Thread(()=>a());
					thread1.Start();
					thread2.Start();
					thread3.Start();
					thread4.Start();
					thread1.Join();
					thread2.Join();
					thread3.Join();
					thread4.Join();

				});

				//we should have seen 40.000 increments
				//Sadly we can't use FakeItEasy's to ensure get is called 40.000 times
				//because it internally uses fixed arrays that will overflow :)
				seen.Should().Be(40000);
			}
		}

	}
}
