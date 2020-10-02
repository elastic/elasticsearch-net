// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;
namespace Nest
{
	public class GetAutoFollowPatternResponse : ResponseBase
	{
		[DataMember(Name = "patterns")]
		[JsonFormatter(typeof(AutoFollowPatternFormatter))]
		public IReadOnlyDictionary<string, AutoFollowPattern> Patterns { get; internal set; } = EmptyReadOnly<string, AutoFollowPattern>.Dictionary;
	}

	internal class AutoFollowPatternFormatter : IJsonFormatter<IReadOnlyDictionary<string, AutoFollowPattern>>
	{
		private static readonly AutomataDictionary AutoFollowPatternFields = new AutomataDictionary
		{
			{ "name", 0 },
			{ "pattern", 1 }
		};

		public IReadOnlyDictionary<string, AutoFollowPattern> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.ReadIsNull())
				return null;

			var count = 0;
			var dictionary = new Dictionary<string, AutoFollowPattern>();
			while (reader.ReadIsInArray(ref count))
			{
				var innerCount = 0;
				string name = null;
				AutoFollowPattern pattern = null;
				var autoFollowPatternFormatter = formatterResolver.GetFormatter<AutoFollowPattern>();
				while (reader.ReadIsInObject(ref innerCount))
				{
					if (AutoFollowPatternFields.TryGetValue(reader.ReadPropertyNameSegmentRaw(), out var value))
					{
						switch (value)
						{
							case 0:
								name = reader.ReadString();
								break;
							case 1:
								pattern = autoFollowPatternFormatter.Deserialize(ref reader, formatterResolver);
								break;
						}
					}
					else
						reader.ReadNextBlock();

					if (name != null && pattern != null)
						dictionary.Add(name, pattern);
				}
			}

			return dictionary;
		}

		public void Serialize(ref JsonWriter writer, IReadOnlyDictionary<string, AutoFollowPattern> value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var count = 0;
			var autoFollowPatternFormatter = formatterResolver.GetFormatter<IAutoFollowPattern>();
			writer.WriteBeginArray();
			foreach (var pattern in value)
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WriteBeginObject();
				writer.WritePropertyName("name");
				writer.WriteString(pattern.Key);
				writer.WriteValueSeparator();
				writer.WritePropertyName("pattern");
				autoFollowPatternFormatter.Serialize(ref writer, pattern.Value, formatterResolver);
				writer.WriteEndObject();
				count++;
			}
			writer.WriteEndArray();
		}
	}

	[InterfaceDataContract]
	[ReadAs(typeof(AutoFollowPattern))]
	public interface IAutoFollowPattern
	{
		/// <summary>
		/// the name of follower index; the template {{leader_index}} can be used to derive the name of the follower index from the
		/// name of the leader index
		/// </summary>
		[DataMember(Name = "follow_index_pattern")]
		string FollowIndexPattern { get; set; }

		/// <summary>
		/// an array of simple index patterns to match against indices in the remote cluster specified by the remote_cluster field
		/// </summary>
		[DataMember(Name = "leader_index_patterns")]
		IEnumerable<string> LeaderIndexPatterns { get; set; }

		/// <summary>
		/// Settings to override from the leader index.
		/// Note that certain settings can not be overrode e.g. index.number_of_shards.
		/// <para />
		/// Valid in Elasticsearch 7.9.0+
		/// </summary>
		[DataMember(Name ="settings")]
		IIndexSettings Settings { get; set; }

		/// <summary>
		/// the maximum number of outstanding reads requests from the remote cluster
		/// </summary>
		[DataMember(Name = "max_outstanding_read_requests")]
		long? MaxOutstandingReadRequests { get; set; }

		/// <summary>
		/// the maximum number of outstanding write requests on the follower
		/// </summary>
		[DataMember(Name = "max_outstanding_write_requests")]
		int? MaxOutstandingWriteRequests { get; set; }

		/// <summary>
		/// the maximum time to wait for new operations on the remote cluster when the follower index is synchronized with the
		/// leader index; when the timeout has elapsed, the poll for operations will return to the follower so that it can update
		/// some statistics, and then the follower will immediately attempt to read from the leader again
		/// </summary>
		[DataMember(Name = "read_poll_timeout")]
		Time MaxPollTimeout { get; set; }

		/// <summary>
		/// the maximum number of operations to pull per read from the remote cluster
		/// </summary>
		[DataMember(Name = "max_read_request_operation_count")]
		int? MaxReadRequestOperationCount { get; set; }

		/// <summary>
		/// the maximum size in bytes of per read of a batch of operations pulled from the remote cluster
		/// </summary>
		[DataMember(Name = "max_read_request_size")]
		string MaxReadRequestSize { get; set; }

		/// <summary>
		/// the maximum time to wait before retrying an operation that failed exceptionally; an exponential backoff strategy is
		/// employed when retrying
		/// </summary>
		[DataMember(Name = "max_retry_delay")]
		Time MaxRetryDelay { get; set; }

		/// <summary>
		/// the maximum number of operations that can be queued for writing; when this limit is reached, reads from the remote
		/// cluster will be deferred until the number of queued operations goes below the limit
		/// </summary>
		[DataMember(Name = "max_write_buffer_count")]
		int? MaxWriteBufferCount { get; set; }

		/// <summary>
		/// the maximum total bytes of operations that can be queued for writing; when this limit is reached, reads from the remote
		/// cluster will be deferred until the total bytes of queued operations goes below the limit
		/// </summary>
		[DataMember(Name = "max_write_buffer_size")]
		string MaxWriteBufferSize { get; set; }

		/// <summary>
		/// the maximum number of operations per bulk write request executed on the follower
		/// </summary>
		[DataMember(Name = "max_write_request_operation_count")]
		int? MaxWriteRequestOperationCount { get; set; }

		/// <summary>
		/// the maximum total bytes of operations per bulk write request executed on the follower
		/// </summary>
		[DataMember(Name = "max_write_request_size")]
		string MaxWriteRequestSize { get; set; }

		/// <summary>
		/// the remote cluster containing the leader indices to match against
		/// </summary>
		[DataMember(Name = "remote_cluster")]
		string RemoteCluster { get; set; }
	}

	public class AutoFollowPattern : IAutoFollowPattern
	{
		/// <inheritdoc cref="IAutoFollowPattern.FollowIndexPattern" />
		public string FollowIndexPattern { get; set; }

		/// <inheritdoc cref="IAutoFollowPattern.LeaderIndexPatterns" />
		public IEnumerable<string> LeaderIndexPatterns { get; set; }

		/// <inheritdoc cref="IAutoFollowPattern.Settings" />
		public IIndexSettings Settings { get; set; }

		/// <inheritdoc cref="IAutoFollowPattern.MaxOutstandingReadRequests" />
		public long? MaxOutstandingReadRequests { get; set; }

		/// <inheritdoc cref="IAutoFollowPattern.MaxOutstandingWriteRequests" />
		public int? MaxOutstandingWriteRequests { get; set; }

		/// <inheritdoc cref="IAutoFollowPattern.MaxPollTimeout" />
		public Time MaxPollTimeout { get; set; }

		/// <inheritdoc cref="IAutoFollowPattern.MaxReadRequestOperationCount" />
		public int? MaxReadRequestOperationCount { get; set; }

		/// <inheritdoc cref="IAutoFollowPattern.MaxReadRequestSize" />
		public string MaxReadRequestSize { get; set; }

		/// <inheritdoc cref="IAutoFollowPattern.MaxRetryDelay" />
		public Time MaxRetryDelay { get; set; }

		/// <inheritdoc cref="IAutoFollowPattern.MaxWriteBufferCount" />
		public int? MaxWriteBufferCount { get; set; }

		/// <inheritdoc cref="IAutoFollowPattern.MaxWriteBufferSize" />
		public string MaxWriteBufferSize { get; set; }

		/// <inheritdoc cref="IAutoFollowPattern.MaxWriteRequestOperationCount" />
		public int? MaxWriteRequestOperationCount { get; set; }

		/// <inheritdoc cref="IAutoFollowPattern.MaxWriteRequestSize" />
		public string MaxWriteRequestSize { get; set; }

		/// <inheritdoc cref="IAutoFollowPattern.RemoteCluster" />
		public string RemoteCluster { get; set; }
	}
}
