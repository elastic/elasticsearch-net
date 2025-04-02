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

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Snapshot;

internal sealed partial class GcsRepositoryConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Snapshot.GcsRepository>
{
	private static readonly System.Text.Json.JsonEncodedText PropSettings = System.Text.Json.JsonEncodedText.Encode("settings");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");
	private static readonly System.Text.Json.JsonEncodedText PropUuid = System.Text.Json.JsonEncodedText.Encode("uuid");

	public override Elastic.Clients.Elasticsearch.Snapshot.GcsRepository Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettings> propSettings = default;
		LocalJsonValue<string?> propUuid = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propSettings.TryReadProperty(ref reader, options, PropSettings, null))
			{
				continue;
			}

			if (reader.ValueTextEquals(PropType))
			{
				reader.Skip();
				continue;
			}

			if (propUuid.TryReadProperty(ref reader, options, PropUuid, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Snapshot.GcsRepository(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Settings = propSettings.Value,
			Uuid = propUuid.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Snapshot.GcsRepository value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropSettings, value.Settings, null, null);
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteProperty(options, PropUuid, value.Uuid, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Snapshot.GcsRepositoryConverter))]
public sealed partial class GcsRepository : Elastic.Clients.Elasticsearch.Snapshot.IRepository
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GcsRepository(Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettings settings)
	{
		Settings = settings;
	}
#if NET7_0_OR_GREATER
	public GcsRepository()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public GcsRepository()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GcsRepository(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The repository settings.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettings Settings { get; set; }

	/// <summary>
	/// <para>
	/// The Google Cloud Storage repository type.
	/// </para>
	/// </summary>
	public string Type => "gcs";

	public string? Uuid { get; set; }
}

public readonly partial struct GcsRepositoryDescriptor
{
	internal Elastic.Clients.Elasticsearch.Snapshot.GcsRepository Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GcsRepositoryDescriptor(Elastic.Clients.Elasticsearch.Snapshot.GcsRepository instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GcsRepositoryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Snapshot.GcsRepository(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Snapshot.GcsRepositoryDescriptor(Elastic.Clients.Elasticsearch.Snapshot.GcsRepository instance) => new Elastic.Clients.Elasticsearch.Snapshot.GcsRepositoryDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Snapshot.GcsRepository(Elastic.Clients.Elasticsearch.Snapshot.GcsRepositoryDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The repository settings.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.GcsRepositoryDescriptor Settings(Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettings value)
	{
		Instance.Settings = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The repository settings.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.GcsRepositoryDescriptor Settings(System.Action<Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettingsDescriptor> action)
	{
		Instance.Settings = Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettingsDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Snapshot.GcsRepositoryDescriptor Uuid(string? value)
	{
		Instance.Uuid = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Snapshot.GcsRepository Build(System.Action<Elastic.Clients.Elasticsearch.Snapshot.GcsRepositoryDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Snapshot.GcsRepositoryDescriptor(new Elastic.Clients.Elasticsearch.Snapshot.GcsRepository(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}