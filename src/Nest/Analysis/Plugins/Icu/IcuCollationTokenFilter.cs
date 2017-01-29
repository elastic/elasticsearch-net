using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Collations are used for sorting documents in a language-specific word order. The icu_collation token filter is available to all indices and defaults to using the DUCET collation, which is a best-effort attempt at language-neutral sorting.
	/// Part of the `analysis-icu` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-icu.html
	/// </summary>
	public interface IIcuCollationTokenFilter : ITokenFilter
	{
		[JsonProperty("language")]
		string Language { get; set; }

		[JsonProperty("country")]
		string Country { get; set; }

		[JsonProperty("variant")]
		string Variant { get; set; }

		/// <summary>
		/// The strength property determines the minimum level of difference considered significant during comparison.
		/// </summary>
		[JsonProperty("strength")]
		IcuCollationStrength? Strength { get; set; }

		/// <summary>
		/// Setting this decomposition property to canonical allows the Collator to handle unnormalized text properly,
		/// producing the same results as if the text were normalized. If no is set, it is the user’s responsibility to
		/// insure that all text is already in the appropriate form before a comparison or before getting a CollationKey.
		/// Adjusting decomposition mode allows the user to select between faster and more complete collation behavior.
		/// Since a great many of the world’s languages do not require text normalization,
		/// most locales set no as the default decomposition mode.
		/// </summary>
		[JsonProperty("decomposition")]
		IcuCollationDecomposition? Decomposition { get; set; }

		/// <summary>
		/// Sets the alternate handling for strength quaternary to be either shifted or non-ignorable.
		/// Which boils down to ignoring punctuation and whitespace.
		/// </summary>
		[JsonProperty("alternate")]
		IcuCollationAlternate? Alternate { get; set; }

		/// <summary>
		/// Whether case level sorting is required. When strength is set to primary this will ignore accent differences
		/// </summary>
		[JsonProperty("caseLevel")]
		bool? CaseLevel { get; set; }

		/// <summary>
		/// Useful to control which case is sorted first when case is not ignored for strength tertiary.
		/// The default depends on the collation.
		/// </summary>
		[JsonProperty("caseFirst")]
		IcuCollationCaseFirst? CaseFirst { get; set; }

		/// <summary>
		/// Whether digits are sorted according to their numeric representation.
		/// For example the value egg-9 is sorted before the value egg-21.
		/// </summary>
		[JsonProperty("numeric")]
		bool? Numeric { get; set; }

		/// <summary>
		/// Single character or contraction. Controls what is variable for <see cref="Alternate"/>.
		/// </summary>
		[JsonProperty("variableTop")]
		string VariableTop { get; set; }

		/// <summary>
		/// Distinguishing between Katakana and Hiragana characters in quaternary strength.
		/// </summary>
		[JsonProperty("hiraganaQuaternaryMode")]
		bool? HiraganaQuaternaryMode { get; set; }
	}

	/// <inheritdoc/>
	public class IcuCollationTokenFilter : TokenFilterBase, IIcuCollationTokenFilter
	{
		public IcuCollationTokenFilter() : base("icu_collation") { }

		///<inheritdoc/>
		public string Language { get; set; }
		///<inheritdoc/>
		public string Country { get; set; }
		///<inheritdoc/>
		public string Variant { get; set; }
		///<inheritdoc/>
		public IcuCollationStrength? Strength { get; set; }
		///<inheritdoc/>
		public IcuCollationDecomposition? Decomposition { get; set; }
		///<inheritdoc/>
		public IcuCollationAlternate? Alternate { get; set; }
		///<inheritdoc/>
		public bool? CaseLevel { get; set; }
		///<inheritdoc/>
		public IcuCollationCaseFirst? CaseFirst { get; set; }
		///<inheritdoc/>
		public bool? Numeric { get; set; }
		///<inheritdoc/>
		public string VariableTop { get; set; }
		///<inheritdoc/>
		public bool? HiraganaQuaternaryMode { get; set; }
	}

	///<inheritdoc/>
	public class IcuCollationTokenFilterDescriptor
		: TokenFilterDescriptorBase<IcuCollationTokenFilterDescriptor, IIcuCollationTokenFilter>, IIcuCollationTokenFilter
	{
		protected override string Type => "icu_collation";

		string IIcuCollationTokenFilter.Language { get; set; }
		string IIcuCollationTokenFilter.Country { get; set; }
		string IIcuCollationTokenFilter.Variant { get; set; }
		IcuCollationStrength? IIcuCollationTokenFilter.Strength { get; set; }
		IcuCollationDecomposition? IIcuCollationTokenFilter.Decomposition { get; set; }
		IcuCollationAlternate? IIcuCollationTokenFilter.Alternate { get; set; }
		bool? IIcuCollationTokenFilter.CaseLevel { get; set; }
		IcuCollationCaseFirst? IIcuCollationTokenFilter.CaseFirst { get; set; }
		bool? IIcuCollationTokenFilter.Numeric { get; set; }
		string IIcuCollationTokenFilter.VariableTop { get; set; }
		bool? IIcuCollationTokenFilter.HiraganaQuaternaryMode { get; set; }

		///<inheritdoc/>
		public IcuCollationTokenFilterDescriptor Language(string language) => Assign(a => a.Language = language);
		///<inheritdoc/>
		public IcuCollationTokenFilterDescriptor Country(string country) => Assign(a => a.Country = country);
		///<inheritdoc/>
		public IcuCollationTokenFilterDescriptor Variant(string variant) => Assign(a => a.Variant = variant);
		///<inheritdoc/>
		public IcuCollationTokenFilterDescriptor Strength(IcuCollationStrength? strength) => Assign(a => a.Strength = strength);
		///<inheritdoc/>
		public IcuCollationTokenFilterDescriptor Decomposition(IcuCollationDecomposition? decomposition) => Assign(a => a.Decomposition = decomposition);
		///<inheritdoc/>
		public IcuCollationTokenFilterDescriptor Alternate(IcuCollationAlternate? alternate) => Assign(a => a.Alternate = alternate);
		///<inheritdoc/>
		public IcuCollationTokenFilterDescriptor CaseFirst(IcuCollationCaseFirst? caseFirst) => Assign(a => a.CaseFirst = caseFirst);
		///<inheritdoc/>
		public IcuCollationTokenFilterDescriptor CaseLevel(bool? caseLevel = true) => Assign(a => a.CaseLevel = caseLevel);
		///<inheritdoc/>
		public IcuCollationTokenFilterDescriptor Numeric(bool? numeric = true) => Assign(a => a.Numeric = numeric);
		///<inheritdoc/>
		public IcuCollationTokenFilterDescriptor HiraganaQuaternaryMode(bool? mode = true) => Assign(a => a.HiraganaQuaternaryMode = mode);
		///<inheritdoc/>
		public IcuCollationTokenFilterDescriptor VariableTop(string variableTop) => Assign(a => a.VariableTop = variableTop);

	}
}
