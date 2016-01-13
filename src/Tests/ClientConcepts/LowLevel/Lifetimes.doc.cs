using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Newtonsoft.Json;
using Tests.Framework;

namespace Tests.ClientConcepts.LowLevel
{
	public class Lifetimes
	{
		/**
		* ## Lifetimes
		*
		* If you are using an IOC container its always useful to know the best practices around the lifetime of your objects 

		* In general we advise folks to register their ElasticClient instances as singleton. The client is thread safe
		* so sharing this instance over threads is ok. 

		* Zooming in however the actual moving part that benefits the most of being static for most of the duration of your
		* application is ConnectionSettings. Caches are per ConnectionSettings. 

		* In some applications it could make perfect sense to have multiple singleton IElasticClient's registered with different
		* connectionsettings. e.g if you have 2 functionally isolated Elasticsearch clusters.
		
		* Disposing a client won't dispose the ConnectionSettings that are passed to its constructor
		*/


		[U] public void DisposingClientDoesNotDisposeMovingParts()
		{
			var connection = new AConnection();
			var connectionPool = new AConnectionPool(new Uri("http://localhost:9200"));
			var settings = new AConnectionSettings(connectionPool, connection);
			var transport = new ATransport(settings);
			var client = new AnElasticClient(transport);
			using (client) { }
			client.IsDisposed.Should().BeTrue();
			transport.IsDisposed.Should().BeTrue();
			settings.IsDisposed.Should().BeFalse();
			connectionPool.IsDisposed.Should().BeFalse();
			connection.IsDisposed.Should().BeFalse();
		}

		/**
		* Disposing the client will only dispose the resources it uses itself and the underlying ITransport.
		* The ConnectionSettings that are passed to the client should be safe to share between multiple instances.
		*/

		[U] public void DisposingTransportDoesNotDisposeMovingParts()
		{
			var connection = new AConnection();
			var connectionPool = new AConnectionPool(new Uri("http://localhost:9200"));
			var settings = new AConnectionSettings(connectionPool, connection);
			var transport = new ATransport(settings);
			using (transport) { }
			transport.IsDisposed.Should().BeTrue();
			settings.IsDisposed.Should().BeFalse();
			connectionPool.IsDisposed.Should().BeFalse();
			connection.IsDisposed.Should().BeFalse();
		}

		/**
		* Disposing the ConnectionSettings will dispose the IConnectionPool and IConnection it has a hold of
		*/

		[U] public void DisposingSettingsDisposesMovingParts()
		{
			var connection = new AConnection();
			var connectionPool = new AConnectionPool(new Uri("http://localhost:9200"));
			var settings = new AConnectionSettings(connectionPool, connection);
			using (settings) { }
			settings.IsDisposed.Should().BeTrue();
			connectionPool.IsDisposed.Should().BeTrue();
			connection.IsDisposed.Should().BeTrue();
		}

		class AConnectionPool : SingleNodeConnectionPool
		{
			public AConnectionPool(Uri uri, IDateTimeProvider dateTimeProvider = null) : base(uri, dateTimeProvider) { }
			
			public bool IsDisposed { get; private set; }
			protected override void DisposeManagedResources() 
			{
				this.IsDisposed = true;
				base.DisposeManagedResources();
			}
		}

		class AConnectionSettings : ConnectionSettings
		{
			public AConnectionSettings(IConnectionPool pool, IConnection connection) 
				: base(pool, connection) { }
			public bool IsDisposed { get; private set; }
			protected override void DisposeManagedResources()
			{
				this.IsDisposed = true;
				base.DisposeManagedResources();
			}
		}

		class AConnection : InMemoryConnection
		{
			public bool IsDisposed { get; private set; }
			protected override void DisposeManagedResources()
			{
				this.IsDisposed = true;
				base.DisposeManagedResources();
			}
		}

		class ATransport : Transport<AConnectionSettings>
		{
			public ATransport(AConnectionSettings configurationValues) : base(configurationValues) { }

			public bool IsDisposed { get; private set; }
			protected override void DisposeManagedResources()
			{
				this.IsDisposed = true;
				base.DisposeManagedResources();
			}
		}

		class AnElasticClient : ElasticClient
		{
			public AnElasticClient(ITransport<IConnectionSettingsValues> transport) : base (transport){ }
			public bool IsDisposed { get; private set; }
			protected override void DisposeManagedResources()
			{
				this.IsDisposed = true;
				base.DisposeManagedResources();
			}
		}

	}
}
