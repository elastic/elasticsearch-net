using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Newtonsoft.Json;
using Tests.Framework;

namespace Tests.ClientConcepts.LowLevel
{
	public class Lifetimes
	{
		/**== Lifetimes
		*
		* If you are using an IOC/Dependency Injection container, it's always useful to know the best practices around
        * the lifetime of your objects.
		*
		* In general, we advise folks to register an `ElasticClient` instance as a singleton; the client is thread safe,
		* so sharing an instance across threads is fine.
		*
		* The actual moving part that benefits from being a singleton is `ConnectionSettings` because
		* **caches are __per__ `ConnectionSettings`**.
		*
		* In some applications ,it could make perfect sense to have multiple `ElasticClient` instances registered with different
		* connection settings such as when your application connects to two different Elasticsearch clusters.
		*
		* IMPORTANT: Due to the semantic versioning of Elasticsearch.Net and NEST and their alignment to versions of Elasticsearch, all instances of `ElasticClient` and
		* Elasticsearch clusters that are connected to must be on the **same major version**
		*
		* Let's demonstrate which components are disposed by creating our own derived `ConnectionSettings`, `IConnectionPool` and `IConnection` types
		*/
		class AConnectionSettings : ConnectionSettings
		{
			public AConnectionSettings(IConnectionPool pool, IConnection connection)
				: base(pool, connection)
			{ }

			public bool IsDisposed { get; private set; }

			protected override void DisposeManagedResources()
			{
				this.IsDisposed = true;
				base.DisposeManagedResources();
			}
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

		class AConnection : InMemoryConnection
		{
			public bool IsDisposed { get; private set; }

			protected override void DisposeManagedResources()
			{
				this.IsDisposed = true;
				base.DisposeManagedResources();
			}
		}

		/**
		* `ConnectionSettings`, `IConnectionPool` and `IConnection` all explictily implement `IDisposable`
		*/
		[U] public void InitialDisposeState()
		{
			var connection = new AConnection();
			var connectionPool = new AConnectionPool(new Uri("http://localhost:9200"));
			var settings = new AConnectionSettings(connectionPool, connection);
			settings.IsDisposed.Should().BeFalse();
			connectionPool.IsDisposed.Should().BeFalse();
			connection.IsDisposed.Should().BeFalse();
		}

		/**
		* Disposing an instance of `ConnectionSettings` will also dispose the `IConnectionPool` and `IConnection` it uses
		*/
		[U] public void DisposingSettingsDisposesMovingParts()
		{
			var connection = new AConnection();
			var connectionPool = new AConnectionPool(new Uri("http://localhost:9200"));
			var settings = new AConnectionSettings(connectionPool, connection);
			using (settings) { } // <1> force the settings to be disposed
			settings.IsDisposed.Should().BeTrue();
			connectionPool.IsDisposed.Should().BeTrue();
			connection.IsDisposed.Should().BeTrue();
		}
	}
}
