using System;
using System.Collections.Generic;

namespace Elasticsearch.Net.Serialization
{
	internal class ElasticsearchNetJsonStrategy : PocoJsonSerializerStrategy
	{
		public override object DeserializeObject(object value, Type type)
		{
			if (type == typeof(DynamicResponse))
			{
				var dict = base.DeserializeObject(value, typeof(Dictionary<string, object>)) as IDictionary<string, object>;
				return dict == null ? null : DynamicResponse.Create(dict);
			}
			return base.DeserializeObject(value, type);
		}

	}
}