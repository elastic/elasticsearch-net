using System;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	public class DateMappingDescriptor<T>
	{
		internal DateMapping _Mapping = new DateMapping();

		public DateMappingDescriptor<T> Name(string name)
		{
			this._Mapping.TypeNameMarker = name;
			return this;
		}
		public DateMappingDescriptor<T> Name(Expression<Func<T, object>> objectPath)
		{
			var name = new PropertyNameResolver().ResolveToLastToken(objectPath);
			this._Mapping.TypeNameMarker = name;
			return this;
		}

		public DateMappingDescriptor<T> IndexName(string indexName)
		{
			this._Mapping.IndexName = indexName;
			return this;
		}
		public DateMappingDescriptor<T> Index(NonStringIndexOption index = NonStringIndexOption.analyzed)
		{
			this._Mapping.Index = index;
			return this;
		}
		public DateMappingDescriptor<T> Store(bool store = true)
		{
			this._Mapping.Store = store;
			return this;
		}

		public DateMappingDescriptor<T> Format(string format)
		{
			this._Mapping.Format = format;
			return this;
		}

		public DateMappingDescriptor<T> Boost(double boost)
		{
			this._Mapping.Boost = boost;
			return this;
		}
		public DateMappingDescriptor<T> NullValue(DateTime nullValue)
		{
			this._Mapping.NullValue = nullValue;
			return this;
		}
			
		public DateMappingDescriptor<T> PrecisionStep(int precisionStep)
		{
			this._Mapping.PrecisionStep = precisionStep;
			return this;
		}

		public DateMappingDescriptor<T> IncludeInAll(bool includeInAll = true)
		{
			this._Mapping.IncludeInAll = includeInAll;
			return this;
		}
		public DateMappingDescriptor<T> IgnoreMalformed(bool ignoreMalformed = true)
		{
			this._Mapping.IgnoreMalformed = ignoreMalformed;
			return this;
		}

	}
}