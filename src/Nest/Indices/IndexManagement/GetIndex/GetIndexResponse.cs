using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetIndexResponse : IResponse
	{
		IReadOnlyDictionary<IndexName, IndexState> Indices { get; }
	}

	[JsonConverter(typeof(ResolvableDictionaryResponseJsonConverter<GetIndexResponse, IndexName, IndexState>))]
	public class GetIndexResponse : DictionaryResponseBase<IndexName, IndexState>, IGetIndexResponse
	{
		public IReadOnlyDictionary<IndexName, IndexState> Indices => Self.BackingDictionary;
	}
}
