using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;

namespace Nest
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public interface IGeoHashCellQuery : IFieldNameQuery
    {
        string Location { get; set; }

        [JsonProperty("distance")]
        object Precision { get; set; }

        [JsonProperty("unit")]
        [JsonConverter(typeof(StringEnumConverter))]
        GeoUnit? Unit { get; set; }

        [JsonProperty("neighbors")]
        bool Neighbors { get; set; }
    }

    public class GeoHashCellQuery : FieldNameQueryBase, IGeoHashCellQuery
    {
		bool IQuery.Conditionless => IsConditionless(this);
        public string Location { get; set; }
        public object Precision { get; set; }
        public GeoUnit? Unit { get; set; }
        public bool Neighbors { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.GeoHashCell = this;

		internal static bool IsConditionless(IGeoHashCellQuery q) => q.Location.IsNullOrEmpty();
	}

	public class GeoHashCellQueryDescriptor<T> 
		: FieldNameQueryDescriptorBase<GeoHashCellQueryDescriptor<T>, IGeoHashCellQuery, T> 
		, IGeoHashCellQuery where T : class
    {
        bool IQuery.Conditionless => GeoHashCellQuery.IsConditionless(this);
        string IGeoHashCellQuery.Location { get; set; }
        object IGeoHashCellQuery.Precision { get; set; }
        GeoUnit? IGeoHashCellQuery.Unit { get; set; }
        bool IGeoHashCellQuery.Neighbors { get; set; }

		public GeoHashCellQueryDescriptor<T> Location(double lat, double lon) => Assign(a =>
		{
			var c = CultureInfo.InvariantCulture;
			a.Location = "{{ lat: {0}, lon: {1} }}".F(lat.ToString(c), lon.ToString(c));
		});

		public GeoHashCellQueryDescriptor<T> Location(string geoHash) => Assign(a => a.Location = geoHash);

		public GeoHashCellQueryDescriptor<T> Precision(int precision) => Assign(a => a.Precision = precision);

		public GeoHashCellQueryDescriptor<T> Distance(double distance, GeoUnit unit) => Assign(a => { a.Precision = distance; a.Unit = unit; });

		public GeoHashCellQueryDescriptor<T> Neighbors(bool neighbors) => Assign(a => a.Neighbors = neighbors);
	}
}