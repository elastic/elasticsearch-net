using System.Runtime.Serialization;

namespace Nest
{
	public enum PhoneticNameType
	{
		[EnumMember(Value = "generic")]
		Generic,

		[EnumMember(Value = "ashkenazi")]
		Ashkenazi,

		[EnumMember(Value = "sephardic")]
		Sephardic
	}
}
