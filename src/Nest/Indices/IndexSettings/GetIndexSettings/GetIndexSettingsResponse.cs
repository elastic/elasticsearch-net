using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetIndexSettingsResponse : IResponse
	{
		IDictionary<string, IIndexState> Indices { get; }
	}

	[JsonConverter(typeof(DictionaryResponseJsonConverter<GetIndexSettingsResponse, string, IIndexState>))]
	public class GetIndexSettingsResponse : DictionaryResponse<string, IIndexState>, IGetIndexSettingsResponse
	{
		[JsonIgnore]
		public IDictionary<string, IIndexState> Indices => Self.BackingDictionary;
	}
}
