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
	/// Elasticsearch supports sorting by array or multi-valued fields. The mode option controls what array value is picked for
	/// sorting the document it belongs to.
	/// </summary>
	[StringEnum]
	public enum SortMode
	{
		/// <summary> Pick the lowest value. </summary>
		[EnumMember(Value = "min")]
		Min,

		/// <summary> Pick the highest value.</summary>
		[EnumMember(Value = "max")]
		Max,

		/// <summary> Use the sum of all values as sort value. Only applicable for number based array fields. </summary>
		[EnumMember(Value = "sum")]
		Sum,

		/// <summary> Use the average of all values as sort value. Only applicable for number based array fields. </summary>
		[EnumMember(Value = "avg")]
		Average,

		/// <summary> Use the median of all values as sort value. Only applicable for number based array fields. </summary>
		[EnumMember(Value = "median")]
		Median
	}
}
