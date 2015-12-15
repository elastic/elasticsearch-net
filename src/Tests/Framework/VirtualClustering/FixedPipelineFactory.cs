using Elasticsearch.Net;
using Nest;

namespace Tests.Framework
{
	public class FixedPipelineFactory : IRequestPipelineFactory
	{
		public IConnectionSettingsValues Settings { get; }
		public Transport<IConnectionSettingsValues> Transport { get; }
		public IRequestPipeline Pipeline { get; }

		public IDateTimeProvider DateTimeProvider { get; }
		public MemoryStreamFactory MemoryStreamFactory { get; }

		public ElasticClient Client => new ElasticClient(this.Transport);

		public FixedPipelineFactory(IConnectionSettingsValues connectionSettings, IDateTimeProvider dateTimeProvider)
		{
			this.DateTimeProvider = dateTimeProvider;
			this.MemoryStreamFactory = new MemoryStreamFactory();

			this.Settings = connectionSettings;
			this.Pipeline = this.Create(this.Settings, this.DateTimeProvider, this.MemoryStreamFactory, new SearchRequestParameters());
			this.Transport = new Transport<IConnectionSettingsValues>(this.Settings, this, this.DateTimeProvider, this.MemoryStreamFactory);
		}

		public IRequestPipeline Create(IConnectionConfigurationValues configurationValues, IDateTimeProvider dateTimeProvider, IMemoryStreamFactory memorystreamFactory, IRequestParameters requestParameters) => 
			new RequestPipeline(this.Settings, this.DateTimeProvider, this.MemoryStreamFactory, new SearchRequestParameters());
	}
}