using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{

	public enum InputType
	{
		[EnumMember(Value = "http")]
		Http,

		[EnumMember(Value = "search")]
		Search,

		[EnumMember(Value = "simple")]
		Simple
	}
}
