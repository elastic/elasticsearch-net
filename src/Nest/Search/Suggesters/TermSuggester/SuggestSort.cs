using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

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
