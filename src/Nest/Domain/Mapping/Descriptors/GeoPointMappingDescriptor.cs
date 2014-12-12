using System;
using System.Linq.Expressions;

namespace Nest
{
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
	}
}