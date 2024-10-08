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

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.Security;

public sealed partial class RoleTemplate
{
	[JsonInclude, JsonPropertyName("format")]
	public Elastic.Clients.Elasticsearch.Serverless.Security.TemplateFormat? Format { get; set; }
	[JsonInclude, JsonPropertyName("template")]
	public Elastic.Clients.Elasticsearch.Serverless.Script Template { get; set; }
}

public sealed partial class RoleTemplateDescriptor : SerializableDescriptor<RoleTemplateDescriptor>
{
	internal RoleTemplateDescriptor(Action<RoleTemplateDescriptor> configure) => configure.Invoke(this);

	public RoleTemplateDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Serverless.Security.TemplateFormat? FormatValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Script TemplateValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor TemplateDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor> TemplateDescriptorAction { get; set; }

	public RoleTemplateDescriptor Format(Elastic.Clients.Elasticsearch.Serverless.Security.TemplateFormat? format)
	{
		FormatValue = format;
		return Self;
	}

	public RoleTemplateDescriptor Template(Elastic.Clients.Elasticsearch.Serverless.Script template)
	{
		TemplateDescriptor = null;
		TemplateDescriptorAction = null;
		TemplateValue = template;
		return Self;
	}

	public RoleTemplateDescriptor Template(Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor descriptor)
	{
		TemplateValue = null;
		TemplateDescriptorAction = null;
		TemplateDescriptor = descriptor;
		return Self;
	}

	public RoleTemplateDescriptor Template(Action<Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor> configure)
	{
		TemplateValue = null;
		TemplateDescriptor = null;
		TemplateDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (FormatValue is not null)
		{
			writer.WritePropertyName("format");
			JsonSerializer.Serialize(writer, FormatValue, options);
		}

		if (TemplateDescriptor is not null)
		{
			writer.WritePropertyName("template");
			JsonSerializer.Serialize(writer, TemplateDescriptor, options);
		}
		else if (TemplateDescriptorAction is not null)
		{
			writer.WritePropertyName("template");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor(TemplateDescriptorAction), options);
		}
		else
		{
			writer.WritePropertyName("template");
			JsonSerializer.Serialize(writer, TemplateValue, options);
		}

		writer.WriteEndObject();
	}
}