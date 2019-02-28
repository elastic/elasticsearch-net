using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	/// <summary>
	/// Sorts highlighted fragments
	/// </summary>
	[StringEnum]
	public enum HighlighterOrder
	{
		/// <summary>
		/// Sorts highlighted fragments by score. Only valid for the <see cref="HighlighterType.Unified" /> highlighter
		/// </summary>
		[EnumMember(Value = "score")]
		Score
	}
}
