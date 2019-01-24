using System;
using System.Collections.Generic;

namespace Nest {
	public interface IPrivilegesActions
	{
		IEnumerable<string> Actions { get; set; }
		IDictionary<string, object> Metadata { get; set; }
	}

	public class PrivilegesActions : IPrivilegesActions
	{
		public IEnumerable<string> Actions { get; set; }
		public IDictionary<string, object> Metadata { get; set; }
	}
	
	public class PrivilegesActionsDescriptor : DescriptorBase<PrivilegesActionsDescriptor, IPrivilegesActions>, IPrivilegesActions
	{
		IEnumerable<string> IPrivilegesActions.Actions { get; set; }
		IDictionary<string, object> IPrivilegesActions.Metadata { get; set; }

		public PrivilegesActionsDescriptor Actions(params string[] actions) => Assign(a => a.Actions = actions);

		public PrivilegesActionsDescriptor Actions(IEnumerable<string> actions) => Assign(a => a.Actions = actions);

		public PrivilegesActionsDescriptor Metadata(IDictionary<string, object> meta) => Assign(a => a.Metadata = meta);

		public PrivilegesActionsDescriptor Metadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> meta) =>
			Assign(a => a.Metadata = meta?.Invoke(new FluentDictionary<string, object>()));

	}
}