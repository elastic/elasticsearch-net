// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;

namespace Nest
{
	public class SignificantTermsBucket<TKey> : BucketBase, IBucket
	{
		public SignificantTermsBucket(IReadOnlyDictionary<string, IAggregate> dict) : base(dict) { }

		public long BgCount { get; set; }
		public long DocCount { get; set; }

		public TKey Key { get; set; }
		public double Score { get; set; }
	}
}
