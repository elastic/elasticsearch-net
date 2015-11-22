using System;
using System.Collections.Specialized;
using System.Net;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using Nest;
using System.Text;
using Elasticsearch.Net.Providers;
using FluentAssertions;
using Tests.Framework;
using System.Linq;
using System.Collections.Generic;

namespace Tests.ClientConcepts.ConnectionPooling.BuildingBlocks
{
	public class RequestPipelines
	{
		/** == Request pipeline
		* Every request is executed in the context of `RequestPipeline` when you are using the default `ITransport` implementation.
		* 
		*/

		[U] public void RequestPipeline()
		{
			var settings = TestClient.CreateSettings();

			/** When calling Request(Async) on Transport the whole coordination of the request is deferred to a new instance in a `using` block. */
			var pipeline = new RequestPipeline(settings, new DateTimeProvider(), new MemoryStreamFactory(), new SearchRequestParameters());
			pipeline.GetType().Should().Implement<IDisposable>();

			/** However the transport does not instantiate RequestPipeline directly, it uses a pluggable `IRequestPipelineFactory`*/
			var requestPipelineFactory = new RequestPipelineFactory();
			var requestPipeline = requestPipelineFactory.Create(settings, new DateTimeProvider(), new MemoryStreamFactory(), new SearchRequestParameters());
			requestPipeline.Should().BeOfType<RequestPipeline>();
			requestPipeline.GetType().Should().Implement<IDisposable>();

			/** which can be passed to the transport when instantiating a client */
			var transport = new Transport<ConnectionSettings>(settings, requestPipelineFactory, new DateTimeProvider(), new MemoryStreamFactory());

			/** this allows you to have requests executed on your own custom request pipeline */
		}

		private IRequestPipeline CreatePipeline(
			Func<IEnumerable<Uri>, IConnectionPool> setupPool, Func<ConnectionSettings, ConnectionSettings> settingsSelector = null, IDateTimeProvider dateTimeProvider= null)
		{
			var pool = setupPool(new[] {TestClient.CreateNode(), TestClient.CreateNode(9201)});
			var settings = new ConnectionSettings(pool, TestClient.CreateConnection());
			settings = settingsSelector?.Invoke(settings) ?? settings;
			return new FixedPipelineFactory(settings, dateTimeProvider ?? new DateTimeProvider()).Pipeline;
		}

		[U] public void FirstUsageCheck()
		{
			var singleNodePipeline = CreatePipeline(uris => new SingleNodeConnectionPool(uris.First()));
			var staticPipeline = CreatePipeline(uris => new StaticConnectionPool(uris));
			var sniffingPipeline = CreatePipeline(uris => new SniffingConnectionPool(uris));
			/** Here we have setup three pipelines using three different connection pools, lets see how they behave*/

			singleNodePipeline.FirstPoolUsageNeedsSniffing.Should().BeFalse();
			staticPipeline.FirstPoolUsageNeedsSniffing.Should().BeFalse();
			sniffingPipeline.FirstPoolUsageNeedsSniffing.Should().BeTrue();


			/** Only the cluster that supports reseeding will opt in to FirstPoolUsageNeedsSniffing() 
			* You can however disable this on ConnectionSettings
			*/
			sniffingPipeline = CreatePipeline(uris => new SniffingConnectionPool(uris), s => s.SniffOnStartup(false));
			sniffingPipeline.FirstPoolUsageNeedsSniffing.Should().BeFalse();
		}

		[U] public void SniffsOnConnectionFailure()
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

		[U] public void SniffsOnStaleCluster()
		{
			var dateTime = new TestableDateTimeProvider();
			var singleNodePipeline = CreatePipeline(uris => new SingleNodeConnectionPool(uris.First(), dateTime), dateTimeProvider: dateTime);
			var staticPipeline = CreatePipeline(uris => new StaticConnectionPool(uris, dateTimeProvider: dateTime), dateTimeProvider: dateTime);
			var sniffingPipeline = CreatePipeline(uris => new SniffingConnectionPool(uris, dateTimeProvider: dateTime), dateTimeProvider: dateTime);

			singleNodePipeline.SniffsOnStaleCluster.Should().BeFalse();
			staticPipeline.SniffsOnStaleCluster.Should().BeFalse();
			sniffingPipeline.SniffsOnStaleCluster.Should().BeTrue();
		
			singleNodePipeline.StaleClusterState.Should().BeFalse();
			staticPipeline.StaleClusterState.Should().BeFalse();
			sniffingPipeline.StaleClusterState.Should().BeFalse();

			/** go one hour into the future */
			dateTime.ChangeTime(d=>d.Add(TimeSpan.FromHours(2)));

			/**connection pools that do not support reseeding never go stale */
			singleNodePipeline.StaleClusterState.Should().BeFalse();
			staticPipeline.StaleClusterState.Should().BeFalse();
			/** the sniffing connection pool supports reseeding so the pipeline will signal the state is out of date */
			sniffingPipeline.StaleClusterState.Should().BeTrue();

		}
		

		/** A request pipeline also checks whether the overall time across multiple retries exceeds the reqeust timeout
		* See the maxretry documentation for more details, here we assert that our request pipeline exposes this propertly
		*/
		[U] public void IsTakingTooLong()
		{
			var dateTime = new TestableDateTimeProvider();
			var singleNodePipeline = CreatePipeline(uris => new SingleNodeConnectionPool(uris.First(), dateTime), dateTimeProvider: dateTime);
			var staticPipeline = CreatePipeline(uris => new StaticConnectionPool(uris, dateTimeProvider: dateTime), dateTimeProvider: dateTime);
			var sniffingPipeline = CreatePipeline(uris => new SniffingConnectionPool(uris, dateTimeProvider: dateTime), dateTimeProvider: dateTime);

			singleNodePipeline.IsTakingTooLong.Should().BeFalse();
			staticPipeline.IsTakingTooLong.Should().BeFalse();
			sniffingPipeline.IsTakingTooLong.Should().BeFalse();

			/** go one hour into the future */
			dateTime.ChangeTime(d=>d.Add(TimeSpan.FromHours(2)));

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


	}
}
