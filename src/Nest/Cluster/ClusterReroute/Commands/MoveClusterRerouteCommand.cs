// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	public interface IMoveClusterRerouteCommand : IClusterRerouteCommand
	{
		[DataMember(Name ="from_node")]
		string FromNode { get; set; }

		[DataMember(Name ="index")]
		IndexName Index { get; set; }

		[DataMember(Name ="shard")]
		int? Shard { get; set; }

		[DataMember(Name ="to_node")]
		string ToNode { get; set; }
	}

	public class MoveClusterRerouteCommand : IMoveClusterRerouteCommand
	{
		public string FromNode { get; set; }

		public IndexName Index { get; set; }
		public string Name => "move";

		public int? Shard { get; set; }

		public string ToNode { get; set; }
	}

	public class MoveClusterRerouteCommandDescriptor
		: DescriptorBase<MoveClusterRerouteCommandDescriptor, IMoveClusterRerouteCommand>, IMoveClusterRerouteCommand
	{
		string IMoveClusterRerouteCommand.FromNode { get; set; }

		IndexName IMoveClusterRerouteCommand.Index { get; set; }
		string IClusterRerouteCommand.Name => "move";

		int? IMoveClusterRerouteCommand.Shard { get; set; }

		string IMoveClusterRerouteCommand.ToNode { get; set; }

		public MoveClusterRerouteCommandDescriptor Index(IndexName index) => Assign(index, (a, v) => a.Index = v);

		public MoveClusterRerouteCommandDescriptor Index<T>() where T : class => Assign(typeof(T), (a, v) => a.Index = v);

		public MoveClusterRerouteCommandDescriptor Shard(int? shard) => Assign(shard, (a, v) => a.Shard = v);

		public MoveClusterRerouteCommandDescriptor FromNode(string fromNode) => Assign(fromNode, (a, v) => a.FromNode = v);

		public MoveClusterRerouteCommandDescriptor ToNode(string toNode) => Assign(toNode, (a, v) => a.ToNode = v);
	}
}
