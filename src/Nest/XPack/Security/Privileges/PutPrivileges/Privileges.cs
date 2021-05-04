// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Nest.Utf8Json;

namespace Nest
{
	[ReadAs(typeof(Privileges))]
	[JsonFormatter(typeof(PrivilegesFormatter))]
	public interface IPrivileges : IIsADictionary<string, IPrivilegesActions> { }

	public class Privileges : IsADictionaryBase<string, IPrivilegesActions>, IPrivileges
	{
		public Privileges() { }

		internal Privileges(IDictionary<string, IPrivilegesActions> backingDictionary) : base(backingDictionary) { }

		public void Add(string name, IPrivilegesActions actions) => BackingDictionary.Add(name, actions);
	}

	public class PrivilegesDescriptor : IsADictionaryDescriptorBase<PrivilegesDescriptor, IPrivileges, string, IPrivilegesActions>
	{
		public PrivilegesDescriptor() : base(new Privileges()) { }

		public PrivilegesDescriptor Privilege(string privilegesName, IPrivilegesActions actions) => Assign(privilegesName, actions);

		public PrivilegesDescriptor Privilege(string privilegesName, Func<PrivilegesActionsDescriptor, IPrivilegesActions> selector) =>
			Assign(privilegesName, selector?.Invoke(new PrivilegesActionsDescriptor()));
	}

	internal class PrivilegesFormatter : IJsonFormatter<IPrivileges>
	{
		public void Serialize(ref JsonWriter writer, IPrivileges value, IJsonFormatterResolver formatterResolver)
		{
			var formatter = formatterResolver.GetFormatter<IDictionary<string, IPrivilegesActions>>();
			formatter.Serialize(ref writer, value, formatterResolver);
		}

		public IPrivileges Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			formatterResolver.GetFormatter<Privileges>().Deserialize(ref reader, formatterResolver);
	}
}
