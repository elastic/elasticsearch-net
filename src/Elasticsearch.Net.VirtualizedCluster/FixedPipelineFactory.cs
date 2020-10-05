// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport;
using Elastic.Transport.Products;

namespace Elasticsearch.Net.VirtualizedCluster
{
	public class FixedPipelineFactory : IRequestPipelineFactory
	{
		public FixedPipelineFactory(IConnectionConfigurationValues connectionSettings, IDateTimeProvider dateTimeProvider, IProductRegistration productRegistration)
		{
			DateTimeProvider = dateTimeProvider;
			MemoryStreamFactory = ConnectionConfiguration.DefaultMemoryStreamFactory;

			Settings = connectionSettings;
			Pipeline = Create(Settings, DateTimeProvider, MemoryStreamFactory, new SearchRequestParameters(), productRegistration);
		}

		public ElasticLowLevelClient Client => new ElasticLowLevelClient(Transport);

		public IRequestPipeline Pipeline { get; }

		private IDateTimeProvider DateTimeProvider { get; }
		private IMemoryStreamFactory MemoryStreamFactory { get; }
		private IConnectionConfigurationValues Settings { get; }

		private Transport<IConnectionConfigurationValues> Transport =>
			new Transport<IConnectionConfigurationValues>(Settings, this, DateTimeProvider, MemoryStreamFactory, ElasticsearchProductRegistration.Default);

		public IRequestPipeline Create(IConnectionConfigurationValues configurationValues, IDateTimeProvider dateTimeProvider,
			IMemoryStreamFactory memoryStreamFactory, IRequestParameters requestParameters, IProductRegistration productRegistration
		) =>
			new RequestPipeline(Settings, DateTimeProvider, MemoryStreamFactory, requestParameters ?? new SearchRequestParameters(), productRegistration);
	}
}
