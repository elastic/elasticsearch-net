using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
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
