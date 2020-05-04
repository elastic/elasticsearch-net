// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[StringEnum]
	public enum PhoneticEncoder
	{
		[EnumMember(Value = "metaphone")]
		Metaphone,

		[EnumMember(Value = "double_metaphone")]
		DoubleMetaphone,

		[EnumMember(Value = "soundex")]
		Soundex,

		[EnumMember(Value = "refined_soundex")]
		RefinedSoundex,

		[EnumMember(Value = "caverphone1")]
		Caverphone1,

		[EnumMember(Value = "caverphone2")]
		Caverphone2,

		[EnumMember(Value = "cologne")]
		Cologne,

		[EnumMember(Value = "nysiis")]
		Nysiis,

		[EnumMember(Value = "koelnerphonetik")]
		KoelnerPhonetik,

		[EnumMember(Value = "haasephonetik")]
		HaasePhonetik,

		[EnumMember(Value = "beider_morse")]
		Beidermorse,

		[EnumMember(Value = "daitch_mokotoff")]
		DaitchMokotoff
	}
}
