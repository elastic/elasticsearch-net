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

namespace Tests.ClientConcepts.LowLevel
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

		private IRequestPipeline CreatePipeline(Func<IEnumerable<Uri>, IConnectionPool> setupPool, Func<ConnectionSettings, ConnectionSettings> settingsSelector = null) => 
			new FixedPipelineFactory(setupPool, settingsSelector).Pipeline;

		[U] public void FirstUsageCheck()
		{
			var singleNodePipeline = CreatePipeline(uris => new SingleNodeConnectionPool(uris.First()));
			var staticPipeline = CreatePipeline(uris => new StaticConnectionPool(uris));
			var sniffingConnectionPool = CreatePipeline(uris => new SniffingConnectionPool(uris));
			/** Here we have setup three pipelines using three different connection pools, lets see how they behave*/

			singleNodePipeline.FirstPoolUsageNeedsSniffing.Should().BeFalse();
			staticPipeline.FirstPoolUsageNeedsSniffing.Should().BeFalse();
			sniffingConnectionPool.FirstPoolUsageNeedsSniffing.Should().BeTrue();

			/** Only the cluster that supports reseeding will opt in to FirstPoolUsageNeedsSniffing() 
			* You can however disable this on ConnectionSettings
			*/
			sniffingConnectionPool = CreatePipeline(uris => new SniffingConnectionPool(uris), s => s.SniffOnStartup(false));
			sniffingConnectionPool.FirstPoolUsageNeedsSniffing.Should().BeFalse();
		}

	}
}
