using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using Nest.Resolvers.Converters;

namespace Nest
{
	[JsonObject]
	public class MultiSearchResponse : BaseResponse
	{
		public MultiSearchResponse()
		{
			this._Responses = new Dictionary<string, object>();
		}
		
		internal IDictionary<string, object> _Responses { get; set; }

		public IEnumerable<QueryResponse<T>> GetResponses<T>() where T : class
		{
			return this._Responses.Values.OfType<QueryResponse<T>>();
		}
		public QueryResponse<T> GetResponse<T>(string name) where T : class
		{
			object response = null;
			this._Responses.TryGetValue(name, out response);
			return response as QueryResponse<T>;
		}
	}
}
