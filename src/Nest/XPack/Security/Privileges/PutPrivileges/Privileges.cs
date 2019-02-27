using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<Privileges, string, IPrivilegesActions>))]
	public interface IPrivileges : IIsADictionary<string, IPrivilegesActions> { }

	public class Privileges : IsADictionaryBase<string, IPrivilegesActions>, IPrivileges
	{
		public Privileges() {}
		internal Privileges(IDictionary<string, IPrivilegesActions> backingDictionary) : base(backingDictionary) {}

		public void Add(string name, IPrivilegesActions actions) => BackingDictionary.Add(name, actions);
	}

	public class PrivilegesDescriptor : IsADictionaryDescriptorBase<PrivilegesDescriptor, IPrivileges, string, IPrivilegesActions>
	{
		public PrivilegesDescriptor() : base(new Privileges()) { }

		public PrivilegesDescriptor Privilege(string privilegesName, IPrivilegesActions actions) => Assign(privilegesName, actions);

		public PrivilegesDescriptor Privilege(string privilegesName, Func<PrivilegesActionsDescriptor, IPrivilegesActions> selector) =>
			Assign(privilegesName, selector?.Invoke(new PrivilegesActionsDescriptor()));
	}
}
