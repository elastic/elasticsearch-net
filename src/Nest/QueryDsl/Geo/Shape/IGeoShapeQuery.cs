using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof (CompositeJsonConverter<GeoShapeQueryJsonConverter, FieldNameQueryJsonConverter<GeoShapeCircleQuery>>))]
	public interface IGeoShapeQuery : IFieldNameQuery
	{
	}
}