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

namespace Elastic.Clients.Elasticsearch.IndexLifecycleManagement;

internal sealed partial class DownsampleActionConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.DownsampleAction>
{
	private static readonly System.Text.Json.JsonEncodedText PropFixedInterval = System.Text.Json.JsonEncodedText.Encode("fixed_interval");
	private static readonly System.Text.Json.JsonEncodedText PropWaitTimeout = System.Text.Json.JsonEncodedText.Encode("wait_timeout");

	public override Elastic.Clients.Elasticsearch.IndexLifecycleManagement.DownsampleAction Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propFixedInterval = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propWaitTimeout = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propFixedInterval.TryReadProperty(ref reader, options, PropFixedInterval, null))
			{
				continue;
			}

			if (propWaitTimeout.TryReadProperty(ref reader, options, PropWaitTimeout, null))
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
		return new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.DownsampleAction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			FixedInterval = propFixedInterval.Value,
			WaitTimeout = propWaitTimeout.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexLifecycleManagement.DownsampleAction value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropFixedInterval, value.FixedInterval, null, null);
		writer.WriteProperty(options, PropWaitTimeout, value.WaitTimeout, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.DownsampleActionConverter))]
public sealed partial class DownsampleAction
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DownsampleAction(string fixedInterval)
	{
		FixedInterval = fixedInterval;
	}
#if NET7_0_OR_GREATER
	public DownsampleAction()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public DownsampleAction()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DownsampleAction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	string FixedInterval { get; set; }
	public Elastic.Clients.Elasticsearch.Duration? WaitTimeout { get; set; }
}

public readonly partial struct DownsampleActionDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexLifecycleManagement.DownsampleAction Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DownsampleActionDescriptor(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.DownsampleAction instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DownsampleActionDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.DownsampleAction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexLifecycleManagement.DownsampleActionDescriptor(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.DownsampleAction instance) => new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.DownsampleActionDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexLifecycleManagement.DownsampleAction(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.DownsampleActionDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.DownsampleActionDescriptor FixedInterval(string value)
	{
		Instance.FixedInterval = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.DownsampleActionDescriptor WaitTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.WaitTimeout = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexLifecycleManagement.DownsampleAction Build(System.Action<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.DownsampleActionDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.DownsampleActionDescriptor(new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.DownsampleAction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}