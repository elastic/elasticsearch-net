using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

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
