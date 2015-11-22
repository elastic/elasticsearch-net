using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Globalization;

namespace Nest
{
	[JsonConverter(typeof(CustomJsonConverter))]
	public interface IGeoDistanceSort : ISort, ICustomJson
	{
		Field Field { get; set; }
		string PinLocation { get; set; }
		IEnumerable<string> Points { get; set; }
		GeoPrecision? GeoUnit { get; set; }
		GeoDistanceType? DistanceType { get; set; }
	}

	public class GeoDistanceSort : SortBase, IGeoDistanceSort
	{
		internal static List<string> Params = new List<string> { "missing", "mode", "order", "unit", "distance_type" };

		internal static int MissingIndex = 0;
		internal static int ModeIndex = 1;
		internal static int OrderIndex = 2;
		internal static int UnitIndex = 3;
		internal static int DistanceTypeIndex = 4;

		public Field Field { get; set; }
		public string PinLocation { get; set; }
		public IEnumerable<string> Points { get; set; }
		public GeoPrecision? GeoUnit { get; set; }
		public override Field SortKey { get { return "_geo_distance"; } }
		public GeoDistanceType? DistanceType { get; set; }

		object ICustomJson.GetCustomJson()
		{
			var sort = this.Points.HasAny() ? (object)this.Points : this.PinLocation;
			return new Dictionary<object, object>
			{
				{ this.Field, sort },
				{ Params[MissingIndex], this.Missing },
				{ Params[ModeIndex], this.Mode},
				{ Params[OrderIndex], this.Order },
				{ Params[UnitIndex], this.GeoUnit },
				{ Params[DistanceTypeIndex], this.DistanceType }
			};
		}
	}

	public class SortGeoDistanceDescriptor<T> : SortDescriptorBase<T, SortGeoDistanceDescriptor<T>>, IGeoDistanceSort where T : class
	{
		private IGeoDistanceSort Self => this;

		Field IGeoDistanceSort.Field { get; set; }

		string IGeoDistanceSort.PinLocation { get; set; }
		IEnumerable<string> IGeoDistanceSort.Points { get; set; }

		GeoPrecision? IGeoDistanceSort.GeoUnit { get; set; }

		Field ISort.SortKey { get { return "_geo_distance"; } }

		GeoDistanceType? IGeoDistanceSort.DistanceType { get; set; }

		public SortGeoDistanceDescriptor<T> PinTo(string geoLocationHash)
		{
			geoLocationHash.ThrowIfNullOrEmpty("geoLocationHash");
			Self.PinLocation = geoLocationHash;
			return this;
		}

		public SortGeoDistanceDescriptor<T> PinTo(IEnumerable<string> geoLocationHashes)
		{
			geoLocationHashes.ThrowIfEmpty("geoLocationHash");
			Self.Points = geoLocationHashes;
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
		//TODO in nest 2.0 normalize all PinTo to params ?
		public SortGeoDistanceDescriptor<T> PinTo(params GeoLocation[] geoLocationHashes)
		{
			geoLocationHashes.ThrowIfEmpty("geoLocationHash");
			Self.Points = geoLocationHashes.Select(g=>g.ToString());
			return this;
		}

		public SortGeoDistanceDescriptor<T> Unit(GeoPrecision unit)
		{
			unit.ThrowIfNull("unit");
			Self.GeoUnit = unit;
			return this;
		}

		public SortGeoDistanceDescriptor<T> DistanceType(GeoDistanceType distanceType)
		{
			distanceType.ThrowIfNull("distanceType");
			Self.DistanceType = distanceType;
			return this;
		}

		public SortGeoDistanceDescriptor<T> Field(string field)
		{
			Self.Field = field;
			return this;
		}
		public SortGeoDistanceDescriptor<T> Field(Expression<Func<T, object>> objectPath)
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

		object ICustomJson.GetCustomJson()
		{
			var sort = Self.Points.HasAny() ? (object)Self.Points : Self.PinLocation;
			return new Dictionary<object, object>
			{
				{ Self.Field, sort },
				{ GeoDistanceSort.Params[GeoDistanceSort.MissingIndex], Self.Missing },
				{ GeoDistanceSort.Params[GeoDistanceSort.ModeIndex], Self.Mode},
				{ GeoDistanceSort.Params[GeoDistanceSort.OrderIndex], Self.Order },
				{ GeoDistanceSort.Params[GeoDistanceSort.UnitIndex], Self.GeoUnit },
				{ GeoDistanceSort.Params[GeoDistanceSort.DistanceTypeIndex], Self.DistanceType }
			};
		}
	}
}
