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
		public IWarmerResponse GetWarmer(string name, Func<GetWarmerDescriptor, GetWarmerDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.Dispatch<GetWarmerDescriptor, GetWarmerRequestParameters, WarmerResponse>(
				d => selector(d.Name(name).AllIndices()),
				(p, d) => this.RawDispatch.IndicesGetWarmerDispatch<WarmerResponse>(
					p.DeserializationState(new GetWarmerConverter(DeserializeWarmerResponse))
				)
			);
		}

		/// <inheritdoc />
		public IWarmerResponse GetWarmer(IGetWarmerRequest getWarmerRequest)
		{
			return this.Dispatcher.Dispatch<IGetWarmerRequest, GetWarmerRequestParameters, WarmerResponse>(
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
			return this.Dispatcher.DispatchAsync<GetWarmerDescriptor, GetWarmerRequestParameters, WarmerResponse, IWarmerResponse>(
				d => selector(d.Name(name).AllIndices()),
				(p, d) => this.RawDispatch.IndicesGetWarmerDispatchAsync<WarmerResponse>(
					p.DeserializationState(new GetWarmerConverter(DeserializeWarmerResponse))
				)
			);
		}

		/// <inheritdoc />
		public Task<IWarmerResponse> GetWarmerAsync(IGetWarmerRequest getWarmerRequest)
		{
			return this.Dispatcher.DispatchAsync<IGetWarmerRequest, GetWarmerRequestParameters, WarmerResponse, IWarmerResponse>(
				getWarmerRequest,
				(p, d) => this.RawDispatch.IndicesGetWarmerDispatchAsync<WarmerResponse>(
					p.DeserializationState(new GetWarmerConverter(DeserializeWarmerResponse))
				)
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