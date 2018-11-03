using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(CompositeJsonConverter<GeoShapeQueryJsonConverter, GeoShapeQueryFieldNameConverter>))]
	public interface IGeoShapeQuery : IFieldNameQuery
	{
		/// <summary>
		/// Will ignore an unmapped field and will not match any documents for this query.
		/// This can be useful when querying multiple indexes which might have different mappings.
		/// </summary>
		[JsonProperty("ignore_unmapped")]
		bool? IgnoreUnmapped { get; set; }

		/// <summary>
		/// Controls the spatial relation operator to use at search time.
		/// </summary>
		[JsonProperty("relation")]
		GeoShapeRelation? Relation { get; set; }
	}

	public abstract class GeoShapeQueryBase : FieldNameQueryBase, IGeoShapeQuery
	{
		/// <inheritdoc />
		public bool? IgnoreUnmapped { get; set; }

		/// <inheritdoc />
		public GeoShapeRelation? Relation { get; set; }
	}

	public abstract class GeoShapeQueryDescriptorBase<TDescriptor, TInterface, T>
		: FieldNameQueryDescriptorBase<TDescriptor, TInterface, T>, IGeoShapeQuery
		where TDescriptor : GeoShapeQueryDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, IGeoShapeQuery
		where T : class
	{
		bool? IGeoShapeQuery.IgnoreUnmapped { get; set; }
		GeoShapeRelation? IGeoShapeQuery.Relation { get; set; }

		/// <inheritdoc cref="IGeoShapeQuery.Relation" />
		public TDescriptor Relation(GeoShapeRelation? relation) => Assign(a => a.Relation = relation);

		/// <inheritdoc cref="IGeoShapeQuery.IgnoreUnmapped" />
		public TDescriptor IgnoreUnmapped(bool? ignoreUnmapped = true) => Assign(a => a.IgnoreUnmapped = ignoreUnmapped);
	}
}
