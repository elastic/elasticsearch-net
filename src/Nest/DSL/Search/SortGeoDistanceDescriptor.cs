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
		IEnumerable<string> Points { get; set; }
		GeoUnit? GeoUnit { get; set; }
	}

	public class GeoDistanceSort : SortBase, IGeoDistanceSort
	{
		internal static List<string> Params = new List<string> { "missing", "mode", "order", "unit" };

		internal static int MissingIndex = 0;
		internal static int ModeIndex = 1;
		internal static int OrderIndex = 2;
		internal static int UnitIndex = 3;

		public PropertyPathMarker Field { get; set; }
		public string PinLocation { get; set; }
		public IEnumerable<string> Points { get; set; }
		public GeoUnit? GeoUnit { get; set; }
		
		object ICustomJson.GetCustomJson()
		{
			var sort = this.Points.HasAny() ? (object)this.Points : this.PinLocation;
			return new Dictionary<object, object>
			{
				{ this.Field, sort },
				{ Params[MissingIndex], this.Missing },
				{ Params[ModeIndex], this.Mode},
				{ Params[OrderIndex], this.Order },
				{ Params[UnitIndex], this.GeoUnit }
			};
		}
	}

	public class SortGeoDistanceDescriptor<T> : SortDescriptorBase<T, SortGeoDistanceDescriptor<T>>, IGeoDistanceSort where T : class
	{
		private IGeoDistanceSort Self { get { return this; } }

		PropertyPathMarker IGeoDistanceSort.Field { get; set; }

		string IGeoDistanceSort.PinLocation { get; set; }
		IEnumerable<string> IGeoDistanceSort.Points { get; set; }

		GeoUnit? IGeoDistanceSort.GeoUnit { get; set; }
	
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

		object ICustomJson.GetCustomJson()
		{
			var sort = Self.Points.HasAny() ? (object)Self.Points : Self.PinLocation;
			return new Dictionary<object, object>
			{
				{ Self.Field, sort },
				{ GeoDistanceSort.Params[GeoDistanceSort.MissingIndex], Self.Missing },
				{ GeoDistanceSort.Params[GeoDistanceSort.ModeIndex], Self.Mode},
				{ GeoDistanceSort.Params[GeoDistanceSort.OrderIndex], Self.Order },
				{ GeoDistanceSort.Params[GeoDistanceSort.UnitIndex], Self.GeoUnit }
			};
		}
	}
}
