// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// How suggestions should be sorted per suggest text term.
	/// </summary>
	[StringEnum]
	public enum SuggestSort
	{
		/// <summary>
		/// Sort by score first, then document frequency and then the term itself
		/// </summary>
		[EnumMember(Value = "score")]
		Score,

		/// <summary>
		/// Sort by document frequency first, then similarity score and then the term itself
		/// </summary>
		[EnumMember(Value = "frequency")]
		Frequency
	}
}
