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

namespace Elastic.Clients.Elasticsearch.Analysis;

internal sealed partial class SimpleAnalyzerConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Analysis.SimpleAnalyzer>
{
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override Elastic.Clients.Elasticsearch.Analysis.SimpleAnalyzer Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (reader.ValueTextEquals(PropType))
			{
				reader.Skip();
				continue;
			}

			if (propVersion.TryReadProperty(ref reader, options, PropVersion, null))
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
		return new Elastic.Clients.Elasticsearch.Analysis.SimpleAnalyzer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
#pragma warning disable CS0618
			Version = propVersion.Value
#pragma warning restore CS0618
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Analysis.SimpleAnalyzer value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropType, value.Type, null, null);
#pragma warning disable CS0618
		writer.WriteProperty(options, PropVersion, value.Version, null, null)
#pragma warning restore CS0618
		;
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Analysis.SimpleAnalyzerConverter))]
public sealed partial class SimpleAnalyzer : Elastic.Clients.Elasticsearch.Analysis.IAnalyzer
{
#if NET7_0_OR_GREATER
	public SimpleAnalyzer()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public SimpleAnalyzer()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal SimpleAnalyzer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public string Type => "simple";

	[System.Obsolete("Deprecated in '7.14.0'.")]
	public string? Version { get; set; }
}

public readonly partial struct SimpleAnalyzerDescriptor
{
	internal Elastic.Clients.Elasticsearch.Analysis.SimpleAnalyzer Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SimpleAnalyzerDescriptor(Elastic.Clients.Elasticsearch.Analysis.SimpleAnalyzer instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SimpleAnalyzerDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Analysis.SimpleAnalyzer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Analysis.SimpleAnalyzerDescriptor(Elastic.Clients.Elasticsearch.Analysis.SimpleAnalyzer instance) => new Elastic.Clients.Elasticsearch.Analysis.SimpleAnalyzerDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Analysis.SimpleAnalyzer(Elastic.Clients.Elasticsearch.Analysis.SimpleAnalyzerDescriptor descriptor) => descriptor.Instance;

	[System.Obsolete("Deprecated in '7.14.0'.")]
	public Elastic.Clients.Elasticsearch.Analysis.SimpleAnalyzerDescriptor Version(string? value)
	{
		Instance.Version = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Analysis.SimpleAnalyzer Build(System.Action<Elastic.Clients.Elasticsearch.Analysis.SimpleAnalyzerDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Analysis.SimpleAnalyzer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Analysis.SimpleAnalyzerDescriptor(new Elastic.Clients.Elasticsearch.Analysis.SimpleAnalyzer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}