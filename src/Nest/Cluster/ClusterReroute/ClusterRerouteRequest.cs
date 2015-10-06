using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public partial interface IClusterRerouteRequest 
	{
		[JsonProperty("commands")]
		[JsonConverter(typeof(ClusterRerouteCommandsJsonConverter))]
		IList<IClusterRerouteCommand> Commands { get; set; }
	}

	public partial class ClusterRerouteRequest 
	{
		public IList<IClusterRerouteCommand> Commands { get; set; }
	}

	public partial class ClusterRerouteDescriptor 
	{
		IList<IClusterRerouteCommand> IClusterRerouteRequest.Commands { get; set; }

		public ClusterRerouteDescriptor Move(Func<MoveClusterRerouteCommandDescriptor, MoveClusterRerouteCommandDescriptor> moveCommandDescriptor)
		{
			moveCommandDescriptor.ThrowIfNull("moveCommandDescriptor");
			var selector = moveCommandDescriptor(new MoveClusterRerouteCommandDescriptor());
			AddCommand(selector.Command);
			return this;
		}

		public ClusterRerouteDescriptor Cancel(Func<CancelClusterRerouteCommandDescriptor, CancelClusterRerouteCommandDescriptor> cancelCommandDescriptor)
		{
			cancelCommandDescriptor.ThrowIfNull("cancelCommandDescriptor");
			var selector = cancelCommandDescriptor(new CancelClusterRerouteCommandDescriptor());
			AddCommand(selector.Command);
			return this;
		}

		public ClusterRerouteDescriptor Allocate(Func<AllocateClusterRerouteCommandDescriptor, AllocateClusterRerouteCommandDescriptor> allocateCommandDescriptor)
		{
			allocateCommandDescriptor.ThrowIfNull("allocateCommandDescriptor");
			var selector = allocateCommandDescriptor(new AllocateClusterRerouteCommandDescriptor());
			AddCommand(selector.Command);
			return this;
		}

		public ClusterRerouteDescriptor Commands(IEnumerable<IClusterRerouteCommand> commands)
		{
			commands.ThrowIfNull("commands");
			((IClusterRerouteRequest)this).Commands = commands.ToList();
			return this;
		}

		private void AddCommand(IClusterRerouteCommand rerouteCommand)
		{
			rerouteCommand.ThrowIfNull("rerouteCommand");
			var s = ((IClusterRerouteRequest)this);
			if (s.Commands == null)
				s.Commands = new List<IClusterRerouteCommand>();
			s.Commands.Add(rerouteCommand);
		}
	}
}
