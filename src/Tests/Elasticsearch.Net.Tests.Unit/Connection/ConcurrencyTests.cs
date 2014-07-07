using System;
using System.IO;
using System.Linq;
using System.Threading;
using Autofac.Extras.FakeItEasy;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Tests.Unit.Stubs;
using FluentAssertions;
using NUnit.Framework;

namespace Elasticsearch.Net.Tests.Unit.Connection
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
		private static readonly int _retries = _uris.Count() - 1;
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

		[Test]
		[Ignore] //TODO Unignore
		public void CallInfo40000TimesOnMultipleThreads()
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
		/// <summary>
		/// This simulates a super flakey elasticsearch cluster.
		/// - if random 1-9 is a muliple of 3 throw a 503
		/// - never throws on node 9202 though so that all calls can be expected to always succeed. 
		/// - Sniff can either get back the full cluster or a sufficient subset of it. 
		/// - Our cluster have 5 nodes the recommendation is to have N/2+1 masters so we should atleast see 3 nodes
		/// - anything less would cause a node to be unavailable which is covered in other tests 
		/// </summary>
		public class ConcurrencyTestConnection : InMemoryConnection
		{
			private static Uri[] _uris = new[]
			{
				new Uri("http://localhost:9200"),
				new Uri("http://localhost:9201"),
				new Uri("http://localhost:9202"),
				new Uri("http://localhost:9203"),
				new Uri("http://localhost:9206"),
			};
			
			private static Uri[] _uris2 = new[]
			{
				new Uri("http://localhost:9202"),
				new Uri("http://localhost:9201"),
				new Uri("http://localhost:9206"),
			};
			private readonly Random _rnd = new Random();
			public ConcurrencyTestConnection(IConnectionConfigurationValues settings) 
				: base(settings)
			{
			}

		

			public override ElasticsearchResponse<Stream> GetSync(Uri uri, IRequestConnectionConfiguration requestConfigurationOverrides = null)
			{
				var statusCode = _rnd.Next(1, 9) % 3 == 0 ? 503 : 200;
				if (uri.Port == 9202)
					statusCode = 200;

				return ElasticsearchResponse<Stream>.Create(this.ConnectionSettings, statusCode, "GET", "/", null);
			
			}
		}

	}
}
