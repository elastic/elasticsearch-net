using System;
using System.Collections.Generic;
using ApiGenerator.Overrides.Descriptors;

namespace ApiGenerator.Overrides
{
	public class GlobalOverrides : EndpointOverridesBase
	{
		public override IEnumerable<string> RenderPartial => new[]
		{
			"stored_fields",
			"script_fields",
			"docvalue_fields"
		};
		public IDictionary<string, Dictionary<string, string>> ObsoleteEnumMembers { get; set; } = new Dictionary<string, Dictionary<string, string>>
		{
			{ "NodesStatsIndexMetric", new Dictionary<string, string>{{"suggest", "As of 5.0 this option always returned an empty object in the response"}}},
			{ "IndicesStatsMetric", new Dictionary<string, string>{{"suggest", "Suggest stats have folded under the search stats, this alias will be removed"}}}
		};

		public override IDictionary<string, string> RenameQueryStringParams { get; } = new Dictionary<string, string>
		{
			{"_source", "source_enabled"},
			{"_source_include", "source_include"},
			{"_source_exclude", "source_exclude"},
			{"docvalue_fields", "doc_value_fields"},
			{"q", "query_on_query_string"},
			//make cat parameters more descriptive
			{"h", "Headers"},
			{"s", "sort_by_columns"},
			{"v", "verbose"},
			{"ts", "include_timestamp"},
		};

		public override IDictionary<string, string> ObsoleteQueryStringParams { get; set; } = new Dictionary<string, string>
		{
			{ "parent", "the parent parameter has been deprecated from elasticsearch, please use routing instead directly."},
			{ "update_all_types", "Elasticsearch 6.x only allows a single type per index so this parameter is now useless"}
		};

		public override IEnumerable<string> SkipQueryStringParams { get; } = new[]
		{
			"source", // allows the body to be specified as a request param, we do not want to advertise this with a strongly typed method
			"ttl",
			"timestamp",
		};
	}
}
