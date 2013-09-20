using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
	public interface IWarmerResponse : IResponse
	{
		Dictionary<string, Dictionary<string, WarmerMapping>> Indices { get; }
	}

	[JsonObject]
	[JsonConverter(typeof(WarmerResponseConverter))]
	public class WarmerResponse : BaseResponse, IWarmerResponse
	{
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public Dictionary<string, Dictionary<string, WarmerMapping>> Indices { get; internal set; }

	}
}
