using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[ContractJsonConverter(typeof(MultiSearchResponseJsonConverter))]
	public class MultiSearchResponse : ResponseBase, IMultiSearchResponse
	{
		public MultiSearchResponse()
		{
			this.Responses = new Dictionary<string, object>();
		}

		public override bool IsValid => base.IsValid && this.AllResponses.All(b => b.IsValid);

		protected override void DebugIsValid(StringBuilder sb)
		{
			sb.AppendLine($"# Invalid searches (inspect individual response.DebugInformation for more detail):");
			foreach(var i in AllResponses.Select((item, i) => new { item, i}).Where(i=>!i.item.IsValid))
				sb.AppendLine($"  search[{i.i}]: {i.item}");
		}

		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, object>))]
		internal IDictionary<string, object> Responses { get; set; }

		public int TotalResponses => this.Responses.HasAny() ? this.Responses.Count() : 0;

		private IEnumerable<T> _allResponses<T>() where T : class, IResponse, IElasticsearchResponse
		{
			foreach (var r in this.Responses.Values.OfType<T>())
			{
				((IElasticsearchResponse)r).ApiCall = this.ApiCall;
				yield return r;
			}
		}

		public IEnumerable<IResponse> AllResponses => this._allResponses<IResponse>();

		public IEnumerable<IResponse> GetInvalidResponses() => this._allResponses<IResponse>().Where(r => !r.IsValid);

		public IEnumerable<ISearchResponse<T>> GetResponses<T>() where T : class => this._allResponses<SearchResponse<T>>();

		public ISearchResponse<T> GetResponse<T>(string name) where T : class
		{
			object response;
			this.Responses.TryGetValue(name, out response);
			var r = response as IElasticsearchResponse;
			if (r != null)
				r.ApiCall = this.ApiCall;
			return response as SearchResponse<T>;
		}
	}
}
