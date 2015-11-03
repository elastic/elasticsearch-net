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
		GeoPrecision Precision { get; set; }

		[JsonProperty("neighbors")]
		bool? Neighbors { get; set; }
	}

	public class GeoHashCellQuery : FieldNameQueryBase, IGeoHashCellQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);

		public GeoLocation Location { get; set; }
		public GeoPrecision Precision { get; set; }
		public bool? Neighbors { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.GeoHashCell = this;

		internal static bool IsConditionless(IGeoHashCellQuery q) => q.Location == null;
	}

	public class GeoHashCellQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<GeoHashCellQueryDescriptor<T>, IGeoHashCellQuery, T>
		, IGeoHashCellQuery where T : class
	{
		bool IQuery.Conditionless => GeoHashCellQuery.IsConditionless(this);

		GeoLocation IGeoHashCellQuery.Location { get; set; }
		GeoPrecision IGeoHashCellQuery.Precision { get; set; }
		bool? IGeoHashCellQuery.Neighbors { get; set; }

		public GeoHashCellQueryDescriptor<T> Pin(GeoLocation location) => Assign(a => a.Location = location);

		public GeoHashCellQueryDescriptor<T> Precision(GeoPrecision precision) => Assign(a => a.Precision = precision);

		public GeoHashCellQueryDescriptor<T> Neighbors(bool? neighbors = true) => Assign(a => a.Neighbors = neighbors);
	}
}