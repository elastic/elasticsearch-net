using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Nest.Resolvers.Writers;

namespace Nest
{
	using GetTemplateConverter = Func<IElasticsearchResponse, Stream, TemplateResponse>;
	public partial class ElasticClient
	{
		public ITemplateResponse GetTemplate(string name, Func<GetTemplateDescriptor, GetTemplateDescriptor> getTemplateSelector = null)
		{
			getTemplateSelector = getTemplateSelector ?? (s => s);
			return this.Dispatch<GetTemplateDescriptor, GetTemplateQueryString, TemplateResponse>(
				(d) => getTemplateSelector(d.Name(name)),
				(p, d) => this.RawDispatch.IndicesGetTemplateDispatch<TemplateResponse>(p, (GetTemplateConverter)this.DeserializeTemplateResponse)
			);
		}



		public Task<ITemplateResponse> GetTemplateAsync(string name, Func<GetTemplateDescriptor, GetTemplateDescriptor> getTemplateSelector = null)
		{
			getTemplateSelector = getTemplateSelector ?? (s => s);
			return this.DispatchAsync<GetTemplateDescriptor, GetTemplateQueryString, TemplateResponse, ITemplateResponse>(
				d => getTemplateSelector(d.Name(name)),
				(p, d) => this.RawDispatch.IndicesGetTemplateDispatchAsync<TemplateResponse>(
					p, (GetTemplateConverter)this.DeserializeTemplateResponse)
			);
		}

		private TemplateResponse DeserializeTemplateResponse(
			IElasticsearchResponse response,
			Stream stream)
		{
			if (!response.Success) return new TemplateResponse { ConnectionStatus = response, IsValid = false };

			var dict = this.Serializer.DeserializeInternal<Dictionary<string, TemplateMapping>>(stream);
			if (dict.Count == 0)
				throw new DslException("Could not deserialize TemplateMapping");

			return new TemplateResponse
			{
				ConnectionStatus = response,
				IsValid = true,
				Name = dict.First().Key,
				TemplateMapping = dict.First().Value
			};
		}

		public IIndicesOperationResponse PutTemplate(string name, Func<PutTemplateDescriptor, PutTemplateDescriptor> putTemplateSelector)
		{
			putTemplateSelector.ThrowIfNull("putTemplateSelector");
			var descriptor = putTemplateSelector(new PutTemplateDescriptor(this._connectionSettings).Name(name));
			return this.Dispatch<PutTemplateDescriptor, PutTemplateQueryString, IndicesOperationResponse>(
				descriptor,
				(p, d) => this.RawDispatch.IndicesPutTemplateDispatch<IndicesOperationResponse>(p, d._TemplateMapping)
			);
		}

		public Task<IIndicesOperationResponse> PutTemplateAsync(string name, Func<PutTemplateDescriptor, PutTemplateDescriptor> putTemplateSelector)
		{
			putTemplateSelector.ThrowIfNull("putTemplateSelector");
			var descriptor = putTemplateSelector(new PutTemplateDescriptor(this._connectionSettings).Name(name));
			return this.DispatchAsync<PutTemplateDescriptor, PutTemplateQueryString, IndicesOperationResponse, IIndicesOperationResponse>(
				descriptor,
				(p, d) => this.RawDispatch.IndicesPutTemplateDispatchAsync<IndicesOperationResponse>(p, d._TemplateMapping)
			);
		}

		public IIndicesOperationResponse DeleteTemplate(string name, Func<DeleteTemplateDescriptor, DeleteTemplateDescriptor> deleteTemplateSelector = null)
		{
			deleteTemplateSelector = deleteTemplateSelector ?? (s => s);
			return this.Dispatch<DeleteTemplateDescriptor, DeleteTemplateQueryString, IndicesOperationResponse>(
				d => deleteTemplateSelector(d.Name(name)),
				(p, d) => this.RawDispatch.IndicesDeleteTemplateDispatch<IndicesOperationResponse>(p)
			);
		}

		public Task<IIndicesOperationResponse> DeleteTemplateAync(string name, Func<DeleteTemplateDescriptor, DeleteTemplateDescriptor> deleteTemplateSelector = null)
		{
			deleteTemplateSelector = deleteTemplateSelector ?? (s => s);
			return this.DispatchAsync<DeleteTemplateDescriptor, DeleteTemplateQueryString, IndicesOperationResponse, IIndicesOperationResponse>(
				d => deleteTemplateSelector(d.Name(name)),
				(p, d) => this.RawDispatch.IndicesDeleteTemplateDispatchAsync<IndicesOperationResponse>(p)
			);
		}
	}
}