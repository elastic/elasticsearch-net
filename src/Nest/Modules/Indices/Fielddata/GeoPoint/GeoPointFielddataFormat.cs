using System.Runtime.Serialization;

namespace Nest
{
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
