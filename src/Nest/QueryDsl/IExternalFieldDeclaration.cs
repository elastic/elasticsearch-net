using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<ExternalFieldDeclaration>))]
	public interface IExternalFieldDeclaration
	{
		[JsonProperty("index")]
		IndexNameMarker Index { get; set; }
		
		[JsonProperty("type")]
		TypeNameMarker Type { get; set; }
		
		[JsonProperty("id")]
		string Id { get; set; }
		
		[JsonProperty("path")]
		PropertyPathMarker Path { get; set; }
	}
}