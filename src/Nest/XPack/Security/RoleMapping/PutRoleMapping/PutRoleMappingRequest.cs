using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IPutRoleMappingRequest
	{
		[JsonProperty("run_as")]
		IEnumerable<string> RunAs { get; set; }

		[JsonProperty("metadata")]
		[JsonConverter(typeof(VerbatimDictionaryKeysPreservingNullJsonConverter<string, object>))]
		IDictionary<string, object> Metadata { get; set; }

		[JsonProperty("enabled")]
		bool? Enabled { get; set; }

		[JsonProperty("roles")]
		IEnumerable<string> Roles { get; set; }

		[JsonProperty("rules")]
		RoleMappingRuleBase Rules { get; set; }
	}

	public partial class PutRoleMappingRequest
	{
		public IEnumerable<string> RunAs { get; set; }

		public IDictionary<string, object> Metadata { get; set; }

		public bool? Enabled { get; set; }

		public IEnumerable<string> Roles { get; set; }

		public RoleMappingRuleBase Rules { get; set; }
	}

	[DescriptorFor("XpackSecurityPutRoleMapping")]
	public partial class PutRoleMappingDescriptor
	{
		IEnumerable<string> IPutRoleMappingRequest.Roles { get; set; }
		IEnumerable<string> IPutRoleMappingRequest.RunAs { get; set; }
		IDictionary<string, object> IPutRoleMappingRequest.Metadata { get; set; }
		bool? IPutRoleMappingRequest.Enabled { get; set; }
		RoleMappingRuleBase IPutRoleMappingRequest.Rules { get; set; }

		/// <inheritdoc/>
		public PutRoleMappingDescriptor Roles(RoleMappingRuleBase rules) => Assign(a => a.Rules = rules);

		/// <inheritdoc/>
		public PutRoleMappingDescriptor Roles(IEnumerable<string> roles) => Assign(a => a.Roles = roles);

		/// <inheritdoc/>
		public PutRoleMappingDescriptor Roles(params string[] roles) => Assign(a => a.Roles = roles);

		/// <inheritdoc/>
		public PutRoleMappingDescriptor Rules(Func<RoleMappingRuleDescriptor, RoleMappingRuleBase> selector) =>
			Assign(a => a.Rules = selector?.Invoke(new RoleMappingRuleDescriptor()));

		/// <inheritdoc/>
		public PutRoleMappingDescriptor Rules(RoleMappingRuleBase rules) => Assign(a => a.Rules = rules);

		/// <inheritdoc/>
		public PutRoleMappingDescriptor RunAs(IEnumerable<string> users) => Assign(a => a.RunAs = users);

		/// <inheritdoc/>
		public PutRoleMappingDescriptor RunAs(params string[] users) => Assign(a => a.RunAs = users);

		/// <inheritdoc/>
		public PutRoleMappingDescriptor Enabled(bool? enabled = true) => Assign(a => a.Enabled = enabled);

		/// <inheritdoc/>
		public PutRoleMappingDescriptor Metadata(IDictionary<string, object> metadata) => Assign(a => a.Metadata = metadata);

		/// <inheritdoc/>
		public PutRoleMappingDescriptor Metadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) =>
			Assign(a => a.Metadata = selector?.Invoke(new FluentDictionary<string, object>()));
	}


}
