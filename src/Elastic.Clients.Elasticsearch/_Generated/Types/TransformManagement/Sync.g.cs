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

namespace Elastic.Clients.Elasticsearch.TransformManagement;

internal sealed partial class SyncConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.TransformManagement.Sync>
{
	private static readonly System.Text.Json.JsonEncodedText VariantTime = System.Text.Json.JsonEncodedText.Encode("time");

	public override Elastic.Clients.Elasticsearch.TransformManagement.Sync Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		string? variantType = null;
		object? variant = null;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (reader.ValueTextEquals(VariantTime))
			{
				variantType = VariantTime.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.TransformManagement.TimeSync>(options, null);
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
		return new Elastic.Clients.Elasticsearch.TransformManagement.Sync(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			VariantType = variantType,
			Variant = variant
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.TransformManagement.Sync value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		switch (value.VariantType)
		{
			case null:
				break;
			case "time":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.TransformManagement.TimeSync)value.Variant, null, null);
				break;
			default:
				throw new System.Text.Json.JsonException($"Variant '{value.VariantType}' is not supported for type '{nameof(Elastic.Clients.Elasticsearch.TransformManagement.Sync)}'.");
		}

		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.TransformManagement.SyncConverter))]
public sealed partial class Sync
{
	internal string? VariantType { get; set; }
	internal object? Variant { get; set; }
#if NET7_0_OR_GREATER
	public Sync()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public Sync()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Sync(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Specifies that the transform uses a time field to synchronize the source and destination indices.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.TimeSync? Time { get => GetVariant<Elastic.Clients.Elasticsearch.TransformManagement.TimeSync>("time"); set => SetVariant("time", value); }

	public static implicit operator Elastic.Clients.Elasticsearch.TransformManagement.Sync(Elastic.Clients.Elasticsearch.TransformManagement.TimeSync value) => new Elastic.Clients.Elasticsearch.TransformManagement.Sync { Time = value };

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

public readonly partial struct SyncDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.TransformManagement.Sync Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SyncDescriptor(Elastic.Clients.Elasticsearch.TransformManagement.Sync instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SyncDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.TransformManagement.Sync(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor<TDocument>(Elastic.Clients.Elasticsearch.TransformManagement.Sync instance) => new Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.TransformManagement.Sync(Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Specifies that the transform uses a time field to synchronize the source and destination indices.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor<TDocument> Time(Elastic.Clients.Elasticsearch.TransformManagement.TimeSync? value)
	{
		Instance.Time = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies that the transform uses a time field to synchronize the source and destination indices.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor<TDocument> Time(System.Action<Elastic.Clients.Elasticsearch.TransformManagement.TimeSyncDescriptor<TDocument>> action)
	{
		Instance.Time = Elastic.Clients.Elasticsearch.TransformManagement.TimeSyncDescriptor<TDocument>.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.TransformManagement.Sync Build(System.Action<Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.TransformManagement.Sync(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct SyncDescriptor
{
	internal Elastic.Clients.Elasticsearch.TransformManagement.Sync Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SyncDescriptor(Elastic.Clients.Elasticsearch.TransformManagement.Sync instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SyncDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.TransformManagement.Sync(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor(Elastic.Clients.Elasticsearch.TransformManagement.Sync instance) => new Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.TransformManagement.Sync(Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Specifies that the transform uses a time field to synchronize the source and destination indices.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor Time(Elastic.Clients.Elasticsearch.TransformManagement.TimeSync? value)
	{
		Instance.Time = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies that the transform uses a time field to synchronize the source and destination indices.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor Time(System.Action<Elastic.Clients.Elasticsearch.TransformManagement.TimeSyncDescriptor> action)
	{
		Instance.Time = Elastic.Clients.Elasticsearch.TransformManagement.TimeSyncDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies that the transform uses a time field to synchronize the source and destination indices.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor Time<T>(System.Action<Elastic.Clients.Elasticsearch.TransformManagement.TimeSyncDescriptor<T>> action)
	{
		Instance.Time = Elastic.Clients.Elasticsearch.TransformManagement.TimeSyncDescriptor<T>.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.TransformManagement.Sync Build(System.Action<Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor(new Elastic.Clients.Elasticsearch.TransformManagement.Sync(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}