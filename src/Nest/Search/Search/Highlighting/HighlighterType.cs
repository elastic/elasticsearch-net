using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	/// <summary>
	/// Type of highlighter
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum HighlighterType
	{
		/// <summary>
		/// Plain Highlighter.
		/// The default choice of highlighter is of type plain and uses the Lucene highlighter. 
		/// It tries hard to reflect the query matching logic in terms of understanding word 
		/// importance and any word positioning criteria in phrase queries.
		/// </summary>
		[EnumMember(Value = "plain")]
		Plain,

		/// <summary>
		/// Postings Highlighter.
		/// If index_options is set to offsets in the mapping the postings highlighter 
		/// will be used instead of the plain highlighter
		/// </summary>
		[EnumMember(Value = "postings")]
		Postings,

		/// <summary>
		/// Fast Vector Highlighter.
		/// If term_vector information is provided by setting term_vector to with_positions_offsets 
		/// in the mapping then the fast vector highlighter will be used instead of the plain highlighter
		/// </summary>
		[EnumMember(Value = "fvh")]
		Fvh
	}
}
