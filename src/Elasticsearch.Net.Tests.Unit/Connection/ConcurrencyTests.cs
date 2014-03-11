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
				.SnifsOnConnectionFault()
				.SniffOnStartup();
		}

		private void ProvideTransport(AutoFake fake)
		{
			var param = new TypedParameter(typeof(IDateTimeProvider), null);
			fake.Provide<ITransport, Transport>(param);
		}
		[Test]
		public void CallInfo40000TimesOnMultipleThreads()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				//set up connection configuration that holds a connection pool
				//with '_uris' (see the constructor)
				fake.Provide<IConnectionConfigurationValues>(_config);
				//prove a real HttpTransport with its unspecified dependencies
				//as fakes

				//set up fake for a call on IConnection.GetSync so that it always throws 
				//an exception
				var connection = fake.Provide<IConnection>(new ConcurrencyTestConnection(this._config));
				this.ProvideTransport(fake);
				//create a real ElasticsearchClient with it unspecified dependencies
				//as fakes
				var client = fake.Resolve<ElasticsearchClient>();
				int seen = 0;
				Assert.DoesNotThrow(()=>
				{
					Action a = () =>
					{
						for(var i=0;i<10000;i++)
						{
							client.Info();
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
				seen.Should().Be(40000);
			}
		}

		public class ConcurrencyTestConnection : InMemoryConnection
		{
			private static Uri[] _uris = new[]
			{
				new Uri("http://localhost:9200"),
				new Uri("http://localhost:9201"),
				new Uri("http://localhost:9202"),
				new Uri("http://localhost:9203"),
			};
			private readonly Random _rnd = new Random();
			public ConcurrencyTestConnection(IConnectionConfigurationValues settings) : base(settings)
			{
			}

			public override IList<Uri> Sniff(Uri uri, int connectTimeout)
			{
				return _uris;
			}

			public override ElasticsearchResponse GetSync(Uri uri)
			{
				var statusCode = _rnd.Next(1, 11) % 7 == 0 ? 503 : 200;
				if (uri.Port == 9202)
					statusCode = 200;

				return ElasticsearchResponse.Create(this._ConnectionSettings, statusCode, "GET", "/", null, null);
			
			}
		}

	}
}
