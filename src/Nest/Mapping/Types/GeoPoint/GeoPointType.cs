using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;

namespace Nest
{
	public interface IGeoPointType : IElasticType
	{

	}

	[JsonObject(MemberSerialization.OptIn)]
	public class GeoPointType : ElasticType, IGeoPointType
	{
		public GeoPointType() : base("geo_point") { }

		[JsonProperty("lat_lon")]
		public bool? IndexLatLon { get; set; }

		[JsonProperty("geohash")]
		public bool? IndexGeoHash { get; set; }

        [JsonProperty("geohash_prefix")]
        public bool? GeoHashPrefix { get; set; }

		[JsonProperty("geohash_precision")]
		public int? GeoHashPrecision { get; set; }
	}

	public class GeoPointTypeDescriptor<T>
	{
		internal GeoPointType _Mapping = new GeoPointType();

		public GeoPointTypeDescriptor<T> Name(string name)
		{
			this._Mapping.Name = name;
			return this;
		}
		public GeoPointTypeDescriptor<T> Name(Expression<Func<T, object>> objectPath)
		{
			this._Mapping.Name = objectPath;
			return this;
		}

		public GeoPointTypeDescriptor<T> IndexLatLon(bool indexLatLon = true)
		{
			this._Mapping.IndexLatLon = indexLatLon;
			return this;
		}

		public GeoPointTypeDescriptor<T> IndexGeoHash(bool indexGeoHash = true)
		{
			this._Mapping.IndexGeoHash = indexGeoHash;
			return this;
		}

		public GeoPointTypeDescriptor<T> GeoHashPrecision(int geoHashPrecision)
		{
			this._Mapping.GeoHashPrecision = geoHashPrecision;
			return this;
		}

        public GeoPointTypeDescriptor<T> GeoHashPrefix(bool geoHashPrefix = true)
        {
            this._Mapping.GeoHashPrefix = geoHashPrefix;
            return this;
        }

		//public GeoPointTypeDescriptor<T> FieldData(Func<FieldDataNonStringMappingDescriptor, FieldDataNonStringMappingDescriptor> fieldDataSelector)
		//{
		//	fieldDataSelector.ThrowIfNull("fieldDataSelector");
		//	var selector = fieldDataSelector(new FieldDataNonStringMappingDescriptor());
		//	this._Mapping.FieldData = selector.FieldData;
		//	return this;
		//}

		//public GeoPointTypeDescriptor<T> FieldData(FieldDataNonStringMapping fieldData)
		//{
		//	this._Mapping.FieldData = fieldData;
		//	return this;
		//}

		public GeoPointTypeDescriptor<T> DocValues(bool docValues = true)
		{
			this._Mapping.DocValues = docValues;
			return this;
		}
	}
}