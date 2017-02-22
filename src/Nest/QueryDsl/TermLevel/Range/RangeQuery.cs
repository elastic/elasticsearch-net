using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof (RangeQueryJsonConverter))]
	public interface IRangeQuery : IFieldNameQuery { }
}
