/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Generic;
using Elasticsearch.Net.Utf8Json;

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
