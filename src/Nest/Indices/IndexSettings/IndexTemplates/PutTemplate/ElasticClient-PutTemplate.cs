using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{

		/// <inheritdoc />
		public IIndicesOperationResponse PutTemplate(string name, Func<PutTemplateDescriptor, PutTemplateDescriptor> putTemplateSelector)
		{
			putTemplateSelector.ThrowIfNull("putTemplateSelector");
			var descriptor = putTemplateSelector(new PutTemplateDescriptor(_connectionSettings).Name(name));
			return this.Dispatcher.Dispatch<IPutTemplateRequest, PutTemplateRequestParameters, IndicesOperationResponse>(
				descriptor,
				(p, d) => this.LowLevelDispatch.IndicesPutTemplateDispatch<IndicesOperationResponse>(p, d.TemplateMapping)
			);
		}

		/// <inheritdoc />
		public IIndicesOperationResponse PutTemplate(IPutTemplateRequest putTemplateRequest)
		{
			return this.Dispatcher.Dispatch<IPutTemplateRequest, PutTemplateRequestParameters, IndicesOperationResponse>(
				putTemplateRequest,
				(p, d) => this.LowLevelDispatch.IndicesPutTemplateDispatch<IndicesOperationResponse>(p, d.TemplateMapping)
			);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> PutTemplateAsync(string name, Func<PutTemplateDescriptor, PutTemplateDescriptor> putTemplateSelector)
		{
			putTemplateSelector.ThrowIfNull("putTemplateSelector");
			var descriptor = putTemplateSelector(new PutTemplateDescriptor(_connectionSettings).Name(name));
			return this.Dispatcher.DispatchAsync<IPutTemplateRequest, PutTemplateRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
					descriptor,
					(p, d) => this.LowLevelDispatch.IndicesPutTemplateDispatchAsync<IndicesOperationResponse>(p, d.TemplateMapping)
				);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> PutTemplateAsync(IPutTemplateRequest putTemplateRequest)
		{
			return this.Dispatcher.DispatchAsync<IPutTemplateRequest, PutTemplateRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
					putTemplateRequest,
					(p, d) => this.LowLevelDispatch.IndicesPutTemplateDispatchAsync<IndicesOperationResponse>(p, d.TemplateMapping)
				);
		}

	}
}