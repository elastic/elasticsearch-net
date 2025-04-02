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

namespace Elastic.Clients.Elasticsearch;

internal sealed partial class ScriptFieldConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.ScriptField>
{
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreFailure = System.Text.Json.JsonEncodedText.Encode("ignore_failure");
	private static readonly System.Text.Json.JsonEncodedText PropScript = System.Text.Json.JsonEncodedText.Encode("script");

	public override Elastic.Clients.Elasticsearch.ScriptField Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool?> propIgnoreFailure = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Script> propScript = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propIgnoreFailure.TryReadProperty(ref reader, options, PropIgnoreFailure, null))
			{
				continue;
			}

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
		return new Elastic.Clients.Elasticsearch.ScriptField(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			IgnoreFailure = propIgnoreFailure.Value,
			Script = propScript.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.ScriptField value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropIgnoreFailure, value.IgnoreFailure, null, null);
		writer.WriteProperty(options, PropScript, value.Script, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.ScriptFieldConverter))]
public sealed partial class ScriptField
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ScriptField(Elastic.Clients.Elasticsearch.Script script)
	{
		Script = script;
	}
#if NET7_0_OR_GREATER
	public ScriptField()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public ScriptField()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ScriptField(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public bool? IgnoreFailure { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Script Script { get; set; }
}

public readonly partial struct ScriptFieldDescriptor
{
	internal Elastic.Clients.Elasticsearch.ScriptField Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ScriptFieldDescriptor(Elastic.Clients.Elasticsearch.ScriptField instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ScriptFieldDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.ScriptField(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.ScriptFieldDescriptor(Elastic.Clients.Elasticsearch.ScriptField instance) => new Elastic.Clients.Elasticsearch.ScriptFieldDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.ScriptField(Elastic.Clients.Elasticsearch.ScriptFieldDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.ScriptFieldDescriptor IgnoreFailure(bool? value = true)
	{
		Instance.IgnoreFailure = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ScriptFieldDescriptor Script(Elastic.Clients.Elasticsearch.Script value)
	{
		Instance.Script = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ScriptFieldDescriptor Script()
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.ScriptFieldDescriptor Script(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.ScriptField Build(System.Action<Elastic.Clients.Elasticsearch.ScriptFieldDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.ScriptFieldDescriptor(new Elastic.Clients.Elasticsearch.ScriptField(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}