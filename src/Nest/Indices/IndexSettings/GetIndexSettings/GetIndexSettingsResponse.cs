using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public interface IGetIndexSettingsResponse : IResponse
	{
		IReadOnlyDictionary<IndexName, IndexState> Indices { get; }
	}

	[JsonFormatter(typeof(ResolvableDictionaryResponseFormatter<GetIndexSettingsResponse, IndexName, IndexState>))]
	public class GetIndexSettingsResponse : DictionaryResponseBase<IndexName, IndexState>, IGetIndexSettingsResponse
	{
		[IgnoreDataMember]
		public IReadOnlyDictionary<IndexName, IndexState> Indices => Self.BackingDictionary;
	}
}
