// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;

namespace Nest
{
	[ReadAs(typeof(AppPrivileges))]
	public interface IAppPrivileges : IIsADictionary<string, IPrivileges> { }

	public class AppPrivileges : IsADictionaryBase<string, IPrivileges>, IAppPrivileges
	{
		public AppPrivileges() { }

		internal AppPrivileges(IDictionary<string, IPrivileges> backingDictionary) : base(backingDictionary) { }

		public void Add(string name, IPrivileges privileges) => BackingDictionary.Add(name, privileges);
	}

	public class AppPrivilegesDescriptor : IsADictionaryDescriptorBase<AppPrivilegesDescriptor, IAppPrivileges, string, IPrivileges>
	{
		public AppPrivilegesDescriptor() : base(new AppPrivileges()) { }

		public AppPrivilegesDescriptor Application(string applicationName, IPrivileges privileges) => Assign(applicationName, privileges);

		public AppPrivilegesDescriptor Application(string applicationName, Func<PrivilegesDescriptor, IPromise<IPrivileges>> selector) =>
			Assign(applicationName, selector?.Invoke(new PrivilegesDescriptor())?.Value);
	}
}
