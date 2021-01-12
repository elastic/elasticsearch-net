// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using System.Runtime.Serialization;
using Elastic.Transport.Products;
using Elastic.Transport.Products.Elasticsearch;
using Elasticsearch.Net;
using Elastic.Transport.VirtualizedCluster;
using Elastic.Transport.VirtualizedCluster.Components;
using Elastic.Transport.VirtualizedCluster.Products.Elasticsearch;
using Elastic.Transport.VirtualizedCluster.Providers;
using Tests.Core.Client;
using Tests.Core.Client.Settings;
using Tests.Framework;
using Xunit;
using Newtonsoft.Json;

namespace Tests.ClientConcepts.ConnectionPooling.BuildingBlocks
{
	/**=== Request pipelines
	* Every request is executed in the context of a `RequestPipeline` when using the
	* default <<transports,ITransport>> implementation.
	*/
	public class RequestPipelines
	{
		[U]
		public void RequestPipeline()
		{
			// hide
			var settings = TestClient.DefaultInMemoryClient.ConnectionSettings;

			/** When calling `Request()` or `RequestAsync()` on an `ITransport`,
			* the whole coordination of the request is deferred to a new instance in a `using` block.
			*/
			var pipeline = new RequestPipeline<IConnectionSettingsValues>(
				settings,
				DateTimeProvider.Default,
				new RecyclableMemoryStreamFactory(),
				new SearchRequestParameters()
			);

			pipeline.GetType().Should().Implement<IDisposable>();

			/** An `ITransport` does not instantiate a `RequestPipeline` directly; it uses a pluggable `IRequestPipelineFactory`
			* to create them
			*/
			var requestPipelineFactory = new RequestPipelineFactory<IConnectionSettingsValues>();
			var requestPipeline = requestPipelineFactory.Create(
				settings,
				DateTimeProvider.Default, //<1> An <<date-time-providers,`IDateTimeProvider`>> implementation
				new RecyclableMemoryStreamFactory(),
				new SearchRequestParameters()
			);

			requestPipeline.Should().BeOfType<RequestPipeline<IConnectionSettingsValues>>();
			requestPipeline.GetType().Should().Implement<IDisposable>();

			/**
			 * You can pass your own `IRequestPipeline` implementation to the transport when instantiating a client,
			* allowing you to have requests executed in your own custom request pipeline
			*/
			var transport = new Transport<IConnectionSettingsValues>(
				settings,
				requestPipelineFactory,
				DateTimeProvider.Default,
				new RecyclableMemoryStreamFactory()
			);

			var client = new ElasticClient(transport);
		}

		// hide
		private IRequestPipeline CreatePipeline(
			Func<IEnumerable<Uri>, IConnectionPool> setupPool, Func<ConnectionSettings, ConnectionSettings> settingsSelector = null, IDateTimeProvider dateTimeProvider = null, InMemoryConnection connection = null)
		{
			var pool = setupPool(new[] { TestConnectionSettings.CreateUri(), TestConnectionSettings.CreateUri(9201) });
			var settings = new ConnectionSettings(pool, connection ?? new InMemoryConnection());
			settings = settingsSelector?.Invoke(settings) ?? settings;
			return new ExposingPipelineFactory<IConnectionSettingsValues>(settings, dateTimeProvider ?? DateTimeProvider.Default).Pipeline;
		}

		/**
		 * Let's now have a look at some of the characteristics of the request pipeline
		 *
		 *==== Sniffing on first usage
		 */
		[U]
		public void FirstUsageCheck()
		{
			/** Here we have setup three pipelines with three different <<connection-pooling, connection pools>> */
			var singleNodePipeline = CreatePipeline(uris => new SingleNodeConnectionPool(uris.First()));
			var staticPipeline = CreatePipeline(uris => new StaticConnectionPool(uris));
			var sniffingPipeline = CreatePipeline(uris => new SniffingConnectionPool(uris));

			/**  Let's see how they behave on first usage */
			singleNodePipeline.FirstPoolUsageNeedsSniffing.Should().BeFalse();
			staticPipeline.FirstPoolUsageNeedsSniffing.Should().BeFalse();
			sniffingPipeline.FirstPoolUsageNeedsSniffing.Should().BeTrue();

			/**
			 * We see that only the <<sniffing-connection-pool, Sniffing connection pool>> supports sniffing on first usage,
			 * since it supports reseeding. Sniffing on startup however can be disabled on `ConnectionSettings` for sniffing
			 * connection pool
			*/
			sniffingPipeline = CreatePipeline(
				uris => new SniffingConnectionPool(uris),
				s => s.SniffOnStartup(false)); //<1> Disable sniffing on startup

			sniffingPipeline.FirstPoolUsageNeedsSniffing.Should().BeFalse();
		}

		/**==== Wait for first sniff
		 *
		 * All threads wait for the sniff on startup to finish, waiting the request timeout period. A
		 * https://msdn.microsoft.com/en-us/library/system.threading.semaphoreslim(v=vs.110).aspx[`SemaphoreSlim`]
		 * is used to block threads until the sniff finishes and waiting threads release the `SemaphoreSlim` appropriately.
		 */
		[U]
		public void FirstUsageCheckConcurrentThreads()
		{
			//hide
			var response = new
			{
				cluster_name = "elasticsearch",
				nodes = new
				{
					node1 = new
					{
						name = "Node Name 1",
						transport_address = "127.0.0.1:9300",
						host = "127.0.0.1",
						ip = "127.0.01",
						version = "5.0.0-alpha3",
						build_hash = "e455fd0",
						roles = new List<string>(),
						http = new
						{
							bound_address = new[] { "127.0.0.1:9200" }
						},
						settings = new Dictionary<string, object>
						{
							{ "cluster.name", "elasticsearch" },
							{ "node.name", "Node Name 1" }
						}
					}
				}
			};

			//hide
			var responseBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));

			/** We can demonstrate this with the following example. First, let's configure
			 * a custom `IConnection` implementation that's simply going to return a known
			 * 200 response after one second
			 */
			var inMemoryConnection = new WaitingInMemoryConnection(
				TimeSpan.FromSeconds(1),
				responseBody);

			/**
			 * Next, we create a <<sniffing-connection-pool, Sniffing connection pool>> using our
			 * custom connection and a timeout for how long a request can take before the client
			 * times out
			 */
			var sniffingPipeline = CreatePipeline(
				uris => new SniffingConnectionPool(uris),
				connection: inMemoryConnection,
				settingsSelector: s => s.RequestTimeout(TimeSpan.FromSeconds(2)));

			/**Now, with a `SemaphoreSlim` in place that allows only one thread to enter at a time,
			 * start three tasks that will initiate a sniff on startup.
			 *
			 * The first task will successfully sniff on startup with the remaining two waiting
			 * tasks exiting without exception. The `SemaphoreSlim` is also released, ready for
			 * when sniffing needs to take place again
			 */
			var semaphoreSlim = new SemaphoreSlim(1, 1);

			var task1 = Task.Run(() => sniffingPipeline.FirstPoolUsage(semaphoreSlim));
			var task2 = Task.Run(() => sniffingPipeline.FirstPoolUsage(semaphoreSlim));
			var task3 = Task.Run(() => sniffingPipeline.FirstPoolUsage(semaphoreSlim));

			var exception = Record.Exception(() => Task.WaitAll(task1, task2, task3));

			exception.Should().BeNull();
			semaphoreSlim.CurrentCount.Should().Be(1);
		}

		/**==== Sniff on connection failure */
		[U]
		public void SniffsOnConnectionFailure()
		{
			/**
			 * Only a connection pool that supports reseeding will opt in to `SniffsOnConnectionFailure()`.
			 * In this case, it is only the Sniffing connection pool
			 */
			var singleNodePipeline = CreatePipeline(uris => new SingleNodeConnectionPool(uris.First()));
			var staticPipeline = CreatePipeline(uris => new StaticConnectionPool(uris));
			var sniffingPipeline = CreatePipeline(uris => new SniffingConnectionPool(uris));

			singleNodePipeline.SniffsOnConnectionFailure.Should().BeFalse();
			staticPipeline.SniffsOnConnectionFailure.Should().BeFalse();
			sniffingPipeline.SniffsOnConnectionFailure.Should().BeTrue();

			/**
			* You can however disable this behaviour on `ConnectionSettings`
			*/
			sniffingPipeline = CreatePipeline(uris => new SniffingConnectionPool(uris), s => s.SniffOnConnectionFault(false));
			sniffingPipeline.SniffsOnConnectionFailure.Should().BeFalse();
		}

		/**==== Sniff on stale cluster  */
		[U]
		public void SniffsOnStaleCluster()
		{
			/**
			 * A connection pool that supports reseeding will sniff after a period of time
			 * to ensure that its understanding of the state of the cluster is not stale.
			 *
			 * Let's set up three request pipelines with different connection pools and a
			 * date time provider that will allow us to artificially change the time
			 */
			var dateTime = new TestableDateTimeProvider();
			var singleNodePipeline = CreatePipeline(uris =>
				new SingleNodeConnectionPool(uris.First(), dateTime), dateTimeProvider: dateTime);

			var staticPipeline = CreatePipeline(uris =>
				new StaticConnectionPool(uris, dateTimeProvider: dateTime), dateTimeProvider: dateTime);

			var sniffingPipeline = CreatePipeline(uris =>
				new SniffingConnectionPool(uris, dateTimeProvider: dateTime), dateTimeProvider: dateTime);

			/**
			 * On the request pipeline with the Sniffing connection pool will sniff when its
			 * understanding of the cluster is stale
			 */
			singleNodePipeline.SniffsOnStaleCluster.Should().BeFalse();
			staticPipeline.SniffsOnStaleCluster.Should().BeFalse();
			sniffingPipeline.SniffsOnStaleCluster.Should().BeTrue();

			/**
			 * To begin with, all request pipelines have a _fresh_ view of cluster state i.e. not stale
			 */
			singleNodePipeline.StaleClusterState.Should().BeFalse();
			staticPipeline.StaleClusterState.Should().BeFalse();
			sniffingPipeline.StaleClusterState.Should().BeFalse();

			/** Now, if we go two hours into the future */
			dateTime.ChangeTime(d => d.Add(TimeSpan.FromHours(2)));

			/** Those connection pools that do not support reseeding never go stale */
			singleNodePipeline.StaleClusterState.Should().BeFalse();
			staticPipeline.StaleClusterState.Should().BeFalse();

			/**
			 * but the Request pipeline using the Sniffing connection pool that supports reseeding,
			 * signals that its understanding of the cluster state is out of date
			 */
			sniffingPipeline.StaleClusterState.Should().BeTrue();

		}


		/**
		* ==== Retrying
		*
		* A request pipeline also checks whether the overall time across multiple retries exceeds the request timeout.
		* See <<retries, Retries>> for more details, here we assert that our request pipeline exposes this properly
		*/
		[U]
		public void IsTakingTooLong()
		{
			var dateTime = new TestableDateTimeProvider();
			var singleNodePipeline = CreatePipeline(uris =>
				new SingleNodeConnectionPool(uris.First(), dateTime), dateTimeProvider: dateTime);

			var staticPipeline = CreatePipeline(uris =>
				new StaticConnectionPool(uris, dateTimeProvider: dateTime), dateTimeProvider: dateTime);

			var sniffingPipeline = CreatePipeline(uris =>
				new SniffingConnectionPool(uris, dateTimeProvider: dateTime), dateTimeProvider: dateTime);

			singleNodePipeline.IsTakingTooLong.Should().BeFalse();
			staticPipeline.IsTakingTooLong.Should().BeFalse();
			sniffingPipeline.IsTakingTooLong.Should().BeFalse();

			/** go one hour into the future */
			dateTime.ChangeTime(d => d.Add(TimeSpan.FromHours(2)));

			/**Connection pools that do not support reseeding never go stale */
			singleNodePipeline.IsTakingTooLong.Should().BeTrue();
			staticPipeline.IsTakingTooLong.Should().BeTrue();
			/** the sniffing connection pool supports reseeding so the pipeline will signal the state is out of date */
			sniffingPipeline.IsTakingTooLong.Should().BeTrue();

			/** request pipeline exposes the DateTime it started; we assert it started 2 hours in the past */
			(dateTime.Now() - singleNodePipeline.StartedOn).Should().BePositive().And.BeCloseTo(TimeSpan.FromHours(2));
			(dateTime.Now() - staticPipeline.StartedOn).Should().BePositive().And.BeCloseTo(TimeSpan.FromHours(2));
			(dateTime.Now() - sniffingPipeline.StartedOn).Should().BePositive().And.BeCloseTo(TimeSpan.FromHours(2));
		}
	}
}
