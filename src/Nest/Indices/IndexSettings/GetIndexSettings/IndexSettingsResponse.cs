using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface IIndexSettingsResponse : IResponse
	{
		IDictionary<IndexName, IIndexState> Indices { get; }
	}

	[JsonConverter(typeof(DictionaryResponseJsonConverter<IndexSettingsResponse, IndexName, IIndexState>))]
	public class IndexSettingsResponse : DictionaryResponse<IndexName, IIndexState>, IIndexSettingsResponse
	{
		[JsonIgnore]
		public IDictionary<IndexName, IIndexState> Indices { get { return Self.BackingDictionary; } }

	}
}
