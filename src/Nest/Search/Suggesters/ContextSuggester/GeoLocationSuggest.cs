using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nest
{
	[JsonObject]
	public interface IGeoLocationSuggestContext : ISuggestContext
	{
		[JsonProperty("precision")]
		IEnumerable<string> Precision { get; set; }

		[JsonProperty("neighbors")]
		bool Neighbors { get; set; }

		[JsonProperty("default")]
		object Default { get; set; }
	}

	[JsonObject]
	public class GeoLocationSuggestContext : IGeoLocationSuggestContext
	{
		public string Type { get { return "geo"; } }

		public IEnumerable<string> Precision { get; set; }

		public bool Neighbors { get; set; }

		public Field Path { get; set; }

		public object Default { get; set; }
	}

	public class GeoLocationSuggestDescriptor<T>
		where T : class
	{
		internal GeoLocationSuggestContext _Context = new GeoLocationSuggestContext();

		public GeoLocationSuggestDescriptor<T> Precision(params string[] precisions)
		{
			this._Context.Precision = precisions;
			return this;
		}

		public GeoLocationSuggestDescriptor<T> Neighbors(bool neighbors = true)
		{
			this._Context.Neighbors = neighbors;
			return this;
		}

		public GeoLocationSuggestDescriptor<T> Path(string path)
		{
			this._Context.Path = path;
			return this;
		}

		public GeoLocationSuggestDescriptor<T> Path(Expression<Func<T, object>> objectPath)
		{
			this._Context.Path = objectPath;
			return this;
		}

		public GeoLocationSuggestDescriptor<T> Default(LatLon geoPoint)
		{
			this._Context.Default = geoPoint;
			return this;
		}

		public GeoLocationSuggestDescriptor<T> Default(string geoHash)
		{
			this._Context.Default = geoHash;
			return this;
		}
	}
}
