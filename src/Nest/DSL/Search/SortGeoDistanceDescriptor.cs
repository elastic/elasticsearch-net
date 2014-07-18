using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Globalization;
using Nest.Resolvers.Converters;

namespace Nest
{
	[JsonConverter(typeof(CustomJsonConverter))]
	public interface IGeoDistanceSort : ISort, ICustomJson
	{
		PropertyPathMarker Field { get; set; }
		string PinLocation { get; set; }
		GeoUnit? GeoUnit { get; set; }
	}

	public class GeoDistanceSort : IGeoDistanceSort
	{
		public string Missing { get; set; }
		public SortOrder? Order { get; set; }
		public SortMode? Mode { get; set; }
		public PropertyPathMarker Field { get; set; }
		public string PinLocation { get; set; }
		public GeoUnit? GeoUnit { get; set; }
		
		object ICustomJson.GetCustomJson()
		{
			return new Dictionary<object, object>
			{
				{ this.Field, this.PinLocation },
				{ "missing", this.Missing },
				{ "mode", this.Mode },
				{ "order", this.Order },
				{ "unit", this.GeoUnit }
			};
		}
	}

	public class SortGeoDistanceDescriptor<T> : IGeoDistanceSort where T : class
	{
		private IGeoDistanceSort Self { get { return this; } }

		PropertyPathMarker IGeoDistanceSort.Field { get; set; }

		string ISort.Missing { get; set; }

		SortOrder? ISort.Order { get; set; }

		SortMode? ISort.Mode { get; set; }

		string IGeoDistanceSort.PinLocation { get; set; }

		GeoUnit? IGeoDistanceSort.GeoUnit { get; set; }
	
		public SortGeoDistanceDescriptor<T> PinTo(string geoLocationHash)
		{
			geoLocationHash.ThrowIfNullOrEmpty("geoLocationHash");
			Self.PinLocation = geoLocationHash;
			return this;
		}
		public SortGeoDistanceDescriptor<T> PinTo(double Lat, double Lon)
		{
			var c = CultureInfo.InvariantCulture;
			Lat.ThrowIfNull("Lat");
			Lon.ThrowIfNull("Lon");
			Self.PinLocation = "{0}, {1}".F(Lat.ToString(c), Lon.ToString(c));
			return this;
		}
		public SortGeoDistanceDescriptor<T> Unit(GeoUnit unit)
		{
			unit.ThrowIfNull("unit");
			Self.GeoUnit = unit;
			return this;
		}

		public SortGeoDistanceDescriptor<T> OnField(string field)
		{
			Self.Field = field;
			return this;
		}
		public SortGeoDistanceDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			Self.Field = objectPath;
			return this;
		}

		public SortGeoDistanceDescriptor<T> MissingLast()
		{
			Self.Missing = "_last";
			return this;
		}
		public SortGeoDistanceDescriptor<T> MissingFirst()
		{
			Self.Missing = "_first";
			return this;
		}
		public SortGeoDistanceDescriptor<T> MissingValue(string value)
		{
			Self.Missing = value;
			return this;
		}
		public SortGeoDistanceDescriptor<T> Ascending()
		{
			Self.Order = SortOrder.Ascending;
			return this;
		}
		public SortGeoDistanceDescriptor<T> Descending()
		{
			Self.Order = SortOrder.Descending;
			return this;
		}

		public SortGeoDistanceDescriptor<T> Order(SortOrder order)
		{
			Self.Order = order;
			return this;
		}

		public SortGeoDistanceDescriptor<T> Mode(SortMode mode)
		{
			Self.Mode = mode;
			return this;
		}

		object ICustomJson.GetCustomJson()
		{
			return new Dictionary<object, object>
			{
				{ Self.Field, Self.PinLocation },
				{ "missing", Self.Missing },
				{ "mode", Self.Mode},
				{ "order", Self.Order },
				{ "unit", Self.GeoUnit }
			};
		}
	}
}
