// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗  
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝  
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// -----------------------------------------------
//  
// This file is automatically generated 
// Please do not edit these files manually
// Run the following in the root of the repos:
//
// 		*NIX 		:	./build.sh codegen
// 		Windows 	:	build.bat codegen
//
// -----------------------------------------------
// ReSharper disable RedundantUsingDirective
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

// ReSharper disable once CheckNamespace
namespace Elasticsearch.Net.Specification.TextStructureApi
{
	///<summary>Request options for FindStructure <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/find-structure.html</para></summary>
	public class FindStructureRequestParameters : RequestParameters<FindStructureRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.POST;
		public override bool SupportsBody => true;
		///<summary>Optional parameter to specify the character set of the file</summary>
		public string Charset
		{
			get => Q<string>("charset");
			set => Q("charset", value);
		}

		///<summary>Optional parameter containing a comma separated list of the column names for a delimited file</summary>
		public string[] ColumnNames
		{
			get => Q<string[]>("column_names");
			set => Q("column_names", value);
		}

		///<summary>Optional parameter to specify the delimiter character for a delimited file - must be a single character</summary>
		public string Delimiter
		{
			get => Q<string>("delimiter");
			set => Q("delimiter", value);
		}

		///<summary>Whether to include a commentary on how the structure was derived</summary>
		public bool? Explain
		{
			get => Q<bool? >("explain");
			set => Q("explain", value);
		}

		///<summary>Optional parameter to specify the high level file format</summary>
		public Format? Format
		{
			get => Q<Format? >("format");
			set => Q("format", value);
		}

		///<summary>Optional parameter to specify the Grok pattern that should be used to extract fields from messages in a semi-structured text file</summary>
		public string GrokPattern
		{
			get => Q<string>("grok_pattern");
			set => Q("grok_pattern", value);
		}

		///<summary>Optional parameter to specify whether a delimited file includes the column names in its first row</summary>
		public bool? HasHeaderRow
		{
			get => Q<bool? >("has_header_row");
			set => Q("has_header_row", value);
		}

		///<summary>Maximum number of characters permitted in a single message when lines are merged to create messages.</summary>
		public int? LineMergeSizeLimit
		{
			get => Q<int? >("line_merge_size_limit");
			set => Q("line_merge_size_limit", value);
		}

		///<summary>How many lines of the file should be included in the analysis</summary>
		public int? LinesToSample
		{
			get => Q<int? >("lines_to_sample");
			set => Q("lines_to_sample", value);
		}

		///<summary>Optional parameter to specify the quote character for a delimited file - must be a single character</summary>
		public string Quote
		{
			get => Q<string>("quote");
			set => Q("quote", value);
		}

		///<summary>Optional parameter to specify whether the values between delimiters in a delimited file should have whitespace trimmed from them</summary>
		public bool? ShouldTrimFields
		{
			get => Q<bool? >("should_trim_fields");
			set => Q("should_trim_fields", value);
		}

		///<summary>Timeout after which the analysis will be aborted</summary>
		public TimeSpan Timeout
		{
			get => Q<TimeSpan>("timeout");
			set => Q("timeout", value);
		}

		///<summary>Optional parameter to specify the timestamp field in the file</summary>
		public string TimestampField
		{
			get => Q<string>("timestamp_field");
			set => Q("timestamp_field", value);
		}

		///<summary>Optional parameter to specify the timestamp format in the file - may be either a Joda or Java time format</summary>
		public string TimestampFormat
		{
			get => Q<string>("timestamp_format");
			set => Q("timestamp_format", value);
		}
	}
}