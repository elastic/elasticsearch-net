using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum ConnectionScheme
	{
		[EnumMember(Value = "http")]
		Http,

		[EnumMember(Value = "https")]
		Https
	}
}
