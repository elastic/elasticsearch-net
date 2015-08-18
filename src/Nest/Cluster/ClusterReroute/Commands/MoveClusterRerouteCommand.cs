using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class MoveClusterRerouteCommand : IClusterRerouteCommand
	{
		[JsonIgnore]
		public string Name { get { return "move";  } }

		[JsonProperty("index")]
		public string Index { get; set; }

		[JsonProperty("shard")]
		public int Shard { get; set; }

		[JsonProperty("from_node")]
		public string FromNode { get; set; }

		[JsonProperty("to_node")]
		public string ToNode { get; set; }
	}

	public class MoveClusterRerouteCommandDescriptor
	{
		internal MoveClusterRerouteCommand Command = new MoveClusterRerouteCommand();

		public MoveClusterRerouteCommandDescriptor Index(string index)
		{
			this.Command.Index = index;
			return this;
		}

		public MoveClusterRerouteCommandDescriptor Shard(int shard)
		{
			this.Command.Shard = shard;
			return this;
		}

		public MoveClusterRerouteCommandDescriptor FromNode(string fromNode)
		{
			this.Command.FromNode = fromNode;
			return this;
		}

		public MoveClusterRerouteCommandDescriptor ToNode(string toNode)
		{
			this.Command.ToNode = toNode;
			return this;
		}
	}
}
