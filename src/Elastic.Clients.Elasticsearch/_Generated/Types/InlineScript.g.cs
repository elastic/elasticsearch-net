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
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch
{
	public partial class InlineScript : ScriptBase
	{
		[JsonInclude]
		[JsonPropertyName("lang")]
		public Elastic.Clients.Elasticsearch.ScriptLanguage? Lang { get; set; }

		[JsonInclude]
		[JsonPropertyName("options")]
		public Dictionary<string, string>? Options { get; set; }

		[JsonInclude]
		[JsonPropertyName("source")]
		public string Source { get; set; }
	}

	public sealed partial class InlineScriptDescriptor : DescriptorBase<InlineScriptDescriptor>
	{
		public InlineScriptDescriptor()
		{
		}

		internal InlineScriptDescriptor(Action<InlineScriptDescriptor> configure) => configure.Invoke(this);
		internal Elastic.Clients.Elasticsearch.ScriptLanguage? LangValue { get; private set; }

		internal Dictionary<string, string>? OptionsValue { get; private set; }

		internal string SourceValue { get; private set; }

		public InlineScriptDescriptor Lang(Elastic.Clients.Elasticsearch.ScriptLanguage? lang) => Assign(lang, (a, v) => a.LangValue = v);
		public InlineScriptDescriptor Options(Func<FluentDictionary<string?, string?>, FluentDictionary<string?, string?>> selector) => Assign(selector, (a, v) => a.OptionsValue = v?.Invoke(new FluentDictionary<string?, string?>()));
		public InlineScriptDescriptor Source(string source) => Assign(source, (a, v) => a.SourceValue = v);
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (LangValue is not null)
			{
				writer.WritePropertyName("lang");
				JsonSerializer.Serialize(writer, LangValue, options);
			}

			if (OptionsValue is not null)
			{
				writer.WritePropertyName("options");
				JsonSerializer.Serialize(writer, OptionsValue, options);
			}

			writer.WritePropertyName("source");
			writer.WriteStringValue(SourceValue);
			writer.WriteEndObject();
		}
	}
}