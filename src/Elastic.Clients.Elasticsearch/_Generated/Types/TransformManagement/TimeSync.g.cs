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

internal sealed partial class TimeSyncConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.TransformManagement.TimeSync>
{
	private static readonly System.Text.Json.JsonEncodedText PropDelay = System.Text.Json.JsonEncodedText.Encode("delay");
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");

	public override Elastic.Clients.Elasticsearch.TransformManagement.TimeSync Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propDelay = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDelay.TryReadProperty(ref reader, options, PropDelay, null))
			{
				continue;
			}

			if (propField.TryReadProperty(ref reader, options, PropField, null))
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
		return new Elastic.Clients.Elasticsearch.TransformManagement.TimeSync(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Delay = propDelay.Value,
			Field = propField.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.TransformManagement.TimeSync value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDelay, value.Delay, null, null);
		writer.WriteProperty(options, PropField, value.Field, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.TransformManagement.TimeSyncConverter))]
public sealed partial class TimeSync
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TimeSync(Elastic.Clients.Elasticsearch.Field field)
	{
		Field = field;
	}
#if NET7_0_OR_GREATER
	public TimeSync()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public TimeSync()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal TimeSync(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The time delay between the current time and the latest input data time.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Delay { get; set; }

	/// <summary>
	/// <para>
	/// The date field that is used to identify new documents in the source. In general, it’s a good idea to use a field
	/// that contains the ingest timestamp. If you use a different field, you might need to set the delay such that it
	/// accounts for data transmission delays.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Field Field { get; set; }
}

public readonly partial struct TimeSyncDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.TransformManagement.TimeSync Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TimeSyncDescriptor(Elastic.Clients.Elasticsearch.TransformManagement.TimeSync instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TimeSyncDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.TransformManagement.TimeSync(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.TransformManagement.TimeSyncDescriptor<TDocument>(Elastic.Clients.Elasticsearch.TransformManagement.TimeSync instance) => new Elastic.Clients.Elasticsearch.TransformManagement.TimeSyncDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.TransformManagement.TimeSync(Elastic.Clients.Elasticsearch.TransformManagement.TimeSyncDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The time delay between the current time and the latest input data time.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.TimeSyncDescriptor<TDocument> Delay(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Delay = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The date field that is used to identify new documents in the source. In general, it’s a good idea to use a field
	/// that contains the ingest timestamp. If you use a different field, you might need to set the delay such that it
	/// accounts for data transmission delays.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.TimeSyncDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The date field that is used to identify new documents in the source. In general, it’s a good idea to use a field
	/// that contains the ingest timestamp. If you use a different field, you might need to set the delay such that it
	/// accounts for data transmission delays.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.TimeSyncDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.TransformManagement.TimeSync Build(System.Action<Elastic.Clients.Elasticsearch.TransformManagement.TimeSyncDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.TransformManagement.TimeSyncDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.TransformManagement.TimeSync(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct TimeSyncDescriptor
{
	internal Elastic.Clients.Elasticsearch.TransformManagement.TimeSync Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TimeSyncDescriptor(Elastic.Clients.Elasticsearch.TransformManagement.TimeSync instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TimeSyncDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.TransformManagement.TimeSync(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.TransformManagement.TimeSyncDescriptor(Elastic.Clients.Elasticsearch.TransformManagement.TimeSync instance) => new Elastic.Clients.Elasticsearch.TransformManagement.TimeSyncDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.TransformManagement.TimeSync(Elastic.Clients.Elasticsearch.TransformManagement.TimeSyncDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The time delay between the current time and the latest input data time.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.TimeSyncDescriptor Delay(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Delay = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The date field that is used to identify new documents in the source. In general, it’s a good idea to use a field
	/// that contains the ingest timestamp. If you use a different field, you might need to set the delay such that it
	/// accounts for data transmission delays.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.TimeSyncDescriptor Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The date field that is used to identify new documents in the source. In general, it’s a good idea to use a field
	/// that contains the ingest timestamp. If you use a different field, you might need to set the delay such that it
	/// accounts for data transmission delays.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.TimeSyncDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.TransformManagement.TimeSync Build(System.Action<Elastic.Clients.Elasticsearch.TransformManagement.TimeSyncDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.TransformManagement.TimeSyncDescriptor(new Elastic.Clients.Elasticsearch.TransformManagement.TimeSync(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}