using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class BooleanMapping : MultiFieldMapping, IElasticType, IElasticCoreType
	{
		public BooleanMapping():base("boolean")
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

		[JsonProperty("boost")]
		public double? Boost { get; set; }

		[JsonProperty("null_value")]
		public bool? NullValue { get; set; }

		[JsonProperty("copy_to")]
		public IEnumerable<PropertyPathMarker> CopyTo { get; set; }

		[JsonProperty("doc_values")]
		public bool? DocValues { get; set; }
	}

	public class BooleanMappingDescriptor<T> where T : class
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
		public BooleanMappingDescriptor<T> Index(NonStringIndexOption index = NonStringIndexOption.No)
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

		public BooleanMappingDescriptor<T> Path(MultiFieldMappingPath path)
		{
			this._Mapping.Path = path.Value;
			return this;
		}

		public BooleanMappingDescriptor<T> Fields(Func<CorePropertiesDescriptor<T>, CorePropertiesDescriptor<T>> fieldSelector)
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

		public BooleanMappingDescriptor<T> DocValues(bool docValues = true)
		{
			this._Mapping.DocValues = docValues;
			return this;
		}
	}
}