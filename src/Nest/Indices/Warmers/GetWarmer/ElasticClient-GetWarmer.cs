using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using GetWarmerConverter = Func<IApiCallDetails, Stream, WarmerResponse>;
	using CrazyWarmerResponse = Dictionary<string, Dictionary<string, IWarmers>>;

	public partial class ElasticClient
	{

		/// <inheritdoc/>
		public IWarmerResponse GetWarmer(string name, Func<GetWarmerDescriptor, GetWarmerDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.Dispatch<GetWarmerDescriptor, GetWarmerRequestParameters, WarmerResponse>(
				d => selector(d.Name(name).AllIndices()),
				(p, d) => this.LowLevelDispatch.IndicesGetWarmerDispatch<WarmerResponse>(
					p.DeserializationState(new GetWarmerConverter(DeserializeWarmerResponse))
				)
			);
		}

		/// <inheritdoc/>
		public IWarmerResponse GetWarmer(IGetWarmerRequest getWarmerRequest)
		{
			return this.Dispatcher.Dispatch<IGetWarmerRequest, GetWarmerRequestParameters, WarmerResponse>(
				getWarmerRequest,
				(p, d) => this.LowLevelDispatch.IndicesGetWarmerDispatch<WarmerResponse>(
					p.DeserializationState(new GetWarmerConverter(DeserializeWarmerResponse))
				)
			);
		}

		/// <inheritdoc/>
		public Task<IWarmerResponse> GetWarmerAsync(string name, Func<GetWarmerDescriptor, GetWarmerDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.DispatchAsync<GetWarmerDescriptor, GetWarmerRequestParameters, WarmerResponse, IWarmerResponse>(
				d => selector(d.Name(name).AllIndices()),
				(p, d) => this.LowLevelDispatch.IndicesGetWarmerDispatchAsync<WarmerResponse>(
					p.DeserializationState(new GetWarmerConverter(DeserializeWarmerResponse))
				)
			);
		}

		/// <inheritdoc/>
		public Task<IWarmerResponse> GetWarmerAsync(IGetWarmerRequest getWarmerRequest)
		{
			return this.Dispatcher.DispatchAsync<IGetWarmerRequest, GetWarmerRequestParameters, WarmerResponse, IWarmerResponse>(
				getWarmerRequest,
				(p, d) => this.LowLevelDispatch.IndicesGetWarmerDispatchAsync<WarmerResponse>(
					p.DeserializationState(new GetWarmerConverter(DeserializeWarmerResponse))
				)
			);
		}

		/// <inheritdoc/>
		private WarmerResponse DeserializeWarmerResponse(IApiCallDetails apiCallDetails, Stream stream)
		{
			throw new NotImplementedException();
			//if (!apiCallDetails.Success) return new WarmerResponse ();

			//var dict = this.Serializer.Deserialize<CrazyWarmerResponse>(stream);
			//var indices = new Dictionary<string, IWarmers>();
			//foreach (var kv in dict)
			//{
			//	var indexDict = kv.Value;
			//	IWarmers warmers;
			//	if (indexDict == null || !indexDict.TryGetValue("warmers", out warmers) || warmers == null)
			//		continue;
			//	foreach (var kvW in warmers)
			//	{
			//		kvW.Value.Name = kvW.Key;
			//	}
			//	indices.Add(kv.Key, warmers);
			//}

			//return new WarmerResponse { Indices = indices };
		}
	}
}