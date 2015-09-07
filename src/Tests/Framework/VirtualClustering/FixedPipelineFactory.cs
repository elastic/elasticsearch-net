using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Providers;
using Nest;

namespace Tests.Framework
{
	public class FixedPipelineFactory : IRequestPipelineFactory
	{
		public IConnectionSettingsValues Settings { get; }
		public Transport<IConnectionSettingsValues> Transport { get; }
		public RequestPipeline Pipeline { get; }

		public IDateTimeProvider DateTimeProvider { get; }
		public MemoryStreamFactory MemoryStreamFactory { get; }
		public RequestParameters RequestParameters { get; }

		public ElasticClient Client => new ElasticClient(this.Transport);

		public FixedPipelineFactory(Func<IEnumerable<Uri>, IConnectionPool> setupPool, Func<ConnectionSettings, ConnectionSettings> settingsSelector = null)
		{
			this.DateTimeProvider = new DateTimeProvider();
			this.MemoryStreamFactory = new MemoryStreamFactory();
			this.RequestParameters = new RequestParameters();

			var uris = new[] { TestClient.CreateNode(), TestClient.CreateNode(9201) };
			var settings = new ConnectionSettings(setupPool(uris), TestClient.CreateConnection());
			this.Settings = settingsSelector?.Invoke(settings) ?? settings;
			this.Pipeline = new RequestPipeline(this.Settings, this.DateTimeProvider, this.MemoryStreamFactory, new RequestParameters());
			this.Transport = new Transport<IConnectionSettingsValues>(this.Settings, this, this.DateTimeProvider, this.MemoryStreamFactory);
		}

		public FixedPipelineFactory(IConnectionSettingsValues connectionSettings, IDateTimeProvider dateTimeProvider)
		{
			this.DateTimeProvider = dateTimeProvider;
			this.MemoryStreamFactory = new MemoryStreamFactory();
			this.RequestParameters = new RequestParameters();

			this.Settings = connectionSettings;
			this.Pipeline = new RequestPipeline(this.Settings, this.DateTimeProvider, this.MemoryStreamFactory, new RequestParameters());
			this.Transport = new Transport<IConnectionSettingsValues>(this.Settings, this, this.DateTimeProvider, this.MemoryStreamFactory);
		}

		public IRequestPipeline Create(IConnectionConfigurationValues configurationValues, IDateTimeProvider dateTimeProvider, IMemoryStreamFactory memorystreamFactory, IRequestParameters requestParameters)
		{
			return this.Pipeline;
		}

	}
}