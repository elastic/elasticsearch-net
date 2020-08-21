// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Case folding of Unicode characters based on UTR#30, like the ASCII-folding token filter on steroids.
	/// </summary>
	/// <remarks>
	/// Requires analysis-icu plugin to be installed
	/// </remarks>
	public interface IIcuFoldingTokenFilter : ITokenFilter
	{
		/// <summary>
		/// Which letters are folded can be controlled by specifying the unicodeSetFilter parameter, which accepts a UnicodeSet.
		/// </summary>
		[DataMember(Name ="unicode_set_filter")]
		string UnicodeSetFilter { get; set; }
	}

	/// <inheritdoc />
	public class IcuFoldingTokenFilter : TokenFilterBase, IIcuFoldingTokenFilter
	{
		public IcuFoldingTokenFilter() : base("icu_folding") { }

		/// <inheritdoc />
		public string UnicodeSetFilter { get; set; }
	}

	/// <inheritdoc />
	public class IcuFoldingTokenFilterDescriptor
		: TokenFilterDescriptorBase<IcuFoldingTokenFilterDescriptor, IIcuFoldingTokenFilter>, IIcuFoldingTokenFilter
	{
		protected override string Type => "icu_folding";

		string IIcuFoldingTokenFilter.UnicodeSetFilter { get; set; }

		/// <inheritdoc />
		public IcuFoldingTokenFilterDescriptor UnicodeSetFilter(string filter) =>
			Assign(filter, (a, v) => a.UnicodeSetFilter = v);
	}
}
