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

namespace Elastic.Clients.Elasticsearch.Serverless.Snapshot;

public sealed partial class SharedFileSystemRepository : IRepository
{
	[JsonInclude, JsonPropertyName("settings")]
	public Elastic.Clients.Elasticsearch.Serverless.Snapshot.SharedFileSystemRepositorySettings Settings { get; set; }

	[JsonInclude, JsonPropertyName("type")]
	public string Type => "fs";

	[JsonInclude, JsonPropertyName("uuid")]
	public string? Uuid { get; set; }
}

public sealed partial class SharedFileSystemRepositoryDescriptor : SerializableDescriptor<SharedFileSystemRepositoryDescriptor>, IBuildableDescriptor<SharedFileSystemRepository>
{
	internal SharedFileSystemRepositoryDescriptor(Action<SharedFileSystemRepositoryDescriptor> configure) => configure.Invoke(this);

	public SharedFileSystemRepositoryDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Serverless.Snapshot.SharedFileSystemRepositorySettings SettingsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Snapshot.SharedFileSystemRepositorySettingsDescriptor SettingsDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.Snapshot.SharedFileSystemRepositorySettingsDescriptor> SettingsDescriptorAction { get; set; }
	private string? UuidValue { get; set; }

	public SharedFileSystemRepositoryDescriptor Settings(Elastic.Clients.Elasticsearch.Serverless.Snapshot.SharedFileSystemRepositorySettings settings)
	{
		SettingsDescriptor = null;
		SettingsDescriptorAction = null;
		SettingsValue = settings;
		return Self;
	}

	public SharedFileSystemRepositoryDescriptor Settings(Elastic.Clients.Elasticsearch.Serverless.Snapshot.SharedFileSystemRepositorySettingsDescriptor descriptor)
	{
		SettingsValue = null;
		SettingsDescriptorAction = null;
		SettingsDescriptor = descriptor;
		return Self;
	}

	public SharedFileSystemRepositoryDescriptor Settings(Action<Elastic.Clients.Elasticsearch.Serverless.Snapshot.SharedFileSystemRepositorySettingsDescriptor> configure)
	{
		SettingsValue = null;
		SettingsDescriptor = null;
		SettingsDescriptorAction = configure;
		return Self;
	}

	public SharedFileSystemRepositoryDescriptor Uuid(string? uuid)
	{
		UuidValue = uuid;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (SettingsDescriptor is not null)
		{
			writer.WritePropertyName("settings");
			JsonSerializer.Serialize(writer, SettingsDescriptor, options);
		}
		else if (SettingsDescriptorAction is not null)
		{
			writer.WritePropertyName("settings");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.Snapshot.SharedFileSystemRepositorySettingsDescriptor(SettingsDescriptorAction), options);
		}
		else
		{
			writer.WritePropertyName("settings");
			JsonSerializer.Serialize(writer, SettingsValue, options);
		}

		writer.WritePropertyName("type");
		writer.WriteStringValue("fs");
		if (!string.IsNullOrEmpty(UuidValue))
		{
			writer.WritePropertyName("uuid");
			writer.WriteStringValue(UuidValue);
		}

		writer.WriteEndObject();
	}

	private Elastic.Clients.Elasticsearch.Serverless.Snapshot.SharedFileSystemRepositorySettings BuildSettings()
	{
		if (SettingsValue is not null)
		{
			return SettingsValue;
		}

		if ((object)SettingsDescriptor is IBuildableDescriptor<Elastic.Clients.Elasticsearch.Serverless.Snapshot.SharedFileSystemRepositorySettings> buildable)
		{
			return buildable.Build();
		}

		if (SettingsDescriptorAction is not null)
		{
			var descriptor = new Elastic.Clients.Elasticsearch.Serverless.Snapshot.SharedFileSystemRepositorySettingsDescriptor(SettingsDescriptorAction);
			if ((object)descriptor is IBuildableDescriptor<Elastic.Clients.Elasticsearch.Serverless.Snapshot.SharedFileSystemRepositorySettings> buildableFromAction)
			{
				return buildableFromAction.Build();
			}
		}

		return null;
	}

	SharedFileSystemRepository IBuildableDescriptor<SharedFileSystemRepository>.Build() => new()
	{
		Settings = BuildSettings(),
		Uuid = UuidValue
	};
}