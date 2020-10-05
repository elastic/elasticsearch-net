// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport.Products;

namespace Elastic.Transport
{
	public interface IRequestPipelineFactory
	{
		IRequestPipeline Create(IConnectionConfigurationValues configurationValues, IDateTimeProvider dateTimeProvider,
			IMemoryStreamFactory memoryStreamFactory, IRequestParameters requestParametersm, IProductRegistration productRegistration
		);
	}

	public class RequestPipelineFactory : IRequestPipelineFactory
	{
		public IRequestPipeline Create(IConnectionConfigurationValues configurationValues, IDateTimeProvider dateTimeProvider,
			IMemoryStreamFactory memoryStreamFactory, IRequestParameters requestParameters, IProductRegistration productRegistration
		) =>
			new RequestPipeline(configurationValues, dateTimeProvider, memoryStreamFactory, requestParameters, productRegistration);
	}
}
