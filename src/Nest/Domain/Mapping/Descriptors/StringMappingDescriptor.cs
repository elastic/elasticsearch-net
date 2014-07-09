using System;
using System.Linq;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	public class StringMappingDescriptor<T>
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
		public StringMappingDescriptor<T> IgnoreAbove(string ignoreAbove)
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
	}
}