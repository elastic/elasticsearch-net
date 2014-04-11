using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Elasticsearch.Net;

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
			return this._Responses.Values.OfType<SearchResponse<T>>();
		}
		public SearchResponse<T> GetResponse<T>(string name) where T : class
		{
			object response = null;
			this._Responses.TryGetValue(name, out response);
			return response as SearchResponse<T>;
		}
	}
}
