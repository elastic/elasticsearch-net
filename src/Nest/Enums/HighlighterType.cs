using System.Runtime.Serialization;

namespace Nest
{
	public enum HighlighterType
	{
		[EnumMember(Value = "plain")]
		Plain,
		[EnumMember(Value = "postings")]
		Postings,
		[EnumMember(Value = "fvh")]
		Fvh
	}
}
