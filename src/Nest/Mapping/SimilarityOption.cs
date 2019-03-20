using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	/// TODO: I think the places that take this currently should just be a string
	[JsonConverter(typeof(StringEnumConverter))]
	public enum SimilarityOption
	{
		[EnumMember(Value = "BM25")]
		BM25
	}
}
