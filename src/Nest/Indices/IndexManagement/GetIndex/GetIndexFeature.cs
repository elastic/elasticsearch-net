using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	// TODO remove and let code generator create this once
	// https://github.com/elastic/elasticsearch/pull/13946 is merged
	[Flags]
	[JsonConverter(typeof(StringEnumConverter))]
	public enum GetIndexFeature
	{
		All = Settings | Mappings | Warmers | Aliases,
		[EnumMember(Value = "_settings")]
		Settings = 1,
		[EnumMember(Value = "_mappings")]
		Mappings = 2,
		[EnumMember(Value = "_warmers")]
		Warmers = 4,
		[EnumMember(Value = "_aliases")]
		Aliases = 8
	}
}