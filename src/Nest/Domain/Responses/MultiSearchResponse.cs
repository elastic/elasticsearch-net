using Elasticsearch.Net;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	[JsonObject]
	public class MultiSearchResponse : BaseResponse, IMultiSearchResponse
	{
		public MultiSearchResponse()
		{
			this._Responses = new Dictionary<string, object>();
		}

		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]	
		internal IDictionary<string, object> _Responses { get; set; }

		public int TotalResponses { get { return this._Responses.HasAny() ? this._Responses.Count() : 0 ; } }

		public IEnumerable<SearchResponse<T>> GetResponses<T>() where T : class
		{
			foreach (var r in this._Responses.Values.OfType<IResponseWithRequestInformation>())
				r.RequestInformation = this.ConnectionStatus;
			return this._Responses.Values.OfType<SearchResponse<T>>();
		}
		public SearchResponse<T> GetResponse<T>(string name) where T : class
		{
			object response = null;
			this._Responses.TryGetValue(name, out response);
			var r = response as IResponseWithRequestInformation;
			if (r != null)
				r.RequestInformation = this.ConnectionStatus;
			return response as SearchResponse<T>;
		}
	}
}
