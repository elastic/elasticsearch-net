using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nest.Resolvers.Writers;

namespace Nest
{
	public partial class ElasticClient
	{
		public ITemplateResponse GetTemplate(Func<GetTemplateDescriptor, GetTemplateDescriptor> getTemplateSelector)
		{
			return this.Dispatch<GetTemplateDescriptor, GetTemplateQueryString, TemplateResponse>(
				getTemplateSelector,
				(p, d) => this.RawDispatch.IndicesGetTemplateDispatch(p)
			);
		}

		public Task<ITemplateResponse> GetTemplateAsync(Func<GetTemplateDescriptor, GetTemplateDescriptor> getTemplateSelector)
		{
			return this.DispatchAsync<GetTemplateDescriptor, GetTemplateQueryString, TemplateResponse, ITemplateResponse>(
				getTemplateSelector,
				(p, d) => this.RawDispatch.IndicesGetTemplateDispatchAsync(p)
			);
		}

		public IIndicesOperationResponse PutTemplate(Func<PutTemplateDescriptor, PutTemplateDescriptor> putTemplateSelector)
		{
			putTemplateSelector.ThrowIfNull("putTemplateSelector");
			var descriptor = putTemplateSelector(new PutTemplateDescriptor(this._connectionSettings));
			return this.Dispatch<PutTemplateDescriptor, PutTemplateQueryString, IndicesOperationResponse>(
				descriptor,
				(p, d) => this.RawDispatch.IndicesPutTemplateDispatch(p, d._TemplateMapping)
			);
		}

		public Task<IIndicesOperationResponse> PutTemplateAsync(Func<PutTemplateDescriptor, PutTemplateDescriptor> putTemplateSelector)
		{
			putTemplateSelector.ThrowIfNull("putTemplateSelector");
			var descriptor = putTemplateSelector(new PutTemplateDescriptor(this._connectionSettings));
			return this.DispatchAsync<PutTemplateDescriptor, PutTemplateQueryString, IndicesOperationResponse, IIndicesOperationResponse>(
				descriptor,
				(p, d) => this.RawDispatch.IndicesPutTemplateDispatchAsync(p, d._TemplateMapping)
			);
		}
		
		public IIndicesOperationResponse DeleteTemplate(Func<DeleteTemplateDescriptor, DeleteTemplateDescriptor> deleteTemplateSelector)
		{
			return this.Dispatch<DeleteTemplateDescriptor, DeleteTemplateQueryString, IndicesOperationResponse>(
				deleteTemplateSelector,
				(p, d) => this.RawDispatch.IndicesDeleteTemplateDispatch(p)
			);
		}
		
		public Task<IIndicesOperationResponse> DeleteTemplateAync(Func<DeleteTemplateDescriptor, DeleteTemplateDescriptor> deleteTemplateSelector)
		{
			return this.DispatchAsync<DeleteTemplateDescriptor, DeleteTemplateQueryString, IndicesOperationResponse, IIndicesOperationResponse>(
				deleteTemplateSelector,
				(p, d) => this.RawDispatch.IndicesDeleteTemplateDispatchAsync(p)
			);
		}
	}
}