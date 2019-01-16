using System.Runtime.Serialization;


namespace Nest
{
	[StringEnum]
	public enum SuggestSort
	{
		[EnumMember(Value = "score")]
		Score,

		[EnumMember(Value = "frequency")]
		Frequency
	}
}
