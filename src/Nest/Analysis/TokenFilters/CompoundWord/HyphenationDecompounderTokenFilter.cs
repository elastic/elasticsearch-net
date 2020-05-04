// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <summary>
	/// Token filters that allow to decompose compound words.
	/// </summary>
	public interface IHyphenationDecompounderTokenFilter : ICompoundWordTokenFilter { }

	/// <inheritdoc />
	public class HyphenationDecompounderTokenFilter : CompoundWordTokenFilterBase, IHyphenationDecompounderTokenFilter
	{
		public HyphenationDecompounderTokenFilter() : base("hyphenation_decompounder") { }
	}

	/// <inheritdoc />
	public class HyphenationDecompounderTokenFilterDescriptor
		: CompoundWordTokenFilterDescriptorBase<HyphenationDecompounderTokenFilterDescriptor, IHyphenationDecompounderTokenFilter>
			, IHyphenationDecompounderTokenFilter
	{
		protected override string Type => "hyphenation_decompounder";
	}
}
