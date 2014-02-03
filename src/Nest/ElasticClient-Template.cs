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
		public ITemplateResponse GetTemplate(string name, Func<GetTemplateDescriptor, GetTemplateDescriptor> getTemplateSelector = null)
		{
		    getTemplateSelector = getTemplateSelector ?? (s => s);
			return this.Dispatch<GetTemplateDescriptor, GetTemplateQueryString, TemplateResponse>(
				(d) => getTemplateSelector(d.Name(name)),
				(p, d) => this.RawDispatch.IndicesGetTemplateDispatch(p),
				(gt, c) =>
				{
					var dict = c.Deserialize<Dictionary<string, TemplateMapping>>();
					if (dict.Count == 0)
						throw new DslException("Could not deserialize TemplateMapping");

					return new TemplateResponse
					{
						Name = dict.First().Key,
						TemplateMapping = dict.First().Value
					};
				}
			);
		}

		public Task<ITemplateResponse> GetTemplateAsync(string name, Func<GetTemplateDescriptor, GetTemplateDescriptor> getTemplateSelector = null)
		{
		    getTemplateSelector = getTemplateSelector ?? (s => s);
			return this.DispatchAsync<GetTemplateDescriptor, GetTemplateQueryString, TemplateResponse, ITemplateResponse>(
				d => getTemplateSelector(d.Name(name)),
				(p, d) => this.RawDispatch.IndicesGetTemplateDispatchAsync(p)
			);
		}

		public IIndicesOperationResponse PutTemplate(string name, Func<PutTemplateDescriptor, PutTemplateDescriptor> putTemplateSelector)
		{
			putTemplateSelector.ThrowIfNull("putTemplateSelector");
			var descriptor = putTemplateSelector(new PutTemplateDescriptor(this._connectionSettings).Name(name));
			return this.Dispatch<PutTemplateDescriptor, PutTemplateQueryString, IndicesOperationResponse>(
				descriptor,
				(p, d) => this.RawDispatch.IndicesPutTemplateDispatch(p, d._TemplateMapping)
			);
		}

		public Task<IIndicesOperationResponse> PutTemplateAsync(string name, Func<PutTemplateDescriptor, PutTemplateDescriptor> putTemplateSelector)
		{
			putTemplateSelector.ThrowIfNull("putTemplateSelector");
			var descriptor = putTemplateSelector(new PutTemplateDescriptor(this._connectionSettings).Name(name));
			return this.DispatchAsync<PutTemplateDescriptor, PutTemplateQueryString, IndicesOperationResponse, IIndicesOperationResponse>(
				descriptor,
				(p, d) => this.RawDispatch.IndicesPutTemplateDispatchAsync(p, d._TemplateMapping)
			);
		}
		
		public IIndicesOperationResponse DeleteTemplate(string name, Func<DeleteTemplateDescriptor, DeleteTemplateDescriptor> deleteTemplateSelector = null)
		{
		    deleteTemplateSelector = deleteTemplateSelector ?? (s => s);
			return this.Dispatch<DeleteTemplateDescriptor, DeleteTemplateQueryString, IndicesOperationResponse>(
				d => deleteTemplateSelector(d.Name(name)),
				(p, d) => this.RawDispatch.IndicesDeleteTemplateDispatch(p)
			);
		}
		
		public Task<IIndicesOperationResponse> DeleteTemplateAync(string name, Func<DeleteTemplateDescriptor, DeleteTemplateDescriptor> deleteTemplateSelector = null)
		{
		    deleteTemplateSelector = deleteTemplateSelector ?? (s => s);
			return this.DispatchAsync<DeleteTemplateDescriptor, DeleteTemplateQueryString, IndicesOperationResponse, IIndicesOperationResponse>(
				d => deleteTemplateSelector(d.Name(name)),
				(p, d) => this.RawDispatch.IndicesDeleteTemplateDispatchAsync(p)
			);
		}
	}
}