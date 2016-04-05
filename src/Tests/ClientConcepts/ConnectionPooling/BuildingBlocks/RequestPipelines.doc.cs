using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;

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
			var settings = TestClient.CreateSettings();

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
			Func<IEnumerable<Uri>, IConnectionPool> setupPool, Func<ConnectionSettings, ConnectionSettings> settingsSelector = null, IDateTimeProvider dateTimeProvider = null)
		{
			var pool = setupPool(new[] { TestClient.CreateNode(), TestClient.CreateNode(9201) });
			var settings = new ConnectionSettings(pool, TestClient.CreateConnection());
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
