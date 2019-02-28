using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum Operator
	{
		[EnumMember(Value = "and")]
		And,

		[EnumMember(Value = "or")]
		Or
	}
}
