using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Elasticsearch.Net
{
	public class ErrorCause
	{
		public ErrorCause CausedBy { get; set; }
		public ErrorCauseMetadata Metadata { get; set; }
		public string Reason { get; set; }
		public string StackTrace { get; set; }
		public string Type { get; set; }

		internal static ErrorCause CreateErrorCause(IDictionary<string, object> dict, IJsonSerializerStrategy strategy)
		{
			var causedBy = new ErrorCause();
			causedBy.FillValues(dict);
			if (dict.TryGetValue("caused_by", out var innerCausedBy))
				causedBy.CausedBy = (ErrorCause)strategy.DeserializeObject(innerCausedBy, typeof(ErrorCause));

			causedBy.Metadata = ErrorCauseMetadata.CreateCauseMetadata(dict, strategy);

			return causedBy;
		}

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

			internal static ErrorCauseMetadata CreateCauseMetadata(IDictionary<string, object> dict, IJsonSerializerStrategy strategy)
			{
				var m = new ErrorCauseMetadata();
				if (dict.TryGetValue("license.expired.feature", out var feature)) m.LicensedExpiredFeature = Convert.ToString(feature);
				if (dict.TryGetValue("index", out var index)) m.Index = Convert.ToString(index);
				if (dict.TryGetValue("index_uuid", out var indexUUID)) m.IndexUUID = Convert.ToString(indexUUID);
				if (dict.TryGetValue("resource.type", out var resourceType)) m.ResourceType = Convert.ToString(resourceType);
				if (dict.TryGetValue("resource.id", out var resourceId)) m.ResourceId = GetStringArray(resourceId, strategy);
				if (dict.TryGetValue("shard", out var shard)) m.Shard = Convert.ToInt32(shard);
				if (dict.TryGetValue("line", out var line)) m.Line = Convert.ToInt32(line);
				if (dict.TryGetValue("col", out var column)) m.Column = Convert.ToInt32(column);
				if (dict.TryGetValue("bytes_wanted", out var bytesWanted)) m.BytesWanted = Convert.ToInt64(bytesWanted);
				if (dict.TryGetValue("bytes_limit", out var bytesLimit)) m.BytesLimit = Convert.ToInt64(bytesLimit);

				if (dict.TryGetValue("phase", out var phase)) m.Phase = Convert.ToString(phase);
				if (dict.TryGetValue("grouped", out var grouped)) m.Grouped = Convert.ToBoolean(grouped);

				if (dict.TryGetValue("script_stack", out var scriptStack)) m.ScriptStack = GetStringArray(scriptStack, strategy);
				if (dict.TryGetValue("script", out var script)) m.Script = Convert.ToString(script);
				if (dict.TryGetValue("lang", out var language)) m.Language = Convert.ToString(language);
				if (dict.TryGetValue("failed_shards", out var failedShards))
					m.FailedShards = GetShardFailures(failedShards, strategy);
				return m;
			}

			private static IReadOnlyCollection<ShardFailure> GetShardFailures(object value, IJsonSerializerStrategy strategy)
			{
				if (!(value is object[] objects))
					return DefaultFailedShards;

				var values = new List<ShardFailure>();
				foreach (var v in objects)
				{
					var cause = (ShardFailure)strategy.DeserializeObject(v, typeof(ShardFailure));
					if (cause != null) values.Add(cause);
				}
				return new ReadOnlyCollection<ShardFailure>(values.ToArray());
			}

			private static IReadOnlyCollection<string> GetStringArray(object value, IJsonSerializerStrategy strategy)
			{
				if (value is string s) return new ReadOnlyCollection<string>(new[] { s });

				if (value is object[] objects)
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
}
