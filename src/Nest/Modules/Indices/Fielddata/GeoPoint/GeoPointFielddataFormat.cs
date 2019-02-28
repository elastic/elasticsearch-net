using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum GeoPointFielddataFormat
	{
		[EnumMember(Value = "array")]
		Array,

		[EnumMember(Value = "doc_values")]
		DocValues,

		[EnumMember(Value = "compressed")]
		Compressed,

		[EnumMember(Value = "disabled")]
		Disabled
	}
}
