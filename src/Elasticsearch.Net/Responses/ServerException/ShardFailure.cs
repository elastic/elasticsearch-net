using System;
using System.Collections.Generic;

namespace Elasticsearch.Net
{
	public class ShardFailure
	{
		public string Index { get; set; }
		public string Node { get; set; }
		public ErrorCause Reason { get; set; }
		public int? Shard { get; set; }
		public string Status { get; set; }

		internal static ShardFailure CreateShardFailure(IDictionary<string, object> dict, IJsonSerializerStrategy strategy)
		{
			var f = new ShardFailure();
			if (dict.TryGetValue("shard", out var shard)) f.Shard = Convert.ToInt32(shard);
			if (dict.TryGetValue("index", out var index)) f.Index = Convert.ToString(index);
			if (dict.TryGetValue("node", out var node)) f.Node = Convert.ToString(node);
			if (dict.TryGetValue("status", out var status)) f.Status = Convert.ToString(status);
			if (dict.TryGetValue("reason", out var reason))
			{
				var cause = (ErrorCause)strategy.DeserializeObject(reason, typeof(ErrorCause));
				f.Reason = cause;
			}
			return f;
		}
	}
}
