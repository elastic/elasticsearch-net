using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	/// <summary>
	/// Normalization forms https://en.wikipedia.org/wiki/Unicode_equivalence#Normal_forms
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum IcuNormalizationType
	{
		/// <summary>
		/// Characters are decomposed and then recomposed by canonical equivalence.
		/// </summary>
		[EnumMember(Value="nfc")]
		Canonical,
		/// <summary>
		/// Characters are decomposed by compatibility, then recomposed by canonical equivalence.
		/// </summary>
		[EnumMember(Value="nfkc")]
		Compatibility,
		/// <summary>
		/// Characters are decomposed by compatibility, then recomposed by canonical equivalence with case folding
		/// </summary>
		[EnumMember(Value="nfkc_cf")]
		CompatibilityCaseFold
	}
}
