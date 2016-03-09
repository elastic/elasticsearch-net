using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(FuzzyQueryJsonConverter))]
	public interface IFuzzyQuery : IFieldNameQuery
	{
		[JsonProperty(PropertyName = "prefix_length")]
		int? PrefixLength { get; set; }
		
		[JsonProperty(PropertyName = "rewrite")]
		RewriteMultiTerm? Rewrite { get; set; }

		[JsonProperty(PropertyName = "max_expansions")]
		int? MaxExpansions { get; set; }

		[JsonProperty(PropertyName = "transpositions")]
		bool? Transpositions { get; set; }
	}
	public interface IFuzzyQuery<TValue, TFuzziness> : IFuzzyQuery
	{
		[JsonProperty(PropertyName = "value")]
		TValue Value { get; set; }

		[JsonProperty(PropertyName = "fuzziness")]
		TFuzziness Fuzziness { get; set; }
	}

	internal static class FuzzyQueryBase
	{
		internal static bool IsConditionless<TValue, TFuzziness>(IFuzzyQuery<TValue, TFuzziness> fuzzy) => 
			fuzzy == null || fuzzy.Value == null || fuzzy.Field == null;
	}

	public abstract class FuzzyQueryBase<TValue, TFuzziness> : FieldNameQueryBase, IFuzzyQuery<TValue, TFuzziness>
	{
		public int? PrefixLength { get; set; }
		
		public TValue Value { get; set; }

		public TFuzziness Fuzziness { get; set; }

		public RewriteMultiTerm? Rewrite { get; set; }

		public int? MaxExpansions { get; set; }

		public bool? Transpositions { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Fuzzy = this;

		protected override bool Conditionless => FuzzyQueryBase.IsConditionless(this);

	}

	public abstract class FuzzyQueryDescriptorBase<TDescriptor, T, TValue, TFuzziness> 
		: FieldNameQueryDescriptorBase<TDescriptor, IFuzzyQuery<TValue, TFuzziness>, T> , IFuzzyQuery<TValue, TFuzziness>
		where T : class
		where TDescriptor : FieldNameQueryDescriptorBase<TDescriptor, IFuzzyQuery<TValue, TFuzziness>, T>, IFuzzyQuery<TValue, TFuzziness>
	{
		protected override bool Conditionless => FuzzyQueryBase.IsConditionless(this);
		int? IFuzzyQuery.PrefixLength { get; set; }
		int? IFuzzyQuery.MaxExpansions { get; set; }
		bool? IFuzzyQuery.Transpositions { get; set; }
		RewriteMultiTerm? IFuzzyQuery.Rewrite { get; set; }
		TFuzziness IFuzzyQuery<TValue, TFuzziness>.Fuzziness { get; set; }
		TValue IFuzzyQuery<TValue, TFuzziness>.Value { get; set; }

		public TDescriptor MaxExpansions(int? maxExpansions) => Assign(a => a.MaxExpansions = maxExpansions);

		public TDescriptor PrefixLength(int? prefixLength) => Assign(a => a.PrefixLength = prefixLength);

		public TDescriptor Transpositions(bool? enable = true) => Assign(a => a.Transpositions = enable);

		public TDescriptor Rewrite(RewriteMultiTerm? rewrite) => Assign(a => a.Rewrite = rewrite);

	}
}
