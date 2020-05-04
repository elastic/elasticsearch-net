// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[StringEnum]
	public enum TokenChar
	{
		[EnumMember(Value = "letter")]
		Letter,

		[EnumMember(Value = "digit")]
		Digit,

		[EnumMember(Value = "whitespace")]
		Whitespace,

		[EnumMember(Value = "punctuation")]
		Punctuation,

		[EnumMember(Value = "symbol")]
		Symbol,

		/// <summary>
		/// Custom token characters.
		/// <para></para>
		/// Available in Elasticsearch 7.6.0+
		/// </summary>
		[EnumMember(Value = "custom")]
		Custom,
	}
}
