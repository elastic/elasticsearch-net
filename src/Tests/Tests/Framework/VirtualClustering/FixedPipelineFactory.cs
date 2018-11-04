using Elasticsearch.Net;
using Nest;

namespace Tests.Framework
{
	public class FixedPipelineFactory : IRequestPipelineFactory
	{
		public FixedPipelineFactory(IConnectionSettingsValues connectionSettings, IDateTimeProvider dateTimeProvider)
		{
			DateTimeProvider = dateTimeProvider;
			MemoryStreamFactory = new MemoryStreamFactory();

			Settings = connectionSettings;
			Pipeline = Create(Settings, DateTimeProvider, MemoryStreamFactory, new SearchRequestParameters());
		}

		public ElasticClient Client => new ElasticClient(Transport);

		public IRequestPipeline Pipeline { get; }

		private IDateTimeProvider DateTimeProvider { get; }
		private MemoryStreamFactory MemoryStreamFactory { get; }
		private IConnectionSettingsValues Settings { get; }

		private Transport<IConnectionSettingsValues> Transport =>
			new Transport<IConnectionSettingsValues>(Settings, this, DateTimeProvider, MemoryStreamFactory);

		public IRequestPipeline Create(IConnectionConfigurationValues configurationValues, IDateTimeProvider dateTimeProvider,
			IMemoryStreamFactory memorystreamFactory, IRequestParameters requestParameters
		) =>
			new RequestPipeline(Settings, DateTimeProvider, MemoryStreamFactory, requestParameters ?? new SearchRequestParameters());
	}
}
