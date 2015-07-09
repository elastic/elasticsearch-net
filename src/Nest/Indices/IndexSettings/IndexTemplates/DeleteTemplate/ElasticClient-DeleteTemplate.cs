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
		public IIndicesOperationResponse DeleteTemplate(string name, Func<DeleteTemplateDescriptor, DeleteTemplateDescriptor> deleteTemplateSelector = null)
		{
			deleteTemplateSelector = deleteTemplateSelector ?? (s => s);
			return this.Dispatcher.Dispatch<DeleteTemplateDescriptor, DeleteTemplateRequestParameters, IndicesOperationResponse>(
				d => deleteTemplateSelector(d.Name(name)),
				(p, d) => this.LowLevelDispatch.IndicesDeleteTemplateDispatch<IndicesOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public IIndicesOperationResponse DeleteTemplate(IDeleteTemplateRequest deleteTemplateRequest)
		{
			return this.Dispatcher.Dispatch<IDeleteTemplateRequest, DeleteTemplateRequestParameters, IndicesOperationResponse>(
				deleteTemplateRequest,
				(p, d) => this.LowLevelDispatch.IndicesDeleteTemplateDispatch<IndicesOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> DeleteTemplateAsync(string name, Func<DeleteTemplateDescriptor, DeleteTemplateDescriptor> deleteTemplateSelector = null)
		{
			deleteTemplateSelector = deleteTemplateSelector ?? (s => s);
			return this.Dispatcher.DispatchAsync
				<DeleteTemplateDescriptor, DeleteTemplateRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
					d => deleteTemplateSelector(d.Name(name)),
					(p, d) => this.LowLevelDispatch.IndicesDeleteTemplateDispatchAsync<IndicesOperationResponse>(p)
				);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> DeleteTemplateAsync(IDeleteTemplateRequest deleteTemplateRequest)
		{
			return this.Dispatcher.DispatchAsync<IDeleteTemplateRequest, DeleteTemplateRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
					deleteTemplateRequest,
					(p, d) => this.LowLevelDispatch.IndicesDeleteTemplateDispatchAsync<IndicesOperationResponse>(p)
				);
		}

	}
}