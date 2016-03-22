using System;
using System.Collections.Generic;

namespace Elasticsearch.Net
{
	internal class ElasticsearchNetJsonStrategy : PocoJsonSerializerStrategy
	{
		public static readonly ElasticsearchNetJsonStrategy Instance = new ElasticsearchNetJsonStrategy();

		public override object DeserializeObject(object value, Type type)
		{
			if (type == typeof(DynamicResponse))
			{
				var dict = base.DeserializeObject(value, typeof(IDictionary<string, object>)) as IDictionary<string, object>;
				return dict == null ? null : DynamicResponse.Create(dict);
			}
			if (type == typeof(ServerError))
			{
				var dict = base.DeserializeObject(value, typeof(IDictionary<string, object>)) as IDictionary<string, object>;
				return ServerError.Create(dict, this);
			}
			if (type == typeof(Error))
			{
				var dict = base.DeserializeObject(value, typeof(IDictionary<string, object>)) as IDictionary<string, object>;
				return Error.Create(dict, this);
			}
			if (type == typeof(RootCause))
			{
				var dict = base.DeserializeObject(value, typeof(IDictionary<string, object>)) as IDictionary<string, object>;
				return RootCause.Create(dict, this);
			}
			if (type == typeof(CausedBy))
			{
				var dict = base.DeserializeObject(value, typeof(IDictionary<string, object>)) as IDictionary<string, object>;
				return CausedBy.Create(dict, this);
			}
			return base.DeserializeObject(value, type);
		}
	}
}