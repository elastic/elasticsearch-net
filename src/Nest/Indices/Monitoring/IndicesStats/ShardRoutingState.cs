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
	public enum ShardRoutingState
	{
		/// <summary>
		/// The shard is not assigned to any node.
		/// </summary>
		[EnumMember(Value = "UNASSIGNED")]
		Unassigned,

		/// <summary>
		/// The shard is initializing (probably recovering from either a peer shard or gateway).
		/// </summary>
		[EnumMember(Value = "INITIALIZING")]
		Initializing,

		/// <summary>
		/// The shard is started.
		/// </summary>
		[EnumMember(Value = "STARTED")]
		Started,

		/// <summary>
		/// The shard is in the process being relocated.
		/// </summary>
		[EnumMember(Value = "RELOCATING")]
		Relocating
	}
}
