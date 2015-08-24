using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	/// <summary>
	/// Snowball compatible languages 
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum SnowballLanguage
	{
		[EnumMember(Value="armenian")]
		Armenian,
		[EnumMember(Value="basque")]
		Basque,
		[EnumMember(Value="catalan")]
		Catalan,
		[EnumMember(Value="danish")]
		Danish,
		[EnumMember(Value="dutch")]
		Dutch,
		[EnumMember(Value="english")]
		English,
		[EnumMember(Value="finnish")]
		Finnish,
		[EnumMember(Value="french")]
		French,
		[EnumMember(Value="german")]
		German,
		[EnumMember(Value="german2")]
		German2,
		[EnumMember(Value="hungarian")]
		Hungarian,
		[EnumMember(Value="italian")]
		Italian,
		[EnumMember(Value="kp")]
		Kp,
		[EnumMember(Value="lovins")]
		Lovins,
		[EnumMember(Value="norwegian")]
		Norwegian,
		[EnumMember(Value="porter")]
		Porter,
		[EnumMember(Value="portuguese")]
		Portuguese,
		[EnumMember(Value="romanian")]
		Romanian,
		[EnumMember(Value="russian")]
		Russian,
		[EnumMember(Value="spanish")]
		Spanish,
		[EnumMember(Value="swedish")]
		Swedish,
		[EnumMember(Value="turkish")]
		Turkish
	}
}