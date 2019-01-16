using System.Runtime.Serialization;


namespace Nest
{
	/// TODO: I think the places that take this currently should just be a string
	[StringEnum]
	public enum SimilarityOption
	{
		[EnumMember(Value = "BM25")]
		BM25
	}
}
