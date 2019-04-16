using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[StringEnum]
	public enum EdgeNGramSide
	{
		[EnumMember(Value = "front")]
		Front,

		[EnumMember(Value = "back")]
		Back,
	}
}
