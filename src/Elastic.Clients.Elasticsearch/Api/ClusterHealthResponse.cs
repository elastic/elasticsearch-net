// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.Cluster.Health;

namespace Elastic.Clients.Elasticsearch.Cluster
{
	[JsonConverter(typeof(ClusterHealthResponseConverter))]
	public partial class ClusterHealthResponse
	{

	}

	/// <summary>
	/// This converter is required to deal with the conflict for "status" on the ResponseBase class for error scenarios.
	/// </summary>
	internal sealed class ClusterHealthResponseConverter : JsonConverter<ClusterHealthResponse>
	{
		public override ClusterHealthResponse? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.Null)
				return null;

			HealthStatus? status = null;
			string clusterName = null;
			var timedOut = false;
			var numberOfNodes = 0;
			var numberOfDataNodes = 0;
			var activeShards = 0;
			var activePrimaryShards = 0;
			var relocatingShards = 0;
			var initializingShards = 0;
			var unassignedShards = 0;
			var delayedUnassignedShards = 0;
			var pendingTasks = 0;
			var inFlightFetch = 0;
			long taskMaxWaitingTimeInQueue = default;
			double activeShardsAsPercentage = default;
			ReadOnlyIndexNameDictionary<IndexHealthStats> indices = null;

			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					var property = reader.GetString();

					switch (property)
					{
						case "cluster_name":
							reader.Read();
							clusterName = reader.GetString();
							break;
						case "status":
							status = JsonSerializer.Deserialize<HealthStatus>(ref reader, options);
							break;
						case "timed_out":
							reader.Read();
							timedOut = reader.GetBoolean();
							break;
						case "number_of_nodes":
							reader.Read();
							numberOfNodes = reader.GetInt32();
							break;
						case "number_of_data_nodes":
							reader.Read();
							numberOfDataNodes = reader.GetInt32();
							break;
						case "active_shards":
							reader.Read();
							activeShards = reader.GetInt32();
							break;
						case "active_primary_shards":
							reader.Read();
							activePrimaryShards = reader.GetInt32();
							break;
						case "relocating_shards":
							reader.Read();
							relocatingShards = reader.GetInt32();
							break;
						case "initializing_shards":
							reader.Read();
							initializingShards = reader.GetInt32();
							break;
						case "unassigned_shards":
							reader.Read();
							unassignedShards = reader.GetInt32();
							break;
						case "delayed_unassigned_shards":
							reader.Read();
							delayedUnassignedShards = reader.GetInt32();
							break;
						case "number_of_pending_tasks":
							reader.Read();
							pendingTasks = reader.GetInt32();
							break;
						case "number_of_in_flight_fetch":
							reader.Read();
							inFlightFetch = reader.GetInt32();
							break;
						case "task_max_waiting_in_queue_millis":
							taskMaxWaitingTimeInQueue = JsonSerializer.Deserialize<long>(ref reader, options);
							break;
						case "active_shards_percent_as_number":
							activeShardsAsPercentage = JsonSerializer.Deserialize<double>(ref reader, options);
							break;
						case "indices":
							indices = JsonSerializer.Deserialize<ReadOnlyIndexNameDictionary<IndexHealthStats>>(ref reader, options);
							break;
					}
				}
			}

			var response = new ClusterHealthResponse
			{
				ClusterName = clusterName,
				Status = status ?? default,
				TimedOut = timedOut,
				NumberOfNodes = numberOfNodes,
				NumberOfDataNodes = numberOfDataNodes,
				ActiveShards = activeShards,
				ActivePrimaryShards = activePrimaryShards,
				RelocatingShards = relocatingShards,
				InitializingShards = initializingShards,
				UnassignedShards = unassignedShards,
				DelayedUnassignedShards = delayedUnassignedShards,
				NumberOfPendingTasks = pendingTasks,
				NumberOfInFlightFetch = inFlightFetch,
				TaskMaxWaitingInQueueMillis = taskMaxWaitingTimeInQueue,
				ActiveShardsPercentAsNumber = activeShardsAsPercentage,
				Indices = indices
			};

			reader.Read();
			return response;
		}

		public override void Write(Utf8JsonWriter writer, ClusterHealthResponse value, JsonSerializerOptions options) => throw new NotImplementedException();
	}
}
