// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;

namespace Nest
{
	public class DateHistogramBucket : KeyedBucket<double>
	{
		private static readonly long EpochTicks = new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero).Ticks;

		public DateHistogramBucket(IReadOnlyDictionary<string, IAggregate> dict) : base(dict) { }

		// Get a DateTime form of the returned key
		public DateTime Date => new DateTime(EpochTicks + (long)Key * TimeSpan.TicksPerMillisecond, DateTimeKind.Utc);
	}
}
