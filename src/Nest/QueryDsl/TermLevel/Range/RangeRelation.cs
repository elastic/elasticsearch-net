using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum RangeRelation
	{
		[EnumMember(Value = "within")] Within,
		[EnumMember(Value = "contains")] Contains,
		[EnumMember(Value = "intersects")] Intersects
	}
}
