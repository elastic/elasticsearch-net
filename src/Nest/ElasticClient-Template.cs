using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using GetTemplateConverter = Func<IElasticsearchResponse, Stream, TemplateResponse>;

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ITemplateResponse GetTemplate(string name, Func<GetTemplateDescriptor, GetTemplateDescriptor> getTemplateSelector = null)
		{
			getTemplateSelector = getTemplateSelector ?? (s => s);
			return this.Dispatch<GetTemplateDescriptor, GetTemplateRequestParameters, TemplateResponse>(
				d => getTemplateSelector(d.Name(name)),
				(p, d) => RawDispatch.IndicesGetTemplateDispatch<TemplateResponse>(
					p.DeserializationState((GetTemplateConverter) DeserializeTemplateResponse)
				)
			);
		}

		/// <inheritdoc />
		public ITemplateResponse GetTemplate(IGetTemplateRequest getTemplateRequest)
		{
			return this.Dispatch<IGetTemplateRequest, GetTemplateRequestParameters, TemplateResponse>(
				getTemplateRequest,
				(p, d) => RawDispatch.IndicesGetTemplateDispatch<TemplateResponse>(
					p.DeserializationState((GetTemplateConverter) DeserializeTemplateResponse)
				)
			);
		}

		/// <inheritdoc />
		public Task<ITemplateResponse> GetTemplateAsync(string name, Func<GetTemplateDescriptor, GetTemplateDescriptor> getTemplateSelector = null)
		{
			getTemplateSelector = getTemplateSelector ?? (s => s);
			return this.DispatchAsync<GetTemplateDescriptor, GetTemplateRequestParameters, TemplateResponse, ITemplateResponse>(
				d => getTemplateSelector(d.Name(name)),
				(p, d) => this.RawDispatch.IndicesGetTemplateDispatchAsync<TemplateResponse>(
					p.DeserializationState((GetTemplateConverter) DeserializeTemplateResponse)
				)
			);
		}

		/// <inheritdoc />
		public Task<ITemplateResponse> GetTemplateAsync(IGetTemplateRequest getTemplateRequest)
		{
			return this.DispatchAsync<IGetTemplateRequest, GetTemplateRequestParameters, TemplateResponse, ITemplateResponse>(
				getTemplateRequest,
				(p, d) => this.RawDispatch.IndicesGetTemplateDispatchAsync<TemplateResponse>(
					p.DeserializationState((GetTemplateConverter) DeserializeTemplateResponse)
				)
			);
		}



		/// <inheritdoc />
		public IIndicesOperationResponse PutTemplate(string name, Func<PutTemplateDescriptor, PutTemplateDescriptor> putTemplateSelector)
		{
			putTemplateSelector.ThrowIfNull("putTemplateSelector");
			var descriptor = putTemplateSelector(new PutTemplateDescriptor(_connectionSettings).Name(name));
			return this.Dispatch<IPutTemplateRequest, PutTemplateRequestParameters, IndicesOperationResponse>(
				descriptor,
				(p, d) => this.RawDispatch.IndicesPutTemplateDispatch<IndicesOperationResponse>(p, d.TemplateMapping)
			);
		}

		/// <inheritdoc />
		public IIndicesOperationResponse PutTemplate(IPutTemplateRequest putTemplateRequest)
		{
			return this.Dispatch<IPutTemplateRequest, PutTemplateRequestParameters, IndicesOperationResponse>(
				putTemplateRequest,
				(p, d) => this.RawDispatch.IndicesPutTemplateDispatch<IndicesOperationResponse>(p, d.TemplateMapping)
			);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> PutTemplateAsync(string name, Func<PutTemplateDescriptor, PutTemplateDescriptor> putTemplateSelector)
		{
			putTemplateSelector.ThrowIfNull("putTemplateSelector");
			var descriptor = putTemplateSelector(new PutTemplateDescriptor(_connectionSettings).Name(name));
			return this.DispatchAsync<IPutTemplateRequest, PutTemplateRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
					descriptor,
					(p, d) => this.RawDispatch.IndicesPutTemplateDispatchAsync<IndicesOperationResponse>(p, d.TemplateMapping)
				);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> PutTemplateAsync(IPutTemplateRequest putTemplateRequest)
		{
			return this.DispatchAsync<IPutTemplateRequest, PutTemplateRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
					putTemplateRequest,
					(p, d) => this.RawDispatch.IndicesPutTemplateDispatchAsync<IndicesOperationResponse>(p, d.TemplateMapping)
				);
		}



		/// <inheritdoc />
		public IIndicesOperationResponse DeleteTemplate(string name, Func<DeleteTemplateDescriptor, DeleteTemplateDescriptor> deleteTemplateSelector = null)
		{
			deleteTemplateSelector = deleteTemplateSelector ?? (s => s);
			return this.Dispatch<DeleteTemplateDescriptor, DeleteTemplateRequestParameters, IndicesOperationResponse>(
				d => deleteTemplateSelector(d.Name(name)),
				(p, d) => this.RawDispatch.IndicesDeleteTemplateDispatch<IndicesOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public IIndicesOperationResponse DeleteTemplate(IDeleteTemplateRequest deleteTemplateRequest)
		{
			return this.Dispatch<IDeleteTemplateRequest, DeleteTemplateRequestParameters, IndicesOperationResponse>(
				deleteTemplateRequest,
				(p, d) => this.RawDispatch.IndicesDeleteTemplateDispatch<IndicesOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		[Obsolete("Scheduled for removal in 2.0, this method name has a typo")]
		public Task<IIndicesOperationResponse> DeleteTemplateAync(string name, Func<DeleteTemplateDescriptor, DeleteTemplateDescriptor> deleteTemplateSelector = null)
		{
			return DeleteTemplateAsync(name, deleteTemplateSelector);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> DeleteTemplateAsync(string name, Func<DeleteTemplateDescriptor, DeleteTemplateDescriptor> deleteTemplateSelector = null)
		{
			deleteTemplateSelector = deleteTemplateSelector ?? (s => s);
			return this.DispatchAsync
				<DeleteTemplateDescriptor, DeleteTemplateRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
					d => deleteTemplateSelector(d.Name(name)),
					(p, d) => this.RawDispatch.IndicesDeleteTemplateDispatchAsync<IndicesOperationResponse>(p)
				);
		}

		/// <inheritdoc />
		[Obsolete("Scheduled for removal in 2.0, this method name has a typo")]
		public Task<IIndicesOperationResponse> DeleteTemplateAync(IDeleteTemplateRequest deleteTemplateRequest)
		{
			return DeleteTemplateAsync(deleteTemplateRequest);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> DeleteTemplateAsync(IDeleteTemplateRequest deleteTemplateRequest)
		{
			return this.DispatchAsync<IDeleteTemplateRequest, DeleteTemplateRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
					deleteTemplateRequest,
					(p, d) => this.RawDispatch.IndicesDeleteTemplateDispatchAsync<IndicesOperationResponse>(p)
				);
		}



		private TemplateResponse DeserializeTemplateResponse(
			IElasticsearchResponse response,
			Stream stream)
		{
			if (!response.Success) return new TemplateResponse {IsValid = false};

			var dict = this.Serializer.Deserialize<Dictionary<string, TemplateMapping>>(stream);
			if (dict.Count == 0)
				throw new DslException("Could not deserialize TemplateMapping");

			return new TemplateResponse
			{
				IsValid = true,
				Name = dict.First().Key,
				TemplateMapping = dict.First().Value
			};
		}
	}
}