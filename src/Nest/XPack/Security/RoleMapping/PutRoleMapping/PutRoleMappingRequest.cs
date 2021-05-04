// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

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
