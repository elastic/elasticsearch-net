using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum SimilarityOption
	{
		[EnumMember(Value = "classic")]
		Classic,
		[EnumMember(Value = "BM25")]
		BM25
	}
}
