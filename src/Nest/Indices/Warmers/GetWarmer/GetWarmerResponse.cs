using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(GetWarmerResponseConverter))]
	public interface IGetWarmerResponse : IResponse
	{
		Dictionary<string, Warmers> Indices { get; }
	}

	[JsonObject]
	public class GetWarmerResponse : ResponseBase, IGetWarmerResponse
	{
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		public Dictionary<string, Warmers> Indices { get; internal set; }
	}
}
