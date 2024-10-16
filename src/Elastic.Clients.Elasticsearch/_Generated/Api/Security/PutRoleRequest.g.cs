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

public sealed partial class PutRoleRequestParameters : RequestParameters
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
/// The role management APIs are generally the preferred way to manage roles, rather than using file-based role management.
/// The create or update roles API cannot update roles that are defined in roles files.
/// </para>
/// </summary>
public sealed partial class PutRoleRequest : PlainRequest<PutRoleRequestParameters>
{
	public PutRoleRequest(Elastic.Clients.Elasticsearch.Name name) : base(r => r.Required("name", name))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityPutRole;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.put_role";

	/// <summary>
	/// <para>
	/// If <c>true</c> (the default) then refresh the affected shards to make this operation visible to search, if <c>wait_for</c> then wait for a refresh to make this operation visible to search, if <c>false</c> then do nothing with refreshes.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Refresh?>("refresh"); set => Q("refresh", value); }

	/// <summary>
	/// <para>
	/// A list of application privilege entries.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("applications")]
	public ICollection<Elastic.Clients.Elasticsearch.Security.ApplicationPrivileges>? Applications { get; set; }

	/// <summary>
	/// <para>
	/// A list of cluster privileges. These privileges define the cluster-level actions for users with this role.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("cluster")]
	public ICollection<Elastic.Clients.Elasticsearch.Security.ClusterPrivilege>? Cluster { get; set; }

	/// <summary>
	/// <para>
	/// Optional description of the role descriptor
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("description")]
	public string? Description { get; set; }

	/// <summary>
	/// <para>
	/// An object defining global privileges. A global privilege is a form of cluster privilege that is request-aware. Support for global privileges is currently limited to the management of application privileges.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("global")]
	public IDictionary<string, object>? Global { get; set; }

	/// <summary>
	/// <para>
	/// A list of indices permissions entries.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("indices")]
	public ICollection<Elastic.Clients.Elasticsearch.Security.IndicesPrivileges>? Indices { get; set; }

	/// <summary>
	/// <para>
	/// Optional metadata. Within the metadata object, keys that begin with an underscore (<c>_</c>) are reserved for system use.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("metadata")]
	public IDictionary<string, object>? Metadata { get; set; }

	/// <summary>
	/// <para>
	/// A list of remote indices permissions entries.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("remote_indices")]
	public ICollection<Elastic.Clients.Elasticsearch.Security.RemoteIndicesPrivileges>? RemoteIndices { get; set; }

	/// <summary>
	/// <para>
	/// A list of users that the owners of this role can impersonate. <em>Note</em>: in Serverless, the run-as feature is disabled. For API compatibility, you can still specify an empty <c>run_as</c> field, but a non-empty list will be rejected.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("run_as")]
	public ICollection<string>? RunAs { get; set; }

	/// <summary>
	/// <para>
	/// Indicates roles that might be incompatible with the current cluster license, specifically roles with document and field level security. When the cluster license doesn’t allow certain features for a given role, this parameter is updated dynamically to list the incompatible features. If <c>enabled</c> is <c>false</c>, the role is ignored, but is still listed in the response from the authenticate API.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("transient_metadata")]
	public IDictionary<string, object>? TransientMetadata { get; set; }
}

/// <summary>
/// <para>
/// The role management APIs are generally the preferred way to manage roles, rather than using file-based role management.
/// The create or update roles API cannot update roles that are defined in roles files.
/// </para>
/// </summary>
public sealed partial class PutRoleRequestDescriptor<TDocument> : RequestDescriptor<PutRoleRequestDescriptor<TDocument>, PutRoleRequestParameters>
{
	internal PutRoleRequestDescriptor(Action<PutRoleRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public PutRoleRequestDescriptor(Elastic.Clients.Elasticsearch.Name name) : base(r => r.Required("name", name))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityPutRole;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.put_role";

	public PutRoleRequestDescriptor<TDocument> Refresh(Elastic.Clients.Elasticsearch.Refresh? refresh) => Qs("refresh", refresh);

	public PutRoleRequestDescriptor<TDocument> Name(Elastic.Clients.Elasticsearch.Name name)
	{
		RouteValues.Required("name", name);
		return Self;
	}

	private ICollection<Elastic.Clients.Elasticsearch.Security.ApplicationPrivileges>? ApplicationsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Security.ApplicationPrivilegesDescriptor ApplicationsDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Security.ApplicationPrivilegesDescriptor> ApplicationsDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Security.ApplicationPrivilegesDescriptor>[] ApplicationsDescriptorActions { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Security.ClusterPrivilege>? ClusterValue { get; set; }
	private string? DescriptionValue { get; set; }
	private IDictionary<string, object>? GlobalValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Security.IndicesPrivileges>? IndicesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Security.IndicesPrivilegesDescriptor<TDocument> IndicesDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Security.IndicesPrivilegesDescriptor<TDocument>> IndicesDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Security.IndicesPrivilegesDescriptor<TDocument>>[] IndicesDescriptorActions { get; set; }
	private IDictionary<string, object>? MetadataValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Security.RemoteIndicesPrivileges>? RemoteIndicesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Security.RemoteIndicesPrivilegesDescriptor<TDocument> RemoteIndicesDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Security.RemoteIndicesPrivilegesDescriptor<TDocument>> RemoteIndicesDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Security.RemoteIndicesPrivilegesDescriptor<TDocument>>[] RemoteIndicesDescriptorActions { get; set; }
	private ICollection<string>? RunAsValue { get; set; }
	private IDictionary<string, object>? TransientMetadataValue { get; set; }

	/// <summary>
	/// <para>
	/// A list of application privilege entries.
	/// </para>
	/// </summary>
	public PutRoleRequestDescriptor<TDocument> Applications(ICollection<Elastic.Clients.Elasticsearch.Security.ApplicationPrivileges>? applications)
	{
		ApplicationsDescriptor = null;
		ApplicationsDescriptorAction = null;
		ApplicationsDescriptorActions = null;
		ApplicationsValue = applications;
		return Self;
	}

	public PutRoleRequestDescriptor<TDocument> Applications(Elastic.Clients.Elasticsearch.Security.ApplicationPrivilegesDescriptor descriptor)
	{
		ApplicationsValue = null;
		ApplicationsDescriptorAction = null;
		ApplicationsDescriptorActions = null;
		ApplicationsDescriptor = descriptor;
		return Self;
	}

	public PutRoleRequestDescriptor<TDocument> Applications(Action<Elastic.Clients.Elasticsearch.Security.ApplicationPrivilegesDescriptor> configure)
	{
		ApplicationsValue = null;
		ApplicationsDescriptor = null;
		ApplicationsDescriptorActions = null;
		ApplicationsDescriptorAction = configure;
		return Self;
	}

	public PutRoleRequestDescriptor<TDocument> Applications(params Action<Elastic.Clients.Elasticsearch.Security.ApplicationPrivilegesDescriptor>[] configure)
	{
		ApplicationsValue = null;
		ApplicationsDescriptor = null;
		ApplicationsDescriptorAction = null;
		ApplicationsDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// A list of cluster privileges. These privileges define the cluster-level actions for users with this role.
	/// </para>
	/// </summary>
	public PutRoleRequestDescriptor<TDocument> Cluster(ICollection<Elastic.Clients.Elasticsearch.Security.ClusterPrivilege>? cluster)
	{
		ClusterValue = cluster;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Optional description of the role descriptor
	/// </para>
	/// </summary>
	public PutRoleRequestDescriptor<TDocument> Description(string? description)
	{
		DescriptionValue = description;
		return Self;
	}

	/// <summary>
	/// <para>
	/// An object defining global privileges. A global privilege is a form of cluster privilege that is request-aware. Support for global privileges is currently limited to the management of application privileges.
	/// </para>
	/// </summary>
	public PutRoleRequestDescriptor<TDocument> Global(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		GlobalValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// A list of indices permissions entries.
	/// </para>
	/// </summary>
	public PutRoleRequestDescriptor<TDocument> Indices(ICollection<Elastic.Clients.Elasticsearch.Security.IndicesPrivileges>? indices)
	{
		IndicesDescriptor = null;
		IndicesDescriptorAction = null;
		IndicesDescriptorActions = null;
		IndicesValue = indices;
		return Self;
	}

	public PutRoleRequestDescriptor<TDocument> Indices(Elastic.Clients.Elasticsearch.Security.IndicesPrivilegesDescriptor<TDocument> descriptor)
	{
		IndicesValue = null;
		IndicesDescriptorAction = null;
		IndicesDescriptorActions = null;
		IndicesDescriptor = descriptor;
		return Self;
	}

	public PutRoleRequestDescriptor<TDocument> Indices(Action<Elastic.Clients.Elasticsearch.Security.IndicesPrivilegesDescriptor<TDocument>> configure)
	{
		IndicesValue = null;
		IndicesDescriptor = null;
		IndicesDescriptorActions = null;
		IndicesDescriptorAction = configure;
		return Self;
	}

	public PutRoleRequestDescriptor<TDocument> Indices(params Action<Elastic.Clients.Elasticsearch.Security.IndicesPrivilegesDescriptor<TDocument>>[] configure)
	{
		IndicesValue = null;
		IndicesDescriptor = null;
		IndicesDescriptorAction = null;
		IndicesDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Optional metadata. Within the metadata object, keys that begin with an underscore (<c>_</c>) are reserved for system use.
	/// </para>
	/// </summary>
	public PutRoleRequestDescriptor<TDocument> Metadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		MetadataValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// A list of remote indices permissions entries.
	/// </para>
	/// </summary>
	public PutRoleRequestDescriptor<TDocument> RemoteIndices(ICollection<Elastic.Clients.Elasticsearch.Security.RemoteIndicesPrivileges>? remoteIndices)
	{
		RemoteIndicesDescriptor = null;
		RemoteIndicesDescriptorAction = null;
		RemoteIndicesDescriptorActions = null;
		RemoteIndicesValue = remoteIndices;
		return Self;
	}

	public PutRoleRequestDescriptor<TDocument> RemoteIndices(Elastic.Clients.Elasticsearch.Security.RemoteIndicesPrivilegesDescriptor<TDocument> descriptor)
	{
		RemoteIndicesValue = null;
		RemoteIndicesDescriptorAction = null;
		RemoteIndicesDescriptorActions = null;
		RemoteIndicesDescriptor = descriptor;
		return Self;
	}

	public PutRoleRequestDescriptor<TDocument> RemoteIndices(Action<Elastic.Clients.Elasticsearch.Security.RemoteIndicesPrivilegesDescriptor<TDocument>> configure)
	{
		RemoteIndicesValue = null;
		RemoteIndicesDescriptor = null;
		RemoteIndicesDescriptorActions = null;
		RemoteIndicesDescriptorAction = configure;
		return Self;
	}

	public PutRoleRequestDescriptor<TDocument> RemoteIndices(params Action<Elastic.Clients.Elasticsearch.Security.RemoteIndicesPrivilegesDescriptor<TDocument>>[] configure)
	{
		RemoteIndicesValue = null;
		RemoteIndicesDescriptor = null;
		RemoteIndicesDescriptorAction = null;
		RemoteIndicesDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// A list of users that the owners of this role can impersonate. <em>Note</em>: in Serverless, the run-as feature is disabled. For API compatibility, you can still specify an empty <c>run_as</c> field, but a non-empty list will be rejected.
	/// </para>
	/// </summary>
	public PutRoleRequestDescriptor<TDocument> RunAs(ICollection<string>? runAs)
	{
		RunAsValue = runAs;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Indicates roles that might be incompatible with the current cluster license, specifically roles with document and field level security. When the cluster license doesn’t allow certain features for a given role, this parameter is updated dynamically to list the incompatible features. If <c>enabled</c> is <c>false</c>, the role is ignored, but is still listed in the response from the authenticate API.
	/// </para>
	/// </summary>
	public PutRoleRequestDescriptor<TDocument> TransientMetadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		TransientMetadataValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (ApplicationsDescriptor is not null)
		{
			writer.WritePropertyName("applications");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, ApplicationsDescriptor, options);
			writer.WriteEndArray();
		}
		else if (ApplicationsDescriptorAction is not null)
		{
			writer.WritePropertyName("applications");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Security.ApplicationPrivilegesDescriptor(ApplicationsDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (ApplicationsDescriptorActions is not null)
		{
			writer.WritePropertyName("applications");
			writer.WriteStartArray();
			foreach (var action in ApplicationsDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Security.ApplicationPrivilegesDescriptor(action), options);
			}

			writer.WriteEndArray();
		}
		else if (ApplicationsValue is not null)
		{
			writer.WritePropertyName("applications");
			JsonSerializer.Serialize(writer, ApplicationsValue, options);
		}

		if (ClusterValue is not null)
		{
			writer.WritePropertyName("cluster");
			JsonSerializer.Serialize(writer, ClusterValue, options);
		}

		if (!string.IsNullOrEmpty(DescriptionValue))
		{
			writer.WritePropertyName("description");
			writer.WriteStringValue(DescriptionValue);
		}

		if (GlobalValue is not null)
		{
			writer.WritePropertyName("global");
			JsonSerializer.Serialize(writer, GlobalValue, options);
		}

		if (IndicesDescriptor is not null)
		{
			writer.WritePropertyName("indices");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, IndicesDescriptor, options);
			writer.WriteEndArray();
		}
		else if (IndicesDescriptorAction is not null)
		{
			writer.WritePropertyName("indices");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Security.IndicesPrivilegesDescriptor<TDocument>(IndicesDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (IndicesDescriptorActions is not null)
		{
			writer.WritePropertyName("indices");
			writer.WriteStartArray();
			foreach (var action in IndicesDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Security.IndicesPrivilegesDescriptor<TDocument>(action), options);
			}

			writer.WriteEndArray();
		}
		else if (IndicesValue is not null)
		{
			writer.WritePropertyName("indices");
			JsonSerializer.Serialize(writer, IndicesValue, options);
		}

		if (MetadataValue is not null)
		{
			writer.WritePropertyName("metadata");
			JsonSerializer.Serialize(writer, MetadataValue, options);
		}

		if (RemoteIndicesDescriptor is not null)
		{
			writer.WritePropertyName("remote_indices");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, RemoteIndicesDescriptor, options);
			writer.WriteEndArray();
		}
		else if (RemoteIndicesDescriptorAction is not null)
		{
			writer.WritePropertyName("remote_indices");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Security.RemoteIndicesPrivilegesDescriptor<TDocument>(RemoteIndicesDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (RemoteIndicesDescriptorActions is not null)
		{
			writer.WritePropertyName("remote_indices");
			writer.WriteStartArray();
			foreach (var action in RemoteIndicesDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Security.RemoteIndicesPrivilegesDescriptor<TDocument>(action), options);
			}

			writer.WriteEndArray();
		}
		else if (RemoteIndicesValue is not null)
		{
			writer.WritePropertyName("remote_indices");
			JsonSerializer.Serialize(writer, RemoteIndicesValue, options);
		}

		if (RunAsValue is not null)
		{
			writer.WritePropertyName("run_as");
			JsonSerializer.Serialize(writer, RunAsValue, options);
		}

		if (TransientMetadataValue is not null)
		{
			writer.WritePropertyName("transient_metadata");
			JsonSerializer.Serialize(writer, TransientMetadataValue, options);
		}

		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// The role management APIs are generally the preferred way to manage roles, rather than using file-based role management.
/// The create or update roles API cannot update roles that are defined in roles files.
/// </para>
/// </summary>
public sealed partial class PutRoleRequestDescriptor : RequestDescriptor<PutRoleRequestDescriptor, PutRoleRequestParameters>
{
	internal PutRoleRequestDescriptor(Action<PutRoleRequestDescriptor> configure) => configure.Invoke(this);

	public PutRoleRequestDescriptor(Elastic.Clients.Elasticsearch.Name name) : base(r => r.Required("name", name))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityPutRole;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.put_role";

	public PutRoleRequestDescriptor Refresh(Elastic.Clients.Elasticsearch.Refresh? refresh) => Qs("refresh", refresh);

	public PutRoleRequestDescriptor Name(Elastic.Clients.Elasticsearch.Name name)
	{
		RouteValues.Required("name", name);
		return Self;
	}

	private ICollection<Elastic.Clients.Elasticsearch.Security.ApplicationPrivileges>? ApplicationsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Security.ApplicationPrivilegesDescriptor ApplicationsDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Security.ApplicationPrivilegesDescriptor> ApplicationsDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Security.ApplicationPrivilegesDescriptor>[] ApplicationsDescriptorActions { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Security.ClusterPrivilege>? ClusterValue { get; set; }
	private string? DescriptionValue { get; set; }
	private IDictionary<string, object>? GlobalValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Security.IndicesPrivileges>? IndicesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Security.IndicesPrivilegesDescriptor IndicesDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Security.IndicesPrivilegesDescriptor> IndicesDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Security.IndicesPrivilegesDescriptor>[] IndicesDescriptorActions { get; set; }
	private IDictionary<string, object>? MetadataValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Security.RemoteIndicesPrivileges>? RemoteIndicesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Security.RemoteIndicesPrivilegesDescriptor RemoteIndicesDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Security.RemoteIndicesPrivilegesDescriptor> RemoteIndicesDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Security.RemoteIndicesPrivilegesDescriptor>[] RemoteIndicesDescriptorActions { get; set; }
	private ICollection<string>? RunAsValue { get; set; }
	private IDictionary<string, object>? TransientMetadataValue { get; set; }

	/// <summary>
	/// <para>
	/// A list of application privilege entries.
	/// </para>
	/// </summary>
	public PutRoleRequestDescriptor Applications(ICollection<Elastic.Clients.Elasticsearch.Security.ApplicationPrivileges>? applications)
	{
		ApplicationsDescriptor = null;
		ApplicationsDescriptorAction = null;
		ApplicationsDescriptorActions = null;
		ApplicationsValue = applications;
		return Self;
	}

	public PutRoleRequestDescriptor Applications(Elastic.Clients.Elasticsearch.Security.ApplicationPrivilegesDescriptor descriptor)
	{
		ApplicationsValue = null;
		ApplicationsDescriptorAction = null;
		ApplicationsDescriptorActions = null;
		ApplicationsDescriptor = descriptor;
		return Self;
	}

	public PutRoleRequestDescriptor Applications(Action<Elastic.Clients.Elasticsearch.Security.ApplicationPrivilegesDescriptor> configure)
	{
		ApplicationsValue = null;
		ApplicationsDescriptor = null;
		ApplicationsDescriptorActions = null;
		ApplicationsDescriptorAction = configure;
		return Self;
	}

	public PutRoleRequestDescriptor Applications(params Action<Elastic.Clients.Elasticsearch.Security.ApplicationPrivilegesDescriptor>[] configure)
	{
		ApplicationsValue = null;
		ApplicationsDescriptor = null;
		ApplicationsDescriptorAction = null;
		ApplicationsDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// A list of cluster privileges. These privileges define the cluster-level actions for users with this role.
	/// </para>
	/// </summary>
	public PutRoleRequestDescriptor Cluster(ICollection<Elastic.Clients.Elasticsearch.Security.ClusterPrivilege>? cluster)
	{
		ClusterValue = cluster;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Optional description of the role descriptor
	/// </para>
	/// </summary>
	public PutRoleRequestDescriptor Description(string? description)
	{
		DescriptionValue = description;
		return Self;
	}

	/// <summary>
	/// <para>
	/// An object defining global privileges. A global privilege is a form of cluster privilege that is request-aware. Support for global privileges is currently limited to the management of application privileges.
	/// </para>
	/// </summary>
	public PutRoleRequestDescriptor Global(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		GlobalValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// A list of indices permissions entries.
	/// </para>
	/// </summary>
	public PutRoleRequestDescriptor Indices(ICollection<Elastic.Clients.Elasticsearch.Security.IndicesPrivileges>? indices)
	{
		IndicesDescriptor = null;
		IndicesDescriptorAction = null;
		IndicesDescriptorActions = null;
		IndicesValue = indices;
		return Self;
	}

	public PutRoleRequestDescriptor Indices(Elastic.Clients.Elasticsearch.Security.IndicesPrivilegesDescriptor descriptor)
	{
		IndicesValue = null;
		IndicesDescriptorAction = null;
		IndicesDescriptorActions = null;
		IndicesDescriptor = descriptor;
		return Self;
	}

	public PutRoleRequestDescriptor Indices(Action<Elastic.Clients.Elasticsearch.Security.IndicesPrivilegesDescriptor> configure)
	{
		IndicesValue = null;
		IndicesDescriptor = null;
		IndicesDescriptorActions = null;
		IndicesDescriptorAction = configure;
		return Self;
	}

	public PutRoleRequestDescriptor Indices(params Action<Elastic.Clients.Elasticsearch.Security.IndicesPrivilegesDescriptor>[] configure)
	{
		IndicesValue = null;
		IndicesDescriptor = null;
		IndicesDescriptorAction = null;
		IndicesDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Optional metadata. Within the metadata object, keys that begin with an underscore (<c>_</c>) are reserved for system use.
	/// </para>
	/// </summary>
	public PutRoleRequestDescriptor Metadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		MetadataValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// A list of remote indices permissions entries.
	/// </para>
	/// </summary>
	public PutRoleRequestDescriptor RemoteIndices(ICollection<Elastic.Clients.Elasticsearch.Security.RemoteIndicesPrivileges>? remoteIndices)
	{
		RemoteIndicesDescriptor = null;
		RemoteIndicesDescriptorAction = null;
		RemoteIndicesDescriptorActions = null;
		RemoteIndicesValue = remoteIndices;
		return Self;
	}

	public PutRoleRequestDescriptor RemoteIndices(Elastic.Clients.Elasticsearch.Security.RemoteIndicesPrivilegesDescriptor descriptor)
	{
		RemoteIndicesValue = null;
		RemoteIndicesDescriptorAction = null;
		RemoteIndicesDescriptorActions = null;
		RemoteIndicesDescriptor = descriptor;
		return Self;
	}

	public PutRoleRequestDescriptor RemoteIndices(Action<Elastic.Clients.Elasticsearch.Security.RemoteIndicesPrivilegesDescriptor> configure)
	{
		RemoteIndicesValue = null;
		RemoteIndicesDescriptor = null;
		RemoteIndicesDescriptorActions = null;
		RemoteIndicesDescriptorAction = configure;
		return Self;
	}

	public PutRoleRequestDescriptor RemoteIndices(params Action<Elastic.Clients.Elasticsearch.Security.RemoteIndicesPrivilegesDescriptor>[] configure)
	{
		RemoteIndicesValue = null;
		RemoteIndicesDescriptor = null;
		RemoteIndicesDescriptorAction = null;
		RemoteIndicesDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// A list of users that the owners of this role can impersonate. <em>Note</em>: in Serverless, the run-as feature is disabled. For API compatibility, you can still specify an empty <c>run_as</c> field, but a non-empty list will be rejected.
	/// </para>
	/// </summary>
	public PutRoleRequestDescriptor RunAs(ICollection<string>? runAs)
	{
		RunAsValue = runAs;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Indicates roles that might be incompatible with the current cluster license, specifically roles with document and field level security. When the cluster license doesn’t allow certain features for a given role, this parameter is updated dynamically to list the incompatible features. If <c>enabled</c> is <c>false</c>, the role is ignored, but is still listed in the response from the authenticate API.
	/// </para>
	/// </summary>
	public PutRoleRequestDescriptor TransientMetadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		TransientMetadataValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (ApplicationsDescriptor is not null)
		{
			writer.WritePropertyName("applications");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, ApplicationsDescriptor, options);
			writer.WriteEndArray();
		}
		else if (ApplicationsDescriptorAction is not null)
		{
			writer.WritePropertyName("applications");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Security.ApplicationPrivilegesDescriptor(ApplicationsDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (ApplicationsDescriptorActions is not null)
		{
			writer.WritePropertyName("applications");
			writer.WriteStartArray();
			foreach (var action in ApplicationsDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Security.ApplicationPrivilegesDescriptor(action), options);
			}

			writer.WriteEndArray();
		}
		else if (ApplicationsValue is not null)
		{
			writer.WritePropertyName("applications");
			JsonSerializer.Serialize(writer, ApplicationsValue, options);
		}

		if (ClusterValue is not null)
		{
			writer.WritePropertyName("cluster");
			JsonSerializer.Serialize(writer, ClusterValue, options);
		}

		if (!string.IsNullOrEmpty(DescriptionValue))
		{
			writer.WritePropertyName("description");
			writer.WriteStringValue(DescriptionValue);
		}

		if (GlobalValue is not null)
		{
			writer.WritePropertyName("global");
			JsonSerializer.Serialize(writer, GlobalValue, options);
		}

		if (IndicesDescriptor is not null)
		{
			writer.WritePropertyName("indices");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, IndicesDescriptor, options);
			writer.WriteEndArray();
		}
		else if (IndicesDescriptorAction is not null)
		{
			writer.WritePropertyName("indices");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Security.IndicesPrivilegesDescriptor(IndicesDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (IndicesDescriptorActions is not null)
		{
			writer.WritePropertyName("indices");
			writer.WriteStartArray();
			foreach (var action in IndicesDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Security.IndicesPrivilegesDescriptor(action), options);
			}

			writer.WriteEndArray();
		}
		else if (IndicesValue is not null)
		{
			writer.WritePropertyName("indices");
			JsonSerializer.Serialize(writer, IndicesValue, options);
		}

		if (MetadataValue is not null)
		{
			writer.WritePropertyName("metadata");
			JsonSerializer.Serialize(writer, MetadataValue, options);
		}

		if (RemoteIndicesDescriptor is not null)
		{
			writer.WritePropertyName("remote_indices");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, RemoteIndicesDescriptor, options);
			writer.WriteEndArray();
		}
		else if (RemoteIndicesDescriptorAction is not null)
		{
			writer.WritePropertyName("remote_indices");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Security.RemoteIndicesPrivilegesDescriptor(RemoteIndicesDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (RemoteIndicesDescriptorActions is not null)
		{
			writer.WritePropertyName("remote_indices");
			writer.WriteStartArray();
			foreach (var action in RemoteIndicesDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Security.RemoteIndicesPrivilegesDescriptor(action), options);
			}

			writer.WriteEndArray();
		}
		else if (RemoteIndicesValue is not null)
		{
			writer.WritePropertyName("remote_indices");
			JsonSerializer.Serialize(writer, RemoteIndicesValue, options);
		}

		if (RunAsValue is not null)
		{
			writer.WritePropertyName("run_as");
			JsonSerializer.Serialize(writer, RunAsValue, options);
		}

		if (TransientMetadataValue is not null)
		{
			writer.WritePropertyName("transient_metadata");
			JsonSerializer.Serialize(writer, TransientMetadataValue, options);
		}

		writer.WriteEndObject();
	}
}