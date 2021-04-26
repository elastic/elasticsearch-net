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
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[MapsApi("security.put_role_mapping.json")]
	public partial interface IPutRoleMappingRequest
	{
		[DataMember(Name = "enabled")]
		bool? Enabled { get; set; }

		[DataMember(Name = "metadata")]
		[JsonFormatter(typeof(VerbatimDictionaryInterfaceKeysPreservingNullFormatter<string, object>))]
		IDictionary<string, object> Metadata { get; set; }

		[DataMember(Name = "roles")]
		IEnumerable<string> Roles { get; set; }

		[DataMember(Name = "rules")]
		RoleMappingRuleBase Rules { get; set; }

		[DataMember(Name = "run_as")]
		IEnumerable<string> RunAs { get; set; }
	}

	[DataContract]
	public partial class PutRoleMappingRequest
	{
		public bool? Enabled { get; set; }

		public IDictionary<string, object> Metadata { get; set; }

		public IEnumerable<string> Roles { get; set; }

		public RoleMappingRuleBase Rules { get; set; }
		public IEnumerable<string> RunAs { get; set; }
	}

	public partial class PutRoleMappingDescriptor
	{
		bool? IPutRoleMappingRequest.Enabled { get; set; }
		IDictionary<string, object> IPutRoleMappingRequest.Metadata { get; set; }
		IEnumerable<string> IPutRoleMappingRequest.Roles { get; set; }
		RoleMappingRuleBase IPutRoleMappingRequest.Rules { get; set; }
		IEnumerable<string> IPutRoleMappingRequest.RunAs { get; set; }

		/// <inheritdoc />
		public PutRoleMappingDescriptor Roles(RoleMappingRuleBase rules) => Assign(rules, (a, v) => a.Rules = v);

		/// <inheritdoc />
		public PutRoleMappingDescriptor Roles(IEnumerable<string> roles) => Assign(roles, (a, v) => a.Roles = v);

		/// <inheritdoc />
		public PutRoleMappingDescriptor Roles(params string[] roles) => Assign(roles, (a, v) => a.Roles = v);

		/// <inheritdoc />
		public PutRoleMappingDescriptor Rules(Func<RoleMappingRuleDescriptor, RoleMappingRuleBase> selector) =>
			Assign(selector, (a, v) => a.Rules = v?.Invoke(new RoleMappingRuleDescriptor()));

		/// <inheritdoc />
		public PutRoleMappingDescriptor Rules(RoleMappingRuleBase rules) => Assign(rules, (a, v) => a.Rules = v);

		/// <inheritdoc />
		public PutRoleMappingDescriptor RunAs(IEnumerable<string> users) => Assign(users, (a, v) => a.RunAs = v);

		/// <inheritdoc />
		public PutRoleMappingDescriptor RunAs(params string[] users) => Assign(users, (a, v) => a.RunAs = v);

		/// <inheritdoc />
		public PutRoleMappingDescriptor Enabled(bool? enabled = true) => Assign(enabled, (a, v) => a.Enabled = v);

		/// <inheritdoc />
		public PutRoleMappingDescriptor Metadata(IDictionary<string, object> metadata) => Assign(metadata, (a, v) => a.Metadata = v);

		/// <inheritdoc />
		public PutRoleMappingDescriptor Metadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) =>
			Assign(selector, (a, v) => a.Metadata = v?.Invoke(new FluentDictionary<string, object>()));
	}
}
