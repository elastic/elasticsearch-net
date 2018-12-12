using System.Runtime.Serialization;


namespace Nest
{

	public enum SuggestSort
	{
		[EnumMember(Value = "score")]
		Score,

		[EnumMember(Value = "frequency")]
		Frequency
	}
}
