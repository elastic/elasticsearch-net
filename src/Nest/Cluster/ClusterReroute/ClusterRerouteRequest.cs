using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<ClusterRerouteRequest>))]
	public partial interface IClusterRerouteRequest
	{
		[JsonProperty("commands")]
		IList<IClusterRerouteCommand> Commands { get; set; }
	}

	public partial class ClusterRerouteRequest
	{
		public IList<IClusterRerouteCommand> Commands { get; set; }
	}

	public partial class ClusterRerouteDescriptor
	{
		IList<IClusterRerouteCommand> IClusterRerouteRequest.Commands { get; set; } = new List<IClusterRerouteCommand>();

		public ClusterRerouteDescriptor AllocateEmptyPrimary(
			Func<AllocateEmptyPrimaryRerouteCommandDescriptor, IAllocateEmptyPrimaryRerouteCommand> selector
		) =>
			AddCommand(selector?.Invoke(new AllocateEmptyPrimaryRerouteCommandDescriptor()));

		public ClusterRerouteDescriptor AllocateReplica(Func<AllocateReplicaClusterRerouteCommandDescriptor, IAllocateClusterRerouteCommand> selector
		) =>
			AddCommand(selector?.Invoke(new AllocateReplicaClusterRerouteCommandDescriptor()));

		public ClusterRerouteDescriptor AllocateStalePrimary(
			Func<AllocateStalePrimaryRerouteCommandDescriptor, IAllocateStalePrimaryRerouteCommand> selector
		) =>
			AddCommand(selector?.Invoke(new AllocateStalePrimaryRerouteCommandDescriptor()));

		public ClusterRerouteDescriptor Cancel(Func<CancelClusterRerouteCommandDescriptor, ICancelClusterRerouteCommand> selector) =>
			AddCommand(selector?.Invoke(new CancelClusterRerouteCommandDescriptor()));

		public ClusterRerouteDescriptor Move(Func<MoveClusterRerouteCommandDescriptor, IMoveClusterRerouteCommand> selector) =>
			AddCommand(selector?.Invoke(new MoveClusterRerouteCommandDescriptor()));

		private ClusterRerouteDescriptor AddCommand(IClusterRerouteCommand rerouteCommand) => Assign(a => a.Commands?.AddIfNotNull(rerouteCommand));
	}
}
