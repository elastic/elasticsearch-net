using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	/// <summary>
	/// Sorts highlighted fragments
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum HighlighterOrder
	{
		/// <summary>
		/// Sorts highlighted fragments by score. Only valid for the <see cref="HighlighterType.Unified"/> highligher
		/// </remarks>
		[EnumMember(Value = "score")]
		Score
	}
}
