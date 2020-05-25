// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum IndexingJobState
	{
		/// <summary> Indexer is running, but not actively indexing data (e.g. it's idle) </summary>
		[EnumMember(Value = "started")] Started,

		/// <summary> Indexer is actively indexing data </summary>
		[EnumMember(Value = "indexing")] Indexing,

		/// <summary> Transition state to where an indexer has acknowledged the stop but is still in process of halting </summary>
		[EnumMember(Value = "stopping")] Stopping,

		/// <summary> Indexer is "paused" and ignoring scheduled triggers </summary>
		[EnumMember(Value = "stopped")] Stopped,

		/// <summary> Something (internal or external) has requested the indexer abort and shutdown </summary>
		[EnumMember(Value = "aborting")] Aborting
	}
}
