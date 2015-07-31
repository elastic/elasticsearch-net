using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;

namespace Nest
{
	public interface IStringType : IElasticType
	{

	}

	[JsonObject(MemberSerialization.OptIn)]
	public class StringType : ElasticType, IStringType
	{
		public StringType() : base("string") { }

		[JsonProperty("analyzer")]
		public string Analyzer { get; set; }

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

		[JsonProperty("position_offset_gap")]
		public int? PositionOffsetGap { get; set; }
	}


	public class StringTypeDescriptor<T> where T : class
	{
		internal StringType _Mapping = new StringType();

		public StringTypeDescriptor<T> Name(string name)
		{
			this._Mapping.Name = name;
			return this;
		}

		public StringTypeDescriptor<T> Name(Expression<Func<T, object>> objectPath)
		{
			this._Mapping.Name = objectPath;
			return this;
		}
		
		/// <summary>
		/// Shortcut into .Index(FieldIndexOption.NotAnalyzed)
		/// </summary>
		public StringTypeDescriptor<T> NotAnalyzed()
		{
			return this.Index(FieldIndexOption.NotAnalyzed);
		}

		//public StringTypeDescriptor<T> Similarity(string similarity)
		//{
		//	this._Mapping.Similarity = similarity;
		//	return this;
		//}

		public StringTypeDescriptor<T> IndexName(string indexName)
		{
			this._Mapping.IndexName = indexName;
			return this;
		}
		public StringTypeDescriptor<T> Index(FieldIndexOption fieldIndexOption)
		{
			this._Mapping.Index = fieldIndexOption;
			return this;
		}
		public StringTypeDescriptor<T> Store(bool store = true)
		{
			this._Mapping.Store = store;
			return this;
		}

		public StringTypeDescriptor<T> TermVector(TermVectorOption termVector)
		{
			this._Mapping.TermVector = termVector;
			return this;
		}
		public StringTypeDescriptor<T> Boost(double boost)
		{
			this._Mapping.Boost = boost;
			return this;
		}
		public StringTypeDescriptor<T> NullValue(string nullValue)
		{
			this._Mapping.NullValue = nullValue;
			return this;
		}
		public StringTypeDescriptor<T> OmitNorms(bool omitNorms = true)
		{
			this._Mapping.OmitNorms = omitNorms;
			return this;
		}
	
		public StringTypeDescriptor<T> IndexOptions(IndexOptions indexOptions)
		{
			this._Mapping.IndexOptions = indexOptions;
			return this;
		}
		public StringTypeDescriptor<T> Analyzer(string analyzer)
		{
			this._Mapping.Analyzer = analyzer;
			return this;
		}
		public StringTypeDescriptor<T> IndexAnalyzer(string indexAnalyzer)
		{
			this._Mapping.IndexAnalyzer = indexAnalyzer;
			return this;
		}
		public StringTypeDescriptor<T> SearchAnalyzer(string searchAnalyzer)
		{
			this._Mapping.SearchAnalyzer = searchAnalyzer;
			return this;
		}
		public StringTypeDescriptor<T> Norms(NormsMapping normsMapping)
		{
			this._Mapping.Norms = normsMapping;
			return this;
		}
		public StringTypeDescriptor<T> IgnoreAbove(int? ignoreAbove)
		{
			this._Mapping.IgnoreAbove = ignoreAbove;
			return this;
		}
		public StringTypeDescriptor<T> DocValues(bool docValues = true)
		{
			this._Mapping.DocValues = docValues;
			return this;
		}
		//public StringTypeDescriptor<T> IncludeInAll(bool includeInAll = true)
		//{
		//	this._Mapping.IncludeInAll = includeInAll;
		//	return this;
		//}

		public StringTypeDescriptor<T> PositionOffsetGap(int positionOffsetGap)
		{
			this._Mapping.PositionOffsetGap = positionOffsetGap;
			return this;
		}

		public StringTypeDescriptor<T> CopyTo(params string[] fields)
		{
			this._Mapping.CopyTo = fields.Select(f => (FieldName)f);
			return this;
		}

		public StringTypeDescriptor<T> CopyTo(params Expression<Func<T, object>>[] objectPaths)
		{
			this._Mapping.CopyTo = objectPaths.Select(e => (FieldName)e);
			return this;
		}

		public StringTypeDescriptor<T> Fields(Func<PropertiesDescriptor<T>, PropertiesDescriptor<T>> fieldSelector)
		{
			fieldSelector.ThrowIfNull("fieldSelector");
			var properties = fieldSelector(new PropertiesDescriptor<T>());
			foreach (var p in properties.Properties)
			{
				var value = p.Value as IElasticType;
				if (value == null)
					continue;
				if (_Mapping.Fields == null) _Mapping.Fields = new Dictionary<FieldName, IElasticType>();
				_Mapping.Fields[p.Key] = value;
			}
			return this;
		}

		//public StringTypeDescriptor<T> FieldData(Func<FieldDataStringMappingDescriptor, FieldDataStringMappingDescriptor> fieldDataSelector)
		//{
		//	fieldDataSelector.ThrowIfNull("fieldDataSelector");
		//	var selector = fieldDataSelector(new FieldDataStringMappingDescriptor());
		//	this._Mapping.FieldData = selector.FieldData;
		//	return this;
		//}

		//public StringTypeDescriptor<T> FieldData(FieldDataStringMapping fieldData)
		//{
		//	this._Mapping.FieldData = fieldData;
		//	return this;
		//}
	}
}