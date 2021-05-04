// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// Normalization mode https://en.wikipedia.org/wiki/Unicode_equivalence#Normal_forms
	/// </summary>
	/// <remarks>
	/// Requires analysis-icu plugin to be installed
	/// </remarks>
	[StringEnum]
	public enum IcuNormalizationMode
	{
		/// <summary>
		/// Switch normalization type to decompose
		/// </summary>
		[EnumMember(Value = "decompose")]
		Decompose,

		/// <summary>
		/// Switch normalization type to compose, which is the default so you'd never need to set this
		/// Included here for completeness sake because the Java API has it.
		/// </summary>
		[EnumMember(Value = "compose")]
		Compose
	}
}
