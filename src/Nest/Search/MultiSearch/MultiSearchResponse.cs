using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
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

		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]	
		internal IDictionary<string, object> _Responses { get; set; }

		public int TotalResponses { get { return this._Responses.HasAny() ? this._Responses.Count() : 0 ; } }

		public IEnumerable<SearchResponse<T>> GetResponses<T>() where T : class
		{
			foreach (var r in this._Responses.Values.OfType<IBodyWithApiCallDetails>())
				r.CallDetails = this.ApiCall;
			return this._Responses.Values.OfType<SearchResponse<T>>();
		}
		public SearchResponse<T> GetResponse<T>(string name) where T : class
		{
			object response;
			this._Responses.TryGetValue(name, out response);
			var r = response as IBodyWithApiCallDetails;
			if (r != null)
				r.CallDetails = this.ApiCall;
			return response as SearchResponse<T>;
		}
	}
}
