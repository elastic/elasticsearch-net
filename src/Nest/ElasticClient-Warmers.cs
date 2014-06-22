using Elasticsearch.Net;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Nest
{
	using CrazyWarmerResponse = Dictionary<string, Dictionary<string, Dictionary<string, WarmerMapping>>>;
	using GetWarmerConverter = Func<IElasticsearchResponse, Stream, WarmerResponse>;

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
		public Task<IIndicesOperationResponse> PutWarmerAsync(string name,
			Func<PutWarmerDescriptor, PutWarmerDescriptor> selector)
		{
			selector.ThrowIfNull("selector");
			return this.DispatchAsync<PutWarmerDescriptor, PutWarmerRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				d => selector(d.Name(name).AllIndices()),
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
		public Task<IWarmerResponse> GetWarmerAsync(string name,
			Func<GetWarmerDescriptor, GetWarmerDescriptor> selector = null)
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
		public IIndicesOperationResponse DeleteWarmer(string name,
			Func<DeleteWarmerDescriptor, DeleteWarmerDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatch<DeleteWarmerDescriptor, DeleteWarmerRequestParameters, IndicesOperationResponse>(
				d => selector(d.Name(name).AllIndices()),
				(p, d) => this.RawDispatch.IndicesDeleteWarmerDispatch<IndicesOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> DeleteWarmerAsync(string name,
			Func<DeleteWarmerDescriptor, DeleteWarmerDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.DispatchAsync
				<DeleteWarmerDescriptor, DeleteWarmerRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
					d => selector(d.Name(name).AllIndices()),
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