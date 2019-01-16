using System.Runtime.Serialization;


namespace Nest
{
	/// <summary>
	/// Sorts highlighted fragments
	/// </summary>
	[StringEnum]
	public enum HighlighterOrder
	{
		/// <summary>
		/// Sorts highlighted fragments by score. Only valid for the <see cref="HighlighterType.Unified" /> highligher
		/// </remarks>
		[EnumMember(Value = "score")]
		Score
	}
}
