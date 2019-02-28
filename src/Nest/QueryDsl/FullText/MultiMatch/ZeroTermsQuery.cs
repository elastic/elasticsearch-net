using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum ZeroTermsQuery
	{
		[EnumMember(Value = "all")]
		All,

		[EnumMember(Value = "none")]
		None
	}
}
