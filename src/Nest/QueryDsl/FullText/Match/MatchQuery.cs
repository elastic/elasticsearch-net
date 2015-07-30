using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof (FieldNameQueryConverter<MatchQuery>))]
	public interface IMatchQuery : IFieldNameQuery 
	{
		[JsonProperty(PropertyName = "type")]
		string Type { get; }

		[JsonProperty(PropertyName = "query")]
		string Query { get; set; }

		[JsonProperty(PropertyName = "analyzer")]
		string Analyzer { get; set; }

		[JsonProperty(PropertyName = "rewrite")]
		[JsonConverter(typeof (StringEnumConverter))]
		RewriteMultiTerm? Rewrite { get; set; }

		[JsonProperty(PropertyName = "fuzziness")]
		IFuzziness Fuzziness { get; set; }

		[JsonProperty(PropertyName = "fuzzy_transpositions")]
		bool? FuzzyTranspositions { get; set; }

		[JsonProperty(PropertyName = "cutoff_frequency")]
		double? CutoffFrequency { get; set; }

		[JsonProperty(PropertyName = "prefix_length")]
		int? PrefixLength { get; set; }

		[JsonProperty(PropertyName = "max_expansions")]
		int? MaxExpansions { get; set; }

		[JsonProperty(PropertyName = "slop")]
		int? Slop { get; set; }

		[JsonProperty(PropertyName = "lenient")]
		bool? Lenient { get; set; }
		
		[JsonProperty("minimum_should_match")]
		string MinimumShouldMatch { get; set; }

		[JsonProperty(PropertyName = "operator")]
		[JsonConverter(typeof (StringEnumConverter))]
		Operator? Operator { get; set; }
	}
	
	public class MatchQuery : FieldNameQueryBase, IMatchQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public string Type { get; set; }
		public string Query { get; set; }
		public string Analyzer { get; set; }
		public RewriteMultiTerm? Rewrite { get; set; }
		public IFuzziness Fuzziness { get; set; }
		public bool? FuzzyTranspositions { get; set; }
		public double? CutoffFrequency { get; set; }
		public int? PrefixLength { get; set; }
		public int? MaxExpansions { get; set; }
		public int? Slop { get; set; }
		public bool? Lenient { get; set; }
		public string MinimumShouldMatch { get; set; }
		public Operator? Operator { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.Match = this;

		internal static bool IsConditionless(IMatchQuery q) => q.Field.IsConditionless() || q.Query.IsNullOrEmpty();
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class MatchQueryDescriptor<T> 
		: FieldNameQueryDescriptorBase<MatchQueryDescriptor<T>, IMatchQuery, T> 
		, IMatchQuery where T : class
	{
		protected virtual string MatchQueryType { get { return null; } }
		bool IQuery.Conditionless => MatchQuery.IsConditionless(this);
		string IMatchQuery.Type { get { return MatchQueryType; } }
		string IMatchQuery.Query { get; set; }
		string IMatchQuery.Analyzer { get; set; }
		string IMatchQuery.MinimumShouldMatch { get; set; }
		RewriteMultiTerm? IMatchQuery.Rewrite { get; set; }
		IFuzziness IMatchQuery.Fuzziness { get; set; }
		bool? IMatchQuery.FuzzyTranspositions { get; set; }
		double? IMatchQuery.CutoffFrequency { get; set; }
		int? IMatchQuery.PrefixLength { get; set; }
		int? IMatchQuery.MaxExpansions { get; set; }
		int? IMatchQuery.Slop { get; set; }
		bool? IMatchQuery.Lenient { get; set; }
		Operator? IMatchQuery.Operator { get; set; }

		public MatchQueryDescriptor<T> Query(string query) => Assign(a => a.Query = query);

		public MatchQueryDescriptor<T> Lenient(bool lenient = true) => Assign(a => a.Lenient = lenient);

		public MatchQueryDescriptor<T> Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		public MatchQueryDescriptor<T> Fuzziness(double ratio) => Assign(a => a.Fuzziness = Nest.Fuzziness.Ratio(ratio));

		public MatchQueryDescriptor<T> Fuzziness() => Assign(a => a.Fuzziness = Nest.Fuzziness.Auto);

		public MatchQueryDescriptor<T> Fuzziness(int editDistance) => Assign(a => a.Fuzziness = Nest.Fuzziness.EditDistance(editDistance));

		public MatchQueryDescriptor<T> FuzzyTranspositions(bool fuzzyTranspositions = true) => Assign(a => a.FuzzyTranspositions = fuzzyTranspositions);

		public MatchQueryDescriptor<T> CutoffFrequency(double cutoffFrequency) => Assign(a => a.CutoffFrequency = cutoffFrequency);

		public MatchQueryDescriptor<T> Rewrite(RewriteMultiTerm rewrite) => Assign(a => a.Rewrite = rewrite);

		public MatchQueryDescriptor<T> PrefixLength(int prefixLength) => Assign(a => a.PrefixLength = prefixLength);

		public MatchQueryDescriptor<T> MaxExpansions(int maxExpansions) => Assign(a => a.MaxExpansions = maxExpansions);
		
		public MatchQueryDescriptor<T> Slop(int slop) => Assign(a => a.Slop = slop);
		
		public MatchQueryDescriptor<T> MinimumShouldMatch(string minimumShouldMatch) => Assign(a => a.MinimumShouldMatch = minimumShouldMatch);
	
		public MatchQueryDescriptor<T> Operator(Operator op) => Assign(a => a.Operator = op);
	}
}
