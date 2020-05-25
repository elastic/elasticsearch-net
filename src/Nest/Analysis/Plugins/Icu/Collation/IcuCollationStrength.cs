// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// The strength property determines the minimum level of
	/// difference considered significant during comparison.
	/// See also: http://icu-project.org/apiref/icu4j/com/ibm/icu/text/Collator.html
	/// </summary>
	/// <remarks>
	/// Requires analysis-icu plugin to be installed
	/// </remarks>
	[StringEnum]
	public enum IcuCollationStrength
	{
		/// <summary>
		/// Typically, this is used to denote differences between base characters (for example, "a" &lt; "b").
		/// It is the strongest difference. For example, dictionaries are divided into different sections by
		/// base character.
		/// </summary>
		[EnumMember(Value = "primary")] Primary,

		/// <summary>
		/// Accents in the characters are considered secondary differences (for example, "as" &lt; "às" &lt; "at").
		/// Other differences between letters can also be considered secondary differences, depending on
		/// the language. A secondary difference is ignored when there is a primary difference anywhere
		/// in the strings.
		/// </summary>
		[EnumMember(Value = "secondary")] Secondary,

		/// <summary>
		/// Upper and lower case differences in characters are distinguished at tertiary strength
		/// (for example, "ao" &lt; "Ao" &lt; "aò"). In addition, a variant of a letter differs from the base
		/// form on the tertiary strength (such as "A" and "Ⓐ"). Another example is the difference between
		/// large and small Kana. A tertiary difference is ignored when there is a primary or secondary
		/// difference anywhere in the strings.
		/// </summary>
		[EnumMember(Value = "tertiary")] Tertiary,

		/// <summary>
		/// When punctuation is ignored (see Ignoring Punctuations in the User Guide) at PRIMARY to
		/// TERTIARY strength, an additional strength level can be used to distinguish words with
		/// and without punctuation (for example, "ab" &lt; "a-b" &lt; "aB"). This difference is ignored
		/// when there is a PRIMARY, SECONDARY or TERTIARY difference. The QUATERNARY strength should
		/// only be used if ignoring punctuation is required.
		/// </summary>
		[EnumMember(Value = "quaternary")] Quaternary,

		/// <summary>
		/// When all other strengths are equal, the IDENTICAL strength is used as a tiebreaker.
		/// The Unicode code point values of the NFD form of each string are compared, just in
		/// case there is no difference. For example, Hebrew cantellation marks are only
		/// distinguished at this strength. This strength should be used sparingly, as only code
		/// point value differences between two strings is an extremely rare occurrence. Using
		/// this strength substantially decreases the performance for both comparison and collation key
		/// generation APIs. This strength also increases the size of the collation key.
		/// </summary>
		[EnumMember(Value = "identical")] Indentical,
	}
}
