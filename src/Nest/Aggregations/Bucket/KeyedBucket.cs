// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;

namespace Nest
{
	public class KeyedBucket<TKey> : BucketBase
	{
		public KeyedBucket(IReadOnlyDictionary<string, IAggregate> dict) : base(dict) { }

		public long? DocCount { get; set; }

		public long? DocCountErrorUpperBound { get; set; }

		public TKey Key { get; set; }
		public string KeyAsString { get; set; }
	}
}
