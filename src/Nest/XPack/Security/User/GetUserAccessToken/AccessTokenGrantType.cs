using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum AccessTokenGrantType
	{
		[EnumMember(Value = "password")] Password
	}
}
