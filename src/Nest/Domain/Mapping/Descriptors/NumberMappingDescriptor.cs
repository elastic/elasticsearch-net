using System;
using System.Linq.Expressions;

namespace Nest
{
	public class NumberMappingDescriptor<T>
	{
		internal NumberMapping _Mapping = new NumberMapping();

		public NumberMappingDescriptor<T> Name(string name)
		{
			this._Mapping.Name = name;
			return this;
		}
		public NumberMappingDescriptor<T> Name(Expression<Func<T, object>> objectPath)
		{
			this._Mapping.Name = objectPath;
			return this;
		}

		public NumberMappingDescriptor<T> Type(NumberType type)
		{
			var stringType = type.GetStringValue();
			this._Mapping.Type = stringType;
			return this;
		}
		

		public NumberMappingDescriptor<T> IndexName(string indexName)
		{
			this._Mapping.IndexName = indexName;
			return this;
		}
		public NumberMappingDescriptor<T> Index(NonStringIndexOption index = NonStringIndexOption.Analyzed)
		{
			this._Mapping.Index = index;
			return this;
		}
		public NumberMappingDescriptor<T> Store(bool store = true)
		{
			this._Mapping.Store = store;
			return this;
		}

		public NumberMappingDescriptor<T> Boost(double boost)
		{
			this._Mapping.Boost = boost;
			return this;
		}
		public NumberMappingDescriptor<T> NullValue(double nullValue)
		{
			this._Mapping.NullValue = nullValue;
			return this;
		}

		public NumberMappingDescriptor<T> PrecisionStep(int precisionStep)
		{
			this._Mapping.PrecisionStep = precisionStep;
			return this;
		}

		public NumberMappingDescriptor<T> DocValues(bool docValues = true)
		{
			this._Mapping.DocValues = docValues;
			return this;
		}

		public NumberMappingDescriptor<T> IncludeInAll(bool includeInAll = true)
		{
			this._Mapping.IncludeInAll = includeInAll;
			return this;
		}
		public NumberMappingDescriptor<T> IgnoreMalformed(bool ignoreMalformed = true)
		{
			this._Mapping.IgnoreMalformed = ignoreMalformed;
			return this;
		}

	}
}