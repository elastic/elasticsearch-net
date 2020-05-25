// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[DataContract]
	public class CatThreadPoolRecord : ICatRecord
	{
		/// <summary>
		/// The number of active threads in the current thread pool
		/// </summary>
		[DataMember(Name = "active")]
		[JsonFormatter(typeof(StringIntFormatter))]
		public int Active { get; set; }

		/// <summary>
		/// The number of tasks completed by the thread pool executor
		/// </summary>
		[DataMember(Name = "completed")]
		[JsonFormatter(typeof(NullableStringLongFormatter))]
		public long? Completed { get; set; }

		/// <summary>
		/// The configured core number of active threads allowed in the current thread pool
		/// </summary>
		[DataMember(Name = "core")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		public int? Core { get; set; }

		/// <summary>
		/// The ephemeral node ID
		/// </summary>
		[DataMember(Name = "ephemeral_node_id")]
		public string EphemeralNodeId { get; set; }

		/// <summary>
		/// The hostname for the current node
		/// </summary>
		[DataMember(Name = "host")]
		public string Host { get; set; }

		/// <summary>
		/// The IP address for the current node
		/// </summary>
		[DataMember(Name = "ip")]
		public string Ip { get; set; }

		/// <summary>
		/// The configured keep alive time for threads
		/// </summary>
		[DataMember(Name = "keep_alive")]
		public Time KeepAlive { get; set; }

		/// <summary>
		/// The highest number of active threads in the current thread pool
		/// </summary>
		[DataMember(Name = "largest")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		public int? Largest { get; set; }

		/// <summary>
		/// The configured maximum number of active threads allowed in the current thread pool
		/// </summary>
		[DataMember(Name = "max")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		public int? Maximum { get; set; }

		/// <summary>
		/// The name of the thread pool
		/// </summary>
		[DataMember(Name = "name")]
		public string Name { get; set; }

		/// <summary>
		/// The unique id of the node
		/// </summary>
		[DataMember(Name = "node_id")]
		public string NodeId { get; set; }

		/// <summary>
		/// The name of the node
		/// </summary>
		[DataMember(Name = "node_name")]
		public string NodeName { get; set; }

		/// <summary>
		/// The number of threads in the current thread pool
		/// </summary>
		[DataMember(Name = "pool_size")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		public int? PoolSize { get; set; }

		/// <summary>
		/// The bound transport port for the current node
		/// </summary>
		[DataMember(Name = "port")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		public int? Port { get; set; }

		/// <summary>
		/// The process ID of the running node
		/// </summary>
		[DataMember(Name = "pid")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		public int? ProcessId { get; set; }

		/// <summary>
		/// The number of tasks in the queue for the current thread pool
		/// </summary>
		[DataMember(Name = "queue")]
		[JsonFormatter(typeof(StringIntFormatter))]
		public int Queue { get; set; }

		/// <summary>
		/// The maximum number of tasks permitted in the queue for the current thread pool
		/// </summary>
		[DataMember(Name = "queue_size")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		public int? QueueSize { get; set; }

		/// <summary>
		/// The number of tasks rejected by the thread pool executor
		/// </summary>
		[DataMember(Name = "rejected")]
		[JsonFormatter(typeof(StringLongFormatter))]
		public long Rejected { get; set; }

		/// <summary>
		/// The configured fixed number of active threads allowed in the current thread pool
		/// </summary>
		[DataMember(Name = "size")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		public int? Size { get; set; }

		/// <summary>
		/// The current (*) type of thread pool (`fixed` or `scaling`)
		/// </summary>
		[DataMember(Name = "type")]
		public string Type { get; set; }
	}
}
