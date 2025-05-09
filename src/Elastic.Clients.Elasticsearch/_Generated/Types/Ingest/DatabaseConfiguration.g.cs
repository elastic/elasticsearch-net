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

internal sealed partial class DatabaseConfigurationConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Ingest.DatabaseConfiguration>
{
	private static readonly System.Text.Json.JsonEncodedText PropName = System.Text.Json.JsonEncodedText.Encode("name");
	private static readonly System.Text.Json.JsonEncodedText VariantIpinfo = System.Text.Json.JsonEncodedText.Encode("ipinfo");
	private static readonly System.Text.Json.JsonEncodedText VariantMaxmind = System.Text.Json.JsonEncodedText.Encode("maxmind");

	public override Elastic.Clients.Elasticsearch.Ingest.DatabaseConfiguration Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Name> propName = default;
		string? variantType = null;
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

			if (reader.ValueTextEquals(VariantMaxmind))
			{
				variantType = VariantMaxmind.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.Ingest.Maxmind>(options, null);
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
		return new Elastic.Clients.Elasticsearch.Ingest.DatabaseConfiguration(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			VariantType = variantType,
			Variant = variant,
			Name = propName.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Ingest.DatabaseConfiguration value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		switch (value.VariantType)
		{
			case null:
				break;
			case "ipinfo":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.Ingest.Ipinfo)value.Variant, null, null);
				break;
			case "maxmind":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.Ingest.Maxmind)value.Variant, null, null);
				break;
			default:
				throw new System.Text.Json.JsonException($"Variant '{value.VariantType}' is not supported for type '{nameof(Elastic.Clients.Elasticsearch.Ingest.DatabaseConfiguration)}'.");
		}

		writer.WriteProperty(options, PropName, value.Name, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Ingest.DatabaseConfigurationConverter))]
public sealed partial class DatabaseConfiguration
{
	internal string? VariantType { get; set; }
	internal object? Variant { get; set; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DatabaseConfiguration(Elastic.Clients.Elasticsearch.Name name)
	{
		Name = name;
	}
#if NET7_0_OR_GREATER
	public DatabaseConfiguration()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public DatabaseConfiguration()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DatabaseConfiguration(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public Elastic.Clients.Elasticsearch.Ingest.Ipinfo? Ipinfo { get => GetVariant<Elastic.Clients.Elasticsearch.Ingest.Ipinfo>("ipinfo"); set => SetVariant("ipinfo", value); }
	public Elastic.Clients.Elasticsearch.Ingest.Maxmind? Maxmind { get => GetVariant<Elastic.Clients.Elasticsearch.Ingest.Maxmind>("maxmind"); set => SetVariant("maxmind", value); }

	/// <summary>
	/// <para>
	/// The provider-assigned name of the IP geolocation database to download.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Name Name { get; set; }

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

public readonly partial struct DatabaseConfigurationDescriptor
{
	internal Elastic.Clients.Elasticsearch.Ingest.DatabaseConfiguration Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DatabaseConfigurationDescriptor(Elastic.Clients.Elasticsearch.Ingest.DatabaseConfiguration instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DatabaseConfigurationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Ingest.DatabaseConfiguration(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Ingest.DatabaseConfigurationDescriptor(Elastic.Clients.Elasticsearch.Ingest.DatabaseConfiguration instance) => new Elastic.Clients.Elasticsearch.Ingest.DatabaseConfigurationDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.DatabaseConfiguration(Elastic.Clients.Elasticsearch.Ingest.DatabaseConfigurationDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Ingest.DatabaseConfigurationDescriptor Ipinfo(Elastic.Clients.Elasticsearch.Ingest.Ipinfo? value)
	{
		Instance.Ipinfo = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.DatabaseConfigurationDescriptor Ipinfo()
	{
		Instance.Ipinfo = Elastic.Clients.Elasticsearch.Ingest.IpinfoDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.DatabaseConfigurationDescriptor Ipinfo(System.Action<Elastic.Clients.Elasticsearch.Ingest.IpinfoDescriptor>? action)
	{
		Instance.Ipinfo = Elastic.Clients.Elasticsearch.Ingest.IpinfoDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.DatabaseConfigurationDescriptor Maxmind(Elastic.Clients.Elasticsearch.Ingest.Maxmind? value)
	{
		Instance.Maxmind = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.DatabaseConfigurationDescriptor Maxmind(System.Action<Elastic.Clients.Elasticsearch.Ingest.MaxmindDescriptor> action)
	{
		Instance.Maxmind = Elastic.Clients.Elasticsearch.Ingest.MaxmindDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The provider-assigned name of the IP geolocation database to download.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DatabaseConfigurationDescriptor Name(Elastic.Clients.Elasticsearch.Name value)
	{
		Instance.Name = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Ingest.DatabaseConfiguration Build(System.Action<Elastic.Clients.Elasticsearch.Ingest.DatabaseConfigurationDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Ingest.DatabaseConfigurationDescriptor(new Elastic.Clients.Elasticsearch.Ingest.DatabaseConfiguration(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}