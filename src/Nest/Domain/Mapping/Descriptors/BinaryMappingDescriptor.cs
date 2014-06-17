using System;
using System.Linq;
using System.Linq.Expressions;
using Nest.Resolvers;

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