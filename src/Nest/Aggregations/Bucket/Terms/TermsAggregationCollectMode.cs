/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Runtime.Serialization;
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
