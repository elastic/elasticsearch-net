// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;

namespace Nest
{
	public class RangeBucket : BucketBase
	{
		public RangeBucket(IReadOnlyDictionary<string, IAggregate> dict) : base(dict) { }

		public long DocCount { get; set; }
		public double? From { get; set; }
		public string FromAsString { get; set; }

		public string Key { get; set; }
		public double? To { get; set; }
		public string ToAsString { get; set; }
	}
}
