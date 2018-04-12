using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum PhoneticLanguage
	{
		[EnumMember(Value = "any")]
		Any,

		[EnumMember(Value = "comomon")]
		Comomon,

		[EnumMember(Value = "cyrillic")]
		Cyrillic,

		[EnumMember(Value = "english")]
		English,

		[EnumMember(Value = "french")]
		French,

		[EnumMember(Value = "german")]
		German,

		[EnumMember(Value = "hebrew")]
		Hebrew,

		[EnumMember(Value = "hungarian")]
		Hungarian,

		[EnumMember(Value = "polish")]
		Polish,

		[EnumMember(Value = "romanian")]
		Romanian,

		[EnumMember(Value = "russian")]
		Russian,

		[EnumMember(Value = "spanish")]
		Spanish,
	}
}