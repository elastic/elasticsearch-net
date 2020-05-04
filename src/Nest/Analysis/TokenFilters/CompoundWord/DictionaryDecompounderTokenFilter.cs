// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	public interface IDictionaryDecompounderTokenFilter : ICompoundWordTokenFilter { }

	/// <inheritdoc />
	public class DictionaryDecompounderTokenFilter : CompoundWordTokenFilterBase, IDictionaryDecompounderTokenFilter
	{
		public DictionaryDecompounderTokenFilter() : base("dictionary_decompounder") { }
	}

	/// <inheritdoc />
	public class DictionaryDecompounderTokenFilterDescriptor
		: CompoundWordTokenFilterDescriptorBase<DictionaryDecompounderTokenFilterDescriptor, IDictionaryDecompounderTokenFilter>
			, IDictionaryDecompounderTokenFilter
	{
		protected override string Type => "dictionary_decompounder";
	}
}
