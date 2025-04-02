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

namespace Elastic.Clients.Elasticsearch.Ingest;

internal sealed partial class DatabaseConfigurationFullConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Ingest.DatabaseConfigurationFull>
{
	private static readonly System.Text.Json.JsonEncodedText PropName = System.Text.Json.JsonEncodedText.Encode("name");
	private static readonly System.Text.Json.JsonEncodedText VariantIpinfo = System.Text.Json.JsonEncodedText.Encode("ipinfo");
	private static readonly System.Text.Json.JsonEncodedText VariantLocal = System.Text.Json.JsonEncodedText.Encode("local");
	private static readonly System.Text.Json.JsonEncodedText VariantMaxmind = System.Text.Json.JsonEncodedText.Encode("maxmind");
	private static readonly System.Text.Json.JsonEncodedText VariantWeb = System.Text.Json.JsonEncodedText.Encode("web");

	public override Elastic.Clients.Elasticsearch.Ingest.DatabaseConfigurationFull Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propName = default;
		var variantType = string.Empty;
		object? variant = null;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propName.TryReadProperty(ref reader, options, PropName, null))
			{
				continue;
			}

			if (reader.ValueTextEquals(VariantIpinfo))
			{
				variantType = VariantIpinfo.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.Ingest.Ipinfo>(options, null);
				continue;
			}

			if (reader.ValueTextEquals(VariantLocal))
			{
				variantType = VariantLocal.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.Ingest.Local>(options, null);
				continue;
			}

			if (reader.ValueTextEquals(VariantMaxmind))
			{
				variantType = VariantMaxmind.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.Ingest.Maxmind>(options, null);
				continue;
			}

			if (reader.ValueTextEquals(VariantWeb))
			{
				variantType = VariantWeb.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.Ingest.Web>(options, null);
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
		return new Elastic.Clients.Elasticsearch.Ingest.DatabaseConfigurationFull(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			VariantType = variantType,
			Variant = variant,
			Name = propName.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Ingest.DatabaseConfigurationFull value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		switch (value.VariantType)
		{
			case "":
				break;
			case "ipinfo":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.Ingest.Ipinfo)value.Variant, null, null);
				break;
			case "local":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.Ingest.Local)value.Variant, null, null);
				break;
			case "maxmind":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.Ingest.Maxmind)value.Variant, null, null);
				break;
			case "web":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.Ingest.Web)value.Variant, null, null);
				break;
			default:
				throw new System.Text.Json.JsonException($"Variant '{value.VariantType}' is not supported for type '{nameof(Elastic.Clients.Elasticsearch.Ingest.DatabaseConfigurationFull)}'.");
		}

		writer.WriteProperty(options, PropName, value.Name, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Ingest.DatabaseConfigurationFullConverter))]
public sealed partial class DatabaseConfigurationFull
{
	public string VariantType { get; internal set; } = string.Empty;
	public object? Variant { get; internal set; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DatabaseConfigurationFull(string name)
	{
		Name = name;
	}
#if NET7_0_OR_GREATER
	public DatabaseConfigurationFull()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public DatabaseConfigurationFull()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DatabaseConfigurationFull(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public Elastic.Clients.Elasticsearch.Ingest.Ipinfo? Ipinfo { get => GetVariant<Elastic.Clients.Elasticsearch.Ingest.Ipinfo>("ipinfo"); set => SetVariant("ipinfo", value); }
	public Elastic.Clients.Elasticsearch.Ingest.Local? Local { get => GetVariant<Elastic.Clients.Elasticsearch.Ingest.Local>("local"); set => SetVariant("local", value); }
	public Elastic.Clients.Elasticsearch.Ingest.Maxmind? Maxmind { get => GetVariant<Elastic.Clients.Elasticsearch.Ingest.Maxmind>("maxmind"); set => SetVariant("maxmind", value); }
	public Elastic.Clients.Elasticsearch.Ingest.Web? Web { get => GetVariant<Elastic.Clients.Elasticsearch.Ingest.Web>("web"); set => SetVariant("web", value); }

	/// <summary>
	/// <para>
	/// The provider-assigned name of the IP geolocation database to download.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Name { get; set; }

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	private T? GetVariant<T>(string type)
	{
		if (string.Equals(VariantType, type, System.StringComparison.Ordinal) && Variant is T result)
		{
			return result;
		}

		return default;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	private void SetVariant<T>(string type, T? value)
	{
		VariantType = type;
		Variant = value;
	}
}