using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(FuzzyQueryFormatter))]
	public interface IFuzzyQuery : IFieldNameQuery
	{
		[DataMember(Name ="max_expansions")]
		int? MaxExpansions { get; set; }

		[DataMember(Name ="prefix_length")]
		int? PrefixLength { get; set; }

		[DataMember(Name ="rewrite")]
		MultiTermQueryRewrite Rewrite { get; set; }

		[DataMember(Name ="transpositions")]
		bool? Transpositions { get; set; }
	}

	public interface IFuzzyQuery<TValue, TFuzziness> : IFuzzyQuery
	{
		[DataMember(Name ="fuzziness")]
		TFuzziness Fuzziness { get; set; }

		[DataMember(Name ="value")]
		TValue Value { get; set; }
	}

	internal static class FuzzyQueryBase
	{
		internal static bool IsConditionless<TValue, TFuzziness>(IFuzzyQuery<TValue, TFuzziness> fuzzy) =>
			fuzzy == null || fuzzy.Value == null || fuzzy.Field == null;
	}

	public abstract class FuzzyQueryBase<TValue, TFuzziness> : FieldNameQueryBase, IFuzzyQuery<TValue, TFuzziness>
	{
		public TFuzziness Fuzziness { get; set; }

		public int? MaxExpansions { get; set; }
		public int? PrefixLength { get; set; }

		public MultiTermQueryRewrite Rewrite { get; set; }

		public bool? Transpositions { get; set; }

		public TValue Value { get; set; }

		protected override bool Conditionless => FuzzyQueryBase.IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Fuzzy = this;
	}

	public abstract class FuzzyQueryDescriptorBase<TDescriptor, T, TValue, TFuzziness>
		: FieldNameQueryDescriptorBase<TDescriptor, IFuzzyQuery<TValue, TFuzziness>, T>, IFuzzyQuery<TValue, TFuzziness>
		where T : class
		where TDescriptor : FieldNameQueryDescriptorBase<TDescriptor, IFuzzyQuery<TValue, TFuzziness>, T>, IFuzzyQuery<TValue, TFuzziness>
	{
		protected override bool Conditionless => FuzzyQueryBase.IsConditionless(this);
		TFuzziness IFuzzyQuery<TValue, TFuzziness>.Fuzziness { get; set; }
		int? IFuzzyQuery.MaxExpansions { get; set; }
		int? IFuzzyQuery.PrefixLength { get; set; }
		MultiTermQueryRewrite IFuzzyQuery.Rewrite { get; set; }
		bool? IFuzzyQuery.Transpositions { get; set; }
		TValue IFuzzyQuery<TValue, TFuzziness>.Value { get; set; }

		public TDescriptor MaxExpansions(int? maxExpansions) => Assign(a => a.MaxExpansions = maxExpansions);

		public TDescriptor PrefixLength(int? prefixLength) => Assign(a => a.PrefixLength = prefixLength);

		public TDescriptor Transpositions(bool? enable = true) => Assign(a => a.Transpositions = enable);

		public TDescriptor Rewrite(MultiTermQueryRewrite rewrite) => Assign(a => Self.Rewrite = rewrite);
	}
}
