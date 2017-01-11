using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tests.Framework;
using Tests.Framework.VirtualClustering;
using Xunit;

namespace Tests.ClientConcepts.ConnectionPooling.BuildingBlocks
{
	/**== Request Pipeline
	* Every request is executed in the context of a `RequestPipeline` when using the
	* default <<transports,ITransport>> implementation.
	*/
	public class RequestPipelines
	{
		[U]
		public void RequestPipeline()
		{
			var settings = TestClient.GlobalDefaultSettings;

			/** When calling `Request()` or `RequestAsync()` on an `ITransport`,
			* the whole coordination of the request is deferred to a new instance in a `using` block.
			*/
			var pipeline = new RequestPipeline(
				settings,
				DateTimeProvider.Default,
				new MemoryStreamFactory(),
				new SearchRequestParameters());

			pipeline.GetType().Should().Implement<IDisposable>();

			/** An `ITransport` does not instantiate a `RequestPipeline` directly; it uses a pluggable `IRequestPipelineFactory`
			* to create it
			*/
			var requestPipelineFactory = new RequestPipelineFactory();
			var requestPipeline = requestPipelineFactory.Create(
				settings,
				DateTimeProvider.Default, //<1> An <<date-time-providers,`IDateTimeProvider` implementation>>
				new MemoryStreamFactory(),
				new SearchRequestParameters());

			requestPipeline.Should().BeOfType<RequestPipeline>();
			requestPipeline.GetType().Should().Implement<IDisposable>();

			/** You can pass your own `IRequestPipeline` implementation to the Transport when instantiating a client,
			* allowing you to have requests executed on your own custom request pipeline
			*/
			var transport = new Transport<ConnectionSettings>(
				settings,
				requestPipelineFactory,
				DateTimeProvider.Default,
				new MemoryStreamFactory());
		}

		private IRequestPipeline CreatePipeline(
			Func<IEnumerable<Uri>, IConnectionPool> setupPool, Func<ConnectionSettings, ConnectionSettings> settingsSelector = null, IDateTimeProvider dateTimeProvider = null, InMemoryConnection connection = null)
		{
			var pool = setupPool(new[] { TestClient.CreateUri(), TestClient.CreateUri(9201) });
			var settings = new ConnectionSettings(pool, connection ?? new InMemoryConnection());
			settings = settingsSelector?.Invoke(settings) ?? settings;
			return new FixedPipelineFactory(settings, dateTimeProvider ?? DateTimeProvider.Default).Pipeline;
		}

		/**=== Pipeline Behavior
		*==== Sniffing on First usage
		*/
		[U]
		public void FirstUsageCheck()
		{
			var singleNodePipeline = CreatePipeline(uris => new SingleNodeConnectionPool(uris.First()));
			var staticPipeline = CreatePipeline(uris => new StaticConnectionPool(uris));
			var sniffingPipeline = CreatePipeline(uris => new SniffingConnectionPool(uris));

			/** Here we have setup three pipelines using three different connection pools. Let's see how they behave
			* on first usage
			*/
			singleNodePipeline.FirstPoolUsageNeedsSniffing.Should().BeFalse();
			staticPipeline.FirstPoolUsageNeedsSniffing.Should().BeFalse();
			sniffingPipeline.FirstPoolUsageNeedsSniffing.Should().BeTrue();

			/** We can see that only the cluster that supports reseeding will opt in to `FirstPoolUsageNeedsSniffing()`;
			* You can however disable reseeding/sniffing on ConnectionSettings
			*/
			sniffingPipeline = CreatePipeline(uris => new SniffingConnectionPool(uris), s => s.SniffOnStartup(false)); //<1> Disable sniffing on startup
			sniffingPipeline.FirstPoolUsageNeedsSniffing.Should().BeFalse();
		}

		/**==== Wait for first Sniff
		 *
		 * All threads wait for the sniff on startup to finish, waiting the request timeout period. A
		 * https://msdn.microsoft.com/en-us/library/system.threading.semaphoreslim(v=vs.110).aspx[`SemaphoreSlim`]
		 * is used to block threads until the sniff finishes and waiting threads release the `SemaphoreSlim` appropriately.
		 */
		[U]
		public void FirstUsageCheckConcurrentThreads()
		{
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
						version = "2.4.3",
						build = "e455fd0",
						http_address = "127.0.0.1:9200",
						settings = new JObject
						{
							{"client.type", "node"},
							{"cluster.name", "elasticsearch"},
							{"config.ignore_system_properties", "true"},
							{"name", "Node Name 1"},
							{"path.home", "c:\\elasticsearch\\elasticsearch"},
							{"path.logs", "c:/ elasticsearch/logs"}
						}
					}
				}
			};

			var responseBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));

			var inMemoryConnection = new WaitingInMemoryConnection(
				TimeSpan.FromSeconds(1),
				responseBody);

			var sniffingPipeline = CreatePipeline(
				uris => new SniffingConnectionPool(uris),
				connection: inMemoryConnection,
				settingsSelector: s => s.RequestTimeout(TimeSpan.FromSeconds(2)));

			var semaphoreSlim = new SemaphoreSlim(1, 1);

			/**
			 * start three tasks that will initiate a sniff on startup. The first task will successfully
			 * sniff on startup with the remaining two waiting tasks exiting without exception and releasing
			 * the `SemaphoreSlim`.
			 */
			var task1 = System.Threading.Tasks.Task.Run(() => sniffingPipeline.FirstPoolUsage(semaphoreSlim));
			var task2 = System.Threading.Tasks.Task.Run(() => sniffingPipeline.FirstPoolUsage(semaphoreSlim));
			var task3 = System.Threading.Tasks.Task.Run(() => sniffingPipeline.FirstPoolUsage(semaphoreSlim));

			var exception = Record.Exception(() => System.Threading.Tasks.Task.WaitAll(task1, task2, task3));
			exception.Should().BeNull();
		}

		/**==== Sniffing on Connection Failure */
		[U]
		public void SniffsOnConnectionFailure()
		{
			var singleNodePipeline = CreatePipeline(uris => new SingleNodeConnectionPool(uris.First()));
			var staticPipeline = CreatePipeline(uris => new StaticConnectionPool(uris));
			var sniffingPipeline = CreatePipeline(uris => new SniffingConnectionPool(uris));

			singleNodePipeline.SniffsOnConnectionFailure.Should().BeFalse();
			staticPipeline.SniffsOnConnectionFailure.Should().BeFalse();
			sniffingPipeline.SniffsOnConnectionFailure.Should().BeTrue();

			/** Only the cluster that supports reseeding will opt in to SniffsOnConnectionFailure()
			* You can however disable this on ConnectionSettings
			*/
			sniffingPipeline = CreatePipeline(uris => new SniffingConnectionPool(uris), s => s.SniffOnConnectionFault(false));
			sniffingPipeline.SniffsOnConnectionFailure.Should().BeFalse();
		}

		/**==== Sniffing on Stale cluster  */
		[U]
		public void SniffsOnStaleCluster()
		{
			var dateTime = new TestableDateTimeProvider();
			var singleNodePipeline = CreatePipeline(uris =>
				new SingleNodeConnectionPool(uris.First(), dateTime), dateTimeProvider: dateTime);

			var staticPipeline = CreatePipeline(uris =>
				new StaticConnectionPool(uris, dateTimeProvider: dateTime), dateTimeProvider: dateTime);

			var sniffingPipeline = CreatePipeline(uris =>
				new SniffingConnectionPool(uris, dateTimeProvider: dateTime), dateTimeProvider: dateTime);

			singleNodePipeline.SniffsOnStaleCluster.Should().BeFalse();
			staticPipeline.SniffsOnStaleCluster.Should().BeFalse();
			sniffingPipeline.SniffsOnStaleCluster.Should().BeTrue();

			singleNodePipeline.StaleClusterState.Should().BeFalse();
			staticPipeline.StaleClusterState.Should().BeFalse();
			sniffingPipeline.StaleClusterState.Should().BeFalse();

			/** go one hour into the future */
			dateTime.ChangeTime(d => d.Add(TimeSpan.FromHours(2)));

			/**connection pools that do not support reseeding never go stale */
			singleNodePipeline.StaleClusterState.Should().BeFalse();
			staticPipeline.StaleClusterState.Should().BeFalse();
			/** the sniffing connection pool supports reseeding so the pipeline will signal the state is out of date */
			sniffingPipeline.StaleClusterState.Should().BeTrue();

		}


		/**=== Retrying requests
		* A request pipeline also checks whether the overall time across multiple retries exceeds the request timeout.
		* See the <<max-retries, max retry documentation>> for more details, here we assert that our request pipeline exposes this propertly
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

			/**connection pools that do not support reseeding never go stale */
			singleNodePipeline.IsTakingTooLong.Should().BeTrue();
			staticPipeline.IsTakingTooLong.Should().BeTrue();
			/** the sniffing connection pool supports reseeding so the pipeline will signal the state is out of date */
			sniffingPipeline.IsTakingTooLong.Should().BeTrue();

			/** request pipeline exposes the DateTime it started, here we assert it started 2 hours in the past */
			(dateTime.Now() - singleNodePipeline.StartedOn).Should().BePositive().And.BeCloseTo(TimeSpan.FromHours(2));
			(dateTime.Now() - staticPipeline.StartedOn).Should().BePositive().And.BeCloseTo(TimeSpan.FromHours(2));
			(dateTime.Now() - sniffingPipeline.StartedOn).Should().BePositive().And.BeCloseTo(TimeSpan.FromHours(2));
		}

		[U]
		public void SetsSniffPathUsingToTimespan()
		{
			var dateTime = new TestableDateTimeProvider();
			var sniffingPipeline = CreatePipeline(uris =>
				new SniffingConnectionPool(uris, dateTimeProvider: dateTime), dateTimeProvider: dateTime) as RequestPipeline;

			sniffingPipeline.SniffPath.Should().Be("_nodes/_all/settings?flat_settings&timeout=2s");
		}
	}
}
