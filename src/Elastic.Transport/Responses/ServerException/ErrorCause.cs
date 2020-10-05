// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Elastic.Transport
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
		public IReadOnlyDictionary<string, object> AdditionalProperties { get; set; } = DefaultDictionary;

		public long? BytesLimit { get; set; }

		public long? BytesWanted { get; set; }

		public ErrorCause CausedBy { get; set; }

		public int? Column { get; set; }

		public IReadOnlyCollection<ShardFailure> FailedShards { get; set; } = DefaultFailedShards;

		public bool? Grouped { get; set; }

		public string Index { get; set; }

		public string IndexUUID { get; set; }

		public string Language { get; set; }

		public string LicensedExpiredFeature { get; set; }

		public int? Line { get; set; }

		public string Phase { get; set; }

		public string Reason { get; set; }

		public IReadOnlyCollection<string> ResourceId { get; set; } = DefaultCollection;

		public string ResourceType { get; set; }

		public string Script { get; set; }

		public IReadOnlyCollection<string> ScriptStack { get; set; } = DefaultCollection;

		public int? Shard { get; set; }

		public string StackTrace { get; set; }

		public string Type { get; set; }

		public override string ToString() => CausedBy == null
			? $"Type: {Type} Reason: \"{Reason}\""
			: $"Type: {Type} Reason: \"{Reason}\" CausedBy: \"{CausedBy}\"";
	}
}
