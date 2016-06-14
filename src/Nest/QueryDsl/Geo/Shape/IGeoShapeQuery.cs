using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof (CompositeJsonConverter<GeoShapeQueryJsonConverter, FieldNameQueryJsonConverter<GeoShapeCircleQuery>>))]
	public interface IGeoShapeQuery : IFieldNameQuery
	{
		/// <summary>
		/// Will ignore an unmapped field and will not match any documents for this query.
		/// This can be useful when querying multiple indexes which might have different mappings.
		/// </summary>
		[JsonProperty("ignore_unmapped")]
		bool? IgnoreUnmapped { get; set; }
	}

	public abstract class GeoShapeQueryBase : FieldNameQueryBase, IGeoShapeQuery
	{
		/// <summary>
		/// Will ignore an unmapped field and will not match any documents for this query.
		/// This can be useful when querying multiple indexes which might have different mappings.
		/// </summary>
		public bool? IgnoreUnmapped { get; set; }
	}

	public abstract class GeoShapeQueryDescriptorBase<TDescriptor, TInterface, T>
		: FieldNameQueryDescriptorBase<TDescriptor, TInterface, T>, IGeoShapeQuery
		where TDescriptor : GeoShapeQueryDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, IGeoShapeQuery
		where T : class
	{
		bool? IGeoShapeQuery.IgnoreUnmapped { get; set; }

		/// <summary>
		/// Will ignore an unmapped field and will not match any documents for this query.
		/// This can be useful when querying multiple indexes which might have different mappings.
		/// </summary>
		public TDescriptor IgnoreUnmapped(bool ignoreUnmapped = false) => Assign(a => a.IgnoreUnmapped = ignoreUnmapped);
	}
}
