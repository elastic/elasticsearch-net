// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	// ReSharper disable once InconsistentNaming
	public enum IBLambda
	{
		/// <summary>
		/// Nw/N or average number of documents where w occurs
		/// </summary>
		[EnumMember(Value = "df")]
		DocumentFrequency,

		/// <summary>
		/// Fw/N or average number of occurrences of w in the collection
		/// </summary>
		[EnumMember(Value = "ttf")]
		TermFrequency,
	}
}
