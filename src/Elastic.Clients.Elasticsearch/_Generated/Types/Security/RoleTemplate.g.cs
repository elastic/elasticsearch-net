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

namespace Elastic.Clients.Elasticsearch.Security;

internal sealed partial class RoleTemplateConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.RoleTemplate>
{
	private static readonly System.Text.Json.JsonEncodedText PropFormat = System.Text.Json.JsonEncodedText.Encode("format");
	private static readonly System.Text.Json.JsonEncodedText PropTemplate = System.Text.Json.JsonEncodedText.Encode("template");

	public override Elastic.Clients.Elasticsearch.Security.RoleTemplate Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Security.TemplateFormat?> propFormat = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Script> propTemplate = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propFormat.TryReadProperty(ref reader, options, PropFormat, static Elastic.Clients.Elasticsearch.Security.TemplateFormat? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<Elastic.Clients.Elasticsearch.Security.TemplateFormat>(o)))
			{
				continue;
			}

			if (propTemplate.TryReadProperty(ref reader, options, PropTemplate, null))
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
		return new Elastic.Clients.Elasticsearch.Security.RoleTemplate(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Format = propFormat.Value,
			Template = propTemplate.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.RoleTemplate value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropFormat, value.Format, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.Security.TemplateFormat? v) => w.WriteNullableValue<Elastic.Clients.Elasticsearch.Security.TemplateFormat>(o, v));
		writer.WriteProperty(options, PropTemplate, value.Template, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.RoleTemplateConverter))]
public sealed partial class RoleTemplate
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RoleTemplate(Elastic.Clients.Elasticsearch.Script template)
	{
		Template = template;
	}
#if NET7_0_OR_GREATER
	public RoleTemplate()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public RoleTemplate()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal RoleTemplate(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public Elastic.Clients.Elasticsearch.Security.TemplateFormat? Format { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Script Template { get; set; }
}

public readonly partial struct RoleTemplateDescriptor
{
	internal Elastic.Clients.Elasticsearch.Security.RoleTemplate Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RoleTemplateDescriptor(Elastic.Clients.Elasticsearch.Security.RoleTemplate instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RoleTemplateDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Security.RoleTemplate(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Security.RoleTemplateDescriptor(Elastic.Clients.Elasticsearch.Security.RoleTemplate instance) => new Elastic.Clients.Elasticsearch.Security.RoleTemplateDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.RoleTemplate(Elastic.Clients.Elasticsearch.Security.RoleTemplateDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Security.RoleTemplateDescriptor Format(Elastic.Clients.Elasticsearch.Security.TemplateFormat? value)
	{
		Instance.Format = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleTemplateDescriptor Template(Elastic.Clients.Elasticsearch.Script value)
	{
		Instance.Template = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleTemplateDescriptor Template()
	{
		Instance.Template = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.RoleTemplateDescriptor Template(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.Template = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Security.RoleTemplate Build(System.Action<Elastic.Clients.Elasticsearch.Security.RoleTemplateDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Security.RoleTemplateDescriptor(new Elastic.Clients.Elasticsearch.Security.RoleTemplate(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}