using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetIndexSettingsResponse : IResponse
	{
		IDictionary<IndexName, IIndexState> Indices { get; }
	}

	[JsonConverter(typeof(DictionaryResponseJsonConverter<GetIndexSettingsResponse, IndexName, IIndexState>))]
	public class GetIndexSettingsResponse : DictionaryResponse<IndexName, IIndexState>, IGetIndexSettingsResponse
	{
		[JsonIgnore]
		public IDictionary<IndexName, IIndexState> Indices { get { return Self.BackingDictionary; } }

	}
}
