// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;

namespace ApiGenerator.Configuration.Overrides
{
	public class GlobalOverrides : EndpointOverridesBase
	{
		public IDictionary<string, Dictionary<string, string>> ObsoleteEnumMembers { get; set; } = new Dictionary<string, Dictionary<string, string>>()
		{
			{ "VersionType", new Dictionary<string, string>() { { "force", "Force is no longer accepted by the server as of 7.5.0 and will result in an error when used" } } }
		};

		public override IDictionary<string, string> ObsoleteQueryStringParams { get; set; } = new Dictionary<string, string>
		{
			{ "copy_settings", "Elasticsearch 6.4 will throw an exception if this is turned off see elastic/elasticsearch#30404" }
		};

		public override IDictionary<string, string> RenameQueryStringParams { get; } = new Dictionary<string, string>
		{
			{ "_source", "source_enabled" },
			{ "_source_includes", "source_includes" },
			{ "_source_excludes", "source_excludes" },
			{ "rest_total_hits_as_int", "total_hits_as_integer" },
			{ "docvalue_fields", "doc_value_fields" },
			{ "q", "query_on_query_string" },
			//make cat parameters more descriptive
			{ "h", "Headers" },
			{ "s", "sort_by_columns" },
			{ "v", "verbose" },
			{ "ts", "include_timestamp" },
			{ "if_seq_no", "if_sequence_number" },
			{ "seq_no_primary_term", "sequence_number_primary_term" },
		};

		public override IEnumerable<string> RenderPartial => new[]
		{
			"stored_fields",
			"docvalue_fields"
		};

		public override IEnumerable<string> SkipQueryStringParams { get; } = new[]
		{
			"parent", //can be removed once https://github.com/elastic/elasticsearch/pull/41098 is in
			"copy_settings", //this still needs a PR?
			"source", // allows the body to be specified as a request param, we do not want to advertise this with a strongly typed method
			"timestamp",
			"time"
		};
	}
}
