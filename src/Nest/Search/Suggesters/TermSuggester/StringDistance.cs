using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum StringDistance
	{
		[EnumMember(Value = "internal")]
		Internal,

		[EnumMember(Value = "damerau_levenshtein")]
		DamerauLevenshtein,

		/// <summary>
		/// Use for Elasticsearch 6.2.0+
		/// </summary>
		/// <remarks>
		/// Use Levenstein for Elasticsearch < 6.2.0</remarks>
		[EnumMember(Value = "levenshtein")]
		Levenshtein,

		/// <summary>
		/// Use for Elasticsearch < 6.2.0
		/// </summary>
		/// <remarks>Use Levenshtein for Elasticsearch 6.2.0+</remarks>
		[EnumMember(Value = "levenstein")]
		Levenstein,

		/// <summary>
		/// Use for Elasticsearch < 6.2.0
		/// </summary>
		/// <remarks>Use Jaro_winkler for Elasticsearch 6.2.0+</remarks>
		[EnumMember(Value = "jarowinkler")]
		Jarowinkler,

		/// <summary>
		/// Use for Elasticsearch 6.2.0+
		/// </summary>
		/// <remarks>
		/// Use Jarowinkler for Elasticsearch < 6.2.0</remarks>
		[EnumMember(Value = "jaro_winkler")]
		Jaro_winkler,

		[EnumMember(Value = "ngram")]
		Ngram
	}
}
