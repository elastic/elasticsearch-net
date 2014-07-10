using System;
using System.Linq;
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
			this._Mapping.Name = objectPath;
			return this;
		}

		public BooleanMappingDescriptor<T> IndexName(string indexName)
		{
			this._Mapping.IndexName = indexName;
			return this;
		}
		public BooleanMappingDescriptor<T> Index(NonStringIndexOption index = NonStringIndexOption.Analyzed)
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

		public BooleanMappingDescriptor<T> CopyTo(params string[] fields)
		{
			this._Mapping.CopyTo = fields.Select(f => (PropertyPathMarker)f);
			return this;
		}

		public BooleanMappingDescriptor<T> CopyTo(params Expression<Func<T, object>>[] objectPaths)
		{
			this._Mapping.CopyTo = objectPaths.Select(e => (PropertyPathMarker)e);
			return this;
		}
	}
}