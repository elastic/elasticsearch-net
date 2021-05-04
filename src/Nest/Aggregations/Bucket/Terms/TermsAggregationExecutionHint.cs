// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// Determines how the terms aggregation is executed
	/// </summary>
	[StringEnum]
	public enum TermsAggregationExecutionHint
	{
		/// <summary>
		/// Order by using field values directly in order to aggregate data per-bucket
		/// </summary>
		[EnumMember(Value = "map")]
		Map,

		/// <summary>
		/// Order by using ordinals of the field and preemptively allocating one bucket per ordinal value
		/// </summary>
		[EnumMember(Value = "global_ordinals")]
		GlobalOrdinals,

		/// <summary>
		/// Order by using ordinals of the field and dynamically allocating one bucket per ordinal value
		/// </summary>
		[EnumMember(Value = "global_ordinals_hash")]
		GlobalOrdinalsHash,

		/// <summary>
		/// Order by using per-segment ordinals to compute counts and remap these counts to global counts using global ordinals
		/// </summary>
		[EnumMember(Value = "global_ordinals_low_cardinality")]
		GlobalOrdinalsLowCardinality
	}
}
