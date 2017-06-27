using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(FuzzyQueryJsonConverter))]
	public interface IFuzzyQuery : IFieldNameQuery
	{
		[JsonProperty("prefix_length")]
		int? PrefixLength { get; set; }

		[JsonIgnore]
		[Obsolete("Use MultiTermQueryRewrite")]
		RewriteMultiTerm? Rewrite { get; set; }

		[JsonProperty("rewrite")]
		MultiTermQueryRewrite MultiTermQueryRewrite { get; set; }

		[JsonProperty("max_expansions")]
		int? MaxExpansions { get; set; }

		[JsonProperty("transpositions")]
		bool? Transpositions { get; set; }
	}
	public interface IFuzzyQuery<TValue, TFuzziness> : IFuzzyQuery
	{
		[JsonProperty("value")]
		TValue Value { get; set; }

		[JsonProperty("fuzziness")]
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

		[Obsolete("Use MultiTermQueryRewrite")]
		public RewriteMultiTerm? Rewrite
		{
			get => MultiTermQueryRewrite?.Rewrite;
			set => MultiTermQueryRewrite = value == null ? null : new MultiTermQueryRewrite(value.Value);
		}

		public MultiTermQueryRewrite MultiTermQueryRewrite { get; set; }

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

		[Obsolete("Use MultiTermQueryRewrite")]
		RewriteMultiTerm? IFuzzyQuery.Rewrite
		{
			get => Self.MultiTermQueryRewrite?.Rewrite;
			set => Self.MultiTermQueryRewrite = value == null ? null : new MultiTermQueryRewrite(value.Value);
		}

		MultiTermQueryRewrite IFuzzyQuery.MultiTermQueryRewrite { get; set; }
		TFuzziness IFuzzyQuery<TValue, TFuzziness>.Fuzziness { get; set; }
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
