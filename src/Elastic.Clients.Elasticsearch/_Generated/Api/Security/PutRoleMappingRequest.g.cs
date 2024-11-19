// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

#nullable restore

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Requests;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Security;

public sealed partial class PutRoleMappingRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// If <c>true</c> (the default) then refresh the affected shards to make this operation visible to search, if <c>wait_for</c> then wait for a refresh to make this operation visible to search, if <c>false</c> then do nothing with refreshes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Refresh?>("refresh"); set => Q("refresh", value); }
}

/// <summary>
/// <para>
/// Create or update role mappings.
/// </para>
/// <para>
/// Role mappings define which roles are assigned to each user.
/// Each mapping has rules that identify users and a list of roles that are granted to those users.
/// The role mapping APIs are generally the preferred way to manage role mappings rather than using role mapping files. The create or update role mappings API cannot update role mappings that are defined in role mapping files.
/// </para>
/// <para>
/// This API does not create roles. Rather, it maps users to existing roles.
/// Roles can be created by using the create or update roles API or roles files.
/// </para>
/// </summary>
public sealed partial class PutRoleMappingRequest : PlainRequest<PutRoleMappingRequestParameters>
{
	public PutRoleMappingRequest(Elastic.Clients.Elasticsearch.Name name) : base(r => r.Required("name", name))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityPutRoleMapping;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.put_role_mapping";

	/// <summary>
	/// <para>
	/// If <c>true</c> (the default) then refresh the affected shards to make this operation visible to search, if <c>wait_for</c> then wait for a refresh to make this operation visible to search, if <c>false</c> then do nothing with refreshes.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Refresh?>("refresh"); set => Q("refresh", value); }
	[JsonInclude, JsonPropertyName("enabled")]
	public bool? Enabled { get; set; }
	[JsonInclude, JsonPropertyName("metadata")]
	public IDictionary<string, object>? Metadata { get; set; }
	[JsonInclude, JsonPropertyName("roles")]
	public ICollection<string>? Roles { get; set; }
	[JsonInclude, JsonPropertyName("role_templates")]
	public ICollection<Elastic.Clients.Elasticsearch.Security.RoleTemplate>? RoleTemplates { get; set; }
	[JsonInclude, JsonPropertyName("rules")]
	public Elastic.Clients.Elasticsearch.Security.RoleMappingRule? Rules { get; set; }
	[JsonInclude, JsonPropertyName("run_as")]
	public ICollection<string>? RunAs { get; set; }
}

/// <summary>
/// <para>
/// Create or update role mappings.
/// </para>
/// <para>
/// Role mappings define which roles are assigned to each user.
/// Each mapping has rules that identify users and a list of roles that are granted to those users.
/// The role mapping APIs are generally the preferred way to manage role mappings rather than using role mapping files. The create or update role mappings API cannot update role mappings that are defined in role mapping files.
/// </para>
/// <para>
/// This API does not create roles. Rather, it maps users to existing roles.
/// Roles can be created by using the create or update roles API or roles files.
/// </para>
/// </summary>
public sealed partial class PutRoleMappingRequestDescriptor : RequestDescriptor<PutRoleMappingRequestDescriptor, PutRoleMappingRequestParameters>
{
	internal PutRoleMappingRequestDescriptor(Action<PutRoleMappingRequestDescriptor> configure) => configure.Invoke(this);

	public PutRoleMappingRequestDescriptor(Elastic.Clients.Elasticsearch.Name name) : base(r => r.Required("name", name))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityPutRoleMapping;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.put_role_mapping";

	public PutRoleMappingRequestDescriptor Refresh(Elastic.Clients.Elasticsearch.Refresh? refresh) => Qs("refresh", refresh);

	public PutRoleMappingRequestDescriptor Name(Elastic.Clients.Elasticsearch.Name name)
	{
		RouteValues.Required("name", name);
		return Self;
	}

	private bool? EnabledValue { get; set; }
	private IDictionary<string, object>? MetadataValue { get; set; }
	private ICollection<string>? RolesValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Security.RoleTemplate>? RoleTemplatesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Security.RoleTemplateDescriptor RoleTemplatesDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Security.RoleTemplateDescriptor> RoleTemplatesDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Security.RoleTemplateDescriptor>[] RoleTemplatesDescriptorActions { get; set; }
	private Elastic.Clients.Elasticsearch.Security.RoleMappingRule? RulesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor RulesDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor> RulesDescriptorAction { get; set; }
	private ICollection<string>? RunAsValue { get; set; }

	public PutRoleMappingRequestDescriptor Enabled(bool? enabled = true)
	{
		EnabledValue = enabled;
		return Self;
	}

	public PutRoleMappingRequestDescriptor Metadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		MetadataValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	public PutRoleMappingRequestDescriptor Roles(ICollection<string>? roles)
	{
		RolesValue = roles;
		return Self;
	}

	public PutRoleMappingRequestDescriptor RoleTemplates(ICollection<Elastic.Clients.Elasticsearch.Security.RoleTemplate>? roleTemplates)
	{
		RoleTemplatesDescriptor = null;
		RoleTemplatesDescriptorAction = null;
		RoleTemplatesDescriptorActions = null;
		RoleTemplatesValue = roleTemplates;
		return Self;
	}

	public PutRoleMappingRequestDescriptor RoleTemplates(Elastic.Clients.Elasticsearch.Security.RoleTemplateDescriptor descriptor)
	{
		RoleTemplatesValue = null;
		RoleTemplatesDescriptorAction = null;
		RoleTemplatesDescriptorActions = null;
		RoleTemplatesDescriptor = descriptor;
		return Self;
	}

	public PutRoleMappingRequestDescriptor RoleTemplates(Action<Elastic.Clients.Elasticsearch.Security.RoleTemplateDescriptor> configure)
	{
		RoleTemplatesValue = null;
		RoleTemplatesDescriptor = null;
		RoleTemplatesDescriptorActions = null;
		RoleTemplatesDescriptorAction = configure;
		return Self;
	}

	public PutRoleMappingRequestDescriptor RoleTemplates(params Action<Elastic.Clients.Elasticsearch.Security.RoleTemplateDescriptor>[] configure)
	{
		RoleTemplatesValue = null;
		RoleTemplatesDescriptor = null;
		RoleTemplatesDescriptorAction = null;
		RoleTemplatesDescriptorActions = configure;
		return Self;
	}

	public PutRoleMappingRequestDescriptor Rules(Elastic.Clients.Elasticsearch.Security.RoleMappingRule? rules)
	{
		RulesDescriptor = null;
		RulesDescriptorAction = null;
		RulesValue = rules;
		return Self;
	}

	public PutRoleMappingRequestDescriptor Rules(Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor descriptor)
	{
		RulesValue = null;
		RulesDescriptorAction = null;
		RulesDescriptor = descriptor;
		return Self;
	}

	public PutRoleMappingRequestDescriptor Rules(Action<Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor> configure)
	{
		RulesValue = null;
		RulesDescriptor = null;
		RulesDescriptorAction = configure;
		return Self;
	}

	public PutRoleMappingRequestDescriptor RunAs(ICollection<string>? runAs)
	{
		RunAsValue = runAs;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (EnabledValue.HasValue)
		{
			writer.WritePropertyName("enabled");
			writer.WriteBooleanValue(EnabledValue.Value);
		}

		if (MetadataValue is not null)
		{
			writer.WritePropertyName("metadata");
			JsonSerializer.Serialize(writer, MetadataValue, options);
		}

		if (RolesValue is not null)
		{
			writer.WritePropertyName("roles");
			JsonSerializer.Serialize(writer, RolesValue, options);
		}

		if (RoleTemplatesDescriptor is not null)
		{
			writer.WritePropertyName("role_templates");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, RoleTemplatesDescriptor, options);
			writer.WriteEndArray();
		}
		else if (RoleTemplatesDescriptorAction is not null)
		{
			writer.WritePropertyName("role_templates");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Security.RoleTemplateDescriptor(RoleTemplatesDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (RoleTemplatesDescriptorActions is not null)
		{
			writer.WritePropertyName("role_templates");
			writer.WriteStartArray();
			foreach (var action in RoleTemplatesDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Security.RoleTemplateDescriptor(action), options);
			}

			writer.WriteEndArray();
		}
		else if (RoleTemplatesValue is not null)
		{
			writer.WritePropertyName("role_templates");
			JsonSerializer.Serialize(writer, RoleTemplatesValue, options);
		}

		if (RulesDescriptor is not null)
		{
			writer.WritePropertyName("rules");
			JsonSerializer.Serialize(writer, RulesDescriptor, options);
		}
		else if (RulesDescriptorAction is not null)
		{
			writer.WritePropertyName("rules");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Security.RoleMappingRuleDescriptor(RulesDescriptorAction), options);
		}
		else if (RulesValue is not null)
		{
			writer.WritePropertyName("rules");
			JsonSerializer.Serialize(writer, RulesValue, options);
		}

		if (RunAsValue is not null)
		{
			writer.WritePropertyName("run_as");
			JsonSerializer.Serialize(writer, RunAsValue, options);
		}

		writer.WriteEndObject();
	}
}