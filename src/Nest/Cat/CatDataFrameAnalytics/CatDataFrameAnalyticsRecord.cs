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

namespace Nest
{
	[DataContract]
	public class CatDataFrameAnalyticsRecord : ICatRecord
	{
		/// <summary>
		/// Contains messages relating to the selection of a node.
		/// </summary>
		[DataMember(Name="assignment_explanation")]
		public string AssignmentExplanation { get; internal set; }

		/// <summary>
		/// (Default)The time when the data frame analytics job was created.
		/// </summary>
		[DataMember(Name="create_time")]
		public string CreateTime { get; internal set; }

		/// <summary>
		/// A description of the job.
		/// </summary>
		[DataMember(Name="description")]
		public string Description { get; internal set; }

		/// <summary>
		/// Name of the destination index.
		/// </summary>
		[DataMember(Name="dest_index")]
		public string DestinationIndex { get; internal set; }

		/// <summary>
		/// Contains messages about the reason why a data frame analytics job failed.
		/// </summary>
		[DataMember(Name="failure_reason")]
		public string FailureReason { get; internal set; }

		/// <summary>
		/// (Default) Identifier for the data frame analytics job.
		/// </summary>
		[DataMember(Name="id")]
		public string Id { get; internal set; }

		/// <summary>
		/// (Default)The approximate maximum amount of memory resources that are permitted for the data frame analytics job.
		/// </summary>
		[DataMember(Name="model_memory_limit")]
		public string ModelMemoryLimit { get; internal set; }

		/// <summary>
		/// The network address of the node that the data frame analytics job is assigned to.
		/// </summary>
		[DataMember(Name="node.address")]
		public string NodeAddress { get; internal set; }

		/// <summary>
		/// The ephemeral ID of the node that the data frame analytics job is assigned to.
		/// </summary>
		[DataMember(Name="node.ephemeral_id")]
		public string NodeEphemeralId { get; internal set; }

		/// <summary>
		/// The unique identifier of the node that the data frame analytics job is assigned to.
		/// </summary>
		[DataMember(Name="node.id")]
		public string NodeId { get; internal set; }

		/// <summary>
		/// The name of the node that the data frame analytics job is assigned to.
		/// </summary>
		[DataMember(Name="node.name")]
		public string NodeName { get; internal set; }

		/// <summary>
		/// The progress report of the data frame analytics job by phase.
		/// </summary>
		[DataMember(Name="progress")]
		public string Progress { get; internal set; }

		/// <summary>
		/// Name of the source index.
		/// </summary>
		[DataMember(Name="source_index")]
		public string SourceIndex { get; internal set; }

		/// <summary>
		/// (Default) Current state of the data frame analytics job.
		/// </summary>
		[DataMember(Name="state")]
		public string State { get; internal set; }

		/// <summary>
		/// (Default) The type of analysis that the data frame analytics job performs.
		/// </summary>
		[DataMember(Name="type")]
		public string Type { get; internal set; }

		/// <summary>
		/// The Elasticsearch version number in which the data frame analytics job was created.
		/// </summary>
		[DataMember(Name="version")]
		public string Version { get; internal set; }
	}
}
