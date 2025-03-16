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
using Elastic.Clients.Elasticsearch.Requests;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.TextStructure;

public sealed partial class FindFieldStructureRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// If <c>format</c> is set to <c>delimited</c>, you can specify the column names in a comma-separated list.
	/// If this parameter is not specified, the structure finder uses the column names from the header row of the text.
	/// If the text does not have a header row, columns are named "column1", "column2", "column3", for example.
	/// </para>
	/// </summary>
	public string? ColumnNames { get => Q<string?>("column_names"); set => Q("column_names", value); }

	/// <summary>
	/// <para>
	/// If you have set <c>format</c> to <c>delimited</c>, you can specify the character used to delimit the values in each row.
	/// Only a single character is supported; the delimiter cannot have multiple characters.
	/// By default, the API considers the following possibilities: comma, tab, semi-colon, and pipe (<c>|</c>).
	/// In this default scenario, all rows must have the same number of fields for the delimited format to be detected.
	/// If you specify a delimiter, up to 10% of the rows can have a different number of columns than the first row.
	/// </para>
	/// </summary>
	public string? Delimiter { get => Q<string?>("delimiter"); set => Q("delimiter", value); }

	/// <summary>
	/// <para>
	/// The number of documents to include in the structural analysis.
	/// The minimum value is 2.
	/// </para>
	/// </summary>
	public int? DocumentsToSample { get => Q<int?>("documents_to_sample"); set => Q("documents_to_sample", value); }

	/// <summary>
	/// <para>
	/// The mode of compatibility with ECS compliant Grok patterns.
	/// Use this parameter to specify whether to use ECS Grok patterns instead of legacy ones when the structure finder creates a Grok pattern.
	/// This setting primarily has an impact when a whole message Grok pattern such as <c>%{CATALINALOG}</c> matches the input.
	/// If the structure finder identifies a common structure but has no idea of the meaning then generic field names such as <c>path</c>, <c>ipaddress</c>, <c>field1</c>, and <c>field2</c> are used in the <c>grok_pattern</c> output.
	/// The intention in that situation is that a user who knows the meanings will rename the fields before using them.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TextStructure.EcsCompatibilityType? EcsCompatibility { get => Q<Elastic.Clients.Elasticsearch.TextStructure.EcsCompatibilityType?>("ecs_compatibility"); set => Q("ecs_compatibility", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes a field named <c>explanation</c>, which is an array of strings that indicate how the structure finder produced its result.
	/// </para>
	/// </summary>
	public bool? Explain { get => Q<bool?>("explain"); set => Q("explain", value); }

	/// <summary>
	/// <para>
	/// The field that should be analyzed.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Field Field { get => Q<Elastic.Clients.Elasticsearch.Field>("field"); set => Q("field", value); }

	/// <summary>
	/// <para>
	/// The high level structure of the text.
	/// By default, the API chooses the format.
	/// In this default scenario, all rows must have the same number of fields for a delimited format to be detected.
	/// If the format is set to delimited and the delimiter is not set, however, the API tolerates up to 5% of rows that have a different number of columns than the first row.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TextStructure.FormatType? Format { get => Q<Elastic.Clients.Elasticsearch.TextStructure.FormatType?>("format"); set => Q("format", value); }

	/// <summary>
	/// <para>
	/// If the format is <c>semi_structured_text</c>, you can specify a Grok pattern that is used to extract fields from every message in the text.
	/// The name of the timestamp field in the Grok pattern must match what is specified in the <c>timestamp_field</c> parameter.
	/// If that parameter is not specified, the name of the timestamp field in the Grok pattern must match "timestamp".
	/// If <c>grok_pattern</c> is not specified, the structure finder creates a Grok pattern.
	/// </para>
	/// </summary>
	public string? GrokPattern { get => Q<string?>("grok_pattern"); set => Q("grok_pattern", value); }

	/// <summary>
	/// <para>
	/// The name of the index that contains the analyzed field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexName Index { get => Q<Elastic.Clients.Elasticsearch.IndexName>("index"); set => Q("index", value); }

	/// <summary>
	/// <para>
	/// If the format is <c>delimited</c>, you can specify the character used to quote the values in each row if they contain newlines or the delimiter character.
	/// Only a single character is supported.
	/// If this parameter is not specified, the default value is a double quote (<c>"</c>).
	/// If your delimited text format does not use quoting, a workaround is to set this argument to a character that does not appear anywhere in the sample.
	/// </para>
	/// </summary>
	public string? Quote { get => Q<string?>("quote"); set => Q("quote", value); }

	/// <summary>
	/// <para>
	/// If the format is <c>delimited</c>, you can specify whether values between delimiters should have whitespace trimmed from them.
	/// If this parameter is not specified and the delimiter is pipe (<c>|</c>), the default value is true.
	/// Otherwise, the default value is <c>false</c>.
	/// </para>
	/// </summary>
	public bool? ShouldTrimFields { get => Q<bool?>("should_trim_fields"); set => Q("should_trim_fields", value); }

	/// <summary>
	/// <para>
	/// The maximum amount of time that the structure analysis can take.
	/// If the analysis is still running when the timeout expires, it will be stopped.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>
	/// The name of the field that contains the primary timestamp of each record in the text.
	/// In particular, if the text was ingested into an index, this is the field that would be used to populate the <c>@timestamp</c> field.
	/// </para>
	/// <para>
	/// If the format is <c>semi_structured_text</c>, this field must match the name of the appropriate extraction in the <c>grok_pattern</c>.
	/// Therefore, for semi-structured text, it is best not to specify this parameter unless <c>grok_pattern</c> is also specified.
	/// </para>
	/// <para>
	/// For structured text, if you specify this parameter, the field must exist within the text.
	/// </para>
	/// <para>
	/// If this parameter is not specified, the structure finder makes a decision about which field (if any) is the primary timestamp field.
	/// For structured text, it is not compulsory to have a timestamp in the text.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Field? TimestampField { get => Q<Elastic.Clients.Elasticsearch.Field?>("timestamp_field"); set => Q("timestamp_field", value); }

	/// <summary>
	/// <para>
	/// The Java time format of the timestamp field in the text.
	/// Only a subset of Java time format letter groups are supported:
	/// </para>
	/// <list type="bullet">
	/// <item>
	/// <para>
	/// <c>a</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>d</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>dd</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>EEE</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>EEEE</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>H</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>HH</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>h</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>M</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>MM</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>MMM</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>MMMM</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>mm</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>ss</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>XX</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>XXX</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>yy</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>yyyy</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>zzz</c>
	/// </para>
	/// </item>
	/// </list>
	/// <para>
	/// Additionally <c>S</c> letter groups (fractional seconds) of length one to nine are supported providing they occur after <c>ss</c> and are separated from the <c>ss</c> by a period (<c>.</c>), comma (<c>,</c>), or colon (<c>:</c>).
	/// Spacing and punctuation is also permitted with the exception a question mark (<c>?</c>), newline, and carriage return, together with literal text enclosed in single quotes.
	/// For example, <c>MM/dd HH.mm.ss,SSSSSS 'in' yyyy</c> is a valid override format.
	/// </para>
	/// <para>
	/// One valuable use case for this parameter is when the format is semi-structured text, there are multiple timestamp formats in the text, and you know which format corresponds to the primary timestamp, but you do not want to specify the full <c>grok_pattern</c>.
	/// Another is when the timestamp format is one that the structure finder does not consider by default.
	/// </para>
	/// <para>
	/// If this parameter is not specified, the structure finder chooses the best format from a built-in set.
	/// </para>
	/// <para>
	/// If the special value <c>null</c> is specified, the structure finder will not look for a primary timestamp in the text.
	/// When the format is semi-structured text, this will result in the structure finder treating the text as single-line messages.
	/// </para>
	/// </summary>
	public string? TimestampFormat { get => Q<string?>("timestamp_format"); set => Q("timestamp_format", value); }
}

/// <summary>
/// <para>
/// Find the structure of a text field.
/// Find the structure of a text field in an Elasticsearch index.
/// </para>
/// <para>
/// This API provides a starting point for extracting further information from log messages already ingested into Elasticsearch.
/// For example, if you have ingested data into a very simple index that has just <c>@timestamp</c> and message fields, you can use this API to see what common structure exists in the message field.
/// </para>
/// <para>
/// The response from the API contains:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// Sample messages.
/// </para>
/// </item>
/// <item>
/// <para>
/// Statistics that reveal the most common values for all fields detected within the text and basic numeric statistics for numeric fields.
/// </para>
/// </item>
/// <item>
/// <para>
/// Information about the structure of the text, which is useful when you write ingest configurations to index it or similarly formatted text.
/// </para>
/// </item>
/// <item>
/// <para>
/// Appropriate mappings for an Elasticsearch index, which you could use to ingest the text.
/// </para>
/// </item>
/// </list>
/// <para>
/// All this information can be calculated by the structure finder with no guidance.
/// However, you can optionally override some of the decisions about the text structure by specifying one or more query parameters.
/// </para>
/// <para>
/// If the structure finder produces unexpected results, specify the <c>explain</c> query parameter and an explanation will appear in the response.
/// It helps determine why the returned structure was chosen.
/// </para>
/// </summary>
public sealed partial class FindFieldStructureRequest : PlainRequest<FindFieldStructureRequestParameters>
{
	internal override ApiUrls ApiUrls => ApiUrlLookup.TextStructureFindFieldStructure;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "text_structure.find_field_structure";

	/// <summary>
	/// <para>
	/// If <c>format</c> is set to <c>delimited</c>, you can specify the column names in a comma-separated list.
	/// If this parameter is not specified, the structure finder uses the column names from the header row of the text.
	/// If the text does not have a header row, columns are named "column1", "column2", "column3", for example.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public string? ColumnNames { get => Q<string?>("column_names"); set => Q("column_names", value); }

	/// <summary>
	/// <para>
	/// If you have set <c>format</c> to <c>delimited</c>, you can specify the character used to delimit the values in each row.
	/// Only a single character is supported; the delimiter cannot have multiple characters.
	/// By default, the API considers the following possibilities: comma, tab, semi-colon, and pipe (<c>|</c>).
	/// In this default scenario, all rows must have the same number of fields for the delimited format to be detected.
	/// If you specify a delimiter, up to 10% of the rows can have a different number of columns than the first row.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public string? Delimiter { get => Q<string?>("delimiter"); set => Q("delimiter", value); }

	/// <summary>
	/// <para>
	/// The number of documents to include in the structural analysis.
	/// The minimum value is 2.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public int? DocumentsToSample { get => Q<int?>("documents_to_sample"); set => Q("documents_to_sample", value); }

	/// <summary>
	/// <para>
	/// The mode of compatibility with ECS compliant Grok patterns.
	/// Use this parameter to specify whether to use ECS Grok patterns instead of legacy ones when the structure finder creates a Grok pattern.
	/// This setting primarily has an impact when a whole message Grok pattern such as <c>%{CATALINALOG}</c> matches the input.
	/// If the structure finder identifies a common structure but has no idea of the meaning then generic field names such as <c>path</c>, <c>ipaddress</c>, <c>field1</c>, and <c>field2</c> are used in the <c>grok_pattern</c> output.
	/// The intention in that situation is that a user who knows the meanings will rename the fields before using them.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.TextStructure.EcsCompatibilityType? EcsCompatibility { get => Q<Elastic.Clients.Elasticsearch.TextStructure.EcsCompatibilityType?>("ecs_compatibility"); set => Q("ecs_compatibility", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes a field named <c>explanation</c>, which is an array of strings that indicate how the structure finder produced its result.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? Explain { get => Q<bool?>("explain"); set => Q("explain", value); }

	/// <summary>
	/// <para>
	/// The field that should be analyzed.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Field Field { get => Q<Elastic.Clients.Elasticsearch.Field>("field"); set => Q("field", value); }

	/// <summary>
	/// <para>
	/// The high level structure of the text.
	/// By default, the API chooses the format.
	/// In this default scenario, all rows must have the same number of fields for a delimited format to be detected.
	/// If the format is set to delimited and the delimiter is not set, however, the API tolerates up to 5% of rows that have a different number of columns than the first row.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.TextStructure.FormatType? Format { get => Q<Elastic.Clients.Elasticsearch.TextStructure.FormatType?>("format"); set => Q("format", value); }

	/// <summary>
	/// <para>
	/// If the format is <c>semi_structured_text</c>, you can specify a Grok pattern that is used to extract fields from every message in the text.
	/// The name of the timestamp field in the Grok pattern must match what is specified in the <c>timestamp_field</c> parameter.
	/// If that parameter is not specified, the name of the timestamp field in the Grok pattern must match "timestamp".
	/// If <c>grok_pattern</c> is not specified, the structure finder creates a Grok pattern.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public string? GrokPattern { get => Q<string?>("grok_pattern"); set => Q("grok_pattern", value); }

	/// <summary>
	/// <para>
	/// The name of the index that contains the analyzed field.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.IndexName Index { get => Q<Elastic.Clients.Elasticsearch.IndexName>("index"); set => Q("index", value); }

	/// <summary>
	/// <para>
	/// If the format is <c>delimited</c>, you can specify the character used to quote the values in each row if they contain newlines or the delimiter character.
	/// Only a single character is supported.
	/// If this parameter is not specified, the default value is a double quote (<c>"</c>).
	/// If your delimited text format does not use quoting, a workaround is to set this argument to a character that does not appear anywhere in the sample.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public string? Quote { get => Q<string?>("quote"); set => Q("quote", value); }

	/// <summary>
	/// <para>
	/// If the format is <c>delimited</c>, you can specify whether values between delimiters should have whitespace trimmed from them.
	/// If this parameter is not specified and the delimiter is pipe (<c>|</c>), the default value is true.
	/// Otherwise, the default value is <c>false</c>.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? ShouldTrimFields { get => Q<bool?>("should_trim_fields"); set => Q("should_trim_fields", value); }

	/// <summary>
	/// <para>
	/// The maximum amount of time that the structure analysis can take.
	/// If the analysis is still running when the timeout expires, it will be stopped.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>
	/// The name of the field that contains the primary timestamp of each record in the text.
	/// In particular, if the text was ingested into an index, this is the field that would be used to populate the <c>@timestamp</c> field.
	/// </para>
	/// <para>
	/// If the format is <c>semi_structured_text</c>, this field must match the name of the appropriate extraction in the <c>grok_pattern</c>.
	/// Therefore, for semi-structured text, it is best not to specify this parameter unless <c>grok_pattern</c> is also specified.
	/// </para>
	/// <para>
	/// For structured text, if you specify this parameter, the field must exist within the text.
	/// </para>
	/// <para>
	/// If this parameter is not specified, the structure finder makes a decision about which field (if any) is the primary timestamp field.
	/// For structured text, it is not compulsory to have a timestamp in the text.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Field? TimestampField { get => Q<Elastic.Clients.Elasticsearch.Field?>("timestamp_field"); set => Q("timestamp_field", value); }

	/// <summary>
	/// <para>
	/// The Java time format of the timestamp field in the text.
	/// Only a subset of Java time format letter groups are supported:
	/// </para>
	/// <list type="bullet">
	/// <item>
	/// <para>
	/// <c>a</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>d</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>dd</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>EEE</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>EEEE</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>H</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>HH</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>h</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>M</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>MM</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>MMM</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>MMMM</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>mm</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>ss</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>XX</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>XXX</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>yy</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>yyyy</c>
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>zzz</c>
	/// </para>
	/// </item>
	/// </list>
	/// <para>
	/// Additionally <c>S</c> letter groups (fractional seconds) of length one to nine are supported providing they occur after <c>ss</c> and are separated from the <c>ss</c> by a period (<c>.</c>), comma (<c>,</c>), or colon (<c>:</c>).
	/// Spacing and punctuation is also permitted with the exception a question mark (<c>?</c>), newline, and carriage return, together with literal text enclosed in single quotes.
	/// For example, <c>MM/dd HH.mm.ss,SSSSSS 'in' yyyy</c> is a valid override format.
	/// </para>
	/// <para>
	/// One valuable use case for this parameter is when the format is semi-structured text, there are multiple timestamp formats in the text, and you know which format corresponds to the primary timestamp, but you do not want to specify the full <c>grok_pattern</c>.
	/// Another is when the timestamp format is one that the structure finder does not consider by default.
	/// </para>
	/// <para>
	/// If this parameter is not specified, the structure finder chooses the best format from a built-in set.
	/// </para>
	/// <para>
	/// If the special value <c>null</c> is specified, the structure finder will not look for a primary timestamp in the text.
	/// When the format is semi-structured text, this will result in the structure finder treating the text as single-line messages.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public string? TimestampFormat { get => Q<string?>("timestamp_format"); set => Q("timestamp_format", value); }
}

/// <summary>
/// <para>
/// Find the structure of a text field.
/// Find the structure of a text field in an Elasticsearch index.
/// </para>
/// <para>
/// This API provides a starting point for extracting further information from log messages already ingested into Elasticsearch.
/// For example, if you have ingested data into a very simple index that has just <c>@timestamp</c> and message fields, you can use this API to see what common structure exists in the message field.
/// </para>
/// <para>
/// The response from the API contains:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// Sample messages.
/// </para>
/// </item>
/// <item>
/// <para>
/// Statistics that reveal the most common values for all fields detected within the text and basic numeric statistics for numeric fields.
/// </para>
/// </item>
/// <item>
/// <para>
/// Information about the structure of the text, which is useful when you write ingest configurations to index it or similarly formatted text.
/// </para>
/// </item>
/// <item>
/// <para>
/// Appropriate mappings for an Elasticsearch index, which you could use to ingest the text.
/// </para>
/// </item>
/// </list>
/// <para>
/// All this information can be calculated by the structure finder with no guidance.
/// However, you can optionally override some of the decisions about the text structure by specifying one or more query parameters.
/// </para>
/// <para>
/// If the structure finder produces unexpected results, specify the <c>explain</c> query parameter and an explanation will appear in the response.
/// It helps determine why the returned structure was chosen.
/// </para>
/// </summary>
public sealed partial class FindFieldStructureRequestDescriptor<TDocument> : RequestDescriptor<FindFieldStructureRequestDescriptor<TDocument>, FindFieldStructureRequestParameters>
{
	internal FindFieldStructureRequestDescriptor(Action<FindFieldStructureRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public FindFieldStructureRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.TextStructureFindFieldStructure;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "text_structure.find_field_structure";

	public FindFieldStructureRequestDescriptor<TDocument> ColumnNames(string? columnNames) => Qs("column_names", columnNames);
	public FindFieldStructureRequestDescriptor<TDocument> Delimiter(string? delimiter) => Qs("delimiter", delimiter);
	public FindFieldStructureRequestDescriptor<TDocument> DocumentsToSample(int? documentsToSample) => Qs("documents_to_sample", documentsToSample);
	public FindFieldStructureRequestDescriptor<TDocument> EcsCompatibility(Elastic.Clients.Elasticsearch.TextStructure.EcsCompatibilityType? ecsCompatibility) => Qs("ecs_compatibility", ecsCompatibility);
	public FindFieldStructureRequestDescriptor<TDocument> Explain(bool? explain = true) => Qs("explain", explain);
	public FindFieldStructureRequestDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field) => Qs("field", field);
	public FindFieldStructureRequestDescriptor<TDocument> Format(Elastic.Clients.Elasticsearch.TextStructure.FormatType? format) => Qs("format", format);
	public FindFieldStructureRequestDescriptor<TDocument> GrokPattern(string? grokPattern) => Qs("grok_pattern", grokPattern);
	public FindFieldStructureRequestDescriptor<TDocument> Index(Elastic.Clients.Elasticsearch.IndexName index) => Qs("index", index);
	public FindFieldStructureRequestDescriptor<TDocument> Quote(string? quote) => Qs("quote", quote);
	public FindFieldStructureRequestDescriptor<TDocument> ShouldTrimFields(bool? shouldTrimFields = true) => Qs("should_trim_fields", shouldTrimFields);
	public FindFieldStructureRequestDescriptor<TDocument> Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);
	public FindFieldStructureRequestDescriptor<TDocument> TimestampField(Elastic.Clients.Elasticsearch.Field? timestampField) => Qs("timestamp_field", timestampField);
	public FindFieldStructureRequestDescriptor<TDocument> TimestampFormat(string? timestampFormat) => Qs("timestamp_format", timestampFormat);

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}

/// <summary>
/// <para>
/// Find the structure of a text field.
/// Find the structure of a text field in an Elasticsearch index.
/// </para>
/// <para>
/// This API provides a starting point for extracting further information from log messages already ingested into Elasticsearch.
/// For example, if you have ingested data into a very simple index that has just <c>@timestamp</c> and message fields, you can use this API to see what common structure exists in the message field.
/// </para>
/// <para>
/// The response from the API contains:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// Sample messages.
/// </para>
/// </item>
/// <item>
/// <para>
/// Statistics that reveal the most common values for all fields detected within the text and basic numeric statistics for numeric fields.
/// </para>
/// </item>
/// <item>
/// <para>
/// Information about the structure of the text, which is useful when you write ingest configurations to index it or similarly formatted text.
/// </para>
/// </item>
/// <item>
/// <para>
/// Appropriate mappings for an Elasticsearch index, which you could use to ingest the text.
/// </para>
/// </item>
/// </list>
/// <para>
/// All this information can be calculated by the structure finder with no guidance.
/// However, you can optionally override some of the decisions about the text structure by specifying one or more query parameters.
/// </para>
/// <para>
/// If the structure finder produces unexpected results, specify the <c>explain</c> query parameter and an explanation will appear in the response.
/// It helps determine why the returned structure was chosen.
/// </para>
/// </summary>
public sealed partial class FindFieldStructureRequestDescriptor : RequestDescriptor<FindFieldStructureRequestDescriptor, FindFieldStructureRequestParameters>
{
	internal FindFieldStructureRequestDescriptor(Action<FindFieldStructureRequestDescriptor> configure) => configure.Invoke(this);

	public FindFieldStructureRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.TextStructureFindFieldStructure;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "text_structure.find_field_structure";

	public FindFieldStructureRequestDescriptor ColumnNames(string? columnNames) => Qs("column_names", columnNames);
	public FindFieldStructureRequestDescriptor Delimiter(string? delimiter) => Qs("delimiter", delimiter);
	public FindFieldStructureRequestDescriptor DocumentsToSample(int? documentsToSample) => Qs("documents_to_sample", documentsToSample);
	public FindFieldStructureRequestDescriptor EcsCompatibility(Elastic.Clients.Elasticsearch.TextStructure.EcsCompatibilityType? ecsCompatibility) => Qs("ecs_compatibility", ecsCompatibility);
	public FindFieldStructureRequestDescriptor Explain(bool? explain = true) => Qs("explain", explain);
	public FindFieldStructureRequestDescriptor Field(Elastic.Clients.Elasticsearch.Field field) => Qs("field", field);
	public FindFieldStructureRequestDescriptor Format(Elastic.Clients.Elasticsearch.TextStructure.FormatType? format) => Qs("format", format);
	public FindFieldStructureRequestDescriptor GrokPattern(string? grokPattern) => Qs("grok_pattern", grokPattern);
	public FindFieldStructureRequestDescriptor Index(Elastic.Clients.Elasticsearch.IndexName index) => Qs("index", index);
	public FindFieldStructureRequestDescriptor Quote(string? quote) => Qs("quote", quote);
	public FindFieldStructureRequestDescriptor ShouldTrimFields(bool? shouldTrimFields = true) => Qs("should_trim_fields", shouldTrimFields);
	public FindFieldStructureRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);
	public FindFieldStructureRequestDescriptor TimestampField(Elastic.Clients.Elasticsearch.Field? timestampField) => Qs("timestamp_field", timestampField);
	public FindFieldStructureRequestDescriptor TimestampFormat(string? timestampFormat) => Qs("timestamp_format", timestampFormat);

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}