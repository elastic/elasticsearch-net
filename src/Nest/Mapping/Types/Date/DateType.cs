using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;

namespace Nest
{
	public interface IDateType : IElasticType
	{

	}

	[JsonObject(MemberSerialization.OptIn)]
	public class DateType : ElasticType, IDateType
	{
		public DateType() : base("date") { }

		[JsonProperty("index"), JsonConverter(typeof(StringEnumConverter))]
		public NonStringIndexOption? Index { get; set; }

		[JsonProperty("format")]
		public string Format { get; set; }

		[JsonProperty("precision_step")]
		public int? PrecisionStep { get; set; }

		[JsonProperty("boost")]
		public double? Boost { get; set; }

		[JsonProperty("null_value")]
		public DateTime? NullValue { get; set; }

		[JsonProperty("ignore_malformed")]
		public bool? IgnoreMalformed { get; set; }

		[JsonProperty("numeric_resolution")]
		public NumericResolutionUnit? NumericResolution { get; set; }
	}

	public class DateTypeDescriptor<T> where T : class
	{
		internal DateType _Mapping = new DateType();

		public DateTypeDescriptor<T> Name(string name)
		{
			this._Mapping.Name = name;
			return this;
		}
		public DateTypeDescriptor<T> Name(Expression<Func<T, object>> objectPath)
		{
			this._Mapping.Name = objectPath;
			return this;
		}

		public DateTypeDescriptor<T> IndexName(string indexName)
		{
			this._Mapping.IndexName = indexName;
			return this;
		}
		public DateTypeDescriptor<T> Index(NonStringIndexOption index = NonStringIndexOption.No)
		{
			this._Mapping.Index = index;
			return this;
		}
		public DateTypeDescriptor<T> Store(bool store = true)
		{
			this._Mapping.Store = store;
			return this;
		}

		public DateTypeDescriptor<T> Format(string format)
		{
			this._Mapping.Format = format;
			return this;
		}

		public DateTypeDescriptor<T> Boost(double boost)
		{
			this._Mapping.Boost = boost;
			return this;
		}
		public DateTypeDescriptor<T> NullValue(DateTime nullValue)
		{
			this._Mapping.NullValue = nullValue;
			return this;
		}
			
		public DateTypeDescriptor<T> PrecisionStep(int precisionStep)
		{
			this._Mapping.PrecisionStep = precisionStep;
			return this;
		}

		//public DateTypeDescriptor<T> IncludeInAll(bool includeInAll = true)
		//{
		//	this._Mapping.IncludeInAll = includeInAll;
		//	return this;
		//}
		public DateTypeDescriptor<T> IgnoreMalformed(bool ignoreMalformed = true)
		{
			this._Mapping.IgnoreMalformed = ignoreMalformed;
			return this;
		}

		public DateTypeDescriptor<T> Fields(Func<PropertiesDescriptor<T>, PropertiesDescriptor<T>> fieldSelector)
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

		public DateTypeDescriptor<T> DocValues(bool docValues = true)
		{
			this._Mapping.DocValues = docValues;
			return this;
		}

		public DateTypeDescriptor<T> NumericResolution(NumericResolutionUnit unit)
		{
			this._Mapping.NumericResolution = unit;
			return this;
		}
	}
}