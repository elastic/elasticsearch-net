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

namespace Elastic.Clients.Elasticsearch.MachineLearning;

internal sealed partial class DelayedDataCheckConfigConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.DelayedDataCheckConfig>
{
	private static readonly System.Text.Json.JsonEncodedText PropCheckWindow = System.Text.Json.JsonEncodedText.Encode("check_window");
	private static readonly System.Text.Json.JsonEncodedText PropEnabled = System.Text.Json.JsonEncodedText.Encode("enabled");

	public override Elastic.Clients.Elasticsearch.MachineLearning.DelayedDataCheckConfig Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propCheckWindow = default;
		LocalJsonValue<bool> propEnabled = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCheckWindow.TryReadProperty(ref reader, options, PropCheckWindow, null))
			{
				continue;
			}

			if (propEnabled.TryReadProperty(ref reader, options, PropEnabled, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.DelayedDataCheckConfig(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			CheckWindow = propCheckWindow.Value,
			Enabled = propEnabled.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.DelayedDataCheckConfig value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCheckWindow, value.CheckWindow, null, null);
		writer.WriteProperty(options, PropEnabled, value.Enabled, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.DelayedDataCheckConfigConverter))]
public sealed partial class DelayedDataCheckConfig
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DelayedDataCheckConfig(bool enabled)
	{
		Enabled = enabled;
	}
#if NET7_0_OR_GREATER
	public DelayedDataCheckConfig()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public DelayedDataCheckConfig()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DelayedDataCheckConfig(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The window of time that is searched for late data. This window of time ends with the latest finalized bucket.
	/// It defaults to null, which causes an appropriate <c>check_window</c> to be calculated when the real-time datafeed runs.
	/// In particular, the default <c>check_window</c> span calculation is based on the maximum of <c>2h</c> or <c>8 * bucket_span</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? CheckWindow { get; set; }

	/// <summary>
	/// <para>
	/// Specifies whether the datafeed periodically checks for delayed data.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool Enabled { get; set; }
}

public readonly partial struct DelayedDataCheckConfigDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.DelayedDataCheckConfig Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DelayedDataCheckConfigDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.DelayedDataCheckConfig instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DelayedDataCheckConfigDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.DelayedDataCheckConfig(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.DelayedDataCheckConfigDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.DelayedDataCheckConfig instance) => new Elastic.Clients.Elasticsearch.MachineLearning.DelayedDataCheckConfigDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.DelayedDataCheckConfig(Elastic.Clients.Elasticsearch.MachineLearning.DelayedDataCheckConfigDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The window of time that is searched for late data. This window of time ends with the latest finalized bucket.
	/// It defaults to null, which causes an appropriate <c>check_window</c> to be calculated when the real-time datafeed runs.
	/// In particular, the default <c>check_window</c> span calculation is based on the maximum of <c>2h</c> or <c>8 * bucket_span</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DelayedDataCheckConfigDescriptor CheckWindow(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.CheckWindow = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies whether the datafeed periodically checks for delayed data.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DelayedDataCheckConfigDescriptor Enabled(bool value = true)
	{
		Instance.Enabled = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.DelayedDataCheckConfig Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.DelayedDataCheckConfigDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.DelayedDataCheckConfigDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.DelayedDataCheckConfig(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}