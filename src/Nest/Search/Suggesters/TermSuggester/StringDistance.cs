using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum StringDistance
	{
		[EnumMember(Value = "internal")]
		Internal,
		[EnumMember(Value = "damerau_levenshtein")]
		DamerauLevenshtein,
		[EnumMember(Value = "levenshtein")]
		Levenshtein,
		[Obsolete("Use Levenshtein - this value is incorrectly spelt.")]
		[EnumMember(Value = "levenshtein")]
		Levenstein,
		[EnumMember(Value = "jarowinkler")]
		Jarowinkler,
		[EnumMember(Value = "ngram")]
		Ngram
	}
}
