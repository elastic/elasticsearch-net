using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public interface IGetIndexResponse : IResponse
	{
		IReadOnlyDictionary<string, IndexState> Indices { get; }
	}

	[JsonConverter(typeof(DictionaryResponseJsonConverter<GetIndexResponse, string, IndexState>))]
	public class GetIndexResponse : DictionaryResponseBase<string, IndexState>, IGetIndexResponse
	{
		public IReadOnlyDictionary<string, IndexState> Indices => Self.BackingDictionary;
	}
}
