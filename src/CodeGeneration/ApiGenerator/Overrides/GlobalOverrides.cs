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
			"doc_value_fields"
		};

		public override IDictionary<string, string> RenameQueryStringParams { get; } = new Dictionary<string, string>
		{
			{"_source", "source_enabled"},
			{"_source_include", "source_include"},
			{"_source_exclude", "source_exclude"},
			{"q", "query_on_query_string"},
			{"docvalue_fields", "doc_value_fields"},
		};

		public override IDictionary<string, string> ObsoleteQueryStringParams { get; set; } = new Dictionary<string, string>
		{
			{ "parent", "the parent parameter has been deprecated from elasticsearch, please use routing instead directly."}
		};
	}
}
