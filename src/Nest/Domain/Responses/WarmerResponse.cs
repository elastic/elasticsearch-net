using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	public interface IWarmerResponse : IResponse
	{
		string Name { get; }
		SearchDescriptor<dynamic> Search { get; }
	}

	[JsonObject]
	[JsonConverter(typeof(WarmerResponseConverter))]
	public class WarmerResponse : BaseResponse, IWarmerResponse
	{
		public string Name { get; internal set; }

		public SearchDescriptor<dynamic> Search { get; internal set; }
	}
}
