using System;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	public class BooleanMappingDescriptor<T>
	{
		internal BooleanMapping _Mapping = new BooleanMapping();

		public BooleanMappingDescriptor<T> Name(string name)
		{
			this._Mapping.Name = name;
			return this;
		}
		public BooleanMappingDescriptor<T> Name(Expression<Func<T, object>> objectPath)
		{
			var name = new PropertyNameResolver().ResolveToLastToken(objectPath);
			this._Mapping.Name = name;
			return this;
		}

		public BooleanMappingDescriptor<T> IndexName(string indexName)
		{
			this._Mapping.IndexName = indexName;
			return this;
		}
		public BooleanMappingDescriptor<T> Index(NonStringIndexOption index = NonStringIndexOption.analyzed)
		{
			this._Mapping.Index = index;
			return this;
		}
		public BooleanMappingDescriptor<T> Store(bool store = true)
		{
			this._Mapping.Store = store;
			return this;
		}

		public BooleanMappingDescriptor<T> Boost(double boost)
		{
			this._Mapping.Boost = boost;
			return this;
		}
		public BooleanMappingDescriptor<T> NullValue(bool nullValue)
		{
			this._Mapping.NullValue = nullValue;
			return this;
		}

		public BooleanMappingDescriptor<T> IncludeInAll(bool includeInAll = true)
		{
			this._Mapping.IncludeInAll = includeInAll;
			return this;
		}


	}
}