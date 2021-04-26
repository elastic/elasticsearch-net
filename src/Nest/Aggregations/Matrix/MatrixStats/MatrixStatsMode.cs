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
	/// The matrix_stats aggregation treats each document field as an independent sample.
	/// The mode parameter controls what array value the aggregation will use for array or
	/// multi-valued fields.
	/// </summary>
	[StringEnum]
	public enum MatrixStatsMode
	{
		/// <summary>
		/// (default) Use the average of all values.
		/// </summary>
		[EnumMember(Value = "avg")]
		Avg,

		/// <summary>
		/// Pick the lowest value.
		/// </summary>
		[EnumMember(Value = "min")]
		Min,

		/// <summary>
		/// 	Pick the highest value.
		/// </summary>
		[EnumMember(Value = "max")]
		Max,

		/// <summary>
		/// Use the sum of all values.
		/// </summary>
		[EnumMember(Value = "sum")]
		Sum,

		/// <summary>
		/// Use the median of all values.
		/// </summary>
		[EnumMember(Value = "median")]
		Median
	}
}
