using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum NumericFielddataFormat
	{
		[EnumMember(Value = "array")]
		Array,

		[EnumMember(Value = "disabled")]
		Disabled
	}
}
