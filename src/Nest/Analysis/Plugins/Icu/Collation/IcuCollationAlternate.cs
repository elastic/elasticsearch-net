// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// Sets the alternate handling for strength quaternary to be either shifted or non-ignorable.
	/// Which boils down to ignoring punctuation and whitespace.
	/// </summary>
	/// <remarks>
	/// Requires analysis-icu plugin to be installed
	/// </remarks>
	[StringEnum]
	public enum IcuCollationAlternate
	{
		[EnumMember(Value = "shifted")] Shifted,
		[EnumMember(Value = "non-ignorable")] NonIgnorable
	}
}
