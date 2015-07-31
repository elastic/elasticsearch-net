using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{

	public interface IGenericMapping : IElasticType
	{

	}

	/// <summary>
	/// Sometimes you need a generic type mapping, i.e when using dynamic templates 
	/// in order to specify "{dynamic_template}" the type, or if you have some plugin that exposes a new type.
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public class GenericMapping : ElasticType, IGenericMapping
	{
		public GenericMapping() : base(null)
		{
		}

		[JsonProperty("index")]
		public string Index { get; set; }

		[JsonProperty("null_value")]
		public object NullValue { get; set; }

		[JsonProperty("boost")]
		public double? Boost { get; set; }

		[JsonProperty("term_vector")]
		public string TermVector { get; set; }

		[JsonProperty("omit_norms")]
		public bool? OmitNorms { get; set; }

		[JsonProperty("omit_term_freq_and_positions")]
		public bool? OmitTermFrequencyAndPositions { get; set; }

		[JsonProperty("index_analyzer")]
		public string IndexAnalyzer { get; set; }

		[JsonProperty("search_analyzer")]
		public string SearchAnalyzer { get; set; }

	}


	public class GenericMappingDescriptor<T> where T : class
	{
		internal GenericMapping _Mapping = new GenericMapping();

		public GenericMappingDescriptor<T> Name(string name, bool noNameProperty = false)
		{
			if (!noNameProperty)
				this._Mapping.Name = name;

			this._Mapping.Name = name;
			return this;
		}
		public GenericMappingDescriptor<T> Name(Expression<Func<T, object>> objectPath, bool noNameProperty = false)
		{
			this._Mapping.Name = objectPath;
			return this;
		}
		public GenericMappingDescriptor<T> Type(string type)
		{
			this._Mapping.Type = type;
			return this;
		}
		public GenericMappingDescriptor<T> IndexName(string indexName)
		{
			this._Mapping.IndexName = indexName;
			return this;
		}
		public GenericMappingDescriptor<T> Index(string fieldIndexOption)
		{
			this._Mapping.Index = fieldIndexOption;
			return this;
		}
		public GenericMappingDescriptor<T> Store(bool store = true)
		{
			this._Mapping.Store = store;
			return this;
		}
		public GenericMappingDescriptor<T> Boost(double boost)
		{
			this._Mapping.Boost = boost;
			return this;
		}
		public GenericMappingDescriptor<T> NullValue(string nullValue)
		{
			this._Mapping.NullValue = nullValue;
			return this;
		}
		public GenericMappingDescriptor<T> OmitNorms(bool omitNorms = true)
		{
			this._Mapping.OmitNorms = omitNorms;
			return this;
		}
		public GenericMappingDescriptor<T> OmitTermFrequencyAndPositions(bool omitTermFrequencyAndPositions = true)
		{
			this._Mapping.OmitTermFrequencyAndPositions = omitTermFrequencyAndPositions;
			return this;
		}
		public GenericMappingDescriptor<T> IndexAnalyzer(string indexAnalyzer)
		{
			this._Mapping.IndexAnalyzer = indexAnalyzer;
			return this;
		}
		public GenericMappingDescriptor<T> SearchAnalyzer(string searchAnalyzer)
		{
			this._Mapping.SearchAnalyzer = searchAnalyzer;
			return this;
		}
		//public GenericMappingDescriptor<T> IncludeInAll(bool includeInAll = true)
		//{
		//	this._Mapping.IncludeInAll = includeInAll;
		//	return this;
		//}

		public GenericMappingDescriptor<T> Fields(Func<PropertiesDescriptor<T>, PropertiesDescriptor<T>> fieldSelector)
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
	}
}