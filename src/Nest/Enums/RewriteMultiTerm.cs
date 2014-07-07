using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Nest
{
	[Flags]
	[JsonConverter(typeof(StringEnumConverter))]
	public enum RewriteMultiTerm
	{
		[EnumMember(Value = "constant_score_default")]
		ConstantScoreDefault,
		[EnumMember(Value = "scoring_boolean")]
		ScoringBoolean,
		[EnumMember(Value = "constant_score_boolean")]
		ConstantScoreBoolean,
		[EnumMember(Value = "constant_score_filter")]
		ConstantScoreFilter,
		[EnumMember(Value = "top_terms_N")]
		TopTermsN,
		[EnumMember(Value = "top_terms_boost_N")]
		TopTermsBoostN
	}
}
