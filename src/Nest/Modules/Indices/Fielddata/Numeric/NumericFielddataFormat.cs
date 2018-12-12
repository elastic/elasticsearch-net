using System.Runtime.Serialization;


namespace Nest
{

	public enum NumericFielddataFormat
	{
		[EnumMember(Value = "array")]
		Array,

		[EnumMember(Value = "disabled")]
		Disabled
	}
}
