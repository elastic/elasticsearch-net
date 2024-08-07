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

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.Aggregations;

public sealed partial class TDigest
{
	/// <summary>
	/// <para>
	/// Limits the maximum number of nodes used by the underlying TDigest algorithm to <c>20 * compression</c>, enabling control of memory usage and approximation error.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("compression")]
	public int? Compression { get; set; }
}

public sealed partial class TDigestDescriptor : SerializableDescriptor<TDigestDescriptor>
{
	internal TDigestDescriptor(Action<TDigestDescriptor> configure) => configure.Invoke(this);

	public TDigestDescriptor() : base()
	{
	}

	private int? CompressionValue { get; set; }

	/// <summary>
	/// <para>
	/// Limits the maximum number of nodes used by the underlying TDigest algorithm to <c>20 * compression</c>, enabling control of memory usage and approximation error.
	/// </para>
	/// </summary>
	public TDigestDescriptor Compression(int? compression)
	{
		CompressionValue = compression;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (CompressionValue.HasValue)
		{
			writer.WritePropertyName("compression");
			writer.WriteNumberValue(CompressionValue.Value);
		}

		writer.WriteEndObject();
	}
}