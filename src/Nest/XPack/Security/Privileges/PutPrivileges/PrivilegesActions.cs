using System;
using System.Collections.Generic;

namespace Nest
{
	public interface IPrivilegesActions
	{
		/// <summary>
		/// A list of action names that are granted by this privilege. This field must exist and cannot be an empty array.
		/// Action names can contain any number of printable ASCII characters and must contain
		/// at least one of the following characters: / *, :
		/// </summary>
		IEnumerable<string> Actions { get; set; }

		/// <summary>
		/// Optional meta-data. Within the metadata object, keys that begin with _ are reserved for system usage.
		/// </summary>
		IDictionary<string, object> Metadata { get; set; }
	}

	public class PrivilegesActions : IPrivilegesActions
	{
		/// <inheritdoc cref="IPrivilegesActions.Actions"/>
		public IEnumerable<string> Actions { get; set; }

		/// <inheritdoc cref="IPrivilegesActions.Metadata"/>
		public IDictionary<string, object> Metadata { get; set; }
	}

	public class PrivilegesActionsDescriptor : DescriptorBase<PrivilegesActionsDescriptor, IPrivilegesActions>, IPrivilegesActions
	{
		/// <inheritdoc cref="IPrivilegesActions.Actions"/>
		IEnumerable<string> IPrivilegesActions.Actions { get; set; }

		/// <inheritdoc cref="IPrivilegesActions.Metadata"/>
		IDictionary<string, object> IPrivilegesActions.Metadata { get; set; }

		/// <inheritdoc cref="IPrivilegesActions.Actions"/>
		public PrivilegesActionsDescriptor Actions(params string[] actions) => Assign(a => a.Actions = actions);

		/// <inheritdoc cref="IPrivilegesActions.Actions"/>
		public PrivilegesActionsDescriptor Actions(IEnumerable<string> actions) => Assign(a => a.Actions = actions);

		/// <inheritdoc cref="IPrivilegesActions.Metadata"/>
		public PrivilegesActionsDescriptor Metadata(IDictionary<string, object> meta) => Assign(a => a.Metadata = meta);

		/// <inheritdoc cref="IPrivilegesActions.Metadata"/>
		public PrivilegesActionsDescriptor Metadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> meta) =>
			Assign(a => a.Metadata = meta?.Invoke(new FluentDictionary<string, object>()));
	}
}
