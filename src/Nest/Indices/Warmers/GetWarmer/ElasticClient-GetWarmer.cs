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

	public partial interface IElasticClient
	{
		/// <summary>
		/// Getting a warmer for specific index (or alias, or several indices) based on its name. 
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-warmers.html#warmer-retrieving
		/// </summary>
		/// <param name="name">The name of the warmer to get</param>
		/// <param name="selector">An optional selector specifying additional parameters for the get warmer operation</param>
		IWarmerResponse GetWarmer(string name, Func<GetWarmerDescriptor, IGetWarmerRequest> selector = null);

		/// <inheritdoc/>
		IWarmerResponse GetWarmer(IGetWarmerRequest getWarmerRequest);

		/// <inheritdoc/>
		Task<IWarmerResponse> GetWarmerAsync(string name, Func<GetWarmerDescriptor, IGetWarmerRequest> selector = null);

		/// <inheritdoc/>
		Task<IWarmerResponse> GetWarmerAsync(IGetWarmerRequest getWarmerRequest);

	}

	public partial class ElasticClient
	{
		//TODO AllIndices seems very weird here

		/// <inheritdoc/>
		public IWarmerResponse GetWarmer(string name, Func<GetWarmerDescriptor, IGetWarmerRequest> selector = null) => 
			this.Dispatcher.Dispatch<IGetWarmerRequest, GetWarmerRequestParameters, WarmerResponse>(
				selector.InvokeOrDefault(new GetWarmerDescriptor().Name(name).AllIndices()),
				new GetWarmerConverter(DeserializeWarmerResponse),
				(p, d) => this.LowLevelDispatch.IndicesGetWarmerDispatch<WarmerResponse>(p)
			);

		/// <inheritdoc/>
		public IWarmerResponse GetWarmer(IGetWarmerRequest getWarmerRequest) => 
			this.Dispatcher.Dispatch<IGetWarmerRequest, GetWarmerRequestParameters, WarmerResponse>(
				getWarmerRequest,
				new GetWarmerConverter(DeserializeWarmerResponse),
				(p, d) => this.LowLevelDispatch.IndicesGetWarmerDispatch<WarmerResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IWarmerResponse> GetWarmerAsync(string name, Func<GetWarmerDescriptor, IGetWarmerRequest> selector = null) => 
			this.Dispatcher.DispatchAsync<IGetWarmerRequest, GetWarmerRequestParameters, WarmerResponse, IWarmerResponse>(
				selector.InvokeOrDefault(new GetWarmerDescriptor().Name(name).AllIndices()),
				new GetWarmerConverter(DeserializeWarmerResponse),
				(p, d) => this.LowLevelDispatch.IndicesGetWarmerDispatchAsync<WarmerResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IWarmerResponse> GetWarmerAsync(IGetWarmerRequest getWarmerRequest) => 
			this.Dispatcher.DispatchAsync<IGetWarmerRequest, GetWarmerRequestParameters, WarmerResponse, IWarmerResponse>(
				getWarmerRequest,
				new GetWarmerConverter(DeserializeWarmerResponse),
				(p, d) => this.LowLevelDispatch.IndicesGetWarmerDispatchAsync<WarmerResponse>(p)
			);

		/// <inheritdoc/>
		private WarmerResponse DeserializeWarmerResponse(IApiCallDetails apiCallDetails, Stream stream)
		{
			//TODO Broke this rething this, DictionaryResponse?
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