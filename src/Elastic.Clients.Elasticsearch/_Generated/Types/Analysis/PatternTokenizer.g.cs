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
	public sealed partial class PatternTokenizer : ITokenizerDefinition
	{
		[JsonInclude]
		[JsonPropertyName("flags")]
		public string Flags { get; set; }

		[JsonInclude]
		[JsonPropertyName("group")]
		public int Group { get; set; }

		[JsonInclude]
		[JsonPropertyName("pattern")]
		public string Pattern { get; set; }

		[JsonInclude]
		[JsonPropertyName("type")]
		public string Type => "pattern";
		[JsonInclude]
		[JsonPropertyName("version")]
		public string? Version { get; set; }
	}

	public sealed partial class PatternTokenizerDescriptor : SerializableDescriptorBase<PatternTokenizerDescriptor>, IBuildableDescriptor<PatternTokenizer>
	{
		internal PatternTokenizerDescriptor(Action<PatternTokenizerDescriptor> configure) => configure.Invoke(this);
		public PatternTokenizerDescriptor() : base()
		{
		}

		private string FlagsValue { get; set; }

		private int GroupValue { get; set; }

		private string PatternValue { get; set; }

		private string? VersionValue { get; set; }

		public PatternTokenizerDescriptor Flags(string flags)
		{
			FlagsValue = flags;
			return Self;
		}

		public PatternTokenizerDescriptor Group(int group)
		{
			GroupValue = group;
			return Self;
		}

		public PatternTokenizerDescriptor Pattern(string pattern)
		{
			PatternValue = pattern;
			return Self;
		}

		public PatternTokenizerDescriptor Version(string? version)
		{
			VersionValue = version;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("flags");
			writer.WriteStringValue(FlagsValue);
			writer.WritePropertyName("group");
			writer.WriteNumberValue(GroupValue);
			writer.WritePropertyName("pattern");
			writer.WriteStringValue(PatternValue);
			writer.WritePropertyName("type");
			writer.WriteStringValue("pattern");
			if (VersionValue is not null)
			{
				writer.WritePropertyName("version");
				JsonSerializer.Serialize(writer, VersionValue, options);
			}

			writer.WriteEndObject();
		}

		PatternTokenizer IBuildableDescriptor<PatternTokenizer>.Build() => new()
		{ Flags = FlagsValue, Group = GroupValue, Pattern = PatternValue, Version = VersionValue };
	}
}