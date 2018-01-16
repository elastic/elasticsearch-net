using Newtonsoft.Json;

namespace Nest
{
	public interface IAllocateClusterRerouteCommand : IClusterRerouteCommand
	{
		[JsonProperty("index")]
		IndexName Index { get; set; }

		[JsonProperty("shard")]
		int? Shard { get; set; }

		[JsonProperty("node")]
		string Node { get; set; }
	}

	public interface IAllocateReplicaClusterRerouteCommand : IAllocateClusterRerouteCommand
	{
	}

	public interface IAllocateEmptyPrimaryRerouteCommand : IAllocateClusterRerouteCommand
	{
		[JsonProperty("accept_data_loss")]
		bool? AcceptDataLoss { get; set; }
	}

	public interface IAllocateStalePrimaryRerouteCommand : IAllocateClusterRerouteCommand
	{
		[JsonProperty("accept_data_loss")]
		bool? AcceptDataLoss { get; set; }
	}

	public abstract class AllocateClusterRerouteCommandBase : IAllocateClusterRerouteCommand
	{
		public abstract string Name { get; }

		public IndexName Index { get; set; }

		public int? Shard { get; set; }

		public string Node { get; set; }
	}

	public class AllocateReplicaClusterRerouteCommand : AllocateClusterRerouteCommandBase, IAllocateReplicaClusterRerouteCommand
	{
		public override string Name => "allocate_replica";
	}

	public class AllocateEmptyPrimaryRerouteCommand : AllocateClusterRerouteCommandBase, IAllocateEmptyPrimaryRerouteCommand
	{
		public override string Name => "allocate_empty_primary";

		public bool? AcceptDataLoss { get; set; }
	}

	public class AllocateStalePrimaryRerouteCommand : AllocateClusterRerouteCommandBase, IAllocateStalePrimaryRerouteCommand
	{
		public override string Name => "allocate_stale_primary";

		public bool? AcceptDataLoss { get; set; }
	}

	public abstract class AllocateClusterRerouteCommandDescriptorBase<TDescriptor, TInterface>
		: DescriptorBase<TDescriptor, TInterface>, IAllocateClusterRerouteCommand
		where TDescriptor : AllocateClusterRerouteCommandDescriptorBase<TDescriptor, TInterface>, TInterface, IAllocateClusterRerouteCommand
		where TInterface : class, IAllocateClusterRerouteCommand
	{
		string IClusterRerouteCommand.Name => Name;

		public abstract string Name { get; }

		IndexName IAllocateClusterRerouteCommand.Index { get; set; }

		int? IAllocateClusterRerouteCommand.Shard { get; set; }

		string IAllocateClusterRerouteCommand.Node { get; set; }

		public TDescriptor Index(IndexName index) => Assign(a => a.Index = index);

		public TDescriptor Index<T>() where T : class => Assign(a => a.Index = typeof(T));

		public TDescriptor Shard(int? shard) => Assign(a => a.Shard = shard);

		public TDescriptor Node(string node) => Assign(a => a.Node = node);
	}

	public class AllocateReplicaClusterRerouteCommandDescriptor
		: AllocateClusterRerouteCommandDescriptorBase<AllocateReplicaClusterRerouteCommandDescriptor, IAllocateReplicaClusterRerouteCommand>, IAllocateReplicaClusterRerouteCommand
	{
		public override string Name => "allocate_replica";
	}

	public class AllocateEmptyPrimaryRerouteCommandDescriptor
		: AllocateClusterRerouteCommandDescriptorBase<AllocateEmptyPrimaryRerouteCommandDescriptor, IAllocateEmptyPrimaryRerouteCommand>, IAllocateEmptyPrimaryRerouteCommand
	{
		public override string Name => "allocate_empty_primary";

		bool? IAllocateEmptyPrimaryRerouteCommand.AcceptDataLoss { get; set; }

		public AllocateEmptyPrimaryRerouteCommandDescriptor AcceptDataLoss(bool? acceptDataLoss = true) => Assign(a => a.AcceptDataLoss = acceptDataLoss);
	}

	public class AllocateStalePrimaryRerouteCommandDescriptor
		: AllocateClusterRerouteCommandDescriptorBase<AllocateStalePrimaryRerouteCommandDescriptor, IAllocateStalePrimaryRerouteCommand>, IAllocateStalePrimaryRerouteCommand
	{
		public override string Name => "allocate_stale_primary";

		bool? IAllocateStalePrimaryRerouteCommand.AcceptDataLoss { get; set; }

		public AllocateStalePrimaryRerouteCommandDescriptor AcceptDataLoss(bool? acceptDataLoss = true) => Assign(a => a.AcceptDataLoss = acceptDataLoss);
	}
}
