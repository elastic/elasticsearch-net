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

namespace Elastic.Clients.Elasticsearch.SearchApplication;

internal sealed partial class SearchApplicationTemplateConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationTemplate>
{
	private static readonly System.Text.Json.JsonEncodedText PropScript = System.Text.Json.JsonEncodedText.Encode("script");

	public override Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationTemplate Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Script> propScript = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propScript.TryReadProperty(ref reader, options, PropScript, null))
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
		return new Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationTemplate(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Script = propScript.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationTemplate value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropScript, value.Script, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationTemplateConverter))]
public sealed partial class SearchApplicationTemplate
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SearchApplicationTemplate(Elastic.Clients.Elasticsearch.Script script)
	{
		Script = script;
	}
#if NET7_0_OR_GREATER
	public SearchApplicationTemplate()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public SearchApplicationTemplate()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal SearchApplicationTemplate(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The associated mustache template.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Script Script { get; set; }
}

public readonly partial struct SearchApplicationTemplateDescriptor
{
	internal Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationTemplate Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SearchApplicationTemplateDescriptor(Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationTemplate instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SearchApplicationTemplateDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationTemplate(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationTemplateDescriptor(Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationTemplate instance) => new Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationTemplateDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationTemplate(Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationTemplateDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The associated mustache template.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationTemplateDescriptor Script(Elastic.Clients.Elasticsearch.Script value)
	{
		Instance.Script = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The associated mustache template.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationTemplateDescriptor Script()
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// The associated mustache template.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationTemplateDescriptor Script(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationTemplate Build(System.Action<Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationTemplateDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationTemplateDescriptor(new Elastic.Clients.Elasticsearch.SearchApplication.SearchApplicationTemplate(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}