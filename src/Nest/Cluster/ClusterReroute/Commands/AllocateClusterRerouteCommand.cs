using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class AllocateClusteRerouteCommand : IClusterRerouteCommand
	{
		[JsonIgnore]
		public string Name { get { return "allocate"; } }

		[JsonProperty("index")]
		public string Index { get; set; }

		[JsonProperty("shard")]
		public int Shard { get; set; }

		[JsonProperty("node")]
		public string Node { get; set; }

		[JsonProperty("allow_primary")]
		public bool? AllowPrimary { get; set; }
	}

	public class AllocateClusterRerouteCommandDescriptor
	{
		internal AllocateClusteRerouteCommand Command = new AllocateClusteRerouteCommand();

		public AllocateClusterRerouteCommandDescriptor Index(string index)
		{
			this.Command.Index = index;
			return this;
		}

		public AllocateClusterRerouteCommandDescriptor Shard(int shard)
		{
			this.Command.Shard = shard;
			return this;
		}

		public AllocateClusterRerouteCommandDescriptor Node(string node)
		{
			this.Command.Node = node;
			return this;
		}

		public AllocateClusterRerouteCommandDescriptor AllowPrimary(bool allowPrimary = true)
		{
			this.Command.AllowPrimary = allowPrimary;
			return this;
		}
	}
}
