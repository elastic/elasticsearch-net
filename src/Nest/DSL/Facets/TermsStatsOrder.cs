using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TermsStatsOrder
	{
		[EnumMember(Value = "term")]
		Term = 0,
		[EnumMember(Value = "reverse_term")]
		ReverseTerm,
		[EnumMember(Value = "count")]
		Count,
		[EnumMember(Value = "reverse_count")]
		ReverseCount,
		[EnumMember(Value = "total")]
		Total,
		[EnumMember(Value = "reverse_total")]
		ReverseTotal,
		[EnumMember(Value = "min")]
		Min,
		[EnumMember(Value = "reverse_min")]
		ReverseMin,
		[EnumMember(Value = "max")]
		Max,
		[EnumMember(Value = "reverse_max")]
		ReverseMax,
		[EnumMember(Value = "mean")]
		Mean,
		[EnumMember(Value = "reverse_mean")]
		ReverseMean
	}
}
