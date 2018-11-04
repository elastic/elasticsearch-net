using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(FuzzyQueryJsonConverter))]
	public interface IFuzzyQuery : IFieldNameQuery
	{
		[JsonProperty("max_expansions")]
		int? MaxExpansions { get; set; }

		[JsonProperty("rewrite")]
		MultiTermQueryRewrite MultiTermQueryRewrite { get; set; }

		[JsonProperty("prefix_length")]
		int? PrefixLength { get; set; }

		[JsonIgnore]
		[Obsolete("Use MultiTermQueryRewrite")]
		RewriteMultiTerm? Rewrite { get; set; }

		[JsonProperty("transpositions")]
		bool? Transpositions { get; set; }
	}

	public interface IFuzzyQuery<TValue, TFuzziness> : IFuzzyQuery
	{
		[JsonProperty("fuzziness")]
		TFuzziness Fuzziness { get; set; }

		[JsonProperty("value")]
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

		public MultiTermQueryRewrite MultiTermQueryRewrite { get; set; }
		public int? PrefixLength { get; set; }

		[Obsolete("Use MultiTermQueryRewrite")]
		public RewriteMultiTerm? Rewrite
		{
			get => MultiTermQueryRewrite?.Rewrite;
			set => MultiTermQueryRewrite = value == null ? null : new MultiTermQueryRewrite(value.Value);
		}

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

		MultiTermQueryRewrite IFuzzyQuery.MultiTermQueryRewrite { get; set; }
		int? IFuzzyQuery.PrefixLength { get; set; }

		[Obsolete("Use MultiTermQueryRewrite")]
		RewriteMultiTerm? IFuzzyQuery.Rewrite
		{
			get => Self.MultiTermQueryRewrite?.Rewrite;
			set => Self.MultiTermQueryRewrite = value == null ? null : new MultiTermQueryRewrite(value.Value);
		}

		bool? IFuzzyQuery.Transpositions { get; set; }
		TValue IFuzzyQuery<TValue, TFuzziness>.Value { get; set; }

		public TDescriptor MaxExpansions(int? maxExpansions) => Assign(a => a.MaxExpansions = maxExpansions);

		public TDescriptor PrefixLength(int? prefixLength) => Assign(a => a.PrefixLength = prefixLength);

		public TDescriptor Transpositions(bool? enable = true) => Assign(a => a.Transpositions = enable);

		[Obsolete("Use Rewrite(MultiTermQueryRewrite rewrite)")]
		public TDescriptor Rewrite(RewriteMultiTerm? rewrite) =>
			Assign(a =>
			{
				a.MultiTermQueryRewrite = rewrite != null
					? new MultiTermQueryRewrite(rewrite.Value)
					: null;
			});

		public TDescriptor Rewrite(MultiTermQueryRewrite rewrite) => Assign(a => Self.MultiTermQueryRewrite = rewrite);
	}
}
