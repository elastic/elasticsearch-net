using System;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Globalization;

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
	public abstract class FuzzyQueryBase<TValue, TFuzziness> : FieldNameQueryBase, IFuzzyQuery
	{
		[JsonProperty(PropertyName = "prefix_length")]
		public int? PrefixLength { get; set; }
		
		[JsonProperty(PropertyName = "value")]
		public TValue Value { get; set; }

		[JsonProperty(PropertyName = "fuzziness")]
		public TFuzziness Fuzziness { get; set; }

		[JsonProperty(PropertyName = "rewrite")]
		public RewriteMultiTerm? Rewrite { get; set; }

		[JsonProperty(PropertyName = "max_expansions")]
		public int? MaxExpansions { get; set; }

		[JsonProperty(PropertyName = "transpositions")]
		public bool? Transpositions { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.Fuzzy = this;
	}

	public class FuzzyQueryDescriptorBase<TDescriptor, T, TValue, TFuzziness> 
		: FieldNameQueryDescriptorBase<TDescriptor, IFuzzyQuery, T> , IFuzzyQuery 
		where T : class
		where TDescriptor : FieldNameQueryDescriptorBase<TDescriptor, IFuzzyQuery, T>, IFuzzyQuery
	{
		int? IFuzzyQuery.PrefixLength { get; set; }
		int? IFuzzyQuery.MaxExpansions { get; set; }
		bool? IFuzzyQuery.Transpositions { get; set; }
		RewriteMultiTerm? IFuzzyQuery.Rewrite { get; set; }

		[JsonProperty(PropertyName = "fuzziness")]
		internal TFuzziness _Fuzziness { get; set; }

		[JsonProperty(PropertyName = "value")]
		internal TValue _Value { get; set; }

		public TDescriptor MaxExpansions(int? maxExpansions) => Assign(a => a.MaxExpansions = maxExpansions);

		public TDescriptor PrefixLength(int? prefixLength) => Assign(a => a.PrefixLength = prefixLength);

		public TDescriptor Transpositions(bool? enable = true) => Assign(a => a.Transpositions = enable);

		public TDescriptor Rewrite(RewriteMultiTerm? rewrite) => Assign(a => a.Rewrite = rewrite);

	}
}
