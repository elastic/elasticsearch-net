using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Collations are used for sorting documents in a language-specific word order. The icu_collation token filter is available to all indices and
	/// defaults to using the DUCET collation, which is a best-effort attempt at language-neutral sorting.
	/// Part of the `analysis-icu` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-icu.html
	/// </summary>
	public interface IIcuCollationTokenFilter : ITokenFilter
	{
		/// <summary>
		/// Sets the alternate handling for strength quaternary to be either shifted or non-ignorable.
		/// Which boils down to ignoring punctuation and whitespace.
		/// </summary>
		[JsonProperty("alternate")]
		IcuCollationAlternate? Alternate { get; set; }

		/// <summary>
		/// Useful to control which case is sorted first when case is not ignored for strength tertiary.
		/// The default depends on the collation.
		/// </summary>
		[JsonProperty("caseFirst")]
		IcuCollationCaseFirst? CaseFirst { get; set; }

		/// <summary>
		/// Whether case level sorting is required. When strength is set to primary this will ignore accent differences
		/// </summary>
		[JsonProperty("caseLevel")]
		bool? CaseLevel { get; set; }

		[JsonProperty("country")]
		string Country { get; set; }

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
		/// Distinguishing between Katakana and Hiragana characters in quaternary strength.
		/// </summary>
		[JsonProperty("hiraganaQuaternaryMode")]
		bool? HiraganaQuaternaryMode { get; set; }

		[JsonProperty("language")]
		string Language { get; set; }

		/// <summary>
		/// Whether digits are sorted according to their numeric representation.
		/// For example the value egg-9 is sorted before the value egg-21.
		/// </summary>
		[JsonProperty("numeric")]
		bool? Numeric { get; set; }

		/// <summary>
		/// The strength property determines the minimum level of difference considered significant during comparison.
		/// </summary>
		[JsonProperty("strength")]
		IcuCollationStrength? Strength { get; set; }

		/// <summary>
		/// Single character or contraction. Controls what is variable for <see cref="Alternate" />.
		/// </summary>
		[JsonProperty("variableTop")]
		string VariableTop { get; set; }

		[JsonProperty("variant")]
		string Variant { get; set; }
	}

	/// <inheritdoc />
	public class IcuCollationTokenFilter : TokenFilterBase, IIcuCollationTokenFilter
	{
		public IcuCollationTokenFilter() : base("icu_collation") { }

		/// <inheritdoc />
		public IcuCollationAlternate? Alternate { get; set; }

		/// <inheritdoc />
		public IcuCollationCaseFirst? CaseFirst { get; set; }

		/// <inheritdoc />
		public bool? CaseLevel { get; set; }

		/// <inheritdoc />
		public string Country { get; set; }

		/// <inheritdoc />
		public IcuCollationDecomposition? Decomposition { get; set; }

		/// <inheritdoc />
		public bool? HiraganaQuaternaryMode { get; set; }

		/// <inheritdoc />
		public string Language { get; set; }

		/// <inheritdoc />
		public bool? Numeric { get; set; }

		/// <inheritdoc />
		public IcuCollationStrength? Strength { get; set; }

		/// <inheritdoc />
		public string VariableTop { get; set; }

		/// <inheritdoc />
		public string Variant { get; set; }
	}

	/// <inheritdoc />
	public class IcuCollationTokenFilterDescriptor
		: TokenFilterDescriptorBase<IcuCollationTokenFilterDescriptor, IIcuCollationTokenFilter>, IIcuCollationTokenFilter
	{
		protected override string Type => "icu_collation";
		IcuCollationAlternate? IIcuCollationTokenFilter.Alternate { get; set; }
		IcuCollationCaseFirst? IIcuCollationTokenFilter.CaseFirst { get; set; }
		bool? IIcuCollationTokenFilter.CaseLevel { get; set; }
		string IIcuCollationTokenFilter.Country { get; set; }
		IcuCollationDecomposition? IIcuCollationTokenFilter.Decomposition { get; set; }
		bool? IIcuCollationTokenFilter.HiraganaQuaternaryMode { get; set; }

		string IIcuCollationTokenFilter.Language { get; set; }
		bool? IIcuCollationTokenFilter.Numeric { get; set; }
		IcuCollationStrength? IIcuCollationTokenFilter.Strength { get; set; }
		string IIcuCollationTokenFilter.VariableTop { get; set; }
		string IIcuCollationTokenFilter.Variant { get; set; }

		/// <inheritdoc />
		public IcuCollationTokenFilterDescriptor Language(string language) => Assign(language, (a, v) => a.Language = v);

		/// <inheritdoc />
		public IcuCollationTokenFilterDescriptor Country(string country) => Assign(country, (a, v) => a.Country = v);

		/// <inheritdoc />
		public IcuCollationTokenFilterDescriptor Variant(string variant) => Assign(variant, (a, v) => a.Variant = v);

		/// <inheritdoc />
		public IcuCollationTokenFilterDescriptor Strength(IcuCollationStrength? strength) => Assign(strength, (a, v) => a.Strength = v);

		/// <inheritdoc />
		public IcuCollationTokenFilterDescriptor Decomposition(IcuCollationDecomposition? decomposition) =>
			Assign(decomposition, (a, v) => a.Decomposition = v);

		/// <inheritdoc />
		public IcuCollationTokenFilterDescriptor Alternate(IcuCollationAlternate? alternate) => Assign(alternate, (a, v) => a.Alternate = v);

		/// <inheritdoc />
		public IcuCollationTokenFilterDescriptor CaseFirst(IcuCollationCaseFirst? caseFirst) => Assign(caseFirst, (a, v) => a.CaseFirst = v);

		/// <inheritdoc />
		public IcuCollationTokenFilterDescriptor CaseLevel(bool? caseLevel = true) => Assign(caseLevel, (a, v) => a.CaseLevel = v);

		/// <inheritdoc />
		public IcuCollationTokenFilterDescriptor Numeric(bool? numeric = true) => Assign(numeric, (a, v) => a.Numeric = v);

		/// <inheritdoc />
		public IcuCollationTokenFilterDescriptor HiraganaQuaternaryMode(bool? mode = true) => Assign(mode, (a, v) => a.HiraganaQuaternaryMode = v);

		/// <inheritdoc />
		public IcuCollationTokenFilterDescriptor VariableTop(string variableTop) => Assign(variableTop, (a, v) => a.VariableTop = v);
	}
}
