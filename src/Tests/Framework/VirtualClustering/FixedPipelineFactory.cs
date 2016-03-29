using Elasticsearch.Net;
using Nest;

namespace Tests.Framework
{
	public class FixedPipelineFactory : IRequestPipelineFactory
	{
		private IConnectionSettingsValues Settings { get; }
		private Transport<IConnectionSettingsValues> Transport =>
			new Transport<IConnectionSettingsValues>(this.Settings, this, this.DateTimeProvider, this.MemoryStreamFactory);

		private IDateTimeProvider DateTimeProvider { get; }
		private MemoryStreamFactory MemoryStreamFactory { get; }

		public IRequestPipeline Pipeline { get; }

		public ElasticClient Client => new ElasticClient(this.Transport);

		public FixedPipelineFactory(IConnectionSettingsValues connectionSettings, IDateTimeProvider dateTimeProvider)
		{
			this.DateTimeProvider = dateTimeProvider;
			this.MemoryStreamFactory = new MemoryStreamFactory();

			this.Settings = connectionSettings;
			this.Pipeline = this.Create(this.Settings, this.DateTimeProvider, this.MemoryStreamFactory, new SearchRequestParameters());
		}

		public IRequestPipeline Create(IConnectionConfigurationValues configurationValues, IDateTimeProvider dateTimeProvider, IMemoryStreamFactory memorystreamFactory, IRequestParameters requestParameters) => 
			new RequestPipeline(this.Settings, this.DateTimeProvider, this.MemoryStreamFactory, requestParameters ?? new SearchRequestParameters());
	}
}