using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
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

		private ClusterRerouteDescriptor AddCommand(IClusterRerouteCommand rerouteCommand) => Assign(a => a.Commands?.AddIfNotNull(rerouteCommand));
	}
}
