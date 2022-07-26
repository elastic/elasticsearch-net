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
namespace Elastic.Clients.Elasticsearch.Analysis
{
	public sealed partial class NoriPartOfSpeechTokenFilter : ITokenFilterDefinition
	{
		[JsonInclude]
		[JsonPropertyName("stoptags")]
		public IEnumerable<string>? Stoptags { get; set; }

		[JsonInclude]
		[JsonPropertyName("type")]
		public string Type => "nori_part_of_speech";
		[JsonInclude]
		[JsonPropertyName("version")]
		public string? Version { get; set; }
	}

	public sealed partial class NoriPartOfSpeechTokenFilterDescriptor : SerializableDescriptorBase<NoriPartOfSpeechTokenFilterDescriptor>, IBuildableDescriptor<NoriPartOfSpeechTokenFilter>
	{
		internal NoriPartOfSpeechTokenFilterDescriptor(Action<NoriPartOfSpeechTokenFilterDescriptor> configure) => configure.Invoke(this);
		public NoriPartOfSpeechTokenFilterDescriptor() : base()
		{
		}

		private IEnumerable<string>? StoptagsValue { get; set; }

		private string? VersionValue { get; set; }

		public NoriPartOfSpeechTokenFilterDescriptor Stoptags(IEnumerable<string>? stoptags)
		{
			StoptagsValue = stoptags;
			return Self;
		}

		public NoriPartOfSpeechTokenFilterDescriptor Version(string? version)
		{
			VersionValue = version;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (StoptagsValue is not null)
			{
				writer.WritePropertyName("stoptags");
				JsonSerializer.Serialize(writer, StoptagsValue, options);
			}

			writer.WritePropertyName("type");
			writer.WriteStringValue("nori_part_of_speech");
			if (VersionValue is not null)
			{
				writer.WritePropertyName("version");
				JsonSerializer.Serialize(writer, VersionValue, options);
			}

			writer.WriteEndObject();
		}

		NoriPartOfSpeechTokenFilter IBuildableDescriptor<NoriPartOfSpeechTokenFilter>.Build() => new()
		{ Stoptags = StoptagsValue, Version = VersionValue };
	}
}