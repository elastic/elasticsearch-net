using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{

	public enum StringDistance
	{
		[EnumMember(Value = "internal")]
		Internal,

		[EnumMember(Value = "damerau_levenshtein")]
		DamerauLevenshtein,

		[EnumMember(Value = "levenshtein")]
		Levenshtein,

		[EnumMember(Value = "jaro_winkler")]
		Jarowinkler,

		[EnumMember(Value = "ngram")]
		Ngram
	}
}
