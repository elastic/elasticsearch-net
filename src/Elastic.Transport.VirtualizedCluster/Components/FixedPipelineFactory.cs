// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport.Products;
using Elastic.Transport.Products.Elasticsearch;
using Elastic.Transport.VirtualizedCluster.Products;

namespace Elastic.Transport.VirtualizedCluster.Components
{
	public class FixedPipelineFactory : IRequestPipelineFactory
	{
		public FixedPipelineFactory(IConnectionConfigurationValues connectionSettings, IDateTimeProvider dateTimeProvider, IMockProductRegistration productRegistration)
		{
			DateTimeProvider = dateTimeProvider;
			MemoryStreamFactory = ConnectionConfiguration.DefaultMemoryStreamFactory;

			Settings = connectionSettings;
			Pipeline = Create(Settings, DateTimeProvider, MemoryStreamFactory, new RequestParameters(HttpMethod.GET, supportsBody: false), productRegistration.ProductRegistration);
			Transport = new Transport<IConnectionConfigurationValues>(Settings, this, DateTimeProvider, MemoryStreamFactory, ElasticsearchProductRegistration.Default);
		}

		public IRequestPipeline Pipeline { get; }

		private IDateTimeProvider DateTimeProvider { get; }
		private IMemoryStreamFactory MemoryStreamFactory { get; }
		private IConnectionConfigurationValues Settings { get; }
		public ITransport<IConnectionConfigurationValues> Transport { get; }


		public IRequestPipeline Create(IConnectionConfigurationValues configurationValues, IDateTimeProvider dateTimeProvider,
			IMemoryStreamFactory memoryStreamFactory, IRequestParameters requestParameters, IProductRegistration productRegistration
		) =>
			new RequestPipeline(Settings, DateTimeProvider, MemoryStreamFactory, requestParameters ?? new RequestParameters(HttpMethod.GET, supportsBody: false), productRegistration);
	}
}
