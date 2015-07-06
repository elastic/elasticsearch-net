using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class StringMapping : MultiFieldMapping, IElasticType, IElasticCoreType
	{
		public StringMapping():base("string")
		{
		}

		/// <summary>
		/// The name of the field that will be stored in the index. Defaults to the property/field name.
		/// </summary>
		[JsonProperty("index_name")]
		public string IndexName { get; set; }

		[JsonProperty("analyzer")]
		public string Analyzer { get; set; }

		[JsonProperty("store")]
		public bool? Store { get; set; }

		[JsonProperty("index"), JsonConverter(typeof(StringEnumConverter))]
		public FieldIndexOption? Index { get; set; }

		[JsonProperty("term_vector"), JsonConverter(typeof(StringEnumConverter))]
		public TermVectorOption? TermVector { get; set; }

		[JsonProperty("boost")]
		public double? Boost { get; set; }

		[JsonProperty("null_value")]
		public string NullValue { get; set; }

		[JsonProperty("norms")]
		public NormsMapping Norms { get; set; }

		[JsonProperty("omit_norms")]
		public bool? OmitNorms { get; set; }

		[JsonProperty("index_options"), JsonConverter(typeof(StringEnumConverter))]
		public IndexOptions? IndexOptions { get; set; }

		[JsonProperty("index_analyzer")]
		public string IndexAnalyzer { get; set; }

		[JsonProperty("ignore_above")]
		public int? IgnoreAbove { get; set; }

		[JsonProperty("search_analyzer")]
		public string SearchAnalyzer { get; set; }

		[JsonProperty("doc_values")]
		public bool? DocValues { get; set; }

		[JsonProperty("position_offset_gap")]
		public int? PositionOffsetGap { get; set; }

		[JsonProperty("copy_to")]
		public IEnumerable<PropertyPathMarker> CopyTo { get; set; }

		[JsonProperty("fielddata")]
		public FieldDataStringMapping FieldData { get; set; }
	}


	public class StringMappingDescriptor<T> where T : class
	{
		internal StringMapping _Mapping = new StringMapping();

		public StringMappingDescriptor<T> Name(string name)
		{
			this._Mapping.Name = name;
			return this;
		}
		public StringMappingDescriptor<T> Name(Expression<Func<T, object>> objectPath)
		{
			this._Mapping.Name = objectPath;
			return this;
		}
		

		/// <summary>
		/// Shortcut into .Index(FieldIndexOption.NotAnalyzed)
		/// </summary>
		public StringMappingDescriptor<T> NotAnalyzed()
		{
			return this.Index(FieldIndexOption.NotAnalyzed);
		}

		public StringMappingDescriptor<T> Similarity(string similarity)
		{
			this._Mapping.Similarity = similarity;
			return this;
		}

		public StringMappingDescriptor<T> IndexName(string indexName)
		{
			this._Mapping.IndexName = indexName;
			return this;
		}
		public StringMappingDescriptor<T> Index(FieldIndexOption fieldIndexOption)
		{
			this._Mapping.Index = fieldIndexOption;
			return this;
		}
		public StringMappingDescriptor<T> Store(bool store = true)
		{
			this._Mapping.Store = store;
			return this;
		}

		public StringMappingDescriptor<T> TermVector(TermVectorOption termVector)
		{
			this._Mapping.TermVector = termVector;
			return this;
		}
		public StringMappingDescriptor<T> Boost(double boost)
		{
			this._Mapping.Boost = boost;
			return this;
		}
		public StringMappingDescriptor<T> NullValue(string nullValue)
		{
			this._Mapping.NullValue = nullValue;
			return this;
		}
		public StringMappingDescriptor<T> OmitNorms(bool omitNorms = true)
		{
			this._Mapping.OmitNorms = omitNorms;
			return this;
		}
	
		public StringMappingDescriptor<T> IndexOptions(IndexOptions indexOptions)
		{
			this._Mapping.IndexOptions = indexOptions;
			return this;
		}
		public StringMappingDescriptor<T> Analyzer(string analyzer)
		{
			this._Mapping.Analyzer = analyzer;
			return this;
		}
		public StringMappingDescriptor<T> IndexAnalyzer(string indexAnalyzer)
		{
			this._Mapping.IndexAnalyzer = indexAnalyzer;
			return this;
		}
		public StringMappingDescriptor<T> SearchAnalyzer(string searchAnalyzer)
		{
			this._Mapping.SearchAnalyzer = searchAnalyzer;
			return this;
		}
		public StringMappingDescriptor<T> Norms(NormsMapping normsMapping)
		{
			this._Mapping.Norms = normsMapping;
			return this;
		}
		public StringMappingDescriptor<T> IgnoreAbove(int? ignoreAbove)
		{
			this._Mapping.IgnoreAbove = ignoreAbove;
			return this;
		}
		public StringMappingDescriptor<T> DocValues(bool docValues = true)
		{
			this._Mapping.DocValues = docValues;
			return this;
		}
		public StringMappingDescriptor<T> IncludeInAll(bool includeInAll = true)
		{
			this._Mapping.IncludeInAll = includeInAll;
			return this;
		}
		public StringMappingDescriptor<T> PositionOffsetGap(int positionOffsetGap)
		{
			this._Mapping.PositionOffsetGap = positionOffsetGap;
			return this;
		}

		public StringMappingDescriptor<T> CopyTo(params string[] fields)
		{
			this._Mapping.CopyTo = fields.Select(f => (PropertyPathMarker)f);
			return this;
		}

		public StringMappingDescriptor<T> CopyTo(params Expression<Func<T, object>>[] objectPaths)
		{
			this._Mapping.CopyTo = objectPaths.Select(e => (PropertyPathMarker)e);
			return this;
		}

		public StringMappingDescriptor<T> Path(MultiFieldMappingPath path)
		{
			this._Mapping.Path = path.Value;
			return this;
		}

		public StringMappingDescriptor<T> Fields(Func<CorePropertiesDescriptor<T>, CorePropertiesDescriptor<T>> fieldSelector)
		{
			fieldSelector.ThrowIfNull("fieldSelector");
			var properties = fieldSelector(new CorePropertiesDescriptor<T>());
			foreach (var p in properties.Properties)
			{
				var value = p.Value as IElasticCoreType;
				if (value == null)
					continue;
				if (_Mapping.Fields == null) _Mapping.Fields = new Dictionary<PropertyNameMarker, IElasticCoreType>();
				_Mapping.Fields[p.Key] = value;
			}
			return this;
		}

		public StringMappingDescriptor<T> FieldData(Func<FieldDataStringMappingDescriptor, FieldDataStringMappingDescriptor> fieldDataSelector)
		{
			fieldDataSelector.ThrowIfNull("fieldDataSelector");
			var selector = fieldDataSelector(new FieldDataStringMappingDescriptor());
			this._Mapping.FieldData = selector.FieldData;
			return this;
		}

		public StringMappingDescriptor<T> FieldData(FieldDataStringMapping fieldData)
		{
			this._Mapping.FieldData = fieldData;
			return this;
		}
	}
}