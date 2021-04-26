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

using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum RangeType
	{
		/// <summary>
		/// A range of signed 32-bit integers with a minimum value of -231 and maximum of 231-1.
		/// </summary>
		[EnumMember(Value = "integer_range")]
		IntegerRange,

		/// <summary>
		/// A range of single-precision 32-bit IEEE 754 floating point values.
		/// </summary>
		[EnumMember(Value = "float_range")]
		FloatRange,

		/// <summary>
		/// A range of signed 64-bit integers with a minimum value of -263 and maximum of 263-1.
		/// </summary>
		[EnumMember(Value = "long_range")]
		LongRange,

		/// <summary>
		/// A range of double-precision 64-bit IEEE 754 floating point values.
		/// </summary>
		[EnumMember(Value = "double_range")]
		DoubleRange,

		/// <summary>
		/// A range of date values represented as unsigned 64-bit integer milliseconds elapsed since system epoch.
		/// </summary>
		[EnumMember(Value = "date_range")]
		DateRange,

		/// <summary>
		/// A range of ip values supporting either IPv4 or IPv6 (or mixed) addresses.
		/// </summary>
		[EnumMember(Value = "ip_range")]
		IpRange
	}

	internal static class RangeTypeExtensions
	{
		public static FieldType ToFieldType(this RangeType rangeType)
		{
			switch (rangeType)
			{
				case RangeType.IntegerRange: return FieldType.IntegerRange;
				case RangeType.FloatRange: return FieldType.FloatRange;
				case RangeType.LongRange: return FieldType.LongRange;
				case RangeType.DoubleRange: return FieldType.DoubleRange;
				case RangeType.DateRange: return FieldType.DateRange;
				case RangeType.IpRange: return FieldType.IpRange;
				default:
					throw new ArgumentOutOfRangeException(nameof(rangeType), rangeType, null);
			}
		}
	}
}
