using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetIndexSettingsResponse : IResponse
	{
		IReadOnlyDictionary<string, IIndexState> Indices { get; }
	}

	[JsonConverter(typeof(DictionaryResponseJsonConverter<GetIndexSettingsResponse, string, IIndexState>))]
	public class GetIndexSettingsResponse : DictionaryResponseBase<string, IIndexState>, IGetIndexSettingsResponse
	{
		[JsonIgnore]
		public IReadOnlyDictionary<string, IIndexState> Indices => Self.BackingDictionary;
	}
}
