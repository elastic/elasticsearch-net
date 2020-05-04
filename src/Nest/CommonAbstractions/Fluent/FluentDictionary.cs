// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;

namespace Nest
{
	public class FluentDictionary<TKey, TValue> : Dictionary<TKey, TValue>
	{
		public FluentDictionary() { }

		public FluentDictionary(IDictionary<TKey, TValue> copy)
		{
			if (copy == null)
				return;

			foreach (var kv in copy)
				this[kv.Key] = kv.Value;
		}

		public new FluentDictionary<TKey, TValue> Add(TKey key, TValue value)
		{
			base.Add(key, value);
			return this;
		}

		public new FluentDictionary<TKey, TValue> Remove(TKey key)
		{
			base.Remove(key);
			return this;
		}
	}
}
