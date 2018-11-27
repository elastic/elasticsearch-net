using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{

	public enum SimilarityOption
	{
		[EnumMember(Value = "classic")]
		Classic,

		[EnumMember(Value = "BM25")]
		BM25
	}
}
