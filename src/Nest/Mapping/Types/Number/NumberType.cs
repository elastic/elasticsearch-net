using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;

namespace Nest
{
	public interface INumberType : IElasticType
	{

	}

	[JsonObject(MemberSerialization.OptIn)]
	public class NumberType : ElasticType, INumberType
	{
		public NumberType() : base("double") { }
		
		[JsonProperty("index"), JsonConverter(typeof(StringEnumConverter))]
		public NonStringIndexOption? Index { get; set; }

		[JsonProperty("precision_step")]
		public int? PrecisionStep { get; set; }

		[JsonProperty("boost")]
		public double? Boost { get; set; }

		[JsonProperty("null_value")]
		public double? NullValue { get; set; }

		[JsonProperty("ignore_malformed")]
		public bool? IgnoreMalformed { get; set; }

		[JsonProperty("coerce")]
		public bool? Coerce { get; set; }
	}

	public class NumberTypeDescriptor<T> where T : class
	{
		internal NumberType _Mapping = new NumberType();

		public NumberTypeDescriptor<T> Name(string name)
		{
			this._Mapping.Name = name;
			return this;
		}
		public NumberTypeDescriptor<T> Name(Expression<Func<T, object>> objectPath)
		{
			this._Mapping.Name = objectPath;
			return this;
		}

		public NumberTypeDescriptor<T> Type(NumericTypeName type)
		{
			var stringType = type.GetStringValue();
			this._Mapping.Type = stringType;
			return this;
		}
		

		public NumberTypeDescriptor<T> IndexName(string indexName)
		{
			this._Mapping.IndexName = indexName;
			return this;
		}
		public NumberTypeDescriptor<T> Index(NonStringIndexOption index = NonStringIndexOption.No)
		{
			this._Mapping.Index = index;
			return this;
		}
		public NumberTypeDescriptor<T> Store(bool store = true)
		{
			this._Mapping.Store = store;
			return this;
		}

		public NumberTypeDescriptor<T> Boost(double boost)
		{
			this._Mapping.Boost = boost;
			return this;
		}
		public NumberTypeDescriptor<T> NullValue(double nullValue)
		{
			this._Mapping.NullValue = nullValue;
			return this;
		}

		public NumberTypeDescriptor<T> PrecisionStep(int precisionStep)
		{
			this._Mapping.PrecisionStep = precisionStep;
			return this;
		}

		public NumberTypeDescriptor<T> DocValues(bool docValues = true)
		{
			this._Mapping.DocValues = docValues;
			return this;
		}

		//public NumberTypeDescriptor<T> IncludeInAll(bool includeInAll = true)
		//{
		//	this._Mapping.IncludeInAll = includeInAll;
		//	return this;
		//}

		public NumberTypeDescriptor<T> IgnoreMalformed(bool ignoreMalformed = true)
		{
			this._Mapping.IgnoreMalformed = ignoreMalformed;
			return this;
		}
		public NumberTypeDescriptor<T> Coerce(bool coerce = true)
		{
			this._Mapping.Coerce = coerce;
			return this;
		}

		public NumberTypeDescriptor<T> Fields(Func<PropertiesDescriptor<T>, PropertiesDescriptor<T>> fieldSelector)
		{
			fieldSelector.ThrowIfNull("fieldSelector");
			var properties = fieldSelector(new PropertiesDescriptor<T>());
			foreach (var p in properties.Properties)
			{
				var value = p.Value as IElasticType;
				if (value == null)
					continue;

				_Mapping.Fields[p.Key] = value;
			}
			return this;
		}

		//public NumberTypeDescriptor<T> FieldData(Func<FieldDataNonStringMappingDescriptor, FieldDataNonStringMappingDescriptor> fieldDataSelector)
		//{
		//	fieldDataSelector.ThrowIfNull("fieldDataSelector");
		//	var selector = fieldDataSelector(new FieldDataNonStringMappingDescriptor());
		//	this._Mapping.FieldData = selector.FieldData;
		//	return this;
		//}

		//public NumberTypeDescriptor<T> FieldData(FieldDataNonStringMapping fieldData)
		//{
		//	this._Mapping.FieldData = fieldData;
		//	return this;
		//}
	}
}