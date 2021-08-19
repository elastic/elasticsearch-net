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

using OneOf;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable restore
namespace Nest.TextStructure
{
	public partial class FindStructureResponse : ResponseBase
	{
		[JsonPropertyName("charset")]
		public string Charset
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("column_names")]
		public IReadOnlyCollection<string>? ColumnNames
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("delimiter")]
		public string? Delimiter
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("exclude_lines_pattern")]
		public string? ExcludeLinesPattern
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("explanation")]
		public IReadOnlyCollection<string>? Explanation
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("field_stats")]
		public Dictionary<Nest.Field, Nest.TextStructure.FindStructure.FieldStat> FieldStats
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("format")]
		public string Format
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("grok_pattern")]
		public string? GrokPattern
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("has_byte_order_marker")]
		public bool HasByteOrderMarker
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("has_header_row")]
		public bool? HasHeaderRow
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("ingest_pipeline")]
		public Nest.Ingest.PipelineConfig IngestPipeline
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("java_timestamp_formats")]
		public IReadOnlyCollection<string>? JavaTimestampFormats
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("joda_timestamp_formats")]
		public IReadOnlyCollection<string>? JodaTimestampFormats
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("mappings")]
		public Nest.Mapping.TypeMapping Mappings
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("multiline_start_pattern")]
		public string? MultilineStartPattern
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("need_client_timezone")]
		public bool NeedClientTimezone
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("num_lines_analyzed")]
		public int NumLinesAnalyzed
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("num_messages_analyzed")]
		public int NumMessagesAnalyzed
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("quote")]
		public string? Quote
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("sample_start")]
		public string SampleStart
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("should_trim_fields")]
		public bool? ShouldTrimFields
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("timestamp_field")]
		public Nest.Field? TimestampField
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}
}