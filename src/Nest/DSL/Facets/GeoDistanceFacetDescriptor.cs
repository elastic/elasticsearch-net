using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;
using System.Globalization;
using Nest.Resolvers;
using System.Runtime.Serialization;
using Nest.Resolvers.Converters;

namespace Nest
{
	[JsonConverter(typeof(ICustomJsonConverter))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class GeoDistanceFacetDescriptor<T> : BaseFacetDescriptor<GeoDistanceFacetDescriptor<T>, T>, ICustomJson
		where T : class
	{
		internal string _Nested { get; set; }
		internal string _Scope { get; set; }
		internal bool? _IsGlobal { get; set; }
		internal BaseFilter _FacetFilter { get; set; }
		internal string _PinLocation { get; set; }
		internal PropertyPathMarker _Field { get; set; }
		internal PropertyPathMarker _ValueField { get; set; }
		internal string _ValueScript { get; set; }
		internal IEnumerable<Range<double>> _Ranges { get; set; }
		internal GeoUnit? _GeoUnit { get; set; }
		internal GeoDistance? _GeoDistance { get; set; }
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<string, object> _Params { get; set; }
	
		public GeoDistanceFacetDescriptor<T> OnField(string field)
		{
			field.ThrowIfNullOrEmpty("field");
			this._Field = field;
			return this;
		}
		public GeoDistanceFacetDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");
			this._Field = objectPath;
			return this;
		}

		public GeoDistanceFacetDescriptor<T> OnValueField(string field)
		{
			field.ThrowIfNullOrEmpty("field");
			this._ValueField = field;
			return this;
		}
		public GeoDistanceFacetDescriptor<T> OnValueField(Expression<Func<T, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");
			this._ValueField = objectPath;
			return this;
		}
		public GeoDistanceFacetDescriptor<T> OnValueScript(string script)
		{
			script.ThrowIfNullOrEmpty("script");
			this._ValueScript = script;
			return this;
		}
		public GeoDistanceFacetDescriptor<T> PinTo(string geoLocationHash)
		{
			geoLocationHash.ThrowIfNullOrEmpty("geoLocationHash");
			this._PinLocation = geoLocationHash;
			return this;
		}
		public GeoDistanceFacetDescriptor<T> PinTo(double Lat, double Lon)
		{
			var c = CultureInfo.InvariantCulture;
			Lat.ThrowIfNull("Lat");
			Lon.ThrowIfNull("Lon");
			this._PinLocation = "{0}, {1}".F(Lat.ToString(c), Lon.ToString(c));
			return this;
		}
		public GeoDistanceFacetDescriptor<T> Unit(GeoUnit unit)
		{
			unit.ThrowIfNull("unit");
			this._GeoUnit = unit;
			return this;
		}
		public GeoDistanceFacetDescriptor<T> DistanceType(GeoDistance distance)
		{
			distance.ThrowIfNull("unit");
			this._GeoDistance = distance;
			return this;
		}
		public GeoDistanceFacetDescriptor<T> Ranges(params Func<Range<double>, Range<double>>[] ranges)
		{
			var newRanges = new List<Range<double>>();
			foreach (var range in ranges)
			{
				var r = new Range<double>();
				newRanges.Add(range(r));
			}
			this._Ranges = newRanges;
			return this;
		}
		public GeoDistanceFacetDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
		{
			paramDictionary.ThrowIfNull("paramDictionary");
			this._Params = paramDictionary(new FluentDictionary<string, object>());
			return this;
		}

		IDictionary<object, object> ICustomJson.GetCustomJson()
		{
			return new Dictionary<object, object>
			{
				{ _Field, _PinLocation },
				{ "value_field", _ValueField },
				{ "value_script", _ValueScript },
				{ "ranges", _Ranges },
				{ "params", _Params },
				{ "unit", _GeoUnit },
				{ "distance_type", _GeoDistance },
			};
		}
	}
}
