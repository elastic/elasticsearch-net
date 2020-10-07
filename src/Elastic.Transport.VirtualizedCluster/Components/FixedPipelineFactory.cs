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
		public FixedPipelineFactory(ITransportConfigurationValues connectionSettings, IDateTimeProvider dateTimeProvider)
		{
			DateTimeProvider = dateTimeProvider;
			MemoryStreamFactory = TransportConfiguration.DefaultMemoryStreamFactory;

			Settings = connectionSettings;
			Pipeline = Create(Settings, DateTimeProvider, MemoryStreamFactory, new RequestParameters(HttpMethod.GET, supportsBody: false));
			Transport = new Transport<ITransportConfigurationValues>(Settings, this, DateTimeProvider, MemoryStreamFactory);
		}

		public IRequestPipeline Pipeline { get; }

		private IDateTimeProvider DateTimeProvider { get; }
		private IMemoryStreamFactory MemoryStreamFactory { get; }
		private ITransportConfigurationValues Settings { get; }
		public ITransport<ITransportConfigurationValues> Transport { get; }


		public IRequestPipeline Create(ITransportConfigurationValues configurationValues, IDateTimeProvider dateTimeProvider,
			IMemoryStreamFactory memoryStreamFactory, IRequestParameters requestParameters
		) =>
			new RequestPipeline(Settings, DateTimeProvider, MemoryStreamFactory, requestParameters ?? new RequestParameters(HttpMethod.GET, supportsBody: false));
	}
}
