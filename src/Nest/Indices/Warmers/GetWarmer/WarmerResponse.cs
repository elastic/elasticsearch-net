using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
	public interface IWarmerResponse : IResponse
	{
		Dictionary<IndexName, IWarmers> Indices { get; }
	}

	[JsonObject]
	public class WarmerResponse : BaseResponse, IWarmerResponse
	{
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		public Dictionary<IndexName, IWarmers> Indices { get; internal set; }

	}
}
