using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum PhoneticEncoder
	{
		[EnumMember(Value = "metaphone")]
		Metaphone,
		[EnumMember(Value = "doublemetaphone")]
		DoubleMetaphone,
		[EnumMember(Value = "soundex")]
		Soundex,
		[EnumMember(Value = "refinedsoundex")]
		RefinedSoundex,
		[EnumMember(Value = "caverphone1")]
		Caverphone1,
		[EnumMember(Value = "caverphone2")]
		Caverphone2,
		[EnumMember(Value = "cologne")]
		Cologne,
		[EnumMember(Value = "nysiis")]
		Nysiis,
		[EnumMember(Value = "koelnerphonetik")]
		KoelnerPhonetik,
		[EnumMember(Value = "haasephonetik")]
		HaasePhonetik,
		[EnumMember(Value = "beidermorse")]
		Beidermorse
	}
}