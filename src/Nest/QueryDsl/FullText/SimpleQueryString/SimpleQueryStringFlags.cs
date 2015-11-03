using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	//TODO make a dedicated string enum flags converter 
	[JsonConverter(typeof(StringEnumConverter))]
	[Flags]
	public enum SimpleQueryStringFlags
	{
		[EnumMember(Value = "ALL")]
		All,
		[EnumMember(Value = "NONE")]
		None,
		[EnumMember(Value = "AND")]
		And,
		[EnumMember(Value = "OR")]
		Or,
		[EnumMember(Value = "NOT")]
		Not,
		[EnumMember(Value = "PREFIX")]
		Prefix,
		[EnumMember(Value = "PHRASE")]
		Phrase,
		[EnumMember(Value = "PRECEDENCE")]
		Precedence,
		[EnumMember(Value = "ESCAPE")]
		Escape,
		[EnumMember(Value = "WHITESPACE")]
		Whitespace,
		[EnumMember(Value = "FUZZY")]
		Fuzzy,
		[EnumMember(Value = "NEAR")]
		Near,
		[EnumMember(Value = "SLOP")]
		Slop,
	}
}