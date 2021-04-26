/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Runtime.Serialization;
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
