using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{
	/// TODO: I think the places that take this currently should just be a string
	public enum SimilarityOption
	{
		[EnumMember(Value = "BM25")]
		BM25
	}
}
