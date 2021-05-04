// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
