// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
