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
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Security;

public sealed partial class GlobalPrivilege
{
	[JsonInclude, JsonPropertyName("application")]
	public Elastic.Clients.Elasticsearch.Security.ApplicationGlobalUserPrivileges Application { get; set; }
}

public sealed partial class GlobalPrivilegeDescriptor : SerializableDescriptor<GlobalPrivilegeDescriptor>
{
	internal GlobalPrivilegeDescriptor(Action<GlobalPrivilegeDescriptor> configure) => configure.Invoke(this);

	public GlobalPrivilegeDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Security.ApplicationGlobalUserPrivileges ApplicationValue { get; set; }
	private Elastic.Clients.Elasticsearch.Security.ApplicationGlobalUserPrivilegesDescriptor ApplicationDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Security.ApplicationGlobalUserPrivilegesDescriptor> ApplicationDescriptorAction { get; set; }

	public GlobalPrivilegeDescriptor Application(Elastic.Clients.Elasticsearch.Security.ApplicationGlobalUserPrivileges application)
	{
		ApplicationDescriptor = null;
		ApplicationDescriptorAction = null;
		ApplicationValue = application;
		return Self;
	}

	public GlobalPrivilegeDescriptor Application(Elastic.Clients.Elasticsearch.Security.ApplicationGlobalUserPrivilegesDescriptor descriptor)
	{
		ApplicationValue = null;
		ApplicationDescriptorAction = null;
		ApplicationDescriptor = descriptor;
		return Self;
	}

	public GlobalPrivilegeDescriptor Application(Action<Elastic.Clients.Elasticsearch.Security.ApplicationGlobalUserPrivilegesDescriptor> configure)
	{
		ApplicationValue = null;
		ApplicationDescriptor = null;
		ApplicationDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (ApplicationDescriptor is not null)
		{
			writer.WritePropertyName("application");
			JsonSerializer.Serialize(writer, ApplicationDescriptor, options);
		}
		else if (ApplicationDescriptorAction is not null)
		{
			writer.WritePropertyName("application");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Security.ApplicationGlobalUserPrivilegesDescriptor(ApplicationDescriptorAction), options);
		}
		else
		{
			writer.WritePropertyName("application");
			JsonSerializer.Serialize(writer, ApplicationValue, options);
		}

		writer.WriteEndObject();
	}
}