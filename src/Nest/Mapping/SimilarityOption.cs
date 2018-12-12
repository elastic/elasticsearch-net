using System.Runtime.Serialization;


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
