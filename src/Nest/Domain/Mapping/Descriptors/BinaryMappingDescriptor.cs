using System;
using System.Linq;
using System.Linq.Expressions;

namespace Nest
{
	public class BinaryMappingDescriptor<T>
	{
		internal BinaryMapping _Mapping = new BinaryMapping();

		public BinaryMappingDescriptor<T> Name(string name)
		{
			this._Mapping.Name = name;
			return this;
		}
		public BinaryMappingDescriptor<T> Name(Expression<Func<T, object>> objectPath)
		{
			this._Mapping.Name = objectPath;
			return this;
		}

		public BinaryMappingDescriptor<T> IndexName(string indexName)
		{
			this._Mapping.IndexName = indexName;
			return this;
		}

		public BinaryMappingDescriptor<T> DocValues(bool docValues = true)
		{
			this._Mapping.DocValues = docValues;
			return this;
		}

		public BinaryMappingDescriptor<T> Store(bool store = true)
		{
			this._Mapping.Store = store;
			return this;
		}

		public BinaryMappingDescriptor<T> Compress(bool compress = true )
		{
			this._Mapping.Compress = compress;
			return this;
		}

		public BinaryMappingDescriptor<T> CompressThreshold(string compressThreshold)
		{
			this._Mapping.CompressThreshold = compressThreshold;
			return this;
		}

		public BinaryMappingDescriptor<T> CopyTo(params string[] fields)
		{
			this._Mapping.CopyTo = fields.Select(f => (PropertyPathMarker)f);
			return this;
		}

		public BinaryMappingDescriptor<T> CopyTo(params Expression<Func<T, object>>[] objectPaths)
		{
			this._Mapping.CopyTo = objectPaths.Select(e => (PropertyPathMarker)e);
			return this;
		}
	}
}