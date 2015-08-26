using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
	public interface IWarmerResponse : IResponse
	{
		Dictionary<string, IWarmers> Indices { get; }
	}

	[JsonObject]
	public class WarmerResponse : BaseResponse, IWarmerResponse
	{
		[JsonConverter(typeof(DictionaryKeysAreNotFieldNamesJsonConverter))]
		public Dictionary<string, IWarmers> Indices { get; internal set; }

	}
}
