using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public interface IGeoHashCellQuery : IFieldNameQuery
    {
        PropertyPathMarker Field { get; set; }
        string Location { get; set; }

        [JsonProperty("distance")]
        object Precision { get; set; }

        [JsonProperty("unit")]
        [JsonConverter(typeof(StringEnumConverter))]
        GeoUnit? Unit { get; set; }

        [JsonProperty("neighbors")]
        bool Neighbors { get; set; }
    }

    public class GeoHashCellQuery : PlainQuery, IGeoHashCellQuery
    {
        protected override void WrapInContainer(IQueryContainer container)
        {
            container.GeoHashCell = this;
        }

		public PropertyPathMarker GetFieldName()
		{
			return Field;
		}

		public void SetFieldName(string fieldName)
		{
			Field = fieldName;
		}

		public PropertyPathMarker Field { get; set; }
        public string Location { get; set; }
        public object Precision { get; set; }
        public GeoUnit? Unit { get; set; }
        public bool Neighbors { get; set; }
		public string Name { get; set; }
		bool IQuery.IsConditionless { get { return QueryCondition.IsConditionless(this); } }
	}

	// TODO : Finish implementing
	public class GeoHashCellQueryDescriptor : IGeoHashCellQuery
    {
		private IGeoHashCellQuery _ { get { return this; } }
        PropertyPathMarker IGeoHashCellQuery.Field { get; set; }
        string IGeoHashCellQuery.Location { get; set; }
        object IGeoHashCellQuery.Precision { get; set; }
        GeoUnit? IGeoHashCellQuery.Unit { get; set; }
        bool IGeoHashCellQuery.Neighbors { get; set; }
        bool IQuery.IsConditionless { get { return QueryCondition.IsConditionless(this); } }
		string IQuery.Name { get; set; }

        public GeoHashCellQueryDescriptor Location(double lat, double lon)
        {
            var c = CultureInfo.InvariantCulture;
            _.Location = "{{ lat: {0}, lon: {1} }}".F(lat.ToString(c), lon.ToString(c));
            return this;
        }

        public GeoHashCellQueryDescriptor Location(string geoHash)
        {
            _.Location = geoHash;
            return this;
        }

        public GeoHashCellQueryDescriptor Precision(int precision)
        {
            _.Precision = precision;
            return this;
        }

        public GeoHashCellQueryDescriptor Distance(double distance, GeoUnit unit)
        {
            _.Precision = distance;
            _.Unit = unit;
            return this;
        }

        public GeoHashCellQueryDescriptor Neighbors(bool neighbors)
        {
            _.Neighbors = neighbors;
            return this;
        }

		public PropertyPathMarker GetFieldName()
		{
			return _.Field;
		}

		public void SetFieldName(string fieldName)
		{
			_.Field = fieldName;
		}
	}
}