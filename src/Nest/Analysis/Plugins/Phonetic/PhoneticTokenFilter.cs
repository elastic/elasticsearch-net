using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A token filter that converts tokens to their phonetic
	/// representation using Soundex, Metaphone, and a variety of other algorithms.
	/// </summary>
	/// <remarks>
	/// Requires the Phonetic Analysis plugin be installed (analysis-phonetic)
	/// </remarks>
	public interface IPhoneticTokenFilter : ITokenFilter
	{
		/// <summary>
		/// The type of phonetic encoding to use
		/// </summary>
		[JsonProperty("encoder")]
		PhoneticEncoder? Encoder { get; set; }

		/// <summary>
		///The replace parameter (defaults to true) controls if the token processed should be replaced
		/// with the encoded one (set it to true), or added (set it to false).
		/// </summary>
		[JsonProperty("replace")]
		bool? Replace { get; set; }

		/// <summary>
		/// The maximum length of the emitted metaphone token. Defaults to <c>4</c>
		/// </summary>
		/// <remarks>
		/// Valid for <see cref="PhoneticEncoder.DoubleMetaphone"/> only
		/// </remarks>
		[JsonProperty("max_code_len")]
		int? MaxCodeLength { get; set; }

		/// <summary>
		/// Whether matching should be exact or approximate. Defaults to approximate
		/// </summary>
		/// <remarks>
		/// Valid for <see cref="PhoneticEncoder.Beidermorse"/> only
		/// </remarks>
		[JsonProperty("rule_type")]
		PhoneticRuleType? RuleType { get; set; }

		/// <summary>
		/// Whether names are ashkenazi, sephardic, or generic. Defaults to generic
		/// </summary>
		/// <remarks>
		/// Valid for <see cref="PhoneticEncoder.Beidermorse"/> only
		/// </remarks>
		[JsonProperty("name_type")]
		PhoneticNameType? NameType { get; set; }

		/// <summary>
		/// An array of languages to check. If not specified, then the language will be guessed
		/// </summary>
		/// <remarks>
		/// Valid for <see cref="PhoneticEncoder.Beidermorse"/> only
		/// </remarks>
		[JsonProperty("languageset")]
		IEnumerable<PhoneticLanguage> LanguageSet { get; set; }
	}

	/// <inheritdoc cref="IPhoneticTokenFilter" />
	public class PhoneticTokenFilter : TokenFilterBase, IPhoneticTokenFilter
	{
		public PhoneticTokenFilter() : base("phonetic") { }

		/// <inheritdoc/>
		public PhoneticEncoder? Encoder { get; set; }

		/// <inheritdoc/>
		public bool? Replace { get; set; }

		/// <inheritdoc/>
		public int? MaxCodeLength { get; set; }

		/// <inheritdoc/>
		public PhoneticRuleType? RuleType { get; set; }

		/// <inheritdoc/>
		public PhoneticNameType? NameType { get; set; }

		/// <inheritdoc/>
		public IEnumerable<PhoneticLanguage> LanguageSet { get; set; }
	}

	/// <inheritdoc cref="IPhoneticTokenFilter" />
	public class PhoneticTokenFilterDescriptor
		: TokenFilterDescriptorBase<PhoneticTokenFilterDescriptor, IPhoneticTokenFilter>, IPhoneticTokenFilter
	{
		protected override string Type => "phonetic";

		bool? IPhoneticTokenFilter.Replace { get; set; }
		PhoneticEncoder? IPhoneticTokenFilter.Encoder { get; set; }
		int? IPhoneticTokenFilter.MaxCodeLength { get; set; }
		PhoneticRuleType? IPhoneticTokenFilter.RuleType { get; set; }
		PhoneticNameType? IPhoneticTokenFilter.NameType { get; set; }
		IEnumerable<PhoneticLanguage> IPhoneticTokenFilter.LanguageSet { get; set; }

		/// <inheritdoc cref="IPhoneticTokenFilter.Replace" />
		public PhoneticTokenFilterDescriptor Replace(bool? replace = true) => Assign(a => a.Replace = replace);

		/// <inheritdoc cref="IPhoneticTokenFilter.Encoder" />
		public PhoneticTokenFilterDescriptor Encoder(PhoneticEncoder? encoder) => Assign(a => a.Encoder = encoder);

		/// <inheritdoc cref="IPhoneticTokenFilter.MaxCodeLength" />
		public PhoneticTokenFilterDescriptor MaxCodeLength(int? maxCodeLength) => Assign(a => a.MaxCodeLength = maxCodeLength);

		/// <inheritdoc cref="IPhoneticTokenFilter.RuleType" />
		public PhoneticTokenFilterDescriptor RuleType(PhoneticRuleType? ruleType) => Assign(a => a.RuleType = ruleType);

		/// <inheritdoc cref="IPhoneticTokenFilter.NameType" />
		public PhoneticTokenFilterDescriptor NameType(PhoneticNameType? nameType) => Assign(a => a.NameType = nameType);

		/// <inheritdoc cref="IPhoneticTokenFilter.LanguageSet" />
		public PhoneticTokenFilterDescriptor LanguageSet(params PhoneticLanguage[] languageSet) =>
			Assign(a => a.LanguageSet = languageSet);

		/// <inheritdoc cref="IPhoneticTokenFilter.LanguageSet" />
		public PhoneticTokenFilterDescriptor LanguageSet(IEnumerable<PhoneticLanguage> languageSet) =>
			Assign(a => a.LanguageSet = languageSet);
	}
}
