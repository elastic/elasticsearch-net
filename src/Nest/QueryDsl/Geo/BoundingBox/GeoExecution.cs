using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum GeoExecution
	{
		[EnumMember(Value = "memory")]
		Memory,

		[EnumMember(Value = "indexed")]
		Indexed
	}
}
