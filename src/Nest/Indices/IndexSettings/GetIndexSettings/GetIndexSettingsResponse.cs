using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetIndexSettingsResponse : IResponse
	{
		IReadOnlyDictionary<IndexName, IndexState> Indices { get; }
	}

	[JsonConverter(typeof(ResolvableDictionaryResponseJsonConverter<GetIndexSettingsResponse, IndexName, IndexState>))]
	public class GetIndexSettingsResponse : DictionaryResponseBase<IndexName, IndexState>, IGetIndexSettingsResponse
	{
		[JsonIgnore]
		public IReadOnlyDictionary<IndexName, IndexState> Indices => Self.BackingDictionary;
	}
}
