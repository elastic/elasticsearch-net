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

namespace Nest
{
	public class DateRange
	{
		[DataMember(Name = "gt")]
		public DateTimeOffset? GreaterThan { get; set; }

		[DataMember(Name = "gte")]
		public DateTimeOffset? GreaterThanOrEqualTo { get; set; }

		[DataMember(Name = "lt")]
		public DateTimeOffset? LessThan { get; set; }

		[DataMember(Name = "lte")]
		public DateTimeOffset? LessThanOrEqualTo { get; set; }
	}

	public class DoubleRange
	{
		[DataMember(Name = "gt")]
		public double? GreaterThan { get; set; }

		[DataMember(Name = "gte")]
		public double? GreaterThanOrEqualTo { get; set; }

		[DataMember(Name = "lt")]
		public double? LessThan { get; set; }

		[DataMember(Name = "lte")]
		public double? LessThanOrEqualTo { get; set; }
	}

	public class FloatRange
	{
		[DataMember(Name = "gt")]
		public float? GreaterThan { get; set; }

		[DataMember(Name = "gte")]
		public float? GreaterThanOrEqualTo { get; set; }

		[DataMember(Name = "lt")]
		public float? LessThan { get; set; }

		[DataMember(Name = "lte")]
		public float? LessThanOrEqualTo { get; set; }
	}

	public class IntegerRange
	{
		[DataMember(Name = "gt")]
		public int? GreaterThan { get; set; }

		[DataMember(Name = "gte")]
		public int? GreaterThanOrEqualTo { get; set; }

		[DataMember(Name = "lt")]
		public int? LessThan { get; set; }

		[DataMember(Name = "lte")]
		public int? LessThanOrEqualTo { get; set; }
	}

	public class LongRange
	{
		[DataMember(Name = "gt")]
		public long? GreaterThan { get; set; }

		[DataMember(Name = "gte")]
		public long? GreaterThanOrEqualTo { get; set; }

		[DataMember(Name = "lt")]
		public long? LessThan { get; set; }

		[DataMember(Name = "lte")]
		public long? LessThanOrEqualTo { get; set; }
	}

	public class IpAddressRange
	{
		[DataMember(Name = "gt")]
		public string GreaterThan { get; set; }

		[DataMember(Name = "gte")]
		public string GreaterThanOrEqualTo { get; set; }

		[DataMember(Name = "lt")]
		public string LessThan { get; set; }

		[DataMember(Name = "lte")]
		public string LessThanOrEqualTo { get; set; }
	}
}
