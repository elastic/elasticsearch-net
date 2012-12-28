using System;
using System.Linq.Expressions;
using Nest.Resolvers;

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
			var name = new PropertyNameResolver().ResolveToLastToken(objectPath);
			this._Mapping.Name = name;
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
		

	}
}