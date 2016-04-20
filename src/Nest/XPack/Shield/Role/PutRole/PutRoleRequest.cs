using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IPutRoleRequest
	{
		[JsonProperty("cluster")]
		IEnumerable<string> Cluster { get; set; }

		[JsonProperty("run_as")]
		IEnumerable<string> RunAs { get; set; }

		[JsonProperty("indices")]
		IEnumerable<IIndicesPrivileges> Indices { get; set; }

	}

	public partial class PutRoleRequest
	{
		public IEnumerable<string> Cluster { get; set; }

		public IEnumerable<string> RunAs { get; set; }

		public IEnumerable<IIndicesPrivileges> Indices { get; set; }
	}

	[DescriptorFor("ShieldPutRole")]
	public partial class PutRoleDescriptor
	{
		IEnumerable<string> IPutRoleRequest.Cluster { get; set; }
		IEnumerable<string> IPutRoleRequest.RunAs { get; set; }
		IEnumerable<IIndicesPrivileges> IPutRoleRequest.Indices { get; set; }

		public PutRoleDescriptor Cluster(IEnumerable<string> clusters) => Assign(a => a.Cluster = clusters);
		public PutRoleDescriptor Cluster(params string[] clusters) => Assign(a => a.Cluster = clusters);
		public PutRoleDescriptor RunAs(IEnumerable<string> users) => Assign(a => a.RunAs = users);
		public PutRoleDescriptor RunAs(params string[] users) => Assign(a => a.RunAs = users);

		/// <inheritdoc/>
		public PutRoleDescriptor Indices(IEnumerable<IIndicesPrivileges> privileges) => Assign(a => a.Indices = privileges.ToListOrNullIfEmpty());

		/// <inheritdoc/>
		public PutRoleDescriptor Indices(Func<IndicesPrivilegesDescriptor, IPromise<IList<IIndicesPrivileges>>> selector) =>
			Assign(a => a.Indices = selector?.Invoke(new IndicesPrivilegesDescriptor())?.Value);

	}
}
