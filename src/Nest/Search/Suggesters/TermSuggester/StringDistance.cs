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

		[EnumMember(Value = "levenstein")]
		Levenstein,

		[EnumMember(Value = "jarowinkler")]
		Jarowinkler,

		[EnumMember(Value = "ngram")]
		Ngram
	}
}
