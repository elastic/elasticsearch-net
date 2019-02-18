using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Adds and updates roles in the native realm.
	/// </summary>
	[MapsApi("security.put_role.json")]
	public partial interface IPutRoleRequest
	{
		/// <summary>
		/// A list of application privileges
		/// </summary>
		[JsonProperty("applications")]
		IEnumerable<IApplicationPrivileges> Applications { get; set; }

		/// <summary>
		/// A list of cluster privileges
		/// </summary>
		[JsonProperty("cluster")]
		IEnumerable<string> Cluster { get; set; }

		/// <summary>
		/// An object defining global privileges. A global privilege is a form of cluster privilege that is
		/// request-aware. Support for global privileges is currently limited to
		/// the management of application privileges.
		/// </summary>
		[JsonProperty("global")]
		[JsonConverter(typeof(VerbatimDictionaryKeysPreservingNullJsonConverter<string, object>))]
		IDictionary<string, object> Global { get; set; }

		/// <summary>
		/// A list of indices permissions entries
		/// </summary>
		[JsonProperty("indices")]
		IEnumerable<IIndicesPrivileges> Indices { get; set; }

		/// <summary>
		/// Optional meta-data. Within the metadata object, keys that begin with _ are reserved for system usage.
		/// </summary>
		[JsonProperty("metadata")]
		[JsonConverter(typeof(VerbatimDictionaryKeysPreservingNullJsonConverter<string, object>))]
		IDictionary<string, object> Metadata { get; set; }

		/// <summary>
		/// A list of users that the owners of this role can impersonate.
		/// </summary>
		[JsonProperty("run_as")]
		IEnumerable<string> RunAs { get; set; }
	}

	/// <inheritdoc cref="IPutRoleRequest" />
	public partial class PutRoleRequest
	{
		/// <inheritdoc cref="IPutRoleRequest.Applications" />
		public IEnumerable<IApplicationPrivileges> Applications { get; set; }

		/// <inheritdoc cref="IPutRoleRequest.Cluster" />
		public IEnumerable<string> Cluster { get; set; }

		/// <inheritdoc cref="IPutRoleRequest.Global" />
		public IDictionary<string, object> Global { get; set; }

		/// <inheritdoc cref="IPutRoleRequest.Indices" />
		public IEnumerable<IIndicesPrivileges> Indices { get; set; }

		/// <inheritdoc cref="IPutRoleRequest.Metadata" />
		public IDictionary<string, object> Metadata { get; set; }

		/// <inheritdoc cref="IPutRoleRequest.RunAs" />
		public IEnumerable<string> RunAs { get; set; }
	}

	/// <inheritdoc cref="IPutRoleRequest" />
	public partial class PutRoleDescriptor
	{
		/// <inheritdoc cref="IPutRoleRequest.Applications" />
		IEnumerable<IApplicationPrivileges> IPutRoleRequest.Applications { get; set; }

		/// <inheritdoc cref="IPutRoleRequest.Cluster" />
		IEnumerable<string> IPutRoleRequest.Cluster { get; set; }

		/// <inheritdoc cref="IPutRoleRequest.Global" />
		IDictionary<string, object> IPutRoleRequest.Global { get; set; }

		/// <inheritdoc cref="IPutRoleRequest.Indices" />
		IEnumerable<IIndicesPrivileges> IPutRoleRequest.Indices { get; set; }

		/// <inheritdoc cref="IPutRoleRequest.Metadata" />
		IDictionary<string, object> IPutRoleRequest.Metadata { get; set; }

		/// <inheritdoc cref="IPutRoleRequest.RunAs" />
		IEnumerable<string> IPutRoleRequest.RunAs { get; set; }

		/// <inheritdoc cref="IPutRoleRequest.Applications" />
		public PutRoleDescriptor Applications(IEnumerable<IApplicationPrivileges> privileges) =>
			Assign(a => a.Applications = privileges.ToListOrNullIfEmpty());

		/// <inheritdoc cref="IPutRoleRequest.Applications" />
		public PutRoleDescriptor Applications(Func<ApplicationPrivilegesDescriptor, IPromise<IList<IApplicationPrivileges>>> selector) =>
			Assign(a => a.Applications = selector?.Invoke(new ApplicationPrivilegesDescriptor())?.Value);

		/// <inheritdoc cref="IPutRoleRequest.Cluster" />
		public PutRoleDescriptor Cluster(IEnumerable<string> clusters) => Assign(a => a.Cluster = clusters);

		/// <inheritdoc cref="IPutRoleRequest.Cluster" />
		public PutRoleDescriptor Cluster(params string[] clusters) => Assign(a => a.Cluster = clusters);

		/// <inheritdoc cref="IPutRoleRequest.Global" />
		public PutRoleDescriptor Global(IDictionary<string, object> global) => Assign(a => a.Global = global);

		/// <inheritdoc cref="IPutRoleRequest.Global" />
		public PutRoleDescriptor Global(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) =>
			Assign(a => a.Global = selector?.Invoke(new FluentDictionary<string, object>()));

		/// <inheritdoc cref="IPutRoleRequest.RunAs" />
		public PutRoleDescriptor RunAs(IEnumerable<string> users) => Assign(a => a.RunAs = users);

		/// <inheritdoc cref="IPutRoleRequest.RunAs" />
		public PutRoleDescriptor RunAs(params string[] users) => Assign(a => a.RunAs = users);

		/// <inheritdoc cref="IPutRoleRequest.Indices" />
		public PutRoleDescriptor Indices(IEnumerable<IIndicesPrivileges> privileges) => Assign(a => a.Indices = privileges.ToListOrNullIfEmpty());

		/// <inheritdoc cref="IPutRoleRequest.Indices" />
		public PutRoleDescriptor Indices(Func<IndicesPrivilegesDescriptor, IPromise<IList<IIndicesPrivileges>>> selector) =>
			Assign(a => a.Indices = selector?.Invoke(new IndicesPrivilegesDescriptor())?.Value);

		/// <inheritdoc cref="IPutRoleRequest.Metadata" />
		public PutRoleDescriptor Metadata(IDictionary<string, object> metadata) => Assign(a => a.Metadata = metadata);

		/// <inheritdoc cref="IPutRoleRequest.Metadata" />
		public PutRoleDescriptor Metadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) =>
			Assign(a => a.Metadata = selector?.Invoke(new FluentDictionary<string, object>()));
	}
}
