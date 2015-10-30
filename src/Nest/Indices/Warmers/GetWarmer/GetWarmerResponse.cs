using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
	[JsonConverter(typeof(GetWarmerResponseConverter))]
	public interface IGetWarmerResponse : IResponse
	{
		Dictionary<string, Warmers> Indices { get; }
	}

	[JsonObject]
	public class GetWarmerResponse : BaseResponse, IGetWarmerResponse
	{
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		public Dictionary<string, Warmers> Indices { get; internal set; }
	}
}
