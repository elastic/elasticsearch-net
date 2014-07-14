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
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoDistanceFacetRequest : IFacetRequest, ICustomJson
	{
		string PinLocation { get; set; }
		PropertyPathMarker Field { get; set; }
		PropertyPathMarker ValueField { get; set; }
		string ValueScript { get; set; }
		IEnumerable<Range<double>> Ranges { get; set; }
		GeoUnit? GeoUnit { get; set; }
		GeoDistance? GeoDistance { get; set; }

		[JsonConverter(typeof (DictionaryKeysAreNotPropertyNamesJsonConverter))]
		Dictionary<string, object> Params { get; set; }
	}

	public class GeoDistanceFacetRequest : FacetRequest, IGeoDistanceFacetRequest
	{
		public string PinLocation { get; set; }
		public PropertyPathMarker Field { get; set; }
		public PropertyPathMarker ValueField { get; set; }
		public string ValueScript { get; set; }
		public IEnumerable<Range<double>> Ranges { get; set; }
		public GeoUnit? GeoUnit { get; set; }
		public GeoDistance? GeoDistance { get; set; }
		public Dictionary<string, object> Params { get; set; }

		public static object CustomJson(IGeoDistanceFacetRequest geoDistanceFacetRequest)
		{
			return new Dictionary<object, object>
			{
				{ geoDistanceFacetRequest.Field, geoDistanceFacetRequest.PinLocation },
				{ "value_field", geoDistanceFacetRequest.ValueField },
				{ "value_script", geoDistanceFacetRequest.ValueScript },
				{ "ranges", geoDistanceFacetRequest.Ranges },
				{ "params", geoDistanceFacetRequest.Params },
				{ "unit", geoDistanceFacetRequest.GeoUnit },
				{ "distance_type", geoDistanceFacetRequest.GeoDistance },
				{ "facet_filter", geoDistanceFacetRequest.FacetFilter },
				{ "global", geoDistanceFacetRequest.Global },
				{ "nested", geoDistanceFacetRequest.Nested },
			};
		}

		object ICustomJson.GetCustomJson()
		{
			return CustomJson(this);
		}
	}

	public class GeoDistanceFacetDescriptor<T> : BaseFacetDescriptor<GeoDistanceFacetDescriptor<T>, T>, IGeoDistanceFacetRequest where T : class
	{
		protected IGeoDistanceFacetRequest Self { get { return this; } }

		string IGeoDistanceFacetRequest.PinLocation { get; set; }
		PropertyPathMarker IGeoDistanceFacetRequest.Field { get; set; }
		PropertyPathMarker IGeoDistanceFacetRequest.ValueField { get; set; }
		string IGeoDistanceFacetRequest.ValueScript { get; set; }
		IEnumerable<Range<double>> IGeoDistanceFacetRequest.Ranges { get; set; }
		GeoUnit? IGeoDistanceFacetRequest.GeoUnit { get; set; }
		GeoDistance? IGeoDistanceFacetRequest.GeoDistance { get; set; }
		Dictionary<string, object> IGeoDistanceFacetRequest.Params { get; set; }
	
		public GeoDistanceFacetDescriptor<T> OnField(string field)
		{
			field.ThrowIfNullOrEmpty("field");
			Self.Field = field;
			return this;
		}
		public GeoDistanceFacetDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");
			Self.Field = objectPath;
			return this;
		}

		public GeoDistanceFacetDescriptor<T> OnValueField(string field)
		{
			field.ThrowIfNullOrEmpty("field");
			Self.ValueField = field;
			return this;
		}
		public GeoDistanceFacetDescriptor<T> OnValueField(Expression<Func<T, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");
			Self.ValueField = objectPath;
			return this;
		}
		public GeoDistanceFacetDescriptor<T> OnValueScript(string script)
		{
			script.ThrowIfNullOrEmpty("script");
			Self.ValueScript = script;
			return this;
		}
		public GeoDistanceFacetDescriptor<T> PinTo(string geoLocationHash)
		{
			geoLocationHash.ThrowIfNullOrEmpty("geoLocationHash");
			Self.PinLocation = geoLocationHash;
			return this;
		}
		public GeoDistanceFacetDescriptor<T> PinTo(double Lat, double Lon)
		{
			var c = CultureInfo.InvariantCulture;
			Lat.ThrowIfNull("Lat");
			Lon.ThrowIfNull("Lon");
			Self.PinLocation = "{0}, {1}".F(Lat.ToString(c), Lon.ToString(c));
			return this;
		}
		public GeoDistanceFacetDescriptor<T> Unit(GeoUnit unit)
		{
			unit.ThrowIfNull("unit");
			Self.GeoUnit = unit;
			return this;
		}
		public GeoDistanceFacetDescriptor<T> DistanceType(GeoDistance distance)
		{
			distance.ThrowIfNull("unit");
			Self.GeoDistance = distance;
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
			Self.Ranges = newRanges;
			return this;
		}
		public GeoDistanceFacetDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
		{
			paramDictionary.ThrowIfNull("paramDictionary");
			Self.Params = paramDictionary(new FluentDictionary<string, object>());
			return this;
		}

		object ICustomJson.GetCustomJson()
		{
			return GeoDistanceFacetRequest.CustomJson(Self);
		}
	}
}
