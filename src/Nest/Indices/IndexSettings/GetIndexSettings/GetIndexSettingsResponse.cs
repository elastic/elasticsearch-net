using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetIndexSettingsResponse : IResponse
	{
		IDictionary<string, IIndexState> Indices { get; }
	}

	[JsonConverter(typeof(DictionaryResponseJsonConverter<GetIndexSettingsResponse, string, IIndexState>))]
	public class GetIndexSettingsResponse : DictionaryResponseBase<string, IIndexState>, IGetIndexSettingsResponse
	{
		[JsonIgnore]
		public IDictionary<string, IIndexState> Indices => Self.BackingDictionary;
	}
}
