using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum SimilarityOption
	{
		[EnumMember(Value = "classic")]
		Classic,

		[EnumMember(Value = "BM25")]
		BM25
	}
}
