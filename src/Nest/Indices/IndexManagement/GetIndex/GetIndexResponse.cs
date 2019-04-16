using System.Collections.Generic;
using Elasticsearch.Net;

namespace Nest
{
	public interface IGetIndexResponse : IResponse
	{
		IReadOnlyDictionary<IndexName, IndexState> Indices { get; }
	}

	[JsonFormatter(typeof(ResolvableDictionaryResponseFormatter<GetIndexResponse, IndexName, IndexState>))]
	public class GetIndexResponse : DictionaryResponseBase<IndexName, IndexState>, IGetIndexResponse
	{
		public IReadOnlyDictionary<IndexName, IndexState> Indices => Self.BackingDictionary;
	}
}
