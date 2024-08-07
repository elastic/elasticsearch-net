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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.MachineLearning;

public sealed partial class DelayedDataCheckConfig
{
	/// <summary>
	/// <para>
	/// The window of time that is searched for late data. This window of time ends with the latest finalized bucket.
	/// It defaults to null, which causes an appropriate <c>check_window</c> to be calculated when the real-time datafeed runs.
	/// In particular, the default <c>check_window</c> span calculation is based on the maximum of <c>2h</c> or <c>8 * bucket_span</c>.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("check_window")]
	public Elastic.Clients.Elasticsearch.Duration? CheckWindow { get; set; }

	/// <summary>
	/// <para>
	/// Specifies whether the datafeed periodically checks for delayed data.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("enabled")]
	public bool Enabled { get; set; }
}

public sealed partial class DelayedDataCheckConfigDescriptor : SerializableDescriptor<DelayedDataCheckConfigDescriptor>
{
	internal DelayedDataCheckConfigDescriptor(Action<DelayedDataCheckConfigDescriptor> configure) => configure.Invoke(this);

	public DelayedDataCheckConfigDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Duration? CheckWindowValue { get; set; }
	private bool EnabledValue { get; set; }

	/// <summary>
	/// <para>
	/// The window of time that is searched for late data. This window of time ends with the latest finalized bucket.
	/// It defaults to null, which causes an appropriate <c>check_window</c> to be calculated when the real-time datafeed runs.
	/// In particular, the default <c>check_window</c> span calculation is based on the maximum of <c>2h</c> or <c>8 * bucket_span</c>.
	/// </para>
	/// </summary>
	public DelayedDataCheckConfigDescriptor CheckWindow(Elastic.Clients.Elasticsearch.Duration? checkWindow)
	{
		CheckWindowValue = checkWindow;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Specifies whether the datafeed periodically checks for delayed data.
	/// </para>
	/// </summary>
	public DelayedDataCheckConfigDescriptor Enabled(bool enabled = true)
	{
		EnabledValue = enabled;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (CheckWindowValue is not null)
		{
			writer.WritePropertyName("check_window");
			JsonSerializer.Serialize(writer, CheckWindowValue, options);
		}

		writer.WritePropertyName("enabled");
		writer.WriteBooleanValue(EnabledValue);
		writer.WriteEndObject();
	}
}