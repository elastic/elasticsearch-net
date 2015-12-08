using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof (VariableFieldNameQueryJsonConverter<GeoHashCellQuery, IGeoHashCellQuery>))]
	public interface IGeoHashCellQuery : IFieldNameQuery
	{
		[VariableField]
		GeoLocation Location { get; set; }

		[JsonProperty("precision")]
		GeoDistance Precision { get; set; }

		[JsonProperty("neighbors")]
		bool? Neighbors { get; set; }
	}

	public class GeoHashCellQuery : FieldNameQueryBase, IGeoHashCellQuery
	{
		protected override bool Conditionless => IsConditionless(this);

		public GeoLocation Location { get; set; }
		public GeoDistance Precision { get; set; }
		public bool? Neighbors { get; set; }

		internal override void WrapInContainer(IQueryContainer c) => c.GeoHashCell = this;

		internal static bool IsConditionless(IGeoHashCellQuery q) => q.Location == null || q.Field == null;
	}

	public class GeoHashCellQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<GeoHashCellQueryDescriptor<T>, IGeoHashCellQuery, T>
		, IGeoHashCellQuery where T : class
	{
		protected override bool Conditionless => GeoHashCellQuery.IsConditionless(this);

		GeoLocation IGeoHashCellQuery.Location { get; set; }
		GeoDistance IGeoHashCellQuery.Precision { get; set; }
		bool? IGeoHashCellQuery.Neighbors { get; set; }

		public GeoHashCellQueryDescriptor<T> Location(GeoLocation location) => Assign(a => a.Location = location);
		public GeoHashCellQueryDescriptor<T> Location(double lat, double lon) => Assign(a => a.Location = new GeoLocation(lat, lon));

		public GeoHashCellQueryDescriptor<T> Precision(GeoDistance distance) => Assign(a => a.Precision = distance);

		public GeoHashCellQueryDescriptor<T> Neighbors(bool? neighbors = true) => Assign(a => a.Neighbors = neighbors);
	}
}