// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Elasticsearch.Net
{
	[DataContract]
	public class ErrorCause
	{
		private static readonly IReadOnlyCollection<string> DefaultCollection =
			new ReadOnlyCollection<string>(new string[0]);

		private static readonly IReadOnlyDictionary<string, object> DefaultDictionary =
			new ReadOnlyDictionary<string, object>(new Dictionary<string, object>());

		private static readonly IReadOnlyCollection<ShardFailure> DefaultFailedShards =
			new ReadOnlyCollection<ShardFailure>(new ShardFailure[0]);

		/// <summary>
		/// Additional properties related to the error cause. Contains properties that
		/// are not explicitly mapped on <see cref="ErrorCause" />
		/// </summary>
		public IReadOnlyDictionary<string, object> AdditionalProperties { get; internal set; } = DefaultDictionary;

		public long? BytesLimit { get; internal set; }

		public long? BytesWanted { get; internal set; }

		public ErrorCause CausedBy { get; internal set; }

		public int? Column { get; internal set; }

		public IReadOnlyCollection<ShardFailure> FailedShards { get; internal set; } = DefaultFailedShards;

		public bool? Grouped { get; internal set; }

		public string Index { get; internal set; }

		public string IndexUUID { get; internal set; }

		public string Language { get; internal set; }

		public string LicensedExpiredFeature { get; internal set; }

		public int? Line { get; internal set; }

		public string Phase { get; internal set; }

		public string Reason { get; internal set; }

		public IReadOnlyCollection<string> ResourceId { get; internal set; } = DefaultCollection;

		public string ResourceType { get; internal set; }

		public string Script { get; internal set; }

		public IReadOnlyCollection<string> ScriptStack { get; internal set; } = DefaultCollection;

		public int? Shard { get; internal set; }

		public string StackTrace { get; internal set; }

		public string Type { get; internal set; }

		public override string ToString() => CausedBy == null
			? $"Type: {Type} Reason: \"{Reason}\""
			: $"Type: {Type} Reason: \"{Reason}\" CausedBy: \"{CausedBy}\"";
	}
}
