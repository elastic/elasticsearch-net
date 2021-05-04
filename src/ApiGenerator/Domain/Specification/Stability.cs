// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace ApiGenerator.Domain.Specification
{
	public enum Stability
	{
		/// <summary>
		/// Highly likely to break in the near future (minor/path), no BWC guarantees. Possibly removed in the future.
		/// </summary>
		[EnumMember(Value = "experimental")]
		Experimental,

		/// <summary>
		/// Less likely to break or be removed but still reserve the right to do so.
		/// </summary>
		[EnumMember(Value = "beta")]
		Beta,

		/// <summary>
		/// No backwards breaking changes in a minor.
		/// </summary>
		[EnumMember(Value = "stable")]
		Stable
	}
}
