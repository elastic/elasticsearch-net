// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Elastic.Transport;
using Nest.Utf8Json;

namespace Nest
{
	[DataContract]
	[JsonFormatter(typeof(MultiSearchResponseFormatter))]
	public class MultiSearchResponse : ResponseBase
	{
		public MultiSearchResponse() => Responses = new Dictionary<string, IResponse>();

		public long Took { get; set; }

		public IEnumerable<IResponse> AllResponses => _allResponses<IResponse>();

		public override bool IsValid => base.IsValid && AllResponses.All(b => b.IsValid);

		public int TotalResponses => Responses.HasAny() ? Responses.Count() : 0;

		[JsonFormatter(typeof(VerbatimDictionaryInterfaceKeysFormatter<string, IResponse>))]
		internal IDictionary<string, IResponse> Responses { get; set; }

		public IEnumerable<IResponse> GetInvalidResponses() => _allResponses<IResponse>().Where(r => !r.IsValid);

		public ISearchResponse<T> GetResponse<T>(string name) where T : class
		{
			if (!Responses.TryGetValue(name, out var response))
				return null;

			if (response is ITransportResponse elasticSearchResponse)
				elasticSearchResponse.ApiCall = ApiCall;

			return response as ISearchResponse<T>;
		}

		public IEnumerable<ISearchResponse<T>> GetResponses<T>() where T : class => _allResponses<SearchResponse<T>>();

		protected override void DebugIsValid(StringBuilder sb)
		{
			sb.AppendLine($"# Invalid searches (inspect individual response.DebugInformation for more detail):");
			foreach (var i in AllResponses.Select((item, i) => new { item, i }).Where(i => !i.item.IsValid))
				sb.AppendLine($"  search[{i.i}]: {i.item}");
		}

		private IEnumerable<T> _allResponses<T>() where T : class, IResponse, ITransportResponse
		{
			foreach (var r in Responses.Values.OfType<T>())
			{
				r.ApiCall = ApiCall;
				yield return r;
			}
		}
	}
}
