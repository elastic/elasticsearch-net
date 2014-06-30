using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TermsExecution
	{
		[EnumMember(Value = "plain")]
		Plain,
		[EnumMember(Value = "bool")]
		Bool,
		[EnumMember(Value = "and")]
		And,
		[EnumMember(Value = "or")]
		Or,
		[EnumMember(Value = "fielddata")]
		FieldData
	}
}
