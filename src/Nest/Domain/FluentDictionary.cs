using System;
using System.Collections.Generic;
using Nest.Resolvers;

namespace Nest
{
	public class FluentDictionary<K, V> : Dictionary<K, V>
	{
		public FluentDictionary()
		{
		}

		public FluentDictionary(IDictionary<K, V> copy)
		{
			if (copy == null)
				return;

			foreach (var kv in copy)
				this[kv.Key] = kv.Value;
		}

		public new FluentDictionary<K, V> Add(K k, V v)
		{
			base.Add(k, v);
			return this;
		}
		public new FluentDictionary<K, V> Remove(K k)
		{
			base.Remove(k);
			return this;
		}
	}
}