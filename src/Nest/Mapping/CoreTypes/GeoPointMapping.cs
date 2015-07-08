using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class GeoPointMapping : IElasticType
	{
		public FieldName Name { get; set; }

		[JsonProperty("type")]
		public virtual TypeName Type { get { return new TypeName { Name = "geo_point" }; } }

		[JsonProperty("similarity")]
		public string Similarity { get; set; }

		[JsonProperty("lat_lon")]
		public bool? IndexLatLon { get; set; }

		[JsonProperty("geohash")]
		public bool? IndexGeoHash { get; set; }

        [JsonProperty("geohash_prefix")]
        public bool? GeoHashPrefix { get; set; }

		[JsonProperty("geohash_precision")]
		public int? GeoHashPrecision { get; set; }

		[JsonProperty("fielddata")]
		public FieldDataNonStringMapping FieldData { get; set; }

		[JsonProperty("doc_values")]
		public bool? DocValues { get; set; }
	}

	public class GeoPointMappingDescriptor<T>
	{
		internal GeoPointMapping _Mapping = new GeoPointMapping();

		public GeoPointMappingDescriptor<T> Name(string name)
		{
			this._Mapping.Name = name;
			return this;
		}
		public GeoPointMappingDescriptor<T> Name(Expression<Func<T, object>> objectPath)
		{
			this._Mapping.Name = objectPath;
			return this;
		}

		public GeoPointMappingDescriptor<T> IndexLatLon(bool indexLatLon = true)
		{
			this._Mapping.IndexLatLon = indexLatLon;
			return this;
		}

		public GeoPointMappingDescriptor<T> IndexGeoHash(bool indexGeoHash = true)
		{
			this._Mapping.IndexGeoHash = indexGeoHash;
			return this;
		}

		public GeoPointMappingDescriptor<T> GeoHashPrecision(int geoHashPrecision)
		{
			this._Mapping.GeoHashPrecision = geoHashPrecision;
			return this;
		}

        public GeoPointMappingDescriptor<T> GeoHashPrefix(bool geoHashPrefix = true)
        {
            this._Mapping.GeoHashPrefix = geoHashPrefix;
            return this;
        }

		public GeoPointMappingDescriptor<T> FieldData(Func<FieldDataNonStringMappingDescriptor, FieldDataNonStringMappingDescriptor> fieldDataSelector)
		{
			fieldDataSelector.ThrowIfNull("fieldDataSelector");
			var selector = fieldDataSelector(new FieldDataNonStringMappingDescriptor());
			this._Mapping.FieldData = selector.FieldData;
			return this;
		}

		public GeoPointMappingDescriptor<T> FieldData(FieldDataNonStringMapping fieldData)
		{
			this._Mapping.FieldData = fieldData;
			return this;
		}

		public GeoPointMappingDescriptor<T> DocValues(bool docValues = true)
		{
			this._Mapping.DocValues = docValues;
			return this;
		}
	}
}