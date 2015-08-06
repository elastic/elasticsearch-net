using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IStringType : IElasticType
	{
		[JsonProperty("index")]
		FieldIndexOption? Index { get; set; }

		[JsonProperty("term_vector")]
		TermVectorOption? TermVector { get; set; }

		[JsonProperty("boost")]
		double? Boost { get; set; }

		[JsonProperty("null_value")]
		string NullValue { get; set; }

		[JsonProperty("norms")]
		NormsMapping Norms { get; set; }

		[JsonProperty("index_options")]
		IndexOptions? IndexOptions { get; set; }

		[JsonProperty("analyzer")]
		string Analyzer { get; set; }

		[JsonProperty("search_analyzer")]
		string SearchAnalyzer { get; set; }

		[JsonProperty("include_in_all")]
		bool? IncludeInAll { get; set; }

		[JsonProperty("ignore_above")]
		int? IgnoreAbove { get; set; }

		[JsonProperty("position_offset_gap")]
		int? PositionOffsetGap { get; set; }

		[JsonProperty("fielddata")]
		IStringFielddata Fielddata { get; set; }
	}

	public class StringType : ElasticType, IStringType
	{
		public StringType() : base("string") { }

		public FieldIndexOption? Index { get; set; }
		public TermVectorOption? TermVector { get; set; }
		public double? Boost { get; set; }
		public string NullValue { get; set; }
		public NormsMapping Norms { get; set; }
		public IndexOptions? IndexOptions { get; set; }
		public string Analyzer { get; set; }
		public string SearchAnalyzer { get; set; }
		public bool? IncludeInAll { get; set; }
		public int? IgnoreAbove { get; set; }
		public int? PositionOffsetGap { get; set; }
		public IStringFielddata Fielddata { get; set; }
	}

	public class StringTypeDescriptor<T> 
		: TypeDescriptorBase<StringTypeDescriptor<T>, IStringType, T>, IStringType
		where T : class
	{
		FieldIndexOption? IStringType.Index { get; set; }
		TermVectorOption? IStringType.TermVector { get; set; }
		double? IStringType.Boost { get; set; }
		string IStringType.NullValue { get; set; }
		NormsMapping IStringType.Norms { get; set; }
		IndexOptions? IStringType.IndexOptions { get; set; }
		string IStringType.Analyzer { get; set; }
		string IStringType.SearchAnalyzer { get; set; }
		bool? IStringType.IncludeInAll { get; set; }
		int? IStringType.IgnoreAbove { get; set; }
		int? IStringType.PositionOffsetGap { get; set; }
		IStringFielddata IStringType.Fielddata { get; set; }
		/// <summary>
		/// Shortcut into .Index(FieldIndexOption.NotAnalyzed)
		/// </summary>
		public StringTypeDescriptor<T> NotAnalyzed() => Index(FieldIndexOption.NotAnalyzed);

		public StringTypeDescriptor<T> Index(FieldIndexOption index) => Assign(a => a.Index = index);

		public StringTypeDescriptor<T> TermVector(TermVectorOption termVector) => Assign(a => a.TermVector = termVector);

		public StringTypeDescriptor<T> Boost(double boost) => Assign(a => a.Boost = boost);

		public StringTypeDescriptor<T> NullValue(string nullValue) => Assign(a => a.NullValue = nullValue);

		public StringTypeDescriptor<T> IndexOptions(IndexOptions indexOptions) => Assign(a => a.IndexOptions = indexOptions);

		public StringTypeDescriptor<T> Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		public StringTypeDescriptor<T> SearchAnalyzer(string searchAnalyzer) => Assign(a => a.SearchAnalyzer = searchAnalyzer);

		public StringTypeDescriptor<T> Norms(NormsMapping normsMapping) => Assign(a => a.Norms = normsMapping);

		public StringTypeDescriptor<T> IgnoreAbove(int ignoreAbove) => Assign(a => a.IgnoreAbove = ignoreAbove);

		public StringTypeDescriptor<T> IncludeInAll(bool includeInAll = true) => Assign(a => a.IncludeInAll = includeInAll);

		public StringTypeDescriptor<T> PositionOffsetGap(int positionOffsetGap) => Assign(a => a.PositionOffsetGap = positionOffsetGap);

		public StringTypeDescriptor<T> Fielddata(Func<StringFielddataDescriptor, IStringFielddata> selector) =>
			Assign(a => selector(new StringFielddataDescriptor()));
	}
}