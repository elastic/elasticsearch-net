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

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("cluster.reroute.json")]
	[ReadAs(typeof(ClusterRerouteRequest))]
	public partial interface IClusterRerouteRequest
	{
		[DataMember(Name ="commands")]
		IList<IClusterRerouteCommand> Commands { get; set; }
	}

	public partial class ClusterRerouteRequest
	{
		public IList<IClusterRerouteCommand> Commands { get; set; }
	}

	public partial class ClusterRerouteDescriptor
	{
		IList<IClusterRerouteCommand> IClusterRerouteRequest.Commands { get; set; } = new List<IClusterRerouteCommand>();

		public ClusterRerouteDescriptor Move(Func<MoveClusterRerouteCommandDescriptor, IMoveClusterRerouteCommand> selector) =>
			AddCommand(selector?.Invoke(new MoveClusterRerouteCommandDescriptor()));

		public ClusterRerouteDescriptor Cancel(Func<CancelClusterRerouteCommandDescriptor, ICancelClusterRerouteCommand> selector) =>
			AddCommand(selector?.Invoke(new CancelClusterRerouteCommandDescriptor()));

		public ClusterRerouteDescriptor AllocateReplica(Func<AllocateReplicaClusterRerouteCommandDescriptor, IAllocateClusterRerouteCommand> selector
		) =>
			AddCommand(selector?.Invoke(new AllocateReplicaClusterRerouteCommandDescriptor()));

		public ClusterRerouteDescriptor AllocateEmptyPrimary(
			Func<AllocateEmptyPrimaryRerouteCommandDescriptor, IAllocateEmptyPrimaryRerouteCommand> selector
		) =>
			AddCommand(selector?.Invoke(new AllocateEmptyPrimaryRerouteCommandDescriptor()));

		public ClusterRerouteDescriptor AllocateStalePrimary(
			Func<AllocateStalePrimaryRerouteCommandDescriptor, IAllocateStalePrimaryRerouteCommand> selector
		) =>
			AddCommand(selector?.Invoke(new AllocateStalePrimaryRerouteCommandDescriptor()));

		private ClusterRerouteDescriptor AddCommand(IClusterRerouteCommand rerouteCommand) => Assign(rerouteCommand,(a, v) => a.Commands?.AddIfNotNull(v));
	}
}
