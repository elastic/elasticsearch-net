using Elasticsearch.Net;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IClusterRerouteRequest : IRequest<ClusterRerouteRequestParameters>
	{
		[JsonProperty("commands")]
		[JsonConverter(typeof(ClusterRerouteCommandCollectionConverter))]
		IList<IClusterRerouteCommand> Commands { get; set; }
	}

	internal static class ClusterReroutePathInfo
	{
		public static void Update(ElasticsearchPathInfo<ClusterRerouteRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}
	}

	public partial class ClusterRerouteRequest 
		: BasePathRequest<ClusterRerouteRequestParameters>, IClusterRerouteRequest
	{
		public IList<IClusterRerouteCommand> Commands { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<ClusterRerouteRequestParameters> pathInfo)
		{
			ClusterReroutePathInfo.Update(pathInfo);
		}
	}

	public partial class ClusterRerouteDescriptor 
		: BasePathDescriptor<ClusterRerouteDescriptor, ClusterRerouteRequestParameters>, IClusterRerouteRequest
	{
		IClusterRerouteRequest Self { get { return this; } }

		IList<IClusterRerouteCommand> IClusterRerouteRequest.Commands { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<ClusterRerouteRequestParameters> pathInfo)
		{
			ClusterReroutePathInfo.Update(pathInfo);
		}

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
			this.Self.Commands = commands.ToList();
			return this;
		}

		private void AddCommand(IClusterRerouteCommand rerouteCommand)
		{
			rerouteCommand.ThrowIfNull("rerouteCommand");
			if (this.Self.Commands == null)
				this.Self.Commands = new List<IClusterRerouteCommand>();
			this.Self.Commands.Add(rerouteCommand);
		}
	}
}
