using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using GetWarmerConverter = Func<IElasticsearchResponse, Stream, WarmerResponse>;
	using CrazyWarmerResponse = Dictionary<string, Dictionary<string, Dictionary<string, WarmerMapping>>>;

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IIndicesOperationResponse PutWarmer(string name, Func<PutWarmerDescriptor, PutWarmerDescriptor> selector)
		{
			selector.ThrowIfNull("selector");
			return this.Dispatch<PutWarmerDescriptor, PutWarmerRequestParameters, IndicesOperationResponse>(
				d => selector(d.Name(name).AllIndices()),
				(p, d) => this.RawDispatch.IndicesPutWarmerDispatch<IndicesOperationResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public IIndicesOperationResponse PutWarmer(IPutWarmerRequest putWarmerRequest)
		{
			return this.Dispatch<IPutWarmerRequest, PutWarmerRequestParameters, IndicesOperationResponse>(
				putWarmerRequest,
				(p, d) => this.RawDispatch.IndicesPutWarmerDispatch<IndicesOperationResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> PutWarmerAsync(string name, Func<PutWarmerDescriptor, PutWarmerDescriptor> selector)
		{
			selector.ThrowIfNull("selector");
			return this.DispatchAsync<PutWarmerDescriptor, PutWarmerRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				d => selector(d.Name(name).AllIndices()),
				(p, d) => this.RawDispatch.IndicesPutWarmerDispatchAsync<IndicesOperationResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> PutWarmerAsync(IPutWarmerRequest putWarmerRequest)
		{
			return this.DispatchAsync<IPutWarmerRequest, PutWarmerRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				putWarmerRequest,
				(p, d) => this.RawDispatch.IndicesPutWarmerDispatchAsync<IndicesOperationResponse>(p, d)
			);
		}



		/// <inheritdoc />
		public IWarmerResponse GetWarmer(string name, Func<GetWarmerDescriptor, GetWarmerDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatch<GetWarmerDescriptor, GetWarmerRequestParameters, WarmerResponse>(
				d => selector(d.Name(name).AllIndices()),
				(p, d) => this.RawDispatch.IndicesGetWarmerDispatch<WarmerResponse>(
					p.DeserializationState(new GetWarmerConverter(DeserializeWarmerResponse))
				)
			);
		}

		/// <inheritdoc />
		public IWarmerResponse GetWarmer(IGetWarmerRequest getWarmerRequest)
		{
			return this.Dispatch<IGetWarmerRequest, GetWarmerRequestParameters, WarmerResponse>(
				getWarmerRequest,
				(p, d) => this.RawDispatch.IndicesGetWarmerDispatch<WarmerResponse>(
					p.DeserializationState(new GetWarmerConverter(DeserializeWarmerResponse))
				)
			);
		}

		/// <inheritdoc />
		public Task<IWarmerResponse> GetWarmerAsync(string name, Func<GetWarmerDescriptor, GetWarmerDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.DispatchAsync<GetWarmerDescriptor, GetWarmerRequestParameters, WarmerResponse, IWarmerResponse>(
				d => selector(d.Name(name).AllIndices()),
				(p, d) => this.RawDispatch.IndicesGetWarmerDispatchAsync<WarmerResponse>(
					p.DeserializationState(new GetWarmerConverter(DeserializeWarmerResponse))
				)
			);
		}

		/// <inheritdoc />
		public Task<IWarmerResponse> GetWarmerAsync(IGetWarmerRequest getWarmerRequest)
		{
			return this.DispatchAsync<IGetWarmerRequest, GetWarmerRequestParameters, WarmerResponse, IWarmerResponse>(
				getWarmerRequest,
				(p, d) => this.RawDispatch.IndicesGetWarmerDispatchAsync<WarmerResponse>(
					p.DeserializationState(new GetWarmerConverter(DeserializeWarmerResponse))
				)
			);
		}



		/// <inheritdoc />
		public IIndicesOperationResponse DeleteWarmer(string name, Func<DeleteWarmerDescriptor, DeleteWarmerDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatch<DeleteWarmerDescriptor, DeleteWarmerRequestParameters, IndicesOperationResponse>(
				d => selector(d.Name(name).AllIndices()),
				(p, d) => this.RawDispatch.IndicesDeleteWarmerDispatch<IndicesOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public IIndicesOperationResponse DeleteWarmer(IDeleteWarmerRequest deleteWarmerRequest)
		{
			return this.Dispatch<IDeleteWarmerRequest, DeleteWarmerRequestParameters, IndicesOperationResponse>(
				deleteWarmerRequest,
				(p, d) => this.RawDispatch.IndicesDeleteWarmerDispatch<IndicesOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> DeleteWarmerAsync(string name, Func<DeleteWarmerDescriptor, DeleteWarmerDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.DispatchAsync
				<DeleteWarmerDescriptor, DeleteWarmerRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
					d => selector(d.Name(name).AllIndices()),
					(p, d) => this.RawDispatch.IndicesDeleteWarmerDispatchAsync<IndicesOperationResponse>(p)
				);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> DeleteWarmerAsync(IDeleteWarmerRequest deleteWarmerRequest)
		{
			return this.DispatchAsync<IDeleteWarmerRequest, DeleteWarmerRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
					deleteWarmerRequest,
					(p, d) => this.RawDispatch.IndicesDeleteWarmerDispatchAsync<IndicesOperationResponse>(p)
				);
		}



		/// <inheritdoc />
		private WarmerResponse DeserializeWarmerResponse(IElasticsearchResponse connectionStatus, Stream stream)
		{
			if (!connectionStatus.Success)
				return new WarmerResponse { IsValid = false};

			var dict = this.Serializer.Deserialize<CrazyWarmerResponse>(stream);
			var indices = new Dictionary<string, Dictionary<string, WarmerMapping>>();
			foreach (var kv in dict)
			{
				var indexDict = kv.Value;
				Dictionary<string, WarmerMapping> warmers;
				if (indexDict == null || !indexDict.TryGetValue("warmers", out warmers) || warmers == null)
					continue;
				foreach (var kvW in warmers)
				{
					kvW.Value.Name = kvW.Key;
				}
				indices.Add(kv.Key, warmers);
			}

			return new WarmerResponse
			{
				IsValid = true,
				Indices = indices
			};
		}
	}
}