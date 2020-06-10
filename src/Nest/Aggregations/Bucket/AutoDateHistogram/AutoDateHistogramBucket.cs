// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;

namespace Nest
{
	public class AutoDateHistogramAggregate : MultiBucketAggregate<DateHistogramBucket>
	{
		[Obsolete("Use AutoInterval. This property is incorrectly mapped to the wrong type")]
		public Time Interval { get; internal set; }

		public DateMathTime AutoInterval { get; internal set; }
	}
}
