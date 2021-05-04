// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

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
		[DataMember(Name ="encoder")]
		PhoneticEncoder? Encoder { get; set; }

		/// <summary>
		/// An array of languages to check. If not specified, then the language will be guessed
		/// </summary>
		/// <remarks>
		/// Valid for <see cref="PhoneticEncoder.Beidermorse" /> only
		/// </remarks>
		[DataMember(Name ="languageset")]
		IEnumerable<PhoneticLanguage> LanguageSet { get; set; }

		/// <summary>
		/// The maximum length of the emitted metaphone token. Defaults to <c>4</c>
		/// </summary>
		/// <remarks>
		/// Valid for <see cref="PhoneticEncoder.DoubleMetaphone" /> only
		/// </remarks>
		[DataMember(Name ="max_code_len")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? MaxCodeLength { get; set; }

		/// <summary>
		/// Whether names are ashkenazi, sephardic, or generic. Defaults to generic
		/// </summary>
		/// <remarks>
		/// Valid for <see cref="PhoneticEncoder.Beidermorse" /> only
		/// </remarks>
		[DataMember(Name ="name_type")]
		PhoneticNameType? NameType { get; set; }

		/// <summary>
		/// The replace parameter (defaults to true) controls if the token processed should be replaced
		///  with the encoded one (set it to true), or added (set it to false).
		/// </summary>
		[DataMember(Name ="replace")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? Replace { get; set; }

		/// <summary>
		/// Whether matching should be exact or approximate. Defaults to approximate
		/// </summary>
		/// <remarks>
		/// Valid for <see cref="PhoneticEncoder.Beidermorse" /> only
		/// </remarks>
		[DataMember(Name ="rule_type")]
		PhoneticRuleType? RuleType { get; set; }
	}

	/// <inheritdoc cref="IPhoneticTokenFilter" />
	public class PhoneticTokenFilter : TokenFilterBase, IPhoneticTokenFilter
	{
		public PhoneticTokenFilter() : base("phonetic") { }

		/// <inheritdoc />
		public PhoneticEncoder? Encoder { get; set; }

		/// <inheritdoc />
		public IEnumerable<PhoneticLanguage> LanguageSet { get; set; }

		/// <inheritdoc />
		public int? MaxCodeLength { get; set; }

		/// <inheritdoc />
		public PhoneticNameType? NameType { get; set; }

		/// <inheritdoc />
		public bool? Replace { get; set; }

		/// <inheritdoc />
		public PhoneticRuleType? RuleType { get; set; }
	}

	/// <inheritdoc cref="IPhoneticTokenFilter" />
	public class PhoneticTokenFilterDescriptor
		: TokenFilterDescriptorBase<PhoneticTokenFilterDescriptor, IPhoneticTokenFilter>, IPhoneticTokenFilter
	{
		protected override string Type => "phonetic";
		PhoneticEncoder? IPhoneticTokenFilter.Encoder { get; set; }
		IEnumerable<PhoneticLanguage> IPhoneticTokenFilter.LanguageSet { get; set; }
		int? IPhoneticTokenFilter.MaxCodeLength { get; set; }
		PhoneticNameType? IPhoneticTokenFilter.NameType { get; set; }

		bool? IPhoneticTokenFilter.Replace { get; set; }
		PhoneticRuleType? IPhoneticTokenFilter.RuleType { get; set; }

		/// <inheritdoc cref="IPhoneticTokenFilter.Replace" />
		public PhoneticTokenFilterDescriptor Replace(bool? replace = true) => Assign(replace, (a, v) => a.Replace = v);

		/// <inheritdoc cref="IPhoneticTokenFilter.Encoder" />
		public PhoneticTokenFilterDescriptor Encoder(PhoneticEncoder? encoder) => Assign(encoder, (a, v) => a.Encoder = v);

		/// <inheritdoc cref="IPhoneticTokenFilter.MaxCodeLength" />
		public PhoneticTokenFilterDescriptor MaxCodeLength(int? maxCodeLength) => Assign(maxCodeLength, (a, v) => a.MaxCodeLength = v);

		/// <inheritdoc cref="IPhoneticTokenFilter.RuleType" />
		public PhoneticTokenFilterDescriptor RuleType(PhoneticRuleType? ruleType) => Assign(ruleType, (a, v) => a.RuleType = v);

		/// <inheritdoc cref="IPhoneticTokenFilter.NameType" />
		public PhoneticTokenFilterDescriptor NameType(PhoneticNameType? nameType) => Assign(nameType, (a, v) => a.NameType = v);

		/// <inheritdoc cref="IPhoneticTokenFilter.LanguageSet" />
		public PhoneticTokenFilterDescriptor LanguageSet(params PhoneticLanguage[] languageSet) =>
			Assign(languageSet, (a, v) => a.LanguageSet = v);

		/// <inheritdoc cref="IPhoneticTokenFilter.LanguageSet" />
		public PhoneticTokenFilterDescriptor LanguageSet(IEnumerable<PhoneticLanguage> languageSet) =>
			Assign(languageSet, (a, v) => a.LanguageSet = v);
	}
}
