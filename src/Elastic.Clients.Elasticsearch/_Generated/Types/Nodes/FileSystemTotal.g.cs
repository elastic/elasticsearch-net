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

namespace Elastic.Clients.Elasticsearch.Nodes;

internal sealed partial class FileSystemTotalConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Nodes.FileSystemTotal>
{
	private static readonly System.Text.Json.JsonEncodedText PropAvailable = System.Text.Json.JsonEncodedText.Encode("available");
	private static readonly System.Text.Json.JsonEncodedText PropAvailableInBytes = System.Text.Json.JsonEncodedText.Encode("available_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropFree = System.Text.Json.JsonEncodedText.Encode("free");
	private static readonly System.Text.Json.JsonEncodedText PropFreeInBytes = System.Text.Json.JsonEncodedText.Encode("free_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropTotal = System.Text.Json.JsonEncodedText.Encode("total");
	private static readonly System.Text.Json.JsonEncodedText PropTotalInBytes = System.Text.Json.JsonEncodedText.Encode("total_in_bytes");

	public override Elastic.Clients.Elasticsearch.Nodes.FileSystemTotal Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propAvailable = default;
		LocalJsonValue<long?> propAvailableInBytes = default;
		LocalJsonValue<string?> propFree = default;
		LocalJsonValue<long?> propFreeInBytes = default;
		LocalJsonValue<string?> propTotal = default;
		LocalJsonValue<long?> propTotalInBytes = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAvailable.TryReadProperty(ref reader, options, PropAvailable, null))
			{
				continue;
			}

			if (propAvailableInBytes.TryReadProperty(ref reader, options, PropAvailableInBytes, null))
			{
				continue;
			}

			if (propFree.TryReadProperty(ref reader, options, PropFree, null))
			{
				continue;
			}

			if (propFreeInBytes.TryReadProperty(ref reader, options, PropFreeInBytes, null))
			{
				continue;
			}

			if (propTotal.TryReadProperty(ref reader, options, PropTotal, null))
			{
				continue;
			}

			if (propTotalInBytes.TryReadProperty(ref reader, options, PropTotalInBytes, null))
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
		return new Elastic.Clients.Elasticsearch.Nodes.FileSystemTotal(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Available = propAvailable.Value,
			AvailableInBytes = propAvailableInBytes.Value,
			Free = propFree.Value,
			FreeInBytes = propFreeInBytes.Value,
			Total = propTotal.Value,
			TotalInBytes = propTotalInBytes.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Nodes.FileSystemTotal value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAvailable, value.Available, null, null);
		writer.WriteProperty(options, PropAvailableInBytes, value.AvailableInBytes, null, null);
		writer.WriteProperty(options, PropFree, value.Free, null, null);
		writer.WriteProperty(options, PropFreeInBytes, value.FreeInBytes, null, null);
		writer.WriteProperty(options, PropTotal, value.Total, null, null);
		writer.WriteProperty(options, PropTotalInBytes, value.TotalInBytes, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Nodes.FileSystemTotalConverter))]
public sealed partial class FileSystemTotal
{
#if NET7_0_OR_GREATER
	public FileSystemTotal()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public FileSystemTotal()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal FileSystemTotal(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Total disk space available to this Java virtual machine on all file stores.
	/// Depending on OS or process level restrictions, this might appear less than <c>free</c>.
	/// This is the actual amount of free disk space the Elasticsearch node can utilise.
	/// </para>
	/// </summary>
	public string? Available { get; set; }

	/// <summary>
	/// <para>
	/// Total number of bytes available to this Java virtual machine on all file stores.
	/// Depending on OS or process level restrictions, this might appear less than <c>free_in_bytes</c>.
	/// This is the actual amount of free disk space the Elasticsearch node can utilise.
	/// </para>
	/// </summary>
	public long? AvailableInBytes { get; set; }

	/// <summary>
	/// <para>
	/// Total unallocated disk space in all file stores.
	/// </para>
	/// </summary>
	public string? Free { get; set; }

	/// <summary>
	/// <para>
	/// Total number of unallocated bytes in all file stores.
	/// </para>
	/// </summary>
	public long? FreeInBytes { get; set; }

	/// <summary>
	/// <para>
	/// Total size of all file stores.
	/// </para>
	/// </summary>
	public string? Total { get; set; }

	/// <summary>
	/// <para>
	/// Total size of all file stores in bytes.
	/// </para>
	/// </summary>
	public long? TotalInBytes { get; set; }
}