using System;
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
			var name = new PropertyNameResolver().ResolveToLastToken(objectPath);
			this._Mapping.Name = name;
			return this;
		}

		public BinaryMappingDescriptor<T> IndexName(string indexName)
		{
			this._Mapping.IndexName = indexName;
			return this;
		}

	}
}