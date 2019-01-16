using System.Runtime.Serialization;


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
