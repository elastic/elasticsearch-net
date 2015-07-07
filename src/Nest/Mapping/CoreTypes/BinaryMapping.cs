using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class BinaryMapping : MultiFieldMapping, IElasticType, IElasticCoreType
	{
		public BinaryMapping()
			: base("binary")
		{
		}

		/// <summary>
		/// The name of the field that will be stored in the index. Defaults to the property/field name.
		/// </summary>
		[JsonProperty("index_name")]
		public string IndexName { get; set; }

		[JsonProperty("doc_values")]
		public bool? DocValues { get; set; }

		[JsonProperty("store")]
		public bool? Store { get; set; }

		[JsonProperty("compress")]
		public bool? Compress { get; set; }

		[JsonProperty("compress_threshold")]
		public string CompressThreshold { get; set; }

		[JsonProperty("copy_to")]
		public IEnumerable<PropertyPath> CopyTo { get; set; }
	}

	public class BinaryMappingDescriptor<T> where T : class
	{
		internal BinaryMapping _Mapping = new BinaryMapping();

		public BinaryMappingDescriptor<T> Name(string name)
		{
			this._Mapping.Name = name;
			return this;
		}
		public BinaryMappingDescriptor<T> Name(Expression<Func<T, object>> objectPath)
		{
			this._Mapping.Name = objectPath;
			return this;
		}

		public BinaryMappingDescriptor<T> IndexName(string indexName)
		{
			this._Mapping.IndexName = indexName;
			return this;
		}

		public BinaryMappingDescriptor<T> DocValues(bool docValues = true)
		{
			this._Mapping.DocValues = docValues;
			return this;
		}

		public BinaryMappingDescriptor<T> Store(bool store = true)
		{
			this._Mapping.Store = store;
			return this;
		}

		public BinaryMappingDescriptor<T> Compress(bool compress = true )
		{
			this._Mapping.Compress = compress;
			return this;
		}

		public BinaryMappingDescriptor<T> CompressThreshold(string compressThreshold)
		{
			this._Mapping.CompressThreshold = compressThreshold;
			return this;
		}

		public BinaryMappingDescriptor<T> CopyTo(params string[] fields)
		{
			this._Mapping.CopyTo = fields.Select(f => (PropertyPath)f);
			return this;
		}

		public BinaryMappingDescriptor<T> CopyTo(params Expression<Func<T, object>>[] objectPaths)
		{
			this._Mapping.CopyTo = objectPaths.Select(e => (PropertyPath)e);
			return this;
		}

		public BinaryMappingDescriptor<T> Path(MultiFieldMappingPath path)
		{
			this._Mapping.Path = path.Value;
			return this;
		}

		public BinaryMappingDescriptor<T> Fields(Func<CorePropertiesDescriptor<T>, CorePropertiesDescriptor<T>> fieldSelector)
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
	}
}