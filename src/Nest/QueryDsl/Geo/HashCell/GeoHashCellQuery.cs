using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof (VariableFieldNameQueryJsonConverter<GeoHashCellQuery, IGeoHashCellQuery>))]
	public interface IGeoHashCellQuery : IFieldNameQuery
	{
		[VariableField]
		GeoLocation Location { get; set; }

		[JsonProperty("precision")]
		Union<int,Distance> Precision { get; set; }

		[JsonProperty("neighbors")]
		bool? Neighbors { get; set; }
	}

	public class GeoHashCellQuery : FieldNameQueryBase, IGeoHashCellQuery
	{
		protected override bool Conditionless => IsConditionless(this);

		public GeoLocation Location { get; set; }
		public Union<int, Distance> Precision { get; set; }
		public bool? Neighbors { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.GeoHashCell = this;

		internal static bool IsConditionless(IGeoHashCellQuery q) => q.Location == null || q.Field == null;
	}

	public class GeoHashCellQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<GeoHashCellQueryDescriptor<T>, IGeoHashCellQuery, T>
		, IGeoHashCellQuery where T : class
	{
		protected override bool Conditionless => GeoHashCellQuery.IsConditionless(this);

		GeoLocation IGeoHashCellQuery.Location { get; set; }
		Union<int, Distance> IGeoHashCellQuery.Precision { get; set; }
		bool? IGeoHashCellQuery.Neighbors { get; set; }

		public GeoHashCellQueryDescriptor<T> Location(GeoLocation location) => Assign(a => a.Location = location);
		public GeoHashCellQueryDescriptor<T> Location(double lat, double lon) => Assign(a => a.Location = new GeoLocation(lat, lon));

		public GeoHashCellQueryDescriptor<T> Precision(Distance precision) => Assign(a => a.Precision = precision);
		public GeoHashCellQueryDescriptor<T> Precision(int precision) => Assign(a => a.Precision = precision);

		public GeoHashCellQueryDescriptor<T> Neighbors(bool? neighbors = true) => Assign(a => a.Neighbors = neighbors);
	}
}