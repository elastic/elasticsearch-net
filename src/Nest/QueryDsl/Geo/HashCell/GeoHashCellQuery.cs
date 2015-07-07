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

    public class GeoHashCellQuery : FieldNameQuery, IGeoHashCellQuery
    {
		bool IQuery.Conditionless => IsConditionless(this);
        public string Location { get; set; }
        public object Precision { get; set; }
        public GeoUnit? Unit { get; set; }
        public bool Neighbors { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.GeoHashCell = this;

		internal static bool IsConditionless(IGeoHashCellQuery q) => q.Location.IsNullOrEmpty();
	}

	public class GeoHashCellQueryDescriptor<T> : IGeoHashCellQuery
    {
		private IGeoHashCellQuery Self => this;
		string IQuery.Name { get; set; }
        bool IQuery.Conditionless => GeoHashCellQuery.IsConditionless(this);
        PropertyPath IFieldNameQuery.Field { get; set; }
        string IGeoHashCellQuery.Location { get; set; }
        object IGeoHashCellQuery.Precision { get; set; }
        GeoUnit? IGeoHashCellQuery.Unit { get; set; }
        bool IGeoHashCellQuery.Neighbors { get; set; }

		public GeoHashCellQueryDescriptor<T> Field(string field)
		{
			Self.Field = field;
			return this;
		}

		public GeoHashCellQueryDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			Self.Field = field;
			return this;
		}

        public GeoHashCellQueryDescriptor<T> Location(double lat, double lon)
        {
            var c = CultureInfo.InvariantCulture;
            Self.Location = "{{ lat: {0}, lon: {1} }}".F(lat.ToString(c), lon.ToString(c));
            return this;
        }

        public GeoHashCellQueryDescriptor<T> Location(string geoHash)
        {
            Self.Location = geoHash;
            return this;
        }

        public GeoHashCellQueryDescriptor<T> Precision(int precision)
        {
            Self.Precision = precision;
            return this;
        }

        public GeoHashCellQueryDescriptor<T> Distance(double distance, GeoUnit unit)
        {
            Self.Precision = distance;
            Self.Unit = unit;
            return this;
        }

        public GeoHashCellQueryDescriptor<T> Neighbors(bool neighbors)
        {
            Self.Neighbors = neighbors;
            return this;
        }
	}
}