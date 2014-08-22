using System;
using System.Linq.Expressions;

namespace Nest
{
	public class NumberMappingDescriptor<T> where T : class
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
		public NumberMappingDescriptor<T> Coerce(bool coerce = true)
		{
			this._Mapping.Coerce = coerce;
			return this;
		}

		public NumberMappingDescriptor<T> Path(MultiFieldMappingPath path)
		{
			this._Mapping.Path = path.Value;
			return this;
		}

		public NumberMappingDescriptor<T> Fields(Func<CorePropertiesDescriptor<T>, CorePropertiesDescriptor<T>> fieldSelector)
		{
			fieldSelector.ThrowIfNull("fieldSelector");
			var properties = fieldSelector(new CorePropertiesDescriptor<T>());
			foreach (var p in properties.Properties)
			{
				var value = p.Value as IElasticCoreType;
				if (value == null)
					continue;

				_Mapping.Fields[p.Key] = value;
			}
			return this;
		}

		public NumberMappingDescriptor<T> FieldData(Func<FieldDataNonStringMappingDescriptor, FieldDataNonStringMappingDescriptor> fieldDataSelector)
		{
			fieldDataSelector.ThrowIfNull("fieldDataSelector");
			var selector = fieldDataSelector(new FieldDataNonStringMappingDescriptor());
			this._Mapping.FieldData = selector.FieldData;
			return this;
		}

		public NumberMappingDescriptor<T> FieldData(FieldDataNonStringMapping fieldData)
		{
			this._Mapping.FieldData = fieldData;
			return this;
		}
	}
}