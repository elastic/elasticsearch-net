using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetIndexSettingsResponse : IResponse
	{
		IReadOnlyDictionary<string, IndexState> Indices { get; }
	}

	[JsonConverter(typeof(DictionaryResponseJsonConverter<GetIndexSettingsResponse, string, IndexState>))]
	public class GetIndexSettingsResponse : DictionaryResponseBase<string, IndexState>, IGetIndexSettingsResponse
	{
		[JsonIgnore]
		public IReadOnlyDictionary<string, IndexState> Indices => Self.BackingDictionary;
	}
}
