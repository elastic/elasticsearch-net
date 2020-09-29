// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Collations are used for sorting documents in a language-specific word order. The icu_collation token filter is available to all indices and
	/// defaults to using the DUCET collation, which is a best-effort attempt at language-neutral sorting.
	/// Part of the `analysis-icu` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-icu.html
	/// </summary>
	/// <remarks>
	/// Requires analysis-icu plugin to be installed
	/// </remarks>
	public interface IIcuCollationTokenFilter : ITokenFilter
	{
		/// <summary>
		/// Sets the alternate handling for strength quaternary to be either shifted or non-ignorable.
		/// Which boils down to ignoring punctuation and whitespace.
		/// </summary>
		[DataMember(Name ="alternate")]
		IcuCollationAlternate? Alternate { get; set; }

		/// <summary>
		/// Useful to control which case is sorted first when case is not ignored for strength tertiary.
		/// The default depends on the collation.
		/// </summary>
		[DataMember(Name ="caseFirst")]
		IcuCollationCaseFirst? CaseFirst { get; set; }

		/// <summary>
		/// Whether case level sorting is required. When strength is set to primary this will ignore accent differences
		/// </summary>
		[DataMember(Name ="caseLevel")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? CaseLevel { get; set; }

		[DataMember(Name ="country")]
		string Country { get; set; }

		/// <summary>
		/// Setting this decomposition property to canonical allows the Collator to handle unnormalized text properly,
		/// producing the same results as if the text were normalized. If no is set, it is the user’s responsibility to
		/// insure that all text is already in the appropriate form before a comparison or before getting a CollationKey.
		/// Adjusting decomposition mode allows the user to select between faster and more complete collation behavior.
		/// Since a great many of the world’s languages do not require text normalization,
		/// most locales set no as the default decomposition mode.
		/// </summary>
		[DataMember(Name ="decomposition")]
		IcuCollationDecomposition? Decomposition { get; set; }

		/// <summary>
		/// Distinguishing between Katakana and Hiragana characters in quaternary strength.
		/// </summary>
		[DataMember(Name ="hiraganaQuaternaryMode")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? HiraganaQuaternaryMode { get; set; }

		[DataMember(Name ="language")]
		string Language { get; set; }

		/// <summary>
		/// Whether digits are sorted according to their numeric representation.
		/// For example the value egg-9 is sorted before the value egg-21.
		/// </summary>
		[DataMember(Name ="numeric")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? Numeric { get; set; }

		/// <summary>
		/// The strength property determines the minimum level of difference considered significant during comparison.
		/// </summary>
		[DataMember(Name ="strength")]
		IcuCollationStrength? Strength { get; set; }

		/// <summary>
		/// Single character or contraction. Controls what is variable for <see cref="Alternate" />.
		/// </summary>
		[DataMember(Name ="variableTop")]
		string VariableTop { get; set; }

		[DataMember(Name ="variant")]
		string Variant { get; set; }
	}

	/// <inheritdoc cref="IIcuCollationTokenFilter" />
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

	/// <inheritdoc cref="IIcuCollationTokenFilter" />
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

		/// <inheritdoc cref="IIcuCollationTokenFilter.Language" />
		public IcuCollationTokenFilterDescriptor Language(string language) => Assign(language, (a, v) => a.Language = v);

		/// <inheritdoc cref="IIcuCollationTokenFilter.Country" />
		public IcuCollationTokenFilterDescriptor Country(string country) => Assign(country, (a, v) => a.Country = v);

		/// <inheritdoc cref="IIcuCollationTokenFilter.Variant" />
		public IcuCollationTokenFilterDescriptor Variant(string variant) => Assign(variant, (a, v) => a.Variant = v);

		/// <inheritdoc cref="IIcuCollationTokenFilter.Strength" />
		public IcuCollationTokenFilterDescriptor Strength(IcuCollationStrength? strength) => Assign(strength, (a, v) => a.Strength = v);

		/// <inheritdoc cref="IIcuCollationTokenFilter.Decomposition" />
		public IcuCollationTokenFilterDescriptor Decomposition(IcuCollationDecomposition? decomposition) =>
			Assign(decomposition, (a, v) => a.Decomposition = v);

		/// <inheritdoc cref="IIcuCollationTokenFilter.Alternate" />
		public IcuCollationTokenFilterDescriptor Alternate(IcuCollationAlternate? alternate) => Assign(alternate, (a, v) => a.Alternate = v);

		/// <inheritdoc cref="IIcuCollationTokenFilter.CaseFirst" />
		public IcuCollationTokenFilterDescriptor CaseFirst(IcuCollationCaseFirst? caseFirst) => Assign(caseFirst, (a, v) => a.CaseFirst = v);

		/// <inheritdoc cref="IIcuCollationTokenFilter.CaseLevel" />
		public IcuCollationTokenFilterDescriptor CaseLevel(bool? caseLevel = true) => Assign(caseLevel, (a, v) => a.CaseLevel = v);

		/// <inheritdoc cref="IIcuCollationTokenFilter.Numeric" />
		public IcuCollationTokenFilterDescriptor Numeric(bool? numeric = true) => Assign(numeric, (a, v) => a.Numeric = v);

		/// <inheritdoc cref="IIcuCollationTokenFilter.HiraganaQuaternaryMode" />
		public IcuCollationTokenFilterDescriptor HiraganaQuaternaryMode(bool? mode = true) => Assign(mode, (a, v) => a.HiraganaQuaternaryMode = v);

		/// <inheritdoc cref="IIcuCollationTokenFilter.VariableTop" />
		public IcuCollationTokenFilterDescriptor VariableTop(string variableTop) => Assign(variableTop, (a, v) => a.VariableTop = v);
	}
}
