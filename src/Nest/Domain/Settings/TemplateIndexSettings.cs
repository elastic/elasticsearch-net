using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using Nest.Resolvers.Converters;

namespace Nest
{
	public class TemplateIndexSettings : Dictionary<string, object>
	{

		public int? NumberOfShards
		{
			get
			{
				return this.GetIntegerValue("index.number_of_shards");
			}
			set
			{
				this.TryAdd("index.number_of_shards", value);
			}
		}
		public int? NumberOfReplicas
		{
			get
			{
				return this.GetIntegerValue("index.number_of_replicas");
			}
			set
			{
				this.TryAdd("index.number_of_replicas", value);
			}
		}

		internal int? GetIntegerValue(string key)
		{
			object value;
			int i = 0;
			if (!this.TryGetValue(key, out value)
				|| value == null
				|| !int.TryParse(value.ToString(), out i))
				return null;
			return i;
		}

		public void TryAdd(string key, object value)
		{
			if (this.ContainsKey(key))
				this[key] = value;
			else
				this.Add(key, value);
		}

	}
}