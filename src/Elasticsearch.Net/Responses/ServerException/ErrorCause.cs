using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Elasticsearch.Net
{
	[JsonFormatter(typeof(ErrorCauseFormatter))]
	public class ErrorCause
	{
		[DataMember(Name = "caused_by")]
		public ErrorCause CausedBy { get; set; }

		public ErrorCauseMetadata Metadata { get; set; }

		[DataMember(Name = "reason")]
		public string Reason { get; set; }

		[DataMember(Name = "stack_trace")]
		public string StackTrace { get; set; }

		[DataMember(Name = "type")]
		public string Type { get; set; }

		public override string ToString() => CausedBy == null
			? $"Type: {Type} Reason: \"{Reason}\""
			: $"Type: {Type} Reason: \"{Reason}\" CausedBy: \"{CausedBy}\"";

		public class ErrorCauseMetadata
		{
			private static readonly IReadOnlyCollection<string> DefaultCollection =
				new ReadOnlyCollection<string>(new string[0] { });

			private static readonly IReadOnlyCollection<ShardFailure> DefaultFailedShards =
				new ReadOnlyCollection<ShardFailure>(new ShardFailure[0] { });

			public long? BytesLimit { get; set; }

			public long? BytesWanted { get; set; }
			public int? Column { get; set; }

			public IReadOnlyCollection<ShardFailure> FailedShards { get; set; } = DefaultFailedShards;
			public bool? Grouped { get; set; }
			public string Index { get; set; }
			public string IndexUUID { get; set; }
			public string Language { get; set; }

			public string LicensedExpiredFeature { get; set; }

			public int? Line { get; set; }

			public string Phase { get; set; }
			public IReadOnlyCollection<string> ResourceId { get; set; } = DefaultCollection;
			public string ResourceType { get; set; }
			public string Script { get; set; }

			public IReadOnlyCollection<string> ScriptStack { get; set; } = DefaultCollection;
			public int? Shard { get; set; }

			internal static ErrorCauseMetadata CreateCauseMetadata(IDictionary<string, object> dict, IJsonFormatterResolver formatterResolver)
			{
				var m = new ErrorCauseMetadata();
				if (dict.TryGetValue("license.expired.feature", out var feature)) m.LicensedExpiredFeature = Convert.ToString(feature);
				if (dict.TryGetValue("index", out var index)) m.Index = Convert.ToString(index);
				if (dict.TryGetValue("index_uuid", out var indexUUID)) m.IndexUUID = Convert.ToString(indexUUID);
				if (dict.TryGetValue("resource.type", out var resourceType)) m.ResourceType = Convert.ToString(resourceType);
				if (dict.TryGetValue("resource.id", out var resourceId)) m.ResourceId = GetStringArray(resourceId);
				if (dict.TryGetValue("shard", out var shard)) m.Shard = Convert.ToInt32(shard);
				if (dict.TryGetValue("line", out var line)) m.Line = Convert.ToInt32(line);
				if (dict.TryGetValue("col", out var column)) m.Column = Convert.ToInt32(column);
				if (dict.TryGetValue("bytes_wanted", out var bytesWanted)) m.BytesWanted = Convert.ToInt64(bytesWanted);
				if (dict.TryGetValue("bytes_limit", out var bytesLimit)) m.BytesLimit = Convert.ToInt64(bytesLimit);

				if (dict.TryGetValue("phase", out var phase)) m.Phase = Convert.ToString(phase);
				if (dict.TryGetValue("grouped", out var grouped)) m.Grouped = Convert.ToBoolean(grouped);

				if (dict.TryGetValue("script_stack", out var scriptStack)) m.ScriptStack = GetStringArray(scriptStack);
				if (dict.TryGetValue("script", out var script)) m.Script = Convert.ToString(script);
				if (dict.TryGetValue("lang", out var language)) m.Language = Convert.ToString(language);
				if (dict.TryGetValue("failed_shards", out var failedShards))
					m.FailedShards = GetShardFailures(failedShards, formatterResolver);
				return m;
			}

			private static IReadOnlyCollection<ShardFailure> GetShardFailures(object value, IJsonFormatterResolver formatterResolver)
			{
				if (!(value is List<object> objects))
					return DefaultFailedShards;

				var values = new List<ShardFailure>();
				foreach (var v in objects)
				{
					var cause = formatterResolver.ReserializeAndDeserialize<ShardFailure>(v);
					if (cause != null) values.Add(cause);
				}
				return new ReadOnlyCollection<ShardFailure>(values.ToArray());
			}

			private static IReadOnlyCollection<string> GetStringArray(object value)
			{
				if (value is string s) return new ReadOnlyCollection<string>(new[] { s });

				if (value is List<object> objects)
				{
					var values = new List<string>();
					foreach (var v in objects)
					{
						if (v is string vs) values.Add(vs);
					}
					return new ReadOnlyCollection<string>(values.ToArray());
				}
				return DefaultCollection;
			}
		}
	}

	internal class ErrorCauseFormatter : IJsonFormatter<ErrorCause>
	{
		public ErrorCause Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();

			switch (token)
			{
				case JsonToken.BeginObject:
				{
					var formatter = formatterResolver.GetFormatter<Dictionary<string, object>>();
					var dict = formatter.Deserialize(ref reader, formatterResolver);
					var errorCause = new ErrorCause();
					errorCause.FillValues(dict);

					if (dict.TryGetValue("caused_by", out var causedBy))
						errorCause.CausedBy = formatterResolver.ReserializeAndDeserialize<ErrorCause>(causedBy);

					errorCause.Metadata = ErrorCause.ErrorCauseMetadata.CreateCauseMetadata(dict, formatterResolver);

					return errorCause;
				}
				case JsonToken.String:
				{
					var errorCause = new ErrorCause { Reason = reader.ReadString() };
					return errorCause;
				}
				default:
					reader.ReadNextBlock();
					return null;
			}
		}

		public void Serialize(ref JsonWriter writer, ErrorCause value, IJsonFormatterResolver formatterResolver) =>
			throw new NotSupportedException();
	}
}
