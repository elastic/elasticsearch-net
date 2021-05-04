// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// Normalization forms https://en.wikipedia.org/wiki/Unicode_equivalence#Normal_forms
	/// </summary>
	/// <remarks>
	/// Requires analysis-icu plugin to be installed
	/// </remarks>
	[StringEnum]
	public enum IcuNormalizationType
	{
		/// <summary>
		/// Characters are decomposed and then recomposed by canonical equivalence.
		/// </summary>
		[EnumMember(Value = "nfc")]
		Canonical,

		/// <summary>
		/// Characters are decomposed by compatibility, then recomposed by canonical equivalence.
		/// </summary>
		[EnumMember(Value = "nfkc")]
		Compatibility,

		/// <summary>
		/// Characters are decomposed by compatibility, then recomposed by canonical equivalence with case folding
		/// </summary>
		[EnumMember(Value = "nfkc_cf")]
		CompatibilityCaseFold
	}
}
