using System;
using System.Collections.Generic;
using Utf8Json;

namespace Elasticsearch.Net
{
	[JsonFormatter(typeof(ShardFailureFormatter))]
	public class ShardFailure
	{
		public string Index { get; set; }
		public string Node { get; set; }
		public ErrorCause Reason { get; set; }
		public int? Shard { get; set; }
		public string Status { get; set; }
	}

	public class ShardFailureFormatter : IJsonFormatter<ShardFailure>
	{
		public void Serialize(ref JsonWriter writer, ShardFailure value, IJsonFormatterResolver formatterResolver) =>
			throw new NotSupportedException();

		public ShardFailure Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var failure = new ShardFailure();

			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
			{
				reader.ReadNextBlock();
				return failure;
			}

			var formatter = formatterResolver.GetFormatter<Dictionary<string, object>>();
			var dict = formatter.Deserialize(ref reader, formatterResolver);

			if (dict.TryGetValue("shard", out var shard)) failure.Shard = Convert.ToInt32(shard);
			if (dict.TryGetValue("index", out var index)) failure.Index = Convert.ToString(index);
			if (dict.TryGetValue("node", out var node)) failure.Node = Convert.ToString(node);
			if (dict.TryGetValue("status", out var status)) failure.Status = Convert.ToString(status);
			if (dict.TryGetValue("reason", out var reason))
			{
				var cause = formatterResolver.ReserializeAndDeserialize<ErrorCause>(reason);
				failure.Reason = cause;
			}
			return failure;
		}
	}
}
