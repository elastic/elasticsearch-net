using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;

namespace Nest
{
	public interface IBooleanType : IElasticType
	{

	}
	
	[JsonObject(MemberSerialization.OptIn)]
	public class BooleanType : ElasticType, IBooleanType
	{
		public BooleanType() : base("boolean") { }
		
		[JsonProperty("index"), JsonConverter(typeof(StringEnumConverter))]
		public NonStringIndexOption? Index { get; set; }

		[JsonProperty("boost")]
		public double? Boost { get; set; }

		[JsonProperty("null_value")]
		public bool? NullValue { get; set; }
	}

	public class BooleanTypeDescriptor<T> where T : class
	{
		internal BooleanType _Mapping = new BooleanType();

		public BooleanTypeDescriptor<T> Name(string name)
		{
			this._Mapping.Name = name;
			return this;
		}
		public BooleanTypeDescriptor<T> Name(Expression<Func<T, object>> objectPath)
		{
			this._Mapping.Name = objectPath;
			return this;
		}

		public BooleanTypeDescriptor<T> IndexName(string indexName)
		{
			this._Mapping.IndexName = indexName;
			return this;
		}

		public BooleanTypeDescriptor<T> Index(NonStringIndexOption index = NonStringIndexOption.No)
		{
			this._Mapping.Index = index;
			return this;
		}

		public BooleanTypeDescriptor<T> Store(bool store = true)
		{
			this._Mapping.Store = store;
			return this;
		}

		public BooleanTypeDescriptor<T> Boost(double boost)
		{
			this._Mapping.Boost = boost;
			return this;
		}

		public BooleanTypeDescriptor<T> NullValue(bool nullValue)
		{
			this._Mapping.NullValue = nullValue;
			return this;
		}

		//public BooleanTypeDescriptor<T> IncludeInAll(bool includeInAll = true)
		//{
		//	this._Mapping.IncludeInAll = includeInAll;
		//	return this;
		//}

		public BooleanTypeDescriptor<T> CopyTo(params string[] fields)
		{
			this._Mapping.CopyTo = fields.Select(f => (FieldName)f);
			return this;
		}

		public BooleanTypeDescriptor<T> CopyTo(params Expression<Func<T, object>>[] objectPaths)
		{
			this._Mapping.CopyTo = objectPaths.Select(e => (FieldName)e);
			return this;
		}

		public BooleanTypeDescriptor<T> Fields(Func<PropertiesDescriptor<T>, PropertiesDescriptor<T>> fieldSelector)
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

		public BooleanTypeDescriptor<T> DocValues(bool docValues = true)
		{
			this._Mapping.DocValues = docValues;
			return this;
		}
	}
}