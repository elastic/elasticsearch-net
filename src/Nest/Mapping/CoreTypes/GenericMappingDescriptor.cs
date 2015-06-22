using System;
using System.Linq.Expressions;

namespace Nest
{
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
		public GenericMappingDescriptor<T> IncludeInAll(bool includeInAll = true)
		{
			this._Mapping.IncludeInAll = includeInAll;
			return this;
		}

		public GenericMappingDescriptor<T> Path(MultiFieldMappingPath path)
		{
			this._Mapping.Path = path.Value;
			return this;
		}

		public GenericMappingDescriptor<T> Fields(Func<CorePropertiesDescriptor<T>, CorePropertiesDescriptor<T>> fieldSelector)
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