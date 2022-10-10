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

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Mapping
{
	public sealed partial class SourceField
	{
		[JsonInclude]
		[JsonPropertyName("compress")]
		public bool? Compress { get; set; }

		[JsonInclude]
		[JsonPropertyName("compress_threshold")]
		public string? CompressThreshold { get; set; }

		[JsonInclude]
		[JsonPropertyName("enabled")]
		public bool? Enabled { get; set; }

		[JsonInclude]
		[JsonPropertyName("excludes")]
		public IEnumerable<string>? Excludes { get; set; }

		[JsonInclude]
		[JsonPropertyName("includes")]
		public IEnumerable<string>? Includes { get; set; }

		[JsonInclude]
		[JsonPropertyName("mode")]
		public Elastic.Clients.Elasticsearch.Mapping.SourceFieldMode? Mode { get; set; }
	}

	public sealed partial class SourceFieldDescriptor : SerializableDescriptorBase<SourceFieldDescriptor>
	{
		internal SourceFieldDescriptor(Action<SourceFieldDescriptor> configure) => configure.Invoke(this);
		public SourceFieldDescriptor() : base()
		{
		}

		private bool? CompressValue { get; set; }

		private string? CompressThresholdValue { get; set; }

		private bool? EnabledValue { get; set; }

		private IEnumerable<string>? ExcludesValue { get; set; }

		private IEnumerable<string>? IncludesValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.SourceFieldMode? ModeValue { get; set; }

		public SourceFieldDescriptor Compress(bool? compress = true)
		{
			CompressValue = compress;
			return Self;
		}

		public SourceFieldDescriptor CompressThreshold(string? compressThreshold)
		{
			CompressThresholdValue = compressThreshold;
			return Self;
		}

		public SourceFieldDescriptor Enabled(bool? enabled = true)
		{
			EnabledValue = enabled;
			return Self;
		}

		public SourceFieldDescriptor Excludes(IEnumerable<string>? excludes)
		{
			ExcludesValue = excludes;
			return Self;
		}

		public SourceFieldDescriptor Includes(IEnumerable<string>? includes)
		{
			IncludesValue = includes;
			return Self;
		}

		public SourceFieldDescriptor Mode(Elastic.Clients.Elasticsearch.Mapping.SourceFieldMode? mode)
		{
			ModeValue = mode;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (CompressValue.HasValue)
			{
				writer.WritePropertyName("compress");
				writer.WriteBooleanValue(CompressValue.Value);
			}

			if (!string.IsNullOrEmpty(CompressThresholdValue))
			{
				writer.WritePropertyName("compress_threshold");
				writer.WriteStringValue(CompressThresholdValue);
			}

			if (EnabledValue.HasValue)
			{
				writer.WritePropertyName("enabled");
				writer.WriteBooleanValue(EnabledValue.Value);
			}

			if (ExcludesValue is not null)
			{
				writer.WritePropertyName("excludes");
				JsonSerializer.Serialize(writer, ExcludesValue, options);
			}

			if (IncludesValue is not null)
			{
				writer.WritePropertyName("includes");
				JsonSerializer.Serialize(writer, IncludesValue, options);
			}

			if (ModeValue is not null)
			{
				writer.WritePropertyName("mode");
				JsonSerializer.Serialize(writer, ModeValue, options);
			}

			writer.WriteEndObject();
		}
	}
}