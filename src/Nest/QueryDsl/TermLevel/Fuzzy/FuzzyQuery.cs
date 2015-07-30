using System;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Globalization;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IFuzzyQuery : IFieldNameQuery
	{
		[JsonProperty(PropertyName = "prefix_length")]
		int? PrefixLength { get; set; }
		
		[JsonProperty(PropertyName = "value")]
		object Value { get; set; }

		[JsonProperty(PropertyName = "fuzziness")]
		string Fuzziness { get; set; }

		[JsonProperty(PropertyName = "rewrite")]
		RewriteMultiTerm? Rewrite { get; set; }

		[JsonProperty(PropertyName = "max_expansions")]
		int? MaxExpansions { get; set; }

		[JsonProperty(PropertyName = "transpositions")]
		bool? Transpositions { get; set; }

		[JsonProperty(PropertyName = "unicode_aware")]
		bool? UnicodeAware { get; set; }
	}

	public class FuzzyQuery : FieldNameQueryBase, IFuzzyQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public string Fuzziness { get; set; }
		public RewriteMultiTerm? Rewrite { get; set; }
		public int? MaxExpansions { get; set; }
		public bool? Transpositions { get; set; }
		public bool? UnicodeAware { get; set; }
		public int? PrefixLength { get; set; }
		public object Value { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.Fuzzy = this;
		internal static bool IsConditionless(IFuzzyQuery q) => q.Field.IsConditionless() || q.Value == null;
	}

	public class FuzzyQueryDescriptor<T> 
		: FieldNameQueryDescriptorBase<FuzzyQueryDescriptor<T>, IFuzzyQuery, T>
		, IFuzzyQuery where T : class
	{
		bool IQuery.Conditionless => FuzzyQuery.IsConditionless(this);
		string IFuzzyQuery.Fuzziness { get; set; }
		object IFuzzyQuery.Value { get; set; }
		int? IFuzzyQuery.PrefixLength { get; set; }
		int? IFuzzyQuery.MaxExpansions { get; set; }
		bool? IFuzzyQuery.Transpositions { get; set; }
		bool? IFuzzyQuery.UnicodeAware { get; set; }
		RewriteMultiTerm? IFuzzyQuery.Rewrite { get; set; }

		public FuzzyQueryDescriptor<T> Fuzziness(double fuzziness) => 
			Assign(a => a.Fuzziness = fuzziness.ToString(CultureInfo.InvariantCulture));

		public FuzzyQueryDescriptor<T> Fuzziness(string fuzziness) => Assign(a => a.Fuzziness = fuzziness);

		public FuzzyQueryDescriptor<T> MaxExpansions(int maxExpansions) => Assign(a => a.MaxExpansions = maxExpansions);

		public FuzzyQueryDescriptor<T> PrefixLength(int prefixLength) => Assign(a => a.PrefixLength = prefixLength);

		public FuzzyQueryDescriptor<T> Transpositions(bool enable = true) => Assign(a => a.Transpositions = enable);

		public FuzzyQueryDescriptor<T> UnicodeAware(bool enable = true) => Assign(a => a.UnicodeAware = enable);

		public FuzzyQueryDescriptor<T> Rewrite(RewriteMultiTerm rewrite) => Assign(a => a.Rewrite = rewrite);

		public FuzzyQueryDescriptor<T> Value(string value) => Assign(a => a.Value = value);

		public FuzzyQueryDescriptor<T> Value(double value) => Assign(a => a.Value = value);

		public FuzzyQueryDescriptor<T> Value(DateTime value) => Assign(a => a.Value = value);
	}
}
