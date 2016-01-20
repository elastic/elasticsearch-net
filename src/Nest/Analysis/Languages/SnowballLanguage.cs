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
		[EnumMember(Value="Armenian")]
		Armenian,
		[EnumMember(Value="Basque")]
		Basque,
		[EnumMember(Value="Catalan")]
		Catalan,
		[EnumMember(Value="Danish")]
		Danish,
		[EnumMember(Value="Dutch")]
		Dutch,
		[EnumMember(Value="English")]
		English,
		[EnumMember(Value="Finnish")]
		Finnish,
		[EnumMember(Value="French")]
		French,
		[EnumMember(Value="Ferman")]
		German,
		[EnumMember(Value="German2")]
		German2,
		[EnumMember(Value="Hungarian")]
		Hungarian,
		[EnumMember(Value="Italian")]
		Italian,
		[EnumMember(Value="Kp")]
		Kp,
		[EnumMember(Value="Lovins")]
		Lovins,
		[EnumMember(Value="Norwegian")]
		Norwegian,
		[EnumMember(Value="Porter")]
		Porter,
		[EnumMember(Value="Portuguese")]
		Portuguese,
		[EnumMember(Value="Romanian")]
		Romanian,
		[EnumMember(Value="Russian")]
		Russian,
		[EnumMember(Value="Spanish")]
		Spanish,
		[EnumMember(Value="Swedish")]
		Swedish,
		[EnumMember(Value="Turkish")]
		Turkish
	}
}