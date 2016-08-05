using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(CompositeJsonConverter<GeoShapeQueryJsonConverter, FieldNameQueryJsonConverter<GeoShapeCircleQuery>>))]
	public interface IGeoShapeQuery : IFieldNameQuery
	{
		[JsonProperty("relation")]
		GeoShapeRelation? Relation { get; set; }
	}

	public abstract class GeoShapeQueryBase : FieldNameQueryBase, IGeoShapeQuery
	{
		public GeoShapeRelation? Relation { get; set; }
	}

	public abstract class GeoShapeQueryDescriptorBase<TDescriptor, TInterface, T>
		: FieldNameQueryDescriptorBase<TDescriptor, TInterface, T>, IGeoShapeQuery
		where TDescriptor : FieldNameQueryDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, IGeoShapeQuery
		where T : class
	{
		GeoShapeRelation? IGeoShapeQuery.Relation { get; set; }

		public TDescriptor Relation(GeoShapeRelation relation) => Assign(a => a.Relation = relation);
	}
}
