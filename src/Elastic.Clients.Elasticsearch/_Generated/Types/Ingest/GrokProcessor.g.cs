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
namespace Elastic.Clients.Elasticsearch.Ingest
{
	public partial class GrokProcessor : Ingest.ProcessorBase, IProcessorContainerVariant
	{
		[JsonIgnore]
		string Ingest.IProcessorContainerVariant.ProcessorContainerVariantName => "grok";
		[JsonInclude]
		[JsonPropertyName("field")]
		public string Field { get; set; }

		[JsonInclude]
		[JsonPropertyName("ignore_missing")]
		public bool? IgnoreMissing { get; set; }

		[JsonInclude]
		[JsonPropertyName("pattern_definitions")]
		public Dictionary<string, string> PatternDefinitions { get; set; }

		[JsonInclude]
		[JsonPropertyName("patterns")]
		public IEnumerable<string> Patterns { get; set; }

		[JsonInclude]
		[JsonPropertyName("trace_match")]
		public bool? TraceMatch { get; set; }
	}

	public sealed partial class GrokProcessorDescriptor<T> : DescriptorBase<GrokProcessorDescriptor<T>>
	{
		public GrokProcessorDescriptor()
		{
		}

		internal GrokProcessorDescriptor(Action<GrokProcessorDescriptor<T>> configure) => configure.Invoke(this);
		internal string FieldValue { get; private set; }

		internal bool? IgnoreMissingValue { get; private set; }

		internal Dictionary<string, string> PatternDefinitionsValue { get; private set; }

		internal IEnumerable<string> PatternsValue { get; private set; }

		internal bool? TraceMatchValue { get; private set; }

		public GrokProcessorDescriptor<T> Field(string field) => Assign(field, (a, v) => a.FieldValue = v);
		public GrokProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) => Assign(ignoreMissing, (a, v) => a.IgnoreMissingValue = v);
		public GrokProcessorDescriptor<T> PatternDefinitions(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector) => Assign(selector, (a, v) => a.PatternDefinitionsValue = v?.Invoke(new FluentDictionary<string, string>()));
		public GrokProcessorDescriptor<T> Patterns(IEnumerable<string> patterns) => Assign(patterns, (a, v) => a.PatternsValue = v);
		public GrokProcessorDescriptor<T> TraceMatch(bool? traceMatch = true) => Assign(traceMatch, (a, v) => a.TraceMatchValue = v);
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("field");
			JsonSerializer.Serialize(writer, FieldValue, options);
			if (IgnoreMissingValue.HasValue)
			{
				writer.WritePropertyName("ignore_missing");
				writer.WriteBooleanValue(IgnoreMissingValue.Value);
			}

			writer.WritePropertyName("pattern_definitions");
			JsonSerializer.Serialize(writer, PatternDefinitionsValue, options);
			writer.WritePropertyName("patterns");
			JsonSerializer.Serialize(writer, PatternsValue, options);
			if (TraceMatchValue.HasValue)
			{
				writer.WritePropertyName("trace_match");
				writer.WriteBooleanValue(TraceMatchValue.Value);
			}

			writer.WriteEndObject();
		}
	}
}