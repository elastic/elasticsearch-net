// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport.Products;

namespace Elastic.Transport
{
	public interface IRequestPipelineFactory
	{
		IRequestPipeline Create(ITransportConfigurationValues configurationValues, IDateTimeProvider dateTimeProvider,
			IMemoryStreamFactory memoryStreamFactory, IRequestParameters requestParameters
		);
	}

	public class RequestPipelineFactory : IRequestPipelineFactory
	{
		public IRequestPipeline Create(ITransportConfigurationValues configurationValues, IDateTimeProvider dateTimeProvider,
			IMemoryStreamFactory memoryStreamFactory, IRequestParameters requestParameters
		) =>
			new RequestPipeline(configurationValues, dateTimeProvider, memoryStreamFactory, requestParameters);
	}
}
