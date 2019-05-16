using System.Collections.Generic;

namespace ApiGenerator.Overrides
{
	public class GlobalOverrides : EndpointOverridesBase
	{
		public IDictionary<string, Dictionary<string, string>> ObsoleteEnumMembers { get; set; } = new Dictionary<string, Dictionary<string, string>>
		{
			{
				"NodesStatsIndexMetric",
				new Dictionary<string, string> { { "suggest", "As of 5.0 this option always returned an empty object in the response" } }
			},
			{
				"IndicesStatsMetric",
				new Dictionary<string, string> { { "suggest", "Suggest stats have folded under the search stats, this alias will be removed" } }
			}
		};

		public override IDictionary<string, string> ObsoleteQueryStringParams { get; set; } = new Dictionary<string, string>
		{
			{ "parent", "the parent parameter has been deprecated from elasticsearch, please use routing instead directly." },
			{ "copy_settings", "Elasticsearch 6.4 will throw an exception if this is turned off see elastic/elasticsearch#30404" }
		};

		public override IDictionary<string, string> RenameQueryStringParams { get; } = new Dictionary<string, string>
		{
			{ "_source", "source_enabled" },
			{ "_source_includes", "source_include" },
			{ "_source_excludes", "source_exclude" },
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
			"_source_include", "_source_exclude" // can be removed once https://github.com/elastic/elasticsearch/pull/41439 is in
		};
	}
}
