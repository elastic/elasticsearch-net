using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Marker class that signals to the CustomJsonConverter to write the string verbatim
	/// </summary>
	internal class RawJson
	{
		public string Data { get; set; }

		public RawJson(string rawJsonData)
		{
			Data = rawJsonData;
		}
	}
	
	[JsonConverter(typeof(CustomJsonConverter))]
	internal class RawJsonWrapper<T> 
	{
		
	}
}
