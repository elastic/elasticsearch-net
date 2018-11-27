using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{

	public enum GeoValidationMethod
	{
		[EnumMember(Value = "coerce")]
		Coerce,

		[EnumMember(Value = "ignore_malformed")]
		IgnoreMalformed,

		[EnumMember(Value = "strict")]
		Strict
	}
}
