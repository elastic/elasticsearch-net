using System;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	public class StringMappingDescriptor<T>
	{
		internal StringMapping _Mapping = new StringMapping();

		public StringMappingDescriptor<T> Name(string name)
		{
			this._Mapping.TypeNameMarker = name;
			return this;
		}
		public StringMappingDescriptor<T> Name(Expression<Func<T, object>> objectPath)
		{
			var name = new PropertyNameResolver().ResolveToLastToken(objectPath);
			this._Mapping.TypeNameMarker = name;
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
		public StringMappingDescriptor<T> OmitTermFrequencyAndPositions(bool omitTermFrequencyAndPositions = true)
		{
			this._Mapping.OmitTermFrequencyAndPositions = omitTermFrequencyAndPositions;
			return this;
		}
		public StringMappingDescriptor<T> IndexOptions(IndexOptions indexOptions)
		{
			this._Mapping.IndexOptions = indexOptions;
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

	}
}