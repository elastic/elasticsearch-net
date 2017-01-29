using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Case folding of Unicode characters based on UTR#30, like the ASCII-folding token filter on steroids.
	/// Part of the `analysis-icu` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-icu.html
	/// </summary>
	public interface IIcuFoldingTokenFilter : ITokenFilter
	{
		/// <summary>
		/// Which letters are folded can be controlled by specifying the unicodeSetFilter parameter, which accepts a UnicodeSet.
		/// </summary>
		[JsonProperty("unicodeSetFilter")]
		string UnicodeSetFilter { get; set; }
	}

	/// <inheritdoc/>
	public class IcuFoldingTokenFilter : TokenFilterBase, IIcuFoldingTokenFilter
	{
		public IcuFoldingTokenFilter() : base("icu_folding") { }

		/// <inheritdoc/>
		public string UnicodeSetFilter { get; set; }
	}

	///<inheritdoc/>
	public class IcuFoldingTokenFilterDescriptor
		: TokenFilterDescriptorBase<IcuFoldingTokenFilterDescriptor, IIcuFoldingTokenFilter>, IIcuFoldingTokenFilter
	{
		protected override string Type => "icu_folding";

		string IIcuFoldingTokenFilter.UnicodeSetFilter { get; set; }

		///<inheritdoc/>
		public IcuFoldingTokenFilterDescriptor UnicodeSetFilter(string filter) =>
			Assign(a => a.UnicodeSetFilter = filter);
	}
}
