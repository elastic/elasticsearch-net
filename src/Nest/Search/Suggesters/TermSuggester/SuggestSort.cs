using System.Runtime.Serialization;
using Elasticsearch.Net;


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
