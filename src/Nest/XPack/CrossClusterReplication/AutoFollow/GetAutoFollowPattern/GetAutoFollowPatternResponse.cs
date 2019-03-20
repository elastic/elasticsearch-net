using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetAutoFollowPatternResponse : IResponse
	{
		[JsonIgnore]
		IReadOnlyDictionary<string, AutoFollowPattern> Patterns { get; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(GetAutoFollowPatternResponseConverter))]
	public class GetAutoFollowPatternResponse : DictionaryResponseBase<string, AutoFollowPattern>, IGetAutoFollowPatternResponse
	{
		[JsonIgnore]
		public IReadOnlyDictionary<string, AutoFollowPattern> Patterns => Self.BackingDictionary;
	}

	// Custom converter required because format of response changed between 6.5 and 6.6.
	internal class GetAutoFollowPatternResponseConverter : JsonConverter
	{
		public override bool CanWrite { get; } = false;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotSupportedException();

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var response = new GetAutoFollowPatternResponse();
			IDictionaryResponse<string, AutoFollowPattern> dictResponse = response;
			var dict = new Dictionary<string, AutoFollowPattern>();
			dictResponse.BackingDictionary = dict;
			bool checkedError = false;

			if (reader.TokenType != JsonToken.StartObject)
				return response;

			while (reader.Read() && reader.TokenType != JsonToken.EndObject)
			{
				var propertyName = (string)reader.Value;
				if (propertyName == "patterns")
				{
					// 6.6
					reader.Read(); // opening [ of patterns array
					while (reader.Read() && reader.TokenType != JsonToken.EndArray)
					{
						string name = null;
						AutoFollowPattern pattern = null;
						while (reader.Read() && reader.TokenType != JsonToken.EndObject)
						{
							propertyName = (string)reader.Value;
							if (propertyName == "name")
								name = reader.ReadAsString();
							else
							{
								reader.Read();
								pattern = serializer.Deserialize<AutoFollowPattern>(reader);
							}
						}

						dict.Add(name, pattern);
					}
				}
				else if (!checkedError && propertyName == "error")
				{
					reader.Read();
					response.Error = reader.TokenType == JsonToken.String
						? new Error { Reason = (string)reader.Value }
						: serializer.Deserialize<Error>(reader);
				}
				else if (!checkedError && propertyName == "status")
					response.StatusCode = reader.ReadAsInt32();
				else
				{
					checkedError = true;

					// 6.5
					reader.Read(); // opening { of AutoFollowPattern
					dict.Add(propertyName, serializer.Deserialize<AutoFollowPattern>(reader));
				}
			}

			return response;
		}

		public override bool CanConvert(Type objectType) => true;
	}

	public interface IAutoFollowPattern
	{
		/// <summary>
		/// the remote cluster containing the leader indices to match against
		/// </summary>
		[JsonProperty("remote_cluster")]
		string RemoteCluster { get; set; }

		/// <summary>
		/// an array of simple index patterns to match against indices in the remote cluster specified by the remote_cluster field
		/// </summary>
		[JsonProperty("leader_index_patterns")]
		IEnumerable<string> LeaderIndexPatterns { get; set; }

		/// <summary>
		/// the name of follower index; the template {{leader_index}} can be used to derive the name of the follower index from the name of the leader index
		/// </summary>
		[JsonProperty("follow_index_pattern")]
		string FollowIndexPattern { get; set; }

		/// <summary>
		/// the maximum number of operations to pull per read from the remote cluster
		/// </summary>
		[JsonProperty("max_read_request_operation_count")]
		int? MaxReadRequestOperationCount { get; set; }

		/// <summary>
		/// the maximum number of outstanding reads requests from the remote cluster
		/// </summary>
		[JsonProperty("max_outstanding_read_requests")]
		long? MaxOutstandingReadRequests { get; set; }

		/// <summary>
		/// the maximum size in bytes of per read of a batch of operations pulled from the remote cluster
		/// </summary>
		[JsonProperty("max_read_request_size")]
		string MaxReadRequestSize { get; set; }

		/// <summary>
		/// the maximum number of operations per bulk write request executed on the follower
		/// </summary>
		[JsonProperty("max_write_request_operation_count")]
		int? MaxWriteRequestOperationCount { get; set; }

		/// <summary>
		/// the maximum total bytes of operations per bulk write request executed on the follower
		/// </summary>
		[JsonProperty("max_write_request_size")]
		string MaxWriteRequestSize { get; set; }

		/// <summary>
		/// the maximum number of outstanding write requests on the follower
		/// </summary>
		[JsonProperty("max_outstanding_write_requests")]
		int? MaxOutstandingWriteRequests { get; set; }

		/// <summary>
		/// the maximum number of operations that can be queued for writing; when this limit is reached, reads from the remote cluster will be deferred until the number of queued operations goes below the limit
		/// </summary>
		[JsonProperty("max_write_buffer_count")]
		int? MaxWriteBufferCount { get; set; }

		/// <summary>
		/// the maximum total bytes of operations that can be queued for writing; when this limit is reached, reads from the remote cluster will be deferred until the total bytes of queued operations goes below the limit
		/// </summary>
		[JsonProperty("max_write_buffer_size")]
		string MaxWriteBufferSize { get; set; }

		/// <summary>
		/// the maximum time to wait before retrying an operation that failed exceptionally; an exponential backoff strategy is employed when retrying
		/// </summary>
		[JsonProperty("max_retry_delay")]
		Time MaxRetryDelay { get; set; }

		/// <summary>
		/// the maximum time to wait for new operations on the remote cluster when the follower index is synchronized with the leader index; when the timeout has elapsed, the poll for operations will return to the follower so that it can update some statistics, and then the follower will immediately attempt to read from the leader again
		/// </summary>
		[JsonProperty("read_poll_timeout")]
		Time MaxPollTimeout { get; set; }
	}

	public class AutoFollowPattern : IAutoFollowPattern
	{
		/// <inheritdoc cref="IAutoFollowPattern.RemoteCluster"/>
		public string RemoteCluster { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.LeaderIndexPatterns"/>
		public IEnumerable<string> LeaderIndexPatterns { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.FollowIndexPattern"/>
		public string FollowIndexPattern { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.MaxReadRequestOperationCount"/>
		public int? MaxReadRequestOperationCount { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.MaxOutstandingReadRequests"/>
		public long? MaxOutstandingReadRequests { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.MaxReadRequestSize"/>
		public string MaxReadRequestSize { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.MaxWriteRequestOperationCount"/>
		public int? MaxWriteRequestOperationCount { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.MaxWriteRequestSize"/>
		public string MaxWriteRequestSize { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.MaxOutstandingWriteRequests"/>
		public int? MaxOutstandingWriteRequests { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.MaxWriteBufferCount"/>
		public int? MaxWriteBufferCount { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.MaxWriteBufferSize"/>
		public string MaxWriteBufferSize { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.MaxRetryDelay"/>
		public Time MaxRetryDelay { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.MaxPollTimeout"/>
		public Time MaxPollTimeout { get; set; }
	}


}
