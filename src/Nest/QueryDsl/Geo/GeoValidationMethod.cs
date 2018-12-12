using System.Runtime.Serialization;


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
