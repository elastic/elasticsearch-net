// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// Determines how the terms aggregation is executed
	/// </summary>
	[StringEnum]
	public enum TermsAggregationCollectMode
	{
		/// <summary>
		/// Order by using field values directly in order to aggregate data per-bucket
		/// </summary>
		[EnumMember(Value = "depth_first")]
		DepthFirst,

		/// <summary>
		/// Order by using ordinals of the field values instead of the values themselves
		/// </summary>
		[EnumMember(Value = "breadth_first")]
		BreadthFirst
	}
}
