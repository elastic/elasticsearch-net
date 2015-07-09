using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class DateMapping : MultiFieldMapping, IElasticType, IElasticCoreType
	{
		public DateMapping():base("date")
		{
		}

		/// <summary>
		/// The name of the field that will be stored in the index. Defaults to the property/field name.
		/// </summary>
		[JsonProperty("index_name")]
		public string IndexName { get; set; }

		[JsonProperty("store")]
		public bool? Store { get; set; }

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

		[JsonProperty("doc_values")]
		public bool? DocValues { get; set; }

		[JsonProperty("numeric_resolution")]
		public NumericResolutionUnit? NumericResolution { get; set; }
	}

	public class DateMappingDescriptor<T> where T : class
	{
		internal DateMapping _Mapping = new DateMapping();

		public DateMappingDescriptor<T> Name(string name)
		{
			this._Mapping.Name = name;
			return this;
		}
		public DateMappingDescriptor<T> Name(Expression<Func<T, object>> objectPath)
		{
			this._Mapping.Name = objectPath;
			return this;
		}

		public DateMappingDescriptor<T> IndexName(string indexName)
		{
			this._Mapping.IndexName = indexName;
			return this;
		}
		public DateMappingDescriptor<T> Index(NonStringIndexOption index = NonStringIndexOption.No)
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

		public DateMappingDescriptor<T> Path(MultiFieldMappingPath path)
		{
			this._Mapping.Path = path.Value;
			return this;
		}

		public DateMappingDescriptor<T> Fields(Func<CorePropertiesDescriptor<T>, CorePropertiesDescriptor<T>> fieldSelector)
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

		public DateMappingDescriptor<T> DocValues(bool docValues = true)
		{
			this._Mapping.DocValues = docValues;
			return this;
		}

		public DateMappingDescriptor<T> NumericResolution(NumericResolutionUnit unit)
		{
			this._Mapping.NumericResolution = unit;
			return this;
		}
	}
}